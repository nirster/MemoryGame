namespace MemoryGameLogic
{
    using System;
    using System.Collections.Generic;

    public sealed class Board
    {
        #region Static Fields

        private static readonly Random sr_Randomizer;

        #endregion

        #region Fields

        private readonly Cell[,] r_CellsMatrix;
        private readonly int r_NumOfCols;
        private readonly int r_NumOfLines;

        #endregion

        #region Constructors and Destructors

        static Board()
        {
            sr_Randomizer = new Random();
        }

        private Board(int i_Lines, int i_Cols)
        {
            r_NumOfLines = i_Lines;
            r_NumOfCols = i_Cols;
            r_CellsMatrix = new Cell[i_Lines, i_Cols];
        }

        #endregion

        #region Public Properties

        public int Cols
        {
            get
            {
                return r_NumOfCols;
            }
        }

        public bool IsPlayable
        {
            get
            {
                bool isPlayable = false;

                for (int i = 0; i < r_NumOfLines; ++i)
                {
                    for (int j = 0; j < r_NumOfCols; ++j)
                    {
                        if (!GetCellAt(i, j).Selected)
                        {
                            isPlayable = true;
                            break;
                        }
                    }
                }

                return isPlayable;
            }
        }

        public int Lines
        {
            get
            {
                return r_NumOfLines;
            }
        }

        #endregion

        #region Public Methods and Operators

        public static Board CreateNewBoard(int i_Lines, int i_Cols)
        {
            var theBoardToBuild = new Board(i_Lines, i_Cols);
            List<char> alphaBetLetters = createAlphabetList();

            shuffle(alphaBetLetters);
            int numUniqueLettersNeeded = i_Lines * i_Cols / 2;
            List<char> uniqueLetters = alphaBetLetters.GetRange(0, numUniqueLettersNeeded);
            uniqueLetters.AddRange(uniqueLetters);
            shuffle(uniqueLetters);
            fillBoardMatrixWithLetters(theBoardToBuild, uniqueLetters);

            return theBoardToBuild;
        }

        public Cell GetCellAt(int i_Line, int i_Col)
        {
            return r_CellsMatrix[i_Line, i_Col];
        }

        #endregion

        #region Methods

        private static List<char> createAlphabetList()
        {
            const int k_AlphabetSize = 26;
            var alphabetList = new List<char>(k_AlphabetSize);

            for (int i = 0; i < k_AlphabetSize; ++i)
            {
                alphabetList.Add((char)(i + 'A'));
            }

            return alphabetList;
        }

        private static void fillBoardMatrixWithLetters(Board io_Board, IList<char> i_UniqueLetters)
        {
            int numberOfLines = io_Board.r_NumOfLines;
            int numberOfCols = io_Board.r_NumOfCols;
            Cell[,] cellMatrix = io_Board.r_CellsMatrix;
            int readIndex = 0;

            for (int i = 0; i < numberOfLines; ++i)
            {
                for (int j = 0; j < numberOfCols; ++j)
                {
                    cellMatrix[i, j] = new Cell(i_UniqueLetters[readIndex++], i, j);
                }
            }
        }

        private static void shuffle(IList<char> i_List)
        {
            int listSize = i_List.Count;

            while (listSize > 1)
            {
                int k = sr_Randomizer.Next(0, listSize) % listSize;
                listSize--;
                char value = i_List[k];

                i_List[k] = i_List[listSize];
                i_List[listSize] = value;
            }
        }

        #endregion

        public class Cell
        {
            #region Fields

            private readonly int r_Col;
            private readonly char r_Letter;
            private readonly int r_Line;
            private bool m_IsSelected;

            #endregion

            #region Constructors and Destructors

            public Cell(char i_Letter, int i_Line, int i_Col)
            {
                r_Letter = i_Letter;
                m_IsSelected = false;
                r_Line = i_Line;
                r_Col = i_Col;
            }

            public Cell(Cell i_OtherCell)
            {
                r_Letter = i_OtherCell.Letter;
                m_IsSelected = i_OtherCell.Selected;
                r_Line = i_OtherCell.r_Line;
                r_Col = i_OtherCell.r_Col;
            }

            #endregion

            #region Public Events

            public event EventHandler VisibilityChanged;

            #endregion

            #region Public Properties

            public int Col
            {
                get
                {
                    return r_Col;
                }
            }

            public char Letter
            {
                get
                {
                    return r_Letter;
                }
            }

            public int Line
            {
                get
                {
                    return r_Line;
                }
            }

            public bool Selected
            {
                get
                {
                    return m_IsSelected;
                }

                set
                {
                    if (m_IsSelected != value)
                    {
                        m_IsSelected = value;
                        OnVisibilityChanged();
                    }
                }
            }

            #endregion

            #region Methods

            private void OnVisibilityChanged()
            {
                EventHandler handler = VisibilityChanged;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }

            #endregion
        }
    }
}