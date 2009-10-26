using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Einfall.Editor
{
    // An ending list and a possibility to work out containing varaibles would be nice
    public class FunctionMetaData
    {
        int _startingLineNumber;
        string _name;

        public string Name
        {
            get { return _name; }
        }

        public int StartLine
        {
            get { return _startingLineNumber;  }
        }

        public FunctionMetaData(int line, string name)
        {
            _startingLineNumber = line;
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
