using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNet; // dont need this can pass the string in

namespace MonMon
{
    class FindResult
    {
        int _start;
        int _end;
        CodePage _codePage;
        string _filePath;

        public int Start
        {
            get
            {
                return _start;
            }
        }

        public int End
        {
            get
            {
                return _end;
            }
        }

        public CodePage CodePage
        {
            get
            {
                return _codePage;
            }
        }

        public FindResult(int start, int end, CodePage cp)
        {
            _start = start;
            _end = end;
            _codePage = cp;
            _filePath = cp.Path;
        }

        public override string ToString()
        {
            Scintilla scintilla = (Scintilla)_codePage.Controls[0];
            
            Line line = scintilla.Lines.FromPosition(_start);
            string locationInfo = _filePath + " (" + line.Number.ToString() + "):\t";
            return locationInfo + line.Text.Trim();
        }
    }
}
