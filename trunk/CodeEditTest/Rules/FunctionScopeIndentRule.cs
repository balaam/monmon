using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Debug = System.Diagnostics.Debug;

namespace MonMon.Rules
{
    /// <summary>
    /// When creating a function this rules causes the indent level to be increased.
    /// </summary>
    class FunctionScopeIndentRule : FormatRule
    {
        public override string Name
        {
            get { return "function_scope_indent"; }
        }

        public override bool CanFire(ScintillaNet.Scintilla scintilla, char keyAdded)
        {
            if (keyAdded != '\r')
            {
                return false; 
            }

            // function()\r\n 12
            //          ^
            // -3 puts the character to before the return
            int justBeforeNewline = scintilla.CurrentPos - 3;

            if (justBeforeNewline < 0)
            {
                return false;
            }

            int functionStart = -1;
            if (IsPosJustAfterFunction(scintilla.Text, justBeforeNewline, out functionStart))
            {
                return true;
            }
            return false;
        }

        // This function look bloody awful - must be a simpler way for it to be written.
        private static bool IsPosJustAfterFunction(string code, int position, out int i)
        {
            Debug.Assert(position >= 0);
            Debug.Assert(position < code.Length);
            i = -1;

            char c = code[position];

            if (c != ')')
            {
                return false;
            }


            position = position - 1;
            c = code[position];

            while (c != '(')
            {

                if (position < 0)
                {
                    return false;
                }
                else
                {
                    if (!IsValidFunctionArgDef(c))
                    {
                        return false;
                    }
                    position = position - 1;
                    c = code[position];
                }
            }

            // move to next character after '('
            position = position - 1;
            c = code[position];

            if (position < 0)
            {
                return false;
            }

            while (Char.IsWhiteSpace(c))
            {

                position = position - 1;
                if (position < 0)
                {
                    return false;
                }
                c = code[position];
            }

            string functionId = "";
            while (IsValidFunctionId(c))
            {


                functionId = c + functionId;
                position = position - 1;
                if (position < 0)
                {
                    break;
                }
                c = code[position];
            }

            if (functionId == "function")
            {
                i = position + 1;
                return true;
            }

            while (Char.IsWhiteSpace(c))
            {
                position = position - 1;
                if (position < 0)
                {
                    return false;
                }
                c = code[position];
            }

            position = position + 1;
            int functionLength = "function".Length;
            int functionStart = position - functionLength;
            if (functionStart < 0)
            {
                return false;
            }

            string functionCut = code.Substring(functionStart, position - functionStart);

            if (functionCut == "function")
            {
                i = functionStart;
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsValidFunctionId(char c)
        {
            if (Char.IsLetterOrDigit(c))
            {
                return true;
            }
            else if (c == '.')
            {
                return true;
            }
            else if (c == ':')
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Is this character valid inside an function defs parameter list
        /// This is a bit of a crude way of doing things
        /// function( >>Anything in here<<)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsValidFunctionArgDef(char c)
        {
            if (Char.IsLetterOrDigit(c))
            {
                return true;
            }
            else if (Char.IsWhiteSpace(c))
            {
                return true;
            }
            else if (c == ',')
            {
                return true;
            }
            else if (c == '.')
            {
                // can have ... as an arg
                return true;
            }
            return false;
        }

        public override void Fire(ScintillaNet.Scintilla scintilla)
        {
            FireRule(scintilla, "default_indent");
            Indent(scintilla);
        }
    }
}
