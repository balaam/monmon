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
        string _defaultSearch = "";

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
            _searchString = _comboBoxFind.Text;
            _comboBoxFind.Text = "";
            _comboBoxFind.Items.Add(_searchString);
            Close();
        }

        public void SetDefaultSearchString(string value)
        {
            _defaultSearch = value;
        }

        protected override void OnShown(EventArgs e)
        {
            _comboBoxFind.Text = _defaultSearch;
            _comboBoxFind.SelectAll();
            _comboBoxFind.Focus();
            base.OnShown(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            _defaultSearch = "";
            base.OnClosed(e);
        }

        private void _comboBoxFind_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonFindAll.PerformClick();
            }
        }
    }
}
