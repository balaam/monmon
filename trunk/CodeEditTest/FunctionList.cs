using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Einfall.Editor;

namespace MonMon
{
    public partial class FunctionList : UserControl
    {
        List<FunctionMetaData> _fullList;
        public FunctionList()
        {
            InitializeComponent();
        }

        public event EventHandler ClickRefresh;
        public event EventHandler DoubleClickFunctionList;

        private void OnClickRefresh(object sender, EventArgs e)
        {
            ClickRefresh(sender, e);
        }

        internal void Update(List<FunctionMetaData> list)
        {
            _fullList = list;
            _listBoxFunctions.Items.Clear();
            list.ForEach(x => _listBoxFunctions.Items.Add(x));
        }

        private void OnDoubleClickFunction(object sender, EventArgs e)
        {
            DoubleClickFunctionList(sender, e);
        }

        private void OnFilterTyping(object sender, EventArgs e)
        {
            if(_filterTextBox.Text == "")
            {
                if(_listBoxFunctions.Items.Count == _fullList.Count)
                {
                    // assume they're the same, no filter so return
                    return;
                }
                else
                {
                    _filterTextBox.Clear();
                    _fullList.ForEach(x => _listBoxFunctions.Items.Add(x));
                }
            }
            _listBoxFunctions.Items.Clear();

            foreach (var item in _fullList)
            {
                if (item.Name.ToLower().Contains(_filterTextBox.Text))
                {
                    _listBoxFunctions.Items.Add(item);
                }
            }

        }
    }
}
