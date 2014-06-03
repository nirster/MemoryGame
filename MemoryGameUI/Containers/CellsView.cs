namespace MemoryGameUI.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using MemoryGameLogic;
    using MemoryGameUI.Controls;
    using MemoryGameUI.EventArgs;

    public sealed class CellsView : Panel
    {
        #region Fields

        private readonly CellButton[,] r_Cells;

        /* represents cells that are currently disabled but can be enabled again
         * if there was no match after the second move */
        private readonly List<CellButton> r_TempDisabledCells;

        #endregion

        #region Constructors and Destructors

        public CellsView(Board i_Board)
        {
            r_TempDisabledCells = new List<CellButton>();
            r_Cells = new CellButton[i_Board.Lines, i_Board.Cols];
            initializeComponent();
            initializeCells(i_Board);
        }

        #endregion

        #region Public Events

        public event EventHandler<CellClickedEventArgs> CellClicked;

        #endregion

        #region Public Methods and Operators

        public void DisableAndMakeVisibleCell(int i_Line, int i_Col)
        {
            CellButton cellButton = r_Cells[i_Line, i_Col];
            r_TempDisabledCells.Add(cellButton);
            cellButton.SetDisabled(true);
        }

        public void DisableEnablingOfCurrentlyDisabledCells()
        {
            r_TempDisabledCells.Clear();
        }

        public void EnableCells()
        {
            foreach (CellButton cellButton in r_TempDisabledCells)
            {
                cellButton.SetDisabled(false);
            }

            r_TempDisabledCells.Clear();
        }

        public CellButton GetButtonAt(int i_Line, int i_Col)
        {
            return r_Cells[i_Line, i_Col];
        }

        #endregion

        #region Methods

        private void OnCellClicked(CellClickedEventArgs e)
        {
            EventHandler<CellClickedEventArgs> handler = CellClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void cellButton_Click(object sender, EventArgs e)
        {
            CellButton button = sender as CellButton;
            if (button != null)
            {
                OnCellClicked(new CellClickedEventArgs(button.Line, button.Col));
            }
        }

        private void initializeCells(Board i_Board)
        {
            const int spacing = 10;
            int lines = i_Board.Lines;
            int cols = i_Board.Cols;
            int startX = Left;
            int startY = Top;

            for (int x = 0; x < lines; ++x)
            {
                for (int y = 0; y < cols; ++y)
                {
                    char letter = i_Board.GetCellAt(x, y).Letter;
                    r_Cells[x, y] = new CellButton(x, y, letter);
                    int xPosition = startX + ((spacing + r_Cells[x, y].Width) * x);
                    int yPosition = startY + ((spacing + r_Cells[x, y].Height) * y);
                    r_Cells[x, y].Location = new Point(xPosition, yPosition);
                    r_Cells[x, y].Click += cellButton_Click;
                    Controls.Add(r_Cells[x, y]);
                }
            }
        }

        private void initializeComponent()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        #endregion
    }
}