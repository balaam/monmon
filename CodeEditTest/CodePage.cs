using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ScintillaNet;
using System.IO;
using Einfall.Editor.Lua;

namespace MonMon
{
    public partial class CodePage : DockContent
    {
        string _path;
        Scintilla _scintilla;
        AutoFormat _autoFormat = new AutoFormat();
        AutoCompleteLua _autoComplete;
        public event EventHandler OnModifiedFlagChanged;
        int _diskHash;
        bool _modified = false;
     
        public Scintilla Scintilla
        {
            get
            {
                return _scintilla;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }

            set
            {
                _path = value;
                UpdateName();
            }
        }

        public CodePage(Scintilla scintilla, Dictionary<string, List<CompleteData>> autoComplete)
        {

            InitializeComponent();
            _scintilla = scintilla;
            _scintilla.Dock = DockStyle.Fill;
            _autoComplete = new AutoCompleteLua(_scintilla, autoComplete, _autoFormat);

            SetHorizontalScrollBar();

            
            Controls.Add(_scintilla);
            _scintilla.UndoRedo.EmptyUndoBuffer();
            _modified = false;
            _diskHash = _scintilla.Text.GetHashCode();
            _scintilla.CharAdded += new EventHandler<CharAddedEventArgs>(OnScintillaCharAdded);
            _scintilla.TextChanged += new EventHandler<EventArgs>(OnTextChanged);    
        }

        /// <summary>
        /// On loading the file go to the end of longest line.
        /// This sets up the scrollbar correctly, at least for opening the file.
        /// </summary>
        private void SetHorizontalScrollBar()
        {
            int maxLineLength = 0;
            Line longestLine = null;
            foreach (Line l in _scintilla.Lines)
            {
                maxLineLength = Math.Max(l.Length, maxLineLength);
                if (l.Length == maxLineLength)
                {
                    longestLine = l;
                }
            }

            if (longestLine != null)
            {
                _scintilla.Caret.Goto(longestLine.EndPosition);
                _scintilla.Caret.Goto(0);

            }
        }




        private void UpdateName()
        {
            if (_modified)
            {
                Text = "*" + System.IO.Path.GetFileName(_path);
            }
            else
            {
                Text = System.IO.Path.GetFileName(_path);
            }
        }

        public void SetModifiedFlag(bool value)
        {
           _modified = value;
           UpdateName();
           OnModifiedFlagChanged(this, EventArgs.Empty);
        }


        void OnTextChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla) sender;
            bool same = scintilla.Text.GetHashCode() == _diskHash;
            SetModifiedFlag(!same);
        }



        void OnScintillaCharAdded(object sender, CharAddedEventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

            if (_autoComplete.State == CharacterEnterState.DuringAutoPrompt)
            {
                bool handled = _autoComplete.OnCharAddedDuringAutoPrompt(scintilla, e.Ch);

                if (handled)
                {
                    return;
                }

            }

            if(e.Ch == '(')
            {
                scintilla.CallTip.Show("Your smart Tooltip functionality", scintilla.CurrentPos);
                
            }
            else
            {
                scintilla.CallTip.Cancel();
            }


            // No need to do intellisense in comments (this check doesnt work)
            if (scintilla.PositionIsOnComment(scintilla.CurrentPos))
            {
                return;
            }


            // If its the end of a function then add end
            if (e.Ch == ')')
            {
                _autoFormat.OnClosingParenAdded(scintilla);

            }
            else if (e.Ch == '\r')
            {
                _autoFormat.OnEnterPressed(scintilla);
            }
            else if (e.Ch == '.' || e.Ch == ':')
            {
                _autoComplete.OnAutoCompleteCharAdded(scintilla);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _scintilla.Dispose();
            
            base.OnClosed(e);
        }

        internal void Save(SaveFileDialog saveDialog)
        {
            if (_path == null)
            {
                if (saveDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _path = saveDialog.FileName;
                }
                else
                {
                 //   MessageBox.Show("Need to write functionality for saving new files");
                    return;
                }

            }
            bool success = true;
            try
            {
                File.WriteAllText(_path, _scintilla.Text);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Can't write to file, perhaps it's readonly?", "Write Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }

            if (success)
            {
                SetModifiedFlag(false);
                _diskHash = _scintilla.Text.GetHashCode();
            }
        }
    }
}
