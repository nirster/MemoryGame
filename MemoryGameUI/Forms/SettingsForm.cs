namespace MemoryGameUI.Forms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using MemoryGameLogic.GameEventArgs;
    using MemoryGameUI.Controls;

    public sealed partial class SettingsForm : Form
    {
        #region Constants

        private const string k_ComputerLabel = "-Computer-";
        private const string k_ComputerName = "Computer";

        #endregion

        #region Fields

        private readonly Color r_CustomGreen = Color.FromArgb(192, 192, 255);
        private bool m_AgainstHuman;
        private BoardSizeButton m_BoardSizeButton;

        #endregion

        #region Constructors and Destructors

        public SettingsForm()
        {
            InitializeComponent();
            intializeCustomSettings();
            intializeCustomControls();
        }

        #endregion

        #region Public Events

        public event EventHandler<MemoryGameEventArgs> Start;

        #endregion

        #region Methods

        private void OnStart(MemoryGameEventArgs e)
        {
            EventHandler<MemoryGameEventArgs> handler = Start;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void settingsForm_Closed(object sender, EventArgs e)
        {
            fireOnStartEventIfSettingsAreValid();
        }

        private bool areNamesValid()
        {
            return !string.IsNullOrEmpty(textBoxFirstName.Text) && !string.IsNullOrEmpty(textBoxSecondName.Text)
                   && !textBoxFirstName.Text.Equals(textBoxSecondName.Text);
        }

        private void buttonFriendComputer_Click(object sender, EventArgs e)
        {
            m_AgainstHuman = !m_AgainstHuman;

            if (!m_AgainstHuman)
            {
                disableSecondPlayerTextBox();
            }
            else
            {
                enableSecondPlayerTextBox();
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            fireOnStartEventIfSettingsAreValid();
        }

        private void changeButtonStartSettings()
        {
            buttonStart.Enabled = areNamesValid();
            buttonStart.BackColor = areNamesValid() ? r_CustomGreen : Color.DarkRed;
        }

        private MemoryGameEventArgs createMemoryGameEventArgs()
        {
            int lines = m_BoardSizeButton.Lines;
            int cols = m_BoardSizeButton.Cols;
            string firstName = textBoxFirstName.Text;
            string secondName = m_AgainstHuman ? textBoxSecondName.Text : k_ComputerName;
            bool gameVsComputer = !m_AgainstHuman;

            return new MemoryGameEventArgs(lines, cols, firstName, secondName, gameVsComputer);
        }

        private void disableSecondPlayerTextBox()
        {
            textBoxSecondName.Enabled = false;
            textBoxSecondName.Text = k_ComputerLabel;
        }

        private void enableSecondPlayerTextBox()
        {
            textBoxSecondName.Enabled = true;
            textBoxSecondName.Text = string.Empty;
        }

        private void fireOnStartEventIfSettingsAreValid()
        {
            if (!areNamesValid())
            {
                MessageBox.Show("Please enter players names.");
            }
            else
            {
                OnStart(createMemoryGameEventArgs());
            }
        }

        private void intializeCustomControls()
        {
            m_BoardSizeButton = new BoardSizeButton();
            Controls.Add(m_BoardSizeButton);
            textBoxFirstName.Select();
        }

        private void intializeCustomSettings()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            buttonStart.Click += buttonStart_Click;
            Closed += settingsForm_Closed;
            textBoxFirstName.KeyDown += settingsForm_KeyDown;
            textBoxSecondName.KeyDown += textBoxSecondName_KeyDown;
        }

        private void settingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && buttonStart.Enabled)
            {
                fireOnStartEventIfSettingsAreValid();
            }
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            changeButtonStartSettings();
        }

        private void textBoxSecondName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && buttonStart.Enabled)
            {
                fireOnStartEventIfSettingsAreValid();
            }
        }

        private void textBoxSecondName_TextChanged(object sender, EventArgs e)
        {
            changeButtonStartSettings();
        }

        #endregion
    }
}