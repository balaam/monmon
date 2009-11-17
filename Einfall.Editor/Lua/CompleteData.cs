using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Einfall.Editor.Lua
{
    public class CompleteData
    {
        string _name;
        string _comment;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Commment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        public CompleteData()
        {
        }
    }
}
