﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;
using System.IO;
using System.Windows.Forms;
using Einfall.Editor.Lua;

namespace MonMon
{
    public class TabData
    {
        string _name;
        string _path;
        Scintilla _scintilla;
        AutoFormat _autoFormat = new AutoFormat();

        // Has the file been modified from the file on disk
        bool _modified = false;
        int _hashOfTextOnDisk = "".GetHashCode();

        public int DiskHash
        {
            get
            {
                return _hashOfTextOnDisk;
            }
        }

        public event EventHandler OnModifiedFlagChanged;

        public string Name
        {
            get
            {
                if (_modified)
                {
                    return "* " + _name;
                }
                else
                {
                    return _name;
                }
            }

            set
            {
                _name = value;
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
            }
        }

        public Scintilla Scintilla
        {
            get
            {
                return _scintilla;
            }
        }

        public TabData(string name, Scintilla scintilla)
        {
            _name = name;
            _scintilla = scintilla;
            
            // This will handle intellisense type stuff
            _scintilla.CharAdded += new EventHandler<CharAddedEventArgs>(OnScintillaCharAdded);
        }

        void OnScintillaCharAdded(object sender, CharAddedEventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

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
        }




        internal void Save()
        {
            bool success = true;
            try
            {
                File.WriteAllText(_path, _scintilla.Text);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("Can't write to file, perhaps it's readonly?", "Write Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }

            if (success)
            {
                SetModifiedFlag(false);
                SetDiskHash(_scintilla.Text.GetHashCode());
            }
        }

        internal void SetModifiedFlag(bool value)
        {
            _modified = value;
            OnModifiedFlagChanged(this, EventArgs.Empty);
        }

        internal void SetDiskHash(int diskHash)
        {
            _hashOfTextOnDisk = diskHash;
        }
    }
}