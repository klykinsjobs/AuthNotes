using AuthNotes.Application.DTOs;
using AuthNotes.Application.Services;
using AuthNotes.Domain.Entities;
using System.Diagnostics;

namespace AuthNotes.UI.Forms
{
    public partial class NotesForm : Form
    {
        private readonly NoteService _notes;
        private User? _currentUser;

        private bool _suppressSelectionEvents = false;

        public NotesForm(NoteService notes)
        {
            InitializeComponent();
            _notes = notes;
        }

        public void SetUser(User user)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));

            _ = LoadNotesAsync();
        }

        private void SetStatus(string message)
        {
            StatusLabel.Text = message;
        }

        private void ClearInputs()
        {
            TitleTextBox.Clear();
            ContentTextBox.Clear();
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = NotesGrid.SelectedRows.Count > 0;

            AddButton.Enabled = !hasSelection;
            UpdateButton.Enabled = hasSelection;
            DeleteButton.Enabled = hasSelection;
        }

        private async Task LoadNotesAsync()
        {
            try
            {
                var data = await _notes.GetAllAsync();
                NotesGrid.DataSource = data ?? [];

                _suppressSelectionEvents = true;
                NotesGrid.ClearSelection();
                _suppressSelectionEvents = false;

                UpdateButtonStates();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load notes");
                Debug.WriteLine(ex);
            }
        }

        private async void AddButton_Click(object sender, EventArgs e)
            => await AddButton_ClickAsync(sender, e);

        private async Task AddButton_ClickAsync(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("No user is logged in");
                return;
            }

            try
            {
                await _notes.AddAsync(TitleTextBox.Text, ContentTextBox.Text, _currentUser.Id);

                await LoadNotesAsync();
                SetStatus("Note added.");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while adding note.");
                Debug.WriteLine(ex);
            }
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
            => await UpdateButton_ClickAsync(sender, e);

        private async Task UpdateButton_ClickAsync(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("No user is logged in");
                return;
            }

            try
            {
                if (NotesGrid.CurrentRow?.DataBoundItem is NoteDto dto)
                {
                    await _notes.UpdateAsync(dto.Id, TitleTextBox.Text, ContentTextBox.Text, _currentUser.Id);

                    await LoadNotesAsync();
                    SetStatus("Note updated.");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while updating note.");
                Debug.WriteLine(ex);
            }
        }

        private async void DeleteButton_Click(object sender, EventArgs e)
            => await DeleteButton_ClickAsync(sender, e);

        private async Task DeleteButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (NotesGrid.CurrentRow?.DataBoundItem is NoteDto dto)
                {
                    await _notes.DeleteAsync(dto.Id);

                    await LoadNotesAsync();
                    SetStatus("Note deleted.");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while deleting note.");
                Debug.WriteLine(ex);
            }
        }

        private void NotesGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionEvents)
                return;

            if (NotesGrid.CurrentRow?.DataBoundItem is not NoteDto dto)
            {
                ClearInputs();
                UpdateButtonStates();
                return;
            }

            TitleTextBox.Text = dto.Title;
            ContentTextBox.Text = dto.Content;

            UpdateButtonStates();
        }

        private void ClearSelectionButton_Click(object sender, EventArgs e)
        {
            _suppressSelectionEvents = true;
            NotesGrid.ClearSelection();
            _suppressSelectionEvents = false;

            ClearInputs();
            UpdateButtonStates();
            SetStatus("Selection cleared.");
        }
    }
}
