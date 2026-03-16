using AuthNotes.Application.DTOs;
using AuthNotes.Application.Services;
using AuthNotes.Domain.Enums;
using System.Diagnostics;

namespace AuthNotes.UI.Forms
{
    public partial class ManageUsersForm : Form
    {
        private readonly UserService _users;
        private readonly BindingSource _binding = [];

        private bool _suppressSelectionEvents = false;

        public ManageUsersForm(UserService users)
        {
            InitializeComponent();
            _users = users;

            UsersGrid.AutoGenerateColumns = true;
            UsersGrid.DataSource = _binding;

            RoleComboBox.DataSource = Enum.GetValues<Role>();

            _ = LoadUsersAsync();
        }

        private void SetStatus(string message)
        {
            StatusLabel.Text = message;
        }

        private void ClearInputs()
        {
            UsernameTextBox.Clear();
            PasswordMaskedTextBox.Clear();
            RoleComboBox.SelectedIndex = -1;
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = UsersGrid.SelectedRows.Count > 0;

            AddButton.Enabled = !hasSelection;
            UpdateButton.Enabled = hasSelection;
            DeleteButton.Enabled = hasSelection;
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var data = await _users.GetAllAsync();
                _binding.DataSource = data ?? [];

                _suppressSelectionEvents = true;
                UsersGrid.ClearSelection();
                _suppressSelectionEvents = false;

                UpdateButtonStates();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load users");
                Debug.WriteLine(ex);
            }
        }

        private async void AddButton_Click(object sender, EventArgs e)
            => await AddButton_ClickAsync(sender, e);

        private async Task AddButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (RoleComboBox.SelectedItem is not Role role)
                {
                    MessageBox.Show("Please select a role.");
                    return;
                }

                await _users.AddAsync(UsernameTextBox.Text.Trim(), PasswordMaskedTextBox.Text, (Role)RoleComboBox.SelectedItem);

                await LoadUsersAsync();
                SetStatus("User added.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while adding user.");
                Debug.WriteLine(ex);
            }
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
            => await UpdateButton_ClickAsync(sender, e);

        private async Task UpdateButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (UsersGrid.CurrentRow?.DataBoundItem is not UserDto dto)
                    return;

                if (RoleComboBox.SelectedItem is not Role role)
                {
                    MessageBox.Show("Please select a role.");
                    return;
                }

                await _users.UpdateAsync(
                    dto.Id,
                    UsernameTextBox.Text.Trim(),
                    string.IsNullOrWhiteSpace(PasswordMaskedTextBox.Text) ? null : PasswordMaskedTextBox.Text,
                    (Role)RoleComboBox.SelectedItem);

                await LoadUsersAsync();
                SetStatus("User updated.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while updating user.");
                Debug.WriteLine(ex);
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
            => await DeleteButton_ClickAsync(sender, e);

        private async Task DeleteButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (UsersGrid.CurrentRow?.DataBoundItem is not UserDto dto)
                    return;

                if (MessageBox.Show($"Delete user '{dto.Username}'?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await _users.DeleteAsync(dto.Id);

                    await LoadUsersAsync();
                    SetStatus("User deleted.");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while deleting user.");
                Debug.WriteLine(ex);
            }
        }

        private void UsersGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionEvents)
                return;

            if (UsersGrid.CurrentRow?.DataBoundItem is not UserDto dto)
            {
                ClearInputs();
                UpdateButtonStates();
                return;
            }

            UsernameTextBox.Text = dto.Username;
            RoleComboBox.SelectedItem = dto.Role;
            PasswordMaskedTextBox.Clear();

            UpdateButtonStates();
        }

        private void ClearSelectionButton_Click(object sender, EventArgs e)
        {
            _suppressSelectionEvents = true;
            UsersGrid.ClearSelection();
            _suppressSelectionEvents = false;

            ClearInputs();
            UpdateButtonStates();
            SetStatus("Selection cleared.");
        }
    }
}
