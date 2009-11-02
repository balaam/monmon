using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonMon
{
    public class ArgumentsReceivedEventArgs : EventArgs
    {
        string[] _args;
        public ArgumentsReceivedEventArgs(string[] args)
            : base()
        {
            _args = args;
        }

        public string[] Args
        {
            get
            {
                return _args;
            }
        }


    }
}
