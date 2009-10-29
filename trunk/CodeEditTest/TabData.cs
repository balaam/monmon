using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;
using System.IO;
using System.Windows.Forms;

namespace MonMon
{
    class TabData
    {
        string _name;
        string _path;
        Scintilla _scintilla;

        public string Name
        {
            get
            {
                return _name;
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
        }

        internal void Save()
        {
            try
            {
                File.WriteAllText(_path, _scintilla.Text);
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show("Can't write to file, perhaps it's readonly?", "Write Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
