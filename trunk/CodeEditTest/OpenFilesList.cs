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
        List<CodePage> _fullList = new List<CodePage>();
        public event EventHandler DoubleClickFileList;
        public OpenFilesList()
        {
            InitializeComponent();
        }

        public void AddFile(CodePage codePage)
        {
            _fullList.Add(codePage);
            RefreshFilter();
        }

        public void RemoveFile(CodePage codePage)
        {
            _fullList.Remove(codePage);
            RefreshFilter();
        }

        public void RefreshItem(CodePage codePage)
        {
            int index = _fullList.FindIndex(x => x == codePage);
            _fullList[index] = codePage;
            RefreshFilter();
        }



        public void RefreshFilter()
        {
            _fileListBox.Items.Clear();

            foreach (CodePage page in _fullList)
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
