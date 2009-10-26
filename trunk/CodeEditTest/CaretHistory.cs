using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonMon
{
    class CaretHistory
    {
        TabPage _tabPage; // this feels a bit strongly coupled.
        int _caretPosition;

        public TabPage TabPage
        {
            get
            {
                return _tabPage;
            }
        }

        public int Position
        {
            get
            {
                return _caretPosition;
            }
        }

        public CaretHistory(TabPage page, int position)
        {
            _tabPage = page;
            _caretPosition = position;
        }

    }
}
