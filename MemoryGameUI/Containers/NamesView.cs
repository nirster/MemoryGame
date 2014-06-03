namespace MemoryGameUI.Containers
{
    using System.Drawing;
    using System.Windows.Forms;
    using MemoryGameLogic.GameEventArgs;
    using MemoryGameUI;
    using MemoryGameUI.Controls;

    public sealed class NamesView : FlowLayoutPanel
    {
        #region Fields

        private readonly NameLabel r_CurrentPlayer;
        private readonly NameLabel r_FirstPlayer;
        private readonly NameLabel r_SecondPlayer;
        private readonly Color r_FirstColor;
        private readonly Color r_SecondColor;
        private string m_CurrentName;
        private string m_FirstName;
        private string m_SecondName;

        #endregion

        #region Constructors and Destructors

        public NamesView(string i_FirstName, string i_SecondName)
        {
            m_FirstName = m_CurrentName = i_FirstName;
            m_SecondName = i_SecondName;
            r_CurrentPlayer = new NameLabel();
            r_FirstPlayer = new NameLabel();
            r_SecondPlayer = new NameLabel();
            r_FirstColor = GameViewSettings.FirstColor;
            r_SecondColor = GameViewSettings.SecondColor;
            setup();
        }

        #endregion

        #region Public Methods and Operators

        public void UpdateInfo(PlayersInfoChangedEventArgs e)
        {
            if (!m_CurrentName.Equals(e.CurrentPlayerName))
            {
                GameViewSettings.SwapCurrentPlayerColor();
            }

            m_FirstName = e.FirstPlayerName;
            m_SecondName = e.SecondPlayerName;
            m_CurrentName = e.CurrentPlayerName;
            r_FirstPlayer.Text = createPlayerText(e.FirstPlayerName, e.FirstPlayerScore);
            r_SecondPlayer.Text = createPlayerText(e.SecondPlayerName, e.SecondPlayerScore);
            r_CurrentPlayer.Text = createCurrentPlayerText(m_CurrentName);
            r_CurrentPlayer.BackColor = GameViewSettings.CurrentPlayerColor;
        }

        #endregion

        #region Methods

        private static string createCurrentPlayerText(string i_Name)
        {
            return string.Format("Current Player: {0}", i_Name);
        }

        private static string createPlayerText(string i_Name, uint i_Score)
        {
            return string.Format("{0}: {1} Pairs", i_Name, i_Score);
        }

        private void setup()
        {
            FlowDirection = FlowDirection.TopDown;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(5, 5, 5, 5);
            Control[] labels = { r_CurrentPlayer, r_FirstPlayer, r_SecondPlayer };
            Controls.AddRange(labels);
            r_CurrentPlayer.BackColor = r_FirstPlayer.BackColor = r_FirstColor;
            r_SecondPlayer.BackColor = r_SecondColor;
            r_CurrentPlayer.Text = createCurrentPlayerText(m_FirstName);
            r_FirstPlayer.Text = createPlayerText(m_FirstName, 0);
            r_SecondPlayer.Text = createPlayerText(m_SecondName, 0);
        }

        #endregion
    }
}