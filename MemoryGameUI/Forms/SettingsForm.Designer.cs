namespace MemoryGameUI.Forms
{
    using System.Windows.Forms;

    public partial class SettingsForm
    {
        private Label labelFirstName;
        private Label labelSecondName;
        private Label labelBoardSIze;
        private Button buttonStart;
        private TextBox textBoxFirstName;
        private TextBox textBoxSecondName;
        private Button buttonFriendComputer;
        private void InitializeComponent()
        {
            this.buttonFriendComputer = new System.Windows.Forms.Button();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.labelSecondName = new System.Windows.Forms.Label();
            this.labelBoardSIze = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxSecondName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonFriendComputer
            // 
            this.buttonFriendComputer.Location = new System.Drawing.Point(390, 64);
            this.buttonFriendComputer.Name = "buttonFriendComputer";
            this.buttonFriendComputer.Size = new System.Drawing.Size(112, 23);
            this.buttonFriendComputer.TabIndex = 0;
            this.buttonFriendComputer.Text = "Against a Friend";
            this.buttonFriendComputer.UseVisualStyleBackColor = true;
            this.buttonFriendComputer.Click += new System.EventHandler(this.buttonFriendComputer_Click);
            // 
            // labelFirstName
            // 
            this.labelFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFirstName.Location = new System.Drawing.Point(44, 28);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(170, 23);
            this.labelFirstName.TabIndex = 1;
            this.labelFirstName.Text = "First Player Name:";
            // 
            // labelSecondName
            // 
            this.labelSecondName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSecondName.Location = new System.Drawing.Point(44, 64);
            this.labelSecondName.Name = "labelSecondName";
            this.labelSecondName.Size = new System.Drawing.Size(206, 23);
            this.labelSecondName.TabIndex = 2;
            this.labelSecondName.Text = "Second Player Name:";
            // 
            // labelBoardSIze
            // 
            this.labelBoardSIze.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBoardSIze.Location = new System.Drawing.Point(44, 116);
            this.labelBoardSIze.Name = "labelBoardSIze";
            this.labelBoardSIze.Size = new System.Drawing.Size(100, 23);
            this.labelBoardSIze.TabIndex = 3;
            this.labelBoardSIze.Text = "Board Size:";
            // 
            // buttonStart
            // 
            this.buttonStart.AutoSize = true;
            this.buttonStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.Location = new System.Drawing.Point(415, 192);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(87, 28);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            //this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(229, 30);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(130, 20);
            this.textBoxFirstName.TabIndex = 6;
            this.textBoxFirstName.TextChanged += new System.EventHandler(this.textBoxFirstName_TextChanged);
            // 
            // textBoxSecondName
            // 
            this.textBoxSecondName.Enabled = false;
            this.textBoxSecondName.Location = new System.Drawing.Point(229, 67);
            this.textBoxSecondName.Name = "textBoxSecondName";
            this.textBoxSecondName.Size = new System.Drawing.Size(130, 20);
            this.textBoxSecondName.TabIndex = 7;
            this.textBoxSecondName.Text = "- Computer -";
            this.textBoxSecondName.TextChanged += new System.EventHandler(this.textBoxSecondName_TextChanged);
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(528, 240);
            this.Controls.Add(this.textBoxSecondName);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelBoardSIze);
            this.Controls.Add(this.labelSecondName);
            this.Controls.Add(this.labelFirstName);
            this.Controls.Add(this.buttonFriendComputer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
