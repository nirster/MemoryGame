namespace MemoryGameUI.EventArgs
{
    using System;

    public class CellClickedEventArgs : EventArgs
    {
        #region Fields

        private readonly int r_Col;
        private readonly int r_Line;

        #endregion

        #region Constructors and Destructors

        public CellClickedEventArgs(int i_Line, int i_Col)
        {
            r_Line = i_Line;
            r_Col = i_Col;
        }

        #endregion

        #region Public Properties

        public int Col
        {
            get
            {
                return r_Col;
            }
        }

        public int Line
        {
            get
            {
                return r_Line;
            }
        }

        #endregion
    }
}