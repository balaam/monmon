using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Einfall.Editor;

namespace MonMon
{
    public partial class FunctionPage : DockContent
    {
        public event EventHandler OnRefreshClicked;
        public event EventHandler OnFunctionDblClicked;

        public FunctionPage()
        {
            InitializeComponent();
            HideOnClose = true;
            _functionListControl.ClickRefresh += delegate(object sender, EventArgs args)
            {
                OnRefreshClicked(sender, args);
            };

            _functionListControl.DoubleClickFunctionList += delegate(object sender, EventArgs args)
            {
                OnFunctionDblClicked(sender, args);
            };
        }

        internal void Update(List<Einfall.Editor.FunctionMetaData> list)
        {
            _functionListControl.Update(list);
        }
    }
}
