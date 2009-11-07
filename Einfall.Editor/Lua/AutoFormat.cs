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
            //          ^
            // -3 puts the character to before the return
            int justBeforeNewline = scintilla.CurrentPos - 3;
            
            int functionStart;
            if (justBeforeNewline < 0)
            {
                DoIndent(scintilla);
                return;
            }


            if (IsPosJustAfterFunction(scintilla.Text, justBeforeNewline, out functionStart))
            {
                int previousStartPos =  scintilla.Lines.Current.Previous.StartPosition;
                Debug.Assert(functionStart >= previousStartPos);
                int offset = functionStart - previousStartPos;

                for (int i = previousStartPos; i < functionStart; i++)
                {
                    if (scintilla.Text[i] == '\t')
                    {
                        offset += 3; // tabs are 4 spaces '\t' is 1 so plus 3
                    }
                }
                // Find function position relative to the current line
                // On the next line do the tab index equivalent.
                //Line l = scintilla.Lines.FromPosition(functionStart);
                //int indentAmount = l.Indentation;
                // This should really be more clever and but in as many tabs as possible.
                //string s = new string(' ', indentAmount) + "\t";
                scintilla.InsertText(new string(' ', offset) + '\t');
            }
            else if (IsPosJustAfterWord(scintilla.Text, "{", justBeforeNewline))
            {
                DoIndent(scintilla, "\t");
            }
            else if (IsPosJustAfterWord(scintilla.Text, "then", justBeforeNewline))
            {
                DoIndent(scintilla, "\t");
            }
            else if (IsPosJustAfterWord(scintilla.Text, "do", justBeforeNewline))
            {
                DoIndent(scintilla, "\t");
            }
            else
            {
                DoIndent(scintilla);
            }
    
        }

        public bool IsPosJustAfterWord(string text, string word, int justBeforeNewline)
        {
            for (int i = word.Length - 1; i != -1; i--)
            {
                if (justBeforeNewline < 0)
                {
                    return false;
                }

                char wordChar = word[i];
                char textChar = text[justBeforeNewline];

                if (wordChar != textChar)
                {
                    return false;
                }

                justBeforeNewline--;

         
            }
            return true;
        }

        private static void DoIndent(Scintilla scintilla)
        {
            DoIndent(scintilla, "");
        }
        private static void DoIndent(Scintilla scintilla, string extra)
        {
            Line curLine = scintilla.Lines.Current;
            if (!string.IsNullOrEmpty(curLine.Text))
                return;
            curLine.Indentation = curLine.Previous.Indentation;
            scintilla.CurrentPos = curLine.IndentPosition;

            if (!string.IsNullOrEmpty(extra))
            {
                scintilla.InsertText(extra);
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
                i = position+1;
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

        private bool IsValidFunctionId(char c)
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
