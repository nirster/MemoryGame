namespace MemoryGameLogic.GameEventArgs
{
    using System;

    public class MemoryGameEventArgs : EventArgs
    {
        #region Fields

        private readonly int r_BoardCols;
        private readonly int r_BoardLines;
        private readonly bool r_GameVsComputer;
        private readonly string r_PlayerOneName;
        private readonly string r_PlayerTwoName;

        #endregion

        #region Constructors and Destructors

        public MemoryGameEventArgs(
            int i_BoardLines,
            int i_BoardCols,
            string i_PlayerOneName,
            string i_PlayerTwoName,
            bool i_GameVsComputer)
        {
            r_BoardLines = i_BoardLines;
            r_BoardCols = i_BoardCols;
            r_PlayerOneName = i_PlayerOneName;
            r_PlayerTwoName = i_PlayerTwoName;
            r_GameVsComputer = i_GameVsComputer;
        }

        #endregion

        #region Public Properties

        public int Cols
        {
            get
            {
                return r_BoardCols;
            }
        }

        public int Lines
        {
            get
            {
                return r_BoardLines;
            }
        }

        public bool GameVsComputer
        {
            get
            {
                return r_GameVsComputer;
            }
        }

        public string PlayerOneName
        {
            get
            {
                return r_PlayerOneName;
            }
        }

        public string PlayerTwoName
        {
            get
            {
                return r_PlayerTwoName;
            }
        }

        #endregion
    }
}