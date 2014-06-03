namespace MemoryGameUI
{
    using System.Drawing;

    public static class GameViewSettings
    {
        #region Static Fields

        private static readonly Color sr_FirstColor = Color.Brown;
        private static readonly Color sr_SecondColor = Color.Chartreuse; 
        private static Color s_CurrentPlayerColor = Color.Brown;

        #endregion

        #region Public Properties

        public static Color CurrentPlayerColor
        {
            get
            {
                return s_CurrentPlayerColor;
            }
        }

        public static Color FirstColor
        {
            get
            {
                return sr_FirstColor;
            }
        }

        public static Color SecondColor
        {
            get
            {
                return sr_SecondColor;
            }
        }

        #endregion

        #region Public Methods and Operators

        public static void SwapCurrentPlayerColor()
        {
            s_CurrentPlayerColor = s_CurrentPlayerColor == sr_FirstColor ? sr_SecondColor : sr_FirstColor;
        }

        #endregion
    }
}