using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonMon
{
    public partial class FindAll : Form
    {
        string _searchString = "";

        public string SearchString
        {
            get
            {
                return _searchString;
            }
        }

        public FindAll()
        {
            InitializeComponent();
            buttonFindAll.DialogResult = DialogResult.OK;
        }

        private void OnClickFindAll(object sender, EventArgs e)
        {
            _searchString = comboBoxFind.Text;
            comboBoxFind.Text = "";
            comboBoxFind.Items.Add(_searchString);
            Close();
        }
    }
}
