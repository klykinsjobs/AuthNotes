namespace AuthNotes.UI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainTLP = new TableLayoutPanel();
            UsernameLabel = new Label();
            ControlsTLP = new TableLayoutPanel();
            LogoutButton = new Button();
            NotesButton = new Button();
            ManageUsersButton = new Button();
            WelcomeLabel = new Label();
            MainTLP.SuspendLayout();
            ControlsTLP.SuspendLayout();
            SuspendLayout();
            // 
            // MainTLP
            // 
            MainTLP.ColumnCount = 2;
            MainTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            MainTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            MainTLP.Controls.Add(UsernameLabel, 0, 1);
            MainTLP.Controls.Add(ControlsTLP, 1, 0);
            MainTLP.Controls.Add(WelcomeLabel, 0, 0);
            MainTLP.Dock = DockStyle.Fill;
            MainTLP.Location = new Point(0, 0);
            MainTLP.Name = "MainTLP";
            MainTLP.RowCount = 2;
            MainTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            MainTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            MainTLP.Size = new Size(800, 450);
            MainTLP.TabIndex = 0;
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.Dock = DockStyle.Fill;
            UsernameLabel.Location = new Point(3, 382);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(594, 68);
            UsernameLabel.TabIndex = 6;
            UsernameLabel.Text = "Username";
            UsernameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ControlsTLP
            // 
            ControlsTLP.ColumnCount = 1;
            ControlsTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ControlsTLP.Controls.Add(LogoutButton, 0, 3);
            ControlsTLP.Controls.Add(NotesButton, 0, 0);
            ControlsTLP.Controls.Add(ManageUsersButton, 0, 1);
            ControlsTLP.Dock = DockStyle.Fill;
            ControlsTLP.Location = new Point(603, 3);
            ControlsTLP.Name = "ControlsTLP";
            ControlsTLP.RowCount = 4;
            MainTLP.SetRowSpan(ControlsTLP, 2);
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.Size = new Size(194, 444);
            ControlsTLP.TabIndex = 1;
            // 
            // LogoutButton
            // 
            LogoutButton.Dock = DockStyle.Fill;
            LogoutButton.Location = new Point(3, 336);
            LogoutButton.Name = "LogoutButton";
            LogoutButton.Size = new Size(188, 105);
            LogoutButton.TabIndex = 5;
            LogoutButton.Text = "Logout";
            LogoutButton.UseVisualStyleBackColor = true;
            LogoutButton.Click += LogoutButton_Click;
            // 
            // NotesButton
            // 
            NotesButton.Dock = DockStyle.Fill;
            NotesButton.Location = new Point(3, 3);
            NotesButton.Name = "NotesButton";
            NotesButton.Size = new Size(188, 105);
            NotesButton.TabIndex = 3;
            NotesButton.Text = "Notes";
            NotesButton.UseVisualStyleBackColor = true;
            NotesButton.Click += NotesButton_Click;
            // 
            // ManageUsersButton
            // 
            ManageUsersButton.Dock = DockStyle.Fill;
            ManageUsersButton.Location = new Point(3, 114);
            ManageUsersButton.Name = "ManageUsersButton";
            ManageUsersButton.Size = new Size(188, 105);
            ManageUsersButton.TabIndex = 4;
            ManageUsersButton.Text = "Manage Users";
            ManageUsersButton.UseVisualStyleBackColor = true;
            ManageUsersButton.Click += ManageUsersButton_Click;
            // 
            // WelcomeLabel
            // 
            WelcomeLabel.AutoSize = true;
            WelcomeLabel.Dock = DockStyle.Fill;
            WelcomeLabel.Location = new Point(3, 0);
            WelcomeLabel.Name = "WelcomeLabel";
            WelcomeLabel.Size = new Size(594, 382);
            WelcomeLabel.TabIndex = 2;
            WelcomeLabel.Text = "Welcome";
            WelcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MainTLP);
            Name = "MainForm";
            Text = "AuthNotes";
            FormClosing += MainForm_FormClosing;
            MainTLP.ResumeLayout(false);
            MainTLP.PerformLayout();
            ControlsTLP.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainTLP;
        private TableLayoutPanel ControlsTLP;
        private Label WelcomeLabel;
        private Button NotesButton;
        private Button ManageUsersButton;
        private Button LogoutButton;
        private Label UsernameLabel;
    }
}