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
        TabPage _tabPage;

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

        public TabPage TabPage
        {
            get
            {
                return _tabPage;
            }
        }

        public FindResult(int start, int end, TabPage tabpage)
        {
            _start = start;
            _end = end;
            _tabPage = tabpage;
        }

        public override string ToString()
        {
            Scintilla scintilla = (Scintilla)_tabPage.Controls[0];
            Line line = scintilla.Lines.FromPosition(_start);
            return line.Text;
        }
    }
}
