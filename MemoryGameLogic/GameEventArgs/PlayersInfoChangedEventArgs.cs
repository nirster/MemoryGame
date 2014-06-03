namespace MemoryGameLogic.GameEventArgs
{
    using System;

    public class PlayersInfoChangedEventArgs : EventArgs
    {
        #region Fields

        private readonly string r_CurrentPlayerName;
        private readonly string r_FirstPlayerName;
        private readonly uint r_FirstPlayerScore;
        private readonly string r_SecondPlayerName;
        private readonly uint r_SecondPlayerScore;

        #endregion

        #region Constructors and Destructors

        public PlayersInfoChangedEventArgs(
            string i_CurrentPlayerName,
            string i_FirstPlayerName,
            string i_SecondPlayerName,
            uint i_FirstPlayerScore,
            uint i_SecondPlayerScore)
        {
            r_CurrentPlayerName = i_CurrentPlayerName;
            r_FirstPlayerName = i_FirstPlayerName;
            r_SecondPlayerName = i_SecondPlayerName;
            r_FirstPlayerScore = i_FirstPlayerScore;
            r_SecondPlayerScore = i_SecondPlayerScore;
        }

        #endregion

        #region Public Properties

        public string CurrentPlayerName
        {
            get
            {
                return r_CurrentPlayerName;
            }
        }

        public string FirstPlayerName
        {
            get
            {
                return r_FirstPlayerName;
            }
        }

        public uint FirstPlayerScore
        {
            get
            {
                return r_FirstPlayerScore;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return r_SecondPlayerName;
            }
        }

        public uint SecondPlayerScore
        {
            get
            {
                return r_SecondPlayerScore;
            }
        }

        #endregion
    }
}