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
    public partial class FilePage : DockContent
    {
        public event EventHandler DoubleClickFileList;
        public FilePage()
        {
            InitializeComponent();
            HideOnClose = true;
            _openFileListControl.DoubleClickFileList += delegate(object sender, EventArgs args)
            {
                DoubleClickFileList(sender, args);
            };
        }

        internal void RefreshItem(CodePage page)
        {
            _openFileListControl.RefreshItem(page);
        }

        internal void Remove(CodePage page)
        {
            _openFileListControl.RemoveFile(page);
        }

        internal void Add(CodePage page)
        {
            _openFileListControl.AddFile(page);
        }
    }
}
