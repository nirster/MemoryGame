namespace MemoryGameLogic
{
    using System;
    using System.Collections.Generic;

    public sealed class ComputerPlayer
    {
        #region Fields

        private readonly Dictionary<char, List<Board.Cell>> r_ActiveCellsMemory;
        private readonly List<Board.Cell> r_AlreadyVisibleCellsOnBoard;
        private readonly int r_MaxMemory;
        private readonly int r_BoardCols;
        private readonly int r_BoardLines;
        private Board.Cell m_FirstChoice;
        private Board.Cell m_SecondChoice;

        #endregion

        #region Constructors and Destructors

        public ComputerPlayer(int i_Lines, int i_Cols)
        {
            r_BoardLines = i_Lines;
            r_BoardCols = i_Cols;
            r_MaxMemory = computeMemory(i_Cols, i_Lines);
            r_AlreadyVisibleCellsOnBoard = new List<Board.Cell>();
            m_FirstChoice = null;
            m_SecondChoice = null;
            r_ActiveCellsMemory = new Dictionary<char, List<Board.Cell>>();
        }

        #endregion

        #region Public Methods and Operators

        public void ForgetCellThatIsAlreadyVisibleOnBoard(Board.Cell i_Cell)
        {
            if (i_Cell != null)
            {
                r_AlreadyVisibleCellsOnBoard.Add(new Board.Cell(i_Cell));
                removeLetterFromMemory(i_Cell);
            }
        }

        public Choice GetFirstChoice()
        {
            m_FirstChoice = m_SecondChoice = null;
            Choice result;

            if (knowsAboutPairPosition())
            {
                result = new Choice(m_FirstChoice);
            }
            else
            {
                result = getEducatedGuess();
                m_SecondChoice = null;
            }

            return result;
        }

        public Choice GetSecondChoice()
        {
            Choice result = null;

            if (m_SecondChoice != null)
            {
                result = new Choice(m_SecondChoice);
            }
            else
            {
                if (m_FirstChoice != null)
                {
                    if (r_ActiveCellsMemory.ContainsKey(m_FirstChoice.Letter))
                    {
                        result = new Choice(r_ActiveCellsMemory[m_FirstChoice.Letter][0]);
                    }
                }
                else
                {
                    result = getEducatedGuess();
                }
            }

            m_FirstChoice = null;
            m_SecondChoice = null;

            return result;
        }

        public void RememberCell(Board.Cell i_Cell)
        {
            if (i_Cell != null)
            {
                if (actualMemoryInUse() >= r_MaxMemory)
                {
                    removeFirstCellFromActiveMemory();
                }

                if (isInActiveMemory(i_Cell))
                {
                    return;
                }

                if (!r_ActiveCellsMemory.ContainsKey(i_Cell.Letter))
                {
                    r_ActiveCellsMemory.Add(i_Cell.Letter, new List<Board.Cell>());
                }

                r_ActiveCellsMemory[i_Cell.Letter].Add(new Board.Cell(i_Cell));
            }
        }

        #endregion

        #region Methods

        private int actualMemoryInUse()
        {
            int sum = 0;

            foreach (var kvp in r_ActiveCellsMemory)
            {
                sum += kvp.Value.Count;
            }

            return sum;
        }

        private int computeMemory(int i_BoardLines, int i_BoardCols)
        {
            int memorySize;
            const double k_FiftyPercent = 0.2;

            memorySize = (int)(i_BoardCols * i_BoardLines * k_FiftyPercent);

            if (memorySize % 2 != 0)
            {
                ++memorySize;
            }

            return memorySize;
        }

        private Choice getEducatedGuess()
        {
            Choice retChoice = null;
            var possibleChoices = new List<Choice>();

            for (int i = 0; i < r_BoardLines; ++i)
            {
                for (int j = 0; j < r_BoardCols; ++j)
                {
                    if (!isCellAlreadyVisibleOnBoard(i, j) && !isInActiveMemory(i, j))
                    {
                        possibleChoices.Add(new Choice(i, j));
                    }
                }
            }

            var random = new Random();
            if (possibleChoices.Count >= 1)
            {
                retChoice = possibleChoices[random.Next(0, possibleChoices.Count - 1)];
            }
            else if (possibleChoices.Count == 0)
            {
                bool breakOuterLoop = false;

                for (int i = 0; i < r_BoardLines && !breakOuterLoop; ++i)
                {
                    for (int j = 0; j < r_BoardCols; ++j)
                    {
                        if (!isCellAlreadyVisibleOnBoard(i, j))
                        {
                            retChoice = new Choice(i, j);
                            breakOuterLoop = true;
                            break;
                        }
                    }
                }
            }

            return retChoice;
        }

        private bool isCellAlreadyVisibleOnBoard(int i_Line, int i_Col)
        {
            bool isAlreadyVisible = false;

            foreach (Board.Cell cell in r_AlreadyVisibleCellsOnBoard)
            {
                if (cell.Line == i_Line && cell.Col == i_Col)
                {
                    isAlreadyVisible = true;
                    break;
                }
            }

            return isAlreadyVisible;
        }

        private bool isInActiveMemory(int i_Line, int i_Col)
        {
            bool alreadyExists = false;

            foreach (var kvp in r_ActiveCellsMemory)
            {
                foreach (Board.Cell cell in kvp.Value)
                {
                    if (cell.Line == i_Line && cell.Col == i_Col)
                    {
                        alreadyExists = true;
                    }
                }
            }

            return alreadyExists;
        }

        private bool isInActiveMemory(Board.Cell i_Cell)
        {
            bool result;

            if (i_Cell != null)
            {
                result = isInActiveMemory(i_Cell.Line, i_Cell.Col);
            }
            else
            {
                result = false;
            }

            return result;
        }

        private bool knowsAboutPairPosition()
        {
            bool foundPair = false;

            foreach (var kvp in r_ActiveCellsMemory)
            {
                if (kvp.Value.Count == 2)
                {
                    m_FirstChoice = kvp.Value[0];
                    m_SecondChoice = kvp.Value[1];
                    foundPair = true;
                    break;
                }
            }

            return foundPair;
        }

        private void removeFirstCellFromActiveMemory()
        {
            foreach (var kvp in r_ActiveCellsMemory)
            {
                if (kvp.Value.Count >= 1)
                {
                    kvp.Value.RemoveAt(0);
                    break;
                }
            }
        }

        private void removeLetterFromMemory(Board.Cell i_Cell)
        {
            if (i_Cell != null)
            {
                r_ActiveCellsMemory.Remove(i_Cell.Letter);
            }
        }

        #endregion
    }
}