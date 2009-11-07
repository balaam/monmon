using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace MonMon
{
    public partial class OutputPage : DockContent
    {
        public OutputPage()
        {
            InitializeComponent();
            _listBoxFindResults.DoubleClick += delegate(object sender, EventArgs eventArgs)
            {
                OnDblClickFindResult(sender, eventArgs);
            };
        }

        public event EventHandler OnDblClickFindResult;

        internal void Clear()
        {
            _listBoxFindResults.Items.Clear();
        }

        internal void AddFindResults(FindResult[] findResult)
        {
            _listBoxFindResults.Items.AddRange(findResult);
        }

        internal FindResult GetSelectedFindResult()
        {
            if (_listBoxFindResults.SelectedItem == null)
            {
                return null;
            }

            return (FindResult)_listBoxFindResults.SelectedItem;
        }
    }
}
