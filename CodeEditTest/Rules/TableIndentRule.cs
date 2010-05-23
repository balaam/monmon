using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonMon.Rules
{
    class TableIndentRule : FormatRule
    {
        public override string Name
        {
            get { return "table_indent"; }
        }

        public override bool CanFire(ScintillaNet.Scintilla scintilla, char keyAdded)
        {
            int justBeforeNewline = scintilla.CurrentPos - 3;
            
            int functionStart;
            if (justBeforeNewline < 0)
            {
                return false;
            }

            return DoesWordExistBefore(scintilla.Text, justBeforeNewline, "{");
        }



        public override void Fire(ScintillaNet.Scintilla scintilla)
        {
            FireRule(scintilla, "default_indent");
            Indent(scintilla);
        }
    }
}
