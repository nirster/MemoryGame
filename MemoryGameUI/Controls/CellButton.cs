namespace MemoryGameUI.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using MemoryGameUI;

    public sealed class CellButton : Button
    {
        #region Fields

        private readonly int r_Col;
        private readonly char r_Letter;
        private readonly int r_Line;

        private bool m_EnableDebugMode = false;
        private ToolTip m_ToolTip;

        #endregion

        #region Constructors and Destructors

        public CellButton(int i_Line, int i_Col, char i_Letter)
        {
            r_Line = i_Line;
            r_Col = i_Col;
            r_Letter = i_Letter;
            const int height = 100;
            const int width = 100;
            Size = new Size(width, height);
            Margin = new Padding(5, 5, 5, 5);
            Text = string.Empty;
            Font = new Font("Microsoft Sans Serif", 15.00F, FontStyle.Bold, GraphicsUnit.Point, 0);

            if (m_EnableDebugMode)
            {
                setupToolTip();
            }
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

        #endregion

        #region Public Methods and Operators

        public void SetDisabled(bool i_Value)
        {
            if (i_Value)
            {
                SetDisabled();
            }
            else
            {
                setEnabled();
            }
        }

        #endregion

        #region Methods

        private void SetDisabled()
        {
            Text = Letter.ToString();
            BackColor = GameViewSettings.CurrentPlayerColor;
            Enabled = false;
        }

        private void cellButton_MouseEnter(object sender, EventArgs e)
        {
            m_ToolTip.Show(Letter.ToString(), this);
        }

        private void cellButton_MouseLeave(object sender, EventArgs e)
        {
            m_ToolTip.Hide(this);
        }

        private void setEnabled()
        {
            Text = string.Empty;
            ResetBackColor();
            Enabled = true;
        }

        private void setupToolTip()
        {
            m_ToolTip = new ToolTip();
            MouseEnter += cellButton_MouseEnter;
            MouseLeave += cellButton_MouseLeave;
        }

        #endregion
    }
}