using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonMon
{
    class Shortcut
    {
        bool _alt = false;
        bool _control = false;
        bool _shift     = false;
        
        Keys _key       = Keys.None;
        EventHandler _event = null;

        public Shortcut(bool alt, bool control, bool shift, Keys keys, EventHandler onExecute)
        {
            _shift = shift;
            _control = control;
            _alt = alt;
            _key = keys;
            _event = onExecute;
        }

        /// <summary>
        /// Test if the shortcut has been activated
        /// </summary>
        /// <returns></returns>
        public bool IsActivated(KeyEventArgs keyEvent)
        {
            if (keyEvent.Alt        == _alt     &&
                keyEvent.Control    == _control &&
                keyEvent.Shift      == _shift   &&
                keyEvent.KeyCode    == _key)
            {
                // It's been correctly activated.
                return true;
            }
            return false;
        }

        public void Execute()
        {
            if (_event != null)
            {
                _event(this, EventArgs.Empty);
            }
        }
    }
}
