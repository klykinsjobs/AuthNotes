using AuthNotes.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace AuthNotes.UI.Forms
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth;
        private readonly IServiceProvider _provider;

        private bool _suppressCheckedEvents = false;

        public LoginForm(AuthService auth, IServiceProvider provider)
        {
            InitializeComponent();
            _auth = auth;
            _provider = provider;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
            => await LoginButton_ClickAsync(sender, e);

        private async Task LoginButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                var user = await _auth.LoginAsync(UsernameTextBox.Text.Trim(), PasswordMaskedTextBox.Text);

                if (user != null)
                {
                    this.Hide();
                    ResetLoginForm();

                    var main = _provider.GetRequiredService<MainForm>();
                    main.SetUser(user);
                    main.ShowDialog();

                    this.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error during login.");
                Debug.WriteLine(ex);
            }
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressCheckedEvents)
                return;

            PasswordMaskedTextBox.UseSystemPasswordChar = !ShowPasswordCheckBox.Checked;
        }

        private void ResetLoginForm()
        {
            UsernameTextBox.Clear();
            PasswordMaskedTextBox.Clear();

            _suppressCheckedEvents = true;
            ShowPasswordCheckBox.Checked = false;
            _suppressCheckedEvents = false;

            PasswordMaskedTextBox.UseSystemPasswordChar = true;
        }
    }
}
