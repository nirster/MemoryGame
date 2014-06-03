namespace MemoryGameUI.Controls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public sealed class BoardSizeButton : Button
    {
        #region Constants

        private const int k_MaxCols = 6;
        private const int k_MaxLines = 6;
        private const int k_MinCols = 4;
        private const int k_MinLines = 4;

        #endregion

        #region Fields

        private int m_CurrentCols;
        private int m_CurrentLines;

        #endregion

        #region Constructors and Destructors

        public BoardSizeButton()
        {
            m_CurrentCols = k_MinCols;
            m_CurrentLines = k_MinLines;
            setup();
            initializeControlEvents();
        }

        #endregion

        #region Public Properties

        public int Cols
        {
            get
            {
                return m_CurrentCols;
            }
        }

        public int Lines
        {
            get
            {
                return m_CurrentLines;
            }
        }

        #endregion

        #region Methods

        private string createLinesAndColsString()
        {
            return string.Format("{0} x {1}", m_CurrentLines, m_CurrentCols);
        }

        private void incrementLinesAndCols()
        {
            m_CurrentCols++;

            if (m_CurrentCols > k_MaxCols)
            {
                m_CurrentLines++;
                m_CurrentCols = k_MinCols;
            }

            if (m_CurrentLines > k_MaxLines)
            {
                m_CurrentLines = k_MinLines;
            }

            if (m_CurrentCols == 5 && m_CurrentLines == 5)
            {
                m_CurrentLines = 5;
                m_CurrentCols = 6;
            }
        }

        private void initializeControlEvents()
        {
            Click += sizeButton_Click;
        }

        private void setup()
        {
            BackColor = SystemColors.ActiveCaption;
            Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Location = new Point(47, 142);
            Name = "buttonBoardSize";
            Size = new Size(137, 78);
            TabIndex = 4;
            UseVisualStyleBackColor = false;
            Text = createLinesAndColsString();
        }

        private void sizeButton_Click(object sender, EventArgs e)
        {
            incrementLinesAndCols();
            Text = createLinesAndColsString();
        }

        #endregion
    }
}