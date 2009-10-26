using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;

namespace Einfall.Editor
{
    public class CodeTransformer
    {
    

        public CodeTransformer()
        {
        }

        public void ReplaceSelectionText(Scintilla scintilla, Selection selection, string text)
        {
            string textSelectRemoved = scintilla.Text.Remove(selection.Start, selection.Text.Length);
            textSelectRemoved = textSelectRemoved.Insert(selection.Start, text);
            scintilla.Text = textSelectRemoved;
        }

        internal string GetTextFromStartTo(string untilText, string text)
        {
            if (text.Length == 0)
            {
                return "";
            }

            int index = text.IndexOf(untilText);

            if (index == -1)
            {
                return text;
            }
            
            return text.Substring(0, index);
        }

        public int WhatLineIsIndexOn(int index, string text)
        {
            int lineNumber = 0;
            int searchIndex = 0;
            searchIndex = text.IndexOf("\r\n", searchIndex);
            while (searchIndex != -1 && searchIndex < index)
            {
                lineNumber++;
                searchIndex++;
                searchIndex = text.IndexOf("\r\n", searchIndex);
            }
            return lineNumber;

        }

   
    }
}
