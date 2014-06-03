namespace MemoryGameUI
{
    using System.Windows.Forms;
    using MemoryGameLogic;
    using MemoryGameLogic.GameEventArgs;
    using MemoryGameUI.EventArgs;
    using MemoryGameUI.Forms;

    public class GameController
    {
        #region Fields

        private readonly GameLogic r_GameLogic;
        private readonly SettingsForm r_SettingsForm;
        private BoardForm m_BoardForm;

        #endregion

        #region Constructors and Destructors

        public GameController()
        {
            r_GameLogic = new GameLogic();
            r_SettingsForm = new SettingsForm();
        }

        #endregion

        #region Public Methods and Operators

        public void StartApplication()
        {
            r_SettingsForm.Start += settingsForm_Start;
            r_GameLogic.NewGame += gameLogic_NewGame;
            r_SettingsForm.ShowDialog();
        }

        #endregion

        #region Methods

        private void boardCell_VisibilityChanged(object sender, System.EventArgs e)
        {
            Board.Cell cell = sender as Board.Cell;

            if (cell != null)
            {
                int line = cell.Line;
                int col = cell.Col;
                m_BoardForm.CellsView.GetButtonAt(line, col).SetDisabled(cell.Selected);
            }
        }

        private void cellsView_CellClicked(object sender, CellClickedEventArgs e)
        {
            r_GameLogic.PerformHumanMove(e.Line, e.Col);
        }

        private void gameLogic_ComputerMove(object sender, System.EventArgs e)
        {
            m_BoardForm.CellsView.EnableCells();

            GameLogic gameLogic = sender as GameLogic;
            if (gameLogic != null)
            {
                r_GameLogic.PerformComputerMove();
            }
        }

        private void gameLogic_GameOver(object sender, System.EventArgs e)
        {
            GameLogic gameLogic = sender as GameLogic;

            if (gameLogic != null)
            {
                MessageBox.Show(string.Format("The winner is: {0}", gameLogic.WinnerMessage));
            }
        }

        private void gameLogic_NewGame(object sender, MemoryGameEventArgs e)
        {
            r_GameLogic.ComputerMove += gameLogic_ComputerMove;
            r_GameLogic.PlayersInfoChanged += gameLogic_PlayersInfoChanged;
            r_GameLogic.GameOver += gameLogic_GameOver;
            setBoardCellsOnVisibilityChanged();
            m_BoardForm = new BoardForm(r_GameLogic.GameBoard, e.PlayerOneName, e.PlayerTwoName);
            m_BoardForm.CellsView.CellClicked += cellsView_CellClicked;
            m_BoardForm.ShowDialog();
        }

        private void gameLogic_PlayersInfoChanged(object sender, PlayersInfoChangedEventArgs e)
        {
            m_BoardForm.NamesView.UpdateInfo(e);
        }

        private void setBoardCellsOnVisibilityChanged()
        {
            int lines = r_GameLogic.GameBoard.Lines;
            int cols = r_GameLogic.GameBoard.Cols;

            for (int x = 0; x < lines; ++x)
            {
                for (int y = 0; y < cols; ++y)
                {
                    r_GameLogic.GameBoard.GetCellAt(x, y).VisibilityChanged += boardCell_VisibilityChanged;
                }
            }
        }

        private void settingsForm_Start(object sender, MemoryGameEventArgs e)
        {
            r_SettingsForm.Hide();
            r_GameLogic.InitializeNewGame(e.Lines, e.Cols, e.PlayerOneName, e.PlayerTwoName, e.GameVsComputer);
        }

        #endregion
    }
}