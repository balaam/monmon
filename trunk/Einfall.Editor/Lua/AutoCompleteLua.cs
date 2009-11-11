using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScintillaNet;

namespace Einfall.Editor.Lua
{
    public enum CharacterEnterState
    {
        Normal,
        DuringAutoPrompt,
    }
    public class AutoCompleteLua
    {
        CharacterEnterState _state = CharacterEnterState.Normal;
        AutoFormat _autoFormat;
        Dictionary<string, List<string>> _completionData = new Dictionary<string, List<string>>();
        List<string> _currentList = new List<string>();
        string _currentWord = "";


        public CharacterEnterState State
        {
            get
            {
                return _state;
            }
        }

        public AutoCompleteLua(Scintilla scintilla, AutoFormat af)
        {
            _autoFormat = af;
            //scintilla.AutoComplete.AutoHide = true;
            //scintilla.AutoComplete.IsCaseSensitive = false;
            scintilla.AutoComplete.SingleLineAccept = false;
            
            scintilla.AutoComplete.StopCharacters = ":(";

            // This should obviously be a data file
            var globalComplete = new List<string>();
            
            // The standard libraries
            globalComplete.Add("math");
            globalComplete.Add("string");
            globalComplete.Add("table");

            globalComplete.Add("next");
            globalComplete.Add("pairs");
            globalComplete.Add("ipairs");
            globalComplete.Add("type");
            globalComplete.Add("tonumber");
            globalComplete.Add("tostring");
            globalComplete.Add("pack");
            globalComplete.Add("unpack");
            globalComplete.Add("setmetatable");
            globalComplete.Add("setfenv");
            globalComplete.Add("print");
            // ... this list is actually incomplete http://www.lua.org/manual/5.1/manual.html

            globalComplete.Add("and");
            globalComplete.Add("break");
            globalComplete.Add("do");
            globalComplete.Add("else");
            globalComplete.Add("elseif");

            globalComplete.Add("end");
            globalComplete.Add("false");
            globalComplete.Add("for");
            globalComplete.Add("function");
            globalComplete.Add("if");
            globalComplete.Add("in");
            globalComplete.Add("local");
            globalComplete.Add("nil");
            globalComplete.Add("not");
            globalComplete.Add("or");
            globalComplete.Add("repeat");
            globalComplete.Add("return");
            globalComplete.Add("then");
            globalComplete.Add("true");
            globalComplete.Add("until");
            globalComplete.Add("while");



            _completionData.Add("", globalComplete);
            var mathList = new List<string>();
            mathList.Add("abs");
            mathList.Add("acos");
            mathList.Add("asin");
            mathList.Add("atan");
            mathList.Add("atan2");
            mathList.Add("ceil");
            mathList.Add("cos");
            mathList.Add("cosh");
            mathList.Add("exp");
            mathList.Add("floor");
            mathList.Add("fmod");
            mathList.Add("frexp");

            mathList.Add("huge");
            mathList.Add("ldexp");
            mathList.Add("log");
            mathList.Add("log10");
            mathList.Add("max");
            mathList.Add("min");
            mathList.Add("pi");

            mathList.Add("pow");
            mathList.Add("rad");
            mathList.Add("random");
            mathList.Add("randomseed");
            mathList.Add("sin");

            mathList.Add("sinh");
            mathList.Add("sqrt");
            mathList.Add("tanh");
            mathList.Add("tan");

            _completionData.Add("math", mathList);

            scintilla.AutoComplete.List = _completionData[""];
        }

        public void OnDotCharAdded(Scintilla scintilla)
        {
            _currentWord = "";
            string word = scintilla.GetWordFromPosition(scintilla.CurrentPos-1);

            if (word == "")
            {
                return;
            }

            if (_completionData.Keys.Contains(word))
            {
                _state = CharacterEnterState.DuringAutoPrompt;

                _currentList.Clear();
                _currentList.AddRange(_completionData[word]);
                scintilla.AutoComplete.Show(_currentList);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scintilla"></param>
        /// <returns>Has the char been hanlded?</returns>
        public bool OnCharAddedDuringAutoPrompt(Scintilla scintilla, char c)
        {
            // This also needs to know about backspace!
            if (!char.IsLetterOrDigit(c))
            {
                scintilla.AutoComplete.Cancel();
            }
            if (!scintilla.AutoComplete.IsActive)
            {
                _currentWord = "";
                _state = CharacterEnterState.Normal;
                scintilla.AutoComplete.List = _completionData[""];
                return false;
            }

            // Should filter the current selection by the word
            _currentWord += c;
            _currentList.RemoveAll(x => !x.StartsWith(_currentWord));
            scintilla.AutoComplete.List = _currentList;
            scintilla.AutoComplete.Show(_currentList);

            return true;
        }
    }
}
