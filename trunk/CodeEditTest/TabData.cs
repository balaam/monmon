using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;
using System.IO;

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
            File.WriteAllText(_path, _scintilla.Text);
        }
    }
}
