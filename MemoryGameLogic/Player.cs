namespace MemoryGameLogic
{
    public sealed class Player
    {
        #region Constants

        private const uint k_InitialScore = 0;

        #endregion

        #region Fields

        private readonly string r_PlayerName;
        private uint m_Score;

        #endregion

        #region Constructors and Destructors

        public Player(string i_Name)
        {
            r_PlayerName = i_Name;
            m_Score = k_InitialScore;
        }

        #endregion

        #region Public Properties

        public string Name
        {
            get
            {
                return r_PlayerName;
            }
        }

        public uint Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        #endregion
    }
}