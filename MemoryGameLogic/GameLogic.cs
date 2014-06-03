namespace MemoryGameLogic
{
    using System;
    using System.Threading;
    using MemoryGameLogic.GameEventArgs;

    public sealed class GameLogic
    {
        #region Fields

        private Board m_Board;
        private ComputerPlayer m_ComputerPlayer;
        private Player m_CurrentPlayer;
        private Board.Cell m_FirstChoice;
        private Player m_FirstPlayer;
        private bool m_GameVsComputer;
        private bool m_IsFirstMove;
        private bool m_IsGameOver;
        private Board.Cell m_SecondChoice;
        private Player m_SecondPlayer;

        #endregion

        #region Public Events

        public event EventHandler ComputerMove;

        public event EventHandler GameOver;

        public event EventHandler<MemoryGameEventArgs> NewGame;

        public event EventHandler<PlayersInfoChangedEventArgs> PlayersInfoChanged;

        #endregion

        #region Public Properties

        public uint FirstPlayerScore
        {
            get
            {
                return m_FirstPlayer.Score;
            }
        }

        public Board GameBoard
        {
            get
            {
                return m_Board;
            }
        }

        public bool IsFirstMove
        {
            get
            {
                return m_IsFirstMove;
            }

            private set
            {
                m_IsFirstMove = value;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return m_IsGameOver;
            }

            private set
            {
                if (value)
                {
                    m_IsGameOver = value;
                    OnGameOver();
                }
            }
        }

        public uint SecondPlayerScore
        {
            get
            {
                return m_SecondPlayer.Score;
            }
        }

        public string WinnerMessage
        {
            get
            {
                string result;

                if (FirstPlayerScore != SecondPlayerScore)
                {
                    result = FirstPlayerScore > SecondPlayerScore ? m_FirstPlayer.Name : m_SecondPlayer.Name;
                }
                else
                {
                    result = "It's a tie!";
                }

                return result;
            }
        }

        #endregion

        #region Properties

        private Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }

            set
            {
                m_CurrentPlayer = value;
            }
        }

        private bool GameVsComputer
        {
            get
            {
                return m_GameVsComputer;
            }

            set
            {
                m_GameVsComputer = value;
            }
        }

        private bool IsMatchFound
        {
            get
            {
                return m_FirstChoice.Letter == m_SecondChoice.Letter;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void InitializeNewGame(
            int i_BoardLines,
            int i_BoardCols,
            string i_PlayerOneName,
            string i_PlayerTwoName,
            bool i_GameVsComputer)
        {
            createBoard(i_BoardLines, i_BoardCols);
            createFirstPlayer(i_PlayerOneName);
            createSecondPlayer(i_PlayerTwoName);
            createComputerPlayer(i_GameVsComputer);
            CurrentPlayer = m_FirstPlayer;
            GameVsComputer = i_GameVsComputer;
            IsFirstMove = true;
            fireNewGameEvent(i_BoardLines, i_BoardCols, i_PlayerOneName, i_PlayerTwoName, i_GameVsComputer);
        }

        public void PerformComputerMove()
        {
            while (true)
            {
                Choice firstChoice = m_ComputerPlayer.GetFirstChoice();
                m_FirstChoice = m_Board.GetCellAt(firstChoice.Line, firstChoice.Col);
                m_Board.GetCellAt(firstChoice.Line, firstChoice.Col).Selected = true;
                Thread.Sleep(400);
                Choice secondChoice = m_ComputerPlayer.GetSecondChoice();
                m_SecondChoice = m_Board.GetCellAt(secondChoice.Line, secondChoice.Col);
                m_Board.GetCellAt(secondChoice.Line, secondChoice.Col).Selected = true;
                Thread.Sleep(400);

                if (IsMatchFound)
                {
                    performComputerPlayerForget();
                    incrementCurrentPlayerScore();
                    setChoicesPermanentOnBoard();
                    checkForGameOver();
                    if (IsGameOver)
                    {
                        break;
                    }
                }
                else
                {
                    performComputerPlayerSave();
                    clearLastChoicesFromBoard();
                    swapPlayers();
                    break;
                }
            }
        }

        public void PerformHumanMove(int i_Line, int i_Col)
        {
            if (IsFirstMove)
            {
                performHumanFirstMove(i_Line, i_Col);
            }
            else
            {
                performHumanSecondMove(i_Line, i_Col);

                if (IsMatchFound)
                {
                    performComputerPlayerForget();
                    incrementCurrentPlayerScore();
                    setChoicesPermanentOnBoard();
                    checkForGameOver();
                }
                else
                {
                    performComputerPlayerSave();
                    clearLastChoicesFromBoard();
                    swapPlayers();
                    if (GameVsComputer)
                    {
                        OnComputerMove();
                    }
                }
            }

            IsFirstMove = !IsFirstMove;
        }

        #endregion

        #region Methods

        private void OnComputerMove()
        {
            EventHandler handler = ComputerMove;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnGameOver()
        {
            EventHandler handler = GameOver;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnNewGame(MemoryGameEventArgs e)
        {
            EventHandler<MemoryGameEventArgs> handler = NewGame;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnPlayersInfoChanged(PlayersInfoChangedEventArgs e)
        {
            EventHandler<PlayersInfoChangedEventArgs> handler = PlayersInfoChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void checkForGameOver()
        {
            IsGameOver = !m_Board.IsPlayable;
        }

        private void clearLastChoicesFromBoard()
        {
            m_Board.GetCellAt(m_FirstChoice.Line, m_FirstChoice.Col).Selected = false;
            m_Board.GetCellAt(m_SecondChoice.Line, m_SecondChoice.Col).Selected = false;
        }

        private void createBoard(int i_BoardLines, int i_BoardCols)
        {
            m_Board = Board.CreateNewBoard(i_BoardLines, i_BoardCols);
        }

        private void createComputerPlayer(bool i_GameVsComputer)
        {
            if (i_GameVsComputer)
            {
                m_ComputerPlayer = new ComputerPlayer(m_Board.Lines, m_Board.Cols);
            }
        }

        private void createFirstPlayer(string i_PlayerOneName)
        {
            m_FirstPlayer = new Player(i_PlayerOneName);
        }

        private PlayersInfoChangedEventArgs createPlayersInfoChangedEventArgs()
        {
            return new PlayersInfoChangedEventArgs(
                m_CurrentPlayer.Name,
                m_FirstPlayer.Name,
                m_SecondPlayer.Name,
                FirstPlayerScore,
                SecondPlayerScore);
        }

        private void createSecondPlayer(string i_PlayerTwoName)
        {
            m_SecondPlayer = new Player(i_PlayerTwoName);
        }

        private void fireNewGameEvent(
            int i_BoardLines,
            int i_BoardCols,
            string i_PlayerOneName,
            string i_PlayerTwoName,
            bool i_GameVsComputer)
        {
            OnNewGame(
                new MemoryGameEventArgs(i_BoardLines, i_BoardCols, i_PlayerOneName, i_PlayerTwoName, i_GameVsComputer));
        }

        private void incrementCurrentPlayerScore()
        {
            ++CurrentPlayer.Score;
            OnPlayersInfoChanged(createPlayersInfoChangedEventArgs());
        }

        private void performComputerPlayerForget()
        {
            if (GameVsComputer)
            {
                m_ComputerPlayer.ForgetCellThatIsAlreadyVisibleOnBoard(m_FirstChoice);
                m_ComputerPlayer.ForgetCellThatIsAlreadyVisibleOnBoard(m_SecondChoice);
            }
        }

        private void performComputerPlayerSave()
        {
            if (GameVsComputer)
            {
                m_ComputerPlayer.RememberCell(m_FirstChoice);
                m_ComputerPlayer.RememberCell(m_SecondChoice);
            }
        }

        private void performHumanFirstMove(int i_Line, int i_Col)
        {
            m_FirstChoice = m_Board.GetCellAt(i_Line, i_Col);
            m_Board.GetCellAt(i_Line, i_Col).Selected = true;
        }

        private void performHumanSecondMove(int i_Line, int i_Col)
        {
            m_SecondChoice = m_Board.GetCellAt(i_Line, i_Col);
            m_Board.GetCellAt(i_Line, i_Col).Selected = true;
            Thread.Sleep(200);
        }

        private void setChoicesPermanentOnBoard()
        {
            m_Board.GetCellAt(m_FirstChoice.Line, m_FirstChoice.Col).Selected = true;
            m_Board.GetCellAt(m_SecondChoice.Line, m_SecondChoice.Col).Selected = true;
        }

        private void swapPlayers()
        {
            CurrentPlayer = CurrentPlayer == m_FirstPlayer ? m_SecondPlayer : m_FirstPlayer;
            OnPlayersInfoChanged(createPlayersInfoChangedEventArgs());
        }

        #endregion
    }
}