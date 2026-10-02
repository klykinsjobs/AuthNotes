namespace AuthNotes.UI.Forms
{
    partial class NotesForm
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
            NotesTLP = new TableLayoutPanel();
            ControlsTLP = new TableLayoutPanel();
            AddButton = new Button();
            UpdateButton = new Button();
            DeleteButton = new Button();
            ClearSelectionButton = new Button();
            StatusLabel = new Label();
            ContentTLP = new TableLayoutPanel();
            ContentTextBox = new TextBox();
            ContentLabel = new Label();
            NotesGrid = new DataGridView();
            TitleLabel = new Label();
            TitleTextBox = new TextBox();
            NotesTLP.SuspendLayout();
            ControlsTLP.SuspendLayout();
            ContentTLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NotesGrid).BeginInit();
            SuspendLayout();
            // 
            // NotesTLP
            // 
            NotesTLP.ColumnCount = 2;
            NotesTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            NotesTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            NotesTLP.Controls.Add(ControlsTLP, 1, 0);
            NotesTLP.Controls.Add(StatusLabel, 0, 1);
            NotesTLP.Controls.Add(ContentTLP, 0, 0);
            NotesTLP.Dock = DockStyle.Fill;
            NotesTLP.Location = new Point(0, 0);
            NotesTLP.Name = "NotesTLP";
            NotesTLP.RowCount = 2;
            NotesTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            NotesTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            NotesTLP.Size = new Size(800, 450);
            NotesTLP.TabIndex = 0;
            // 
            // ControlsTLP
            // 
            ControlsTLP.ColumnCount = 1;
            ControlsTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ControlsTLP.Controls.Add(AddButton, 0, 0);
            ControlsTLP.Controls.Add(UpdateButton, 0, 1);
            ControlsTLP.Controls.Add(DeleteButton, 0, 2);
            ControlsTLP.Controls.Add(ClearSelectionButton, 0, 3);
            ControlsTLP.Dock = DockStyle.Fill;
            ControlsTLP.Location = new Point(603, 3);
            ControlsTLP.Name = "ControlsTLP";
            ControlsTLP.RowCount = 4;
            NotesTLP.SetRowSpan(ControlsTLP, 2);
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            ControlsTLP.Size = new Size(194, 444);
            ControlsTLP.TabIndex = 2;
            // 
            // AddButton
            // 
            AddButton.Dock = DockStyle.Fill;
            AddButton.Location = new Point(3, 3);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(188, 105);
            AddButton.TabIndex = 6;
            AddButton.Text = "Add";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Dock = DockStyle.Fill;
            UpdateButton.Location = new Point(3, 114);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(188, 105);
            UpdateButton.TabIndex = 7;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Dock = DockStyle.Fill;
            DeleteButton.Location = new Point(3, 225);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(188, 105);
            DeleteButton.TabIndex = 8;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // ClearSelectionButton
            // 
            ClearSelectionButton.Dock = DockStyle.Fill;
            ClearSelectionButton.Location = new Point(3, 336);
            ClearSelectionButton.Name = "ClearSelectionButton";
            ClearSelectionButton.Size = new Size(188, 105);
            ClearSelectionButton.TabIndex = 9;
            ClearSelectionButton.Text = "Clear Selection";
            ClearSelectionButton.UseVisualStyleBackColor = true;
            ClearSelectionButton.Click += ClearSelectionButton_Click;
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Dock = DockStyle.Fill;
            StatusLabel.Location = new Point(3, 382);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(594, 68);
            StatusLabel.TabIndex = 10;
            StatusLabel.Text = "...";
            StatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ContentTLP
            // 
            ContentTLP.ColumnCount = 1;
            ContentTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            ContentTLP.Controls.Add(ContentTextBox, 0, 4);
            ContentTLP.Controls.Add(ContentLabel, 0, 3);
            ContentTLP.Controls.Add(NotesGrid, 0, 0);
            ContentTLP.Controls.Add(TitleLabel, 0, 1);
            ContentTLP.Controls.Add(TitleTextBox, 0, 2);
            ContentTLP.Dock = DockStyle.Fill;
            ContentTLP.Location = new Point(3, 3);
            ContentTLP.Name = "ContentTLP";
            ContentTLP.RowCount = 5;
            ContentTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            ContentTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
            ContentTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
            ContentTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 11.1111107F));
            ContentTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            ContentTLP.Size = new Size(594, 376);
            ContentTLP.TabIndex = 1;
            // 
            // ContentTextBox
            // 
            ContentTextBox.Dock = DockStyle.Fill;
            ContentTextBox.Location = new Point(3, 251);
            ContentTextBox.Multiline = true;
            ContentTextBox.Name = "ContentTextBox";
            ContentTextBox.ScrollBars = ScrollBars.Vertical;
            ContentTextBox.Size = new Size(588, 122);
            ContentTextBox.TabIndex = 5;
            // 
            // ContentLabel
            // 
            ContentLabel.AutoSize = true;
            ContentLabel.Dock = DockStyle.Fill;
            ContentLabel.Location = new Point(3, 207);
            ContentLabel.Name = "ContentLabel";
            ContentLabel.Size = new Size(588, 41);
            ContentLabel.TabIndex = 12;
            ContentLabel.Text = "Content:";
            ContentLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NotesGrid
            // 
            NotesGrid.AllowUserToAddRows = false;
            NotesGrid.AllowUserToDeleteRows = false;
            NotesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            NotesGrid.Dock = DockStyle.Fill;
            NotesGrid.Location = new Point(3, 3);
            NotesGrid.MultiSelect = false;
            NotesGrid.Name = "NotesGrid";
            NotesGrid.ReadOnly = true;
            NotesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            NotesGrid.Size = new Size(588, 119);
            NotesGrid.TabIndex = 3;
            NotesGrid.SelectionChanged += NotesGrid_SelectionChanged;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Dock = DockStyle.Fill;
            TitleLabel.Location = new Point(3, 125);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(588, 41);
            TitleLabel.TabIndex = 11;
            TitleLabel.Text = "Title:";
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TitleTextBox
            // 
            TitleTextBox.Dock = DockStyle.Fill;
            TitleTextBox.Location = new Point(3, 169);
            TitleTextBox.Name = "TitleTextBox";
            TitleTextBox.Size = new Size(588, 23);
            TitleTextBox.TabIndex = 4;
            // 
            // NotesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(NotesTLP);
            Name = "NotesForm";
            Text = "Notes";
            NotesTLP.ResumeLayout(false);
            NotesTLP.PerformLayout();
            ControlsTLP.ResumeLayout(false);
            ContentTLP.ResumeLayout(false);
            ContentTLP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NotesGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel NotesTLP;
        private TableLayoutPanel ContentTLP;
        private TableLayoutPanel ControlsTLP;
        private DataGridView NotesGrid;
        private TextBox TitleTextBox;
        private TextBox ContentTextBox;
        private Button AddButton;
        private Button UpdateButton;
        private Button DeleteButton;
        private Button ClearSelectionButton;
        private Label StatusLabel;
        private Label TitleLabel;
        private Label ContentLabel;
    }
}