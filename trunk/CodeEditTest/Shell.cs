using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonMon
{
    /// <summary>
    /// Handles shell commands
    /// </summary>
    class Shell
    {
        TextBox _commandTextBox;
        ListBox _commandHistoryListBox;
        public Shell(TextBox commandBox, ListBox commandHistory)
        {
            _commandTextBox = commandBox;
            _commandHistoryListBox = commandHistory;
            _commandTextBox.KeyDown += new KeyEventHandler(OnKeyDown);
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string command = _commandTextBox.Text;
                _commandTextBox.Text = "";
                int newIndex = _commandHistoryListBox.Items.Add("[" + command + "] unrecognised.");
                _commandHistoryListBox.SelectedIndex = newIndex;
                e.SuppressKeyPress = true;
            }

        }
    }
}
