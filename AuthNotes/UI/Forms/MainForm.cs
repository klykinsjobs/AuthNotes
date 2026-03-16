using AuthNotes.Domain.Entities;
using AuthNotes.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace AuthNotes.UI.Forms
{
    public partial class MainForm : Form
    {
        private User? _currentUser;
        private readonly IServiceProvider _provider;

        private bool _isLogout = false;

        public MainForm(IServiceProvider provider)
        {
            InitializeComponent();
            _provider = provider;
        }

        public void SetUser(User user)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));

            UsernameLabel.Text = $"{user.Username} ({user.Role})";
            ManageUsersButton.Enabled = user.Role == Role.Admin;
        }

        private void NotesButton_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("No user is logged in");
                return;
            }

            var notes = _provider.GetRequiredService<NotesForm>();
            notes.SetUser(_currentUser);
            notes.ShowDialog();
        }

        private void ManageUsersButton_Click(object sender, EventArgs e)
        {
            var users = _provider.GetRequiredService<ManageUsersForm>();
            users.ShowDialog();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            _isLogout = true;
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isLogout)
                return;

            System.Windows.Forms.Application.Exit();
        }
    }
}
