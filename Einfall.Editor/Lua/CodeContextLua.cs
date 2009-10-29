using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;
using System.Text.RegularExpressions;

namespace Einfall.Editor.Lua
{
    public class CodeContextLua : ICodeContext
    {
        CodeTransformer _codeTransformer;
        List<FunctionMetaData> _functionList = new List<FunctionMetaData>();

        Regex _matchFunction = new Regex(@"function(\s+\w+\:?\w*\s*\([\w\s,]*\))|function\s*\([\w\s,]*\)");



        public CodeContextLua()
        {
            _codeTransformer = new CodeTransformer();
        }
        #region ICodeContext Members

        public bool IsStartOfComment(char c)
        {
            return c == '-';
        }

        public void CommentOutSelection(Scintilla scintilla, Selection selection)
        {
            bool midLineSelection = DoesSelectionStartMidLine(selection) || DoesSelectionEndMidLine(selection);
            string firstLineOfSelection = _codeTransformer.GetTextFromStartTo("\r\n", selection.Text);
           // string text = selection.Range.StartingLine.Text;
            //_codeTransformer.DoesSelectionStartsMidLine(, )
            if (selection.Text.Contains("\r\n") && !midLineSelection)
            {
                string newText = "--" + selection.Text.Replace("\r\n", "\r\n--");
                _codeTransformer.ReplaceSelectionText(scintilla, selection, newText);
            }
            else
            {
                string newText = "--[[" + selection.Text + "--]]";
                _codeTransformer.ReplaceSelectionText(scintilla, selection, newText);
            }
        }

        private bool DoesSelectionEndMidLine(Selection selection)
        {
            string text = selection.Range.StartingLine.Text.TrimEnd('\r', '\n');
            return selection.Text.EndsWith(text) == false;
        }

        private bool DoesSelectionStartMidLine(Selection selection)
        {
            string text = selection.Range.StartingLine.Text.TrimEnd('\r', '\n');
            return selection.Text.StartsWith(text) == false;
        }

    
        public void UpdateFunctionList(Scintilla scintilla)
        {
            UpdateFunctionList(scintilla.Text);
        }

        public void UpdateFunctionList(string text)
        {
            _functionList.Clear();
            // Find all occurances of functions in a lua file.
            int searchIndex = 0;
            do
            {
                searchIndex = text.IndexOf("function", searchIndex);
                
                if( searchIndex == -1 )
                {
                    // Failed to find any more functions
                    // End function search
                    continue;
                }

                // Find closing brace of function definition
                int closingBraceIndex = text.IndexOf(")", searchIndex);

                if (closingBraceIndex == -1)
                {
                    searchIndex++;
                    continue;
                }

               // int what = text.Length;
                string functionId = text.Substring(searchIndex, (closingBraceIndex - searchIndex) + 1);
                Match match = _matchFunction.Match(functionId);
                

                if (match.Success == false)
                {
                    searchIndex++;
                    continue;
                }

                string functionName = "Unknown";

                if (match.Captures.Count > 0)
                {
                    functionName = match.Captures[0].Value.Remove(0, "function".Length).Trim();
                    functionName = functionName.Remove(functionName.IndexOf('('));   
                    if( functionName.Length == 0 )
                    {
                        functionName = "Unknown";
                    }
                }

                // In lua a function keyword *must be followed* by [whitespace][name-optional][whitespace]()
                _functionList.Add(new FunctionMetaData(_codeTransformer.WhatLineIsIndexOn(searchIndex, text), functionName));
                searchIndex++; // move to the next character after the search.
                

               
            } while (searchIndex > -1);

        }

        public List<FunctionMetaData> FunctionList
        {
            get { return _functionList; }
        }

        public void ClearFunctionList()
        {
            _functionList.Clear();
        }

        #endregion
    }
}
