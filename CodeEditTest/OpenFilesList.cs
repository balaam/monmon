using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonMon
{
    public partial class OpenFilesList : UserControl
    {
        List<TabPage> _fullList = new List<TabPage>();
        public event EventHandler DoubleClickFileList;
        public OpenFilesList()
        {
            InitializeComponent();
        }

        public void AddFile(TabPage tabData)
        {
            _fullList.Add(tabData);
            RefreshFilter();
        }

        public void RemoveFile(TabPage tabData)
        {
            _fullList.Remove(tabData);
            RefreshFilter();
        }

        public void RefreshItem(TabPage tabPage)
        {
            int index = _fullList.FindIndex(x => x == tabPage);
            _fullList[index] = tabPage;
            RefreshFilter();
        }

        public void RefreshFilter()
        {
            _fileListBox.Items.Clear();

            foreach (TabPage page in _fullList)
            {
                if (_textBoxFilter.Text == "")
                {
                    _fileListBox.Items.Add(page.Text);
                }
                else if(page.Text.ToLower().Contains(_textBoxFilter.Text.ToLower()))
                {
                    _fileListBox.Items.Add(page.Text);
                }
            }
        }

        private void OnFilterTextChanged(object sender, EventArgs e)
        {
            RefreshFilter();
        }

        private void OnDoubleClickFileList(object sender, MouseEventArgs e)
        {
            DoubleClickFileList(sender, e);
        }
    }
}
