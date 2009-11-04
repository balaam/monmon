using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;
using Debug = System.Diagnostics.Debug;

namespace Einfall.Editor.Lua
{
    public class AutoFormat
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scintilla"></param>
        /// <returns>Was the keypress handled.</returns>
        public void OnClosingParenAdded(Scintilla scintilla)
        {
            if(ShouldAutoFinishFunctionDef(scintilla.Text, scintilla.CurrentPos))
            {
               
            }

        
        }


        public void OnEnterPressed(Scintilla scintilla)
        {
            // function()\r\n 12
            // -3 puts the character to before the return
            int justBeforeFunction = scintilla.CurrentPos - 3;
            int functionStart;
            if (justBeforeFunction != -1 &&
                IsPosJustAfterFunction(scintilla.Text, justBeforeFunction, out functionStart))
            {
                // Find function position relative to the current line
                // On the next line do the tab index equivalent.
                Line l = scintilla.Lines.FromPosition(functionStart);
                int indentAmount = l.Indentation;      
                // This should really be more clever and but in as many tabs as possible.
                string s = new string(' ', indentAmount) + "\t";
                scintilla.InsertText(s);
            }
            else
            {
                Line curLine = scintilla.Lines.Current;
                if (!string.IsNullOrEmpty(curLine.Text))
                    return;
                curLine.Indentation = curLine.Previous.Indentation;
                scintilla.CurrentPos = curLine.IndentPosition;
            }
        }

        public bool IsPosJustAfterFunction(string code, int position)
        {
            int i = 0;
            return IsPosJustAfterFunction(code, position, out i);
        }

        public bool IsPosJustAfterFunction(string code, int position, out int i)
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


        /// <summary>
        /// Is this character valid inside an function defs parameter list
        /// This is a bit of a crude way of doing things
        /// function( >>Anything in here<<)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsValidFunctionArgDef(char c)
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

        public bool ShouldAutoFinishFunctionDef(string text, int pos)
        {
            return true;
        }

    }
}
