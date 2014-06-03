namespace MemoryGameLogic
{
    public sealed class Choice
    {
        #region Fields

        private readonly int r_Col;
        private readonly int r_Line;

        #endregion

        #region Constructors and Destructors

        public Choice(int i_Line, int i_Col)
        {
            r_Line = i_Line;
            r_Col = i_Col;
        }

        public Choice(Board.Cell i_Cell)
        {
            r_Line = i_Cell.Line;
            r_Col = i_Cell.Col;
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