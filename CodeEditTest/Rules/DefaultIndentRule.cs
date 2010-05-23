using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;

namespace MonMon.Rules
{
    class DefaultIndentRule : FormatRule
    {
        public override string Name
        {
            get { return "default_indent"; }
        }

        public override bool CanFire(Scintilla scintilla, char keyAdded)
        {
            if (keyAdded == '\r')
            {
                return true;
            }
            return false;
        }

        public override void Fire(Scintilla scintilla)
        {
            string priorIndent = GetStartingWhiteSpace(scintilla.Lines.Current.Previous.Text);
            priorIndent = priorIndent.Replace("\r\n", "");
            scintilla.Lines.Current.Text = priorIndent;
            scintilla.CurrentPos = scintilla.Lines.Current.EndPosition;
        }
    }
}
