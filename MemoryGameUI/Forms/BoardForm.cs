namespace MemoryGameUI.Forms
{
    using System;
    using System.Windows.Forms;
    using MemoryGameLogic;
    using MemoryGameUI.Containers;

    public sealed partial class BoardForm : Form
    {
        #region Fields

        private readonly CellsView r_CellsView;
        private readonly NamesView r_NamesView;

        #endregion

        #region Constructors and Destructors

        public BoardForm(Board i_Board, string i_FirstName, string i_SecondName)
        {
            InitializeComponent();
            FlowLayoutPanel mainLayout = new FlowLayoutPanel();
            mainLayout.FlowDirection = FlowDirection.TopDown;
            mainLayout.Padding = new Padding(15, 5, 5, 5);
            mainLayout.AutoSize = true;
            mainLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            r_CellsView = new CellsView(i_Board);
            r_NamesView = new NamesView(i_FirstName, i_SecondName);
            mainLayout.Controls.AddRange(new Control[] { r_CellsView, r_NamesView });
            Controls.Add(mainLayout);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(0, 0, 0, 0);
            Closed += BoardForm_Closed;
        }

        #endregion

        #region Public Properties

        public CellsView CellsView
        {
            get
            {
                return r_CellsView;
            }
        }

        public NamesView NamesView
        {
            get
            {
                return r_NamesView;
            }
        }

        #endregion

        #region Methods

        private void BoardForm_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion
    }
}