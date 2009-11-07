using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;


// Scintilla Specific
using Einfall.Editor;
using Einfall.Editor.Lua;
using ScintillaNet;

// Other bits
using MonMon.Utilities;

namespace MonMon
{
    public partial class MonMonMainForm : Form
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        bool _debug = true;
        ICodeContext _codeContext;
        Selection _selection;
        private static MRUManager<string> _mruManager = new MRUManager<string>("Paths", 10);
      
        List<CaretHistory> _caretHistory = new List<CaretHistory>();
        int _caretIndex = -1;
        FindAll _findAllDialog = new FindAll();
        List<FindResult> _findResults = new List<FindResult>();



        //Dictionary<TabPage, TabData> _tabData = new Dictionary<TabPage, TabData>();
        List<CodePage> _codePageList = new List<CodePage>();

        // Helper Windows
        FunctionPage _functionPage = new FunctionPage();
        FilePage _filePage = new FilePage();

        public MonMonMainForm(string[] args)
        {
            InitializeComponent();
            _codeContext = new CodeContextLua();
            FillUpRecentFilesMenu();
            recentFilesToolStripMenuItem.DropDownItemClicked += new ToolStripItemClickedEventHandler(OnOpenRecentFile);
            //!_statusLabel.Text = "Ready";
            //!_tabControl.MouseDown += new MouseEventHandler(OnMouseDownOnTab);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.OnKeyDownOnForm);
            //!_listBoxFindResults.DoubleClick += new EventHandler(OnDoubleClickFindResult);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(OnThreadException);
            LoadArgs(args);
            //!_functionListControl.ClickRefresh += new EventHandler(Refresh_Click);
            //!_functionListControl.DoubleClickFunctionList += new EventHandler(OnFunctionShortCutDblClicked);
            //!_openFilesControl.DoubleClickFileList += new EventHandler(OnFileListDoubleClicked);
            _functionPage.Show(_dockPanel);
            _filePage.Show(_dockPanel);

        }



        void OnThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (!_debug)
            {
                return;
            }
            var trace = new System.Diagnostics.StackTrace(e.Exception);
           
            Console.WriteLine("Unexpected Exception.");
            Console.WriteLine(sender.ToString());
            Console.WriteLine(e);
            Console.WriteLine(trace.ToString());
        }

        void OnDoubleClickFindResult(object sender, EventArgs e)
        {
            //!
            /*
            if (_listBoxFindResults.SelectedItem == null)
            {
                return;
            }

            FindResult findResult = (FindResult)_listBoxFindResults.SelectedItem;
            _tabControl.SelectedTab = findResult.TabPage;
            _tabData[_tabControl.SelectedTab].Scintilla.GoTo.Position(findResult.Start);
            _tabControl.Focus();
            _tabData[_tabControl.SelectedTab].Scintilla.Focus();
             */
        }

        // This should be more generalized
        private void OnKeyDownOnForm(object sender, KeyEventArgs e)
        {
            //!
            /*
            if (e.Control && e.Shift && e.KeyCode == Keys.F)
            {
               // Find all

                // preload find all with selected text
                if (_tabControl.SelectedTab != null)
                {
                    string selectedText = _tabData[_tabControl.SelectedTab].Scintilla.Selection.Text;
                    _findAllDialog.SetDefaultSearchString(selectedText);
                }
           
               if (_findAllDialog.ShowDialog() == DialogResult.OK)
               {
                   FindAll(_findAllDialog.SearchString);
               }
            }
             */
        }

        private void FindAll(string searchString)
        {
            //!
            /*
            _listBoxFindResults.Items.Clear();
            _findResults.Clear();
            foreach (TabPage t in _tabControl.TabPages)
            {
                List<Range> finds = _tabData[t].Scintilla.FindReplace.FindAll(searchString);

                finds.ForEach(x => _findResults.Add(new FindResult(x.Start, x.End, t, _tabData[t].Path)));
            }


            _listBoxFindResults.Items.AddRange(_findResults.ToArray());
             */
        }

        void OnMouseDownOnTab(object sender, MouseEventArgs e)
        {
            //!
            /*
            if (e.Button == MouseButtons.Middle)
            {
                TabControl tabControl = (TabControl)sender;
         
                for(int i = 0; i < _tabControl.TabCount; i++)
                {
                    if(_tabControl.GetTabRect(i).Contains(e.Location))
                    {
                       CloseFile(tabControl.TabPages[i]);
                       break;
                    }
                }
            }
             */
        }

        void OnOpenRecentFile(object sender, ToolStripItemClickedEventArgs e)
        {
            LoadFileFromPath(e.ClickedItem.Text);
        }

        private void FillUpRecentFilesMenu()
        {
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            foreach (string path in _mruManager.List)
            {
                recentFilesToolStripMenuItem.DropDownItems.Add(path);
            }
          
        }

        void OnScintillaKeyUp(object sender, KeyEventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;
            scintilla.IsReadOnly = false;
        }

        void OnPreviewScintillaKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Scintilla localScintilla = (Scintilla)sender;
            _selection = localScintilla.Selection;
            char charEntered = KeyHelper.KeycodeToChar(e.KeyCode);


            if (ThereIsASelection(_selection) == false)
            {
                return;
            }

            if (_codeContext.IsStartOfComment(charEntered))
            {
                _codeContext.CommentOutSelection(localScintilla, _selection);
        
               // Forces the comment key not to be input.
                localScintilla.IsReadOnly = true;
              
            }
        }

        /// <summary>
        /// Tests if a selection has actually selects anything.
        /// </summary>
        /// <param name="selection"></param>
        /// <returns></returns>
        private bool ThereIsASelection(Selection selection)
        {
            return selection.Length > 0;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            //!
            /*
            if (_tabControl.SelectedTab == null)
            {
                return;
            }
            Scintilla scintilla = (Scintilla)_tabControl.SelectedTab.Controls[0];
            
            _codeContext.UpdateFunctionList(scintilla);
  
         
            _functionListControl.Update(_codeContext.FunctionList);
             */
        }

        private void OnNewClicked(object sender, EventArgs e)
        {
            CodePage cp = CreateCodePage("");
            cp.Show(_dockPanel);
            //!
            //  _tabControl.TabPages[_tabControl.TabPages.Count - 1];
            //CreateNewFileTab("Unnamed");
        
        }

        public CodePage GetVisibleCodePage()
        {
            foreach (CodePage cp in _codePageList)
            {
                if (!cp.IsHidden)
                {
                    return cp;
                }
            }
            return null;
        }

        private CodePage CreateCodePage(string fullText)
        {
            // This could be inside the codepage?
            Scintilla scintilla = CreateNewLuaScintilla();
            scintilla.Text = fullText;
            CodePage page = new CodePage(scintilla);
            page.Disposed += delegate(object sender, EventArgs e)
            {
                _codePageList.Remove(page);
            };
            _codePageList.Add(page);
            return page;
        }

        private TabData CreateNewFileTab(string name)
        {
            return null;
            //!
            /*
            TabPage tabPage = new TabPage(name);

            Scintilla scintilla = CreateNewLuaScintilla();
            tabPage.Controls.Add(scintilla);
            _tabControl.Controls.Add(tabPage);
            _tabControl.SelectedTab = tabPage;
            tabPage.Focus();
            scintilla.Focus();
            TabData tabData = new TabData(name, scintilla);
            _tabData.Add(tabPage, tabData);
            _openFilesControl.AddFile(tabPage);
            tabData.OnModifiedFlagChanged += delegate(object sender, EventArgs e)
            {
                tabPage.Text = tabData.Name;
                _openFilesControl.RefreshItem(tabPage);
               
            };
            tabData.SetModifiedFlag(true);
         
            return _tabData[tabPage];
             */
        }

  
        private Scintilla CreateNewLuaScintilla()
        {
            Scintilla scintilla = new Scintilla();
            scintilla.ConfigurationManager.CustomLocation = "lua.xml";
            scintilla.ConfigurationManager.Language = "lua";
            
            scintilla.Dock = System.Windows.Forms.DockStyle.Fill;
            scintilla.Location = new System.Drawing.Point(3, 3);
            scintilla.Name = "_scintilla";
            scintilla.Scrolling.HorizontalWidth = 400;
            scintilla.Size = new System.Drawing.Size(570, 373);
            scintilla.Styles.BraceBad.FontName = "Verdana";
            scintilla.Styles.BraceLight.FontName = "Verdana";
            scintilla.Styles.ControlChar.FontName = "Verdana";
            scintilla.Styles.Default.FontName = "Verdana";
            scintilla.Styles.IndentGuide.FontName = "Verdana";
            scintilla.Styles.LastPredefined.FontName = "Verdana";
            scintilla.Styles.LineNumber.FontName = "Verdana";
            scintilla.Styles.Max.FontName = "Verdana";

            _codeContext.ApplyStyling(scintilla);

            scintilla.TabIndex = 1;
            scintilla.Indentation.TabIndents    = true;
            //scintilla.Indentation.IndentWidth   = 4;
            scintilla.Indentation.TabWidth = 6;
            
        //    scintilla.Indentation.UseTabs       = true;

            
            scintilla.PreviewKeyDown += new PreviewKeyDownEventHandler(OnPreviewScintillaKeyDown);
            scintilla.KeyUp += new KeyEventHandler(OnScintillaKeyUp);
            scintilla.Margins[0].Width = 35; // some room for line numbers
            //ScintillaNet.Configuration.Builtin.LexerKeywordListNames.lua.txt
            scintilla.AutoComplete.AutoHide = false;
            scintilla.AutoComplete.AutomaticLengthEntered = true;
            scintilla.MouseClick += new MouseEventHandler(OnScintillaMouseClick);
            scintilla.TextChanged += new EventHandler<EventArgs>(OnScintillaTextChanged);
            scintilla.Focus();
            
            return scintilla;
        }

        void OnScintillaTextChanged(object sender, EventArgs e)
        {
            //!
            /*
            Scintilla scintilla = (Scintilla) sender;
            
            // This could go a little wrong, in say mass find replace?
            if (_tabControl.SelectedTab == null)
            {
                return;
            }

            bool same = scintilla.Text.GetHashCode() == _tabData[_tabControl.SelectedTab].DiskHash;

            _tabData[_tabControl.SelectedTab].SetModifiedFlag(!same);
            */
        }

  

        void OnScintillaMouseClick(object sender, MouseEventArgs e)
        {
            //!
            /*
            Scintilla scintilla = (Scintilla)sender;
            
            int cursorPosition = scintilla.PositionFromPoint(e.X, e.Y);
      
            if (cursorPosition != -1)
            {
                _caretHistory.Add(new CaretHistory(_tabControl.SelectedTab, cursorPosition));
                _caretIndex = _caretHistory.Count - 1;
            }
             */
        }

        private void OnOpenFile(object sender, EventArgs e)
        {
            if(_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach(string filePath in _openFileDialog.FileNames)
                {
                    LoadFileFromPath(filePath);
                    _mruManager.Add(filePath);
                }
                FillUpRecentFilesMenu();
                
            }
        }

        private void LoadFileFromPath(string path)
        {
            //!CloseTabByPath(path);
            string fullText = File.ReadAllText(path);

            CodePage codePage = CreateCodePage(fullText);
            codePage.Show(_dockPanel);
            codePage.Path = path;
        }



        private void OnSaveClicked(object sender, EventArgs e)
        {
            //!
            /*
            if (_tabControl.SelectedTab == null)
            {
                return;
            }

            _tabData[_tabControl.SelectedTab].Save();
            _statusLabel.Text = "Saved " + _tabData[_tabControl.SelectedTab].Name;
             */
        }

        private void OnSaveAllClicked(object sender, EventArgs e)
        {
            //!
            /*
            // Base save all on what the user is seeing.
            foreach (TabPage page in _tabControl.TabPages)
            {
                _tabData[page].Save();
                _statusLabel.Text = "Saved " + _tabData[page].Name;
            }
             */
        }

        private void OnCloseFileClicked(object sender, EventArgs e)
        {
            //!
            /*
            if (_tabControl.SelectedTab == null)
            {
                return;
            }

            CloseFile(_tabControl.SelectedTab);
             */
        }

        private void CloseFile(TabPage tabPage)
        {
            //!
            /*
            _openFilesControl.RemoveFile(tabPage);
            _tabData.Remove(tabPage); // probably should be a save prompt.
            _tabControl.TabPages.Remove(tabPage);
           
            tabPage.Dispose();
             */
        }

        private void OnGotoToPreviousCursorPosition(object sender, EventArgs e)
        {
            //!
            /*
            // I think this can be done by extending the navigation points
            // override the push create a wrapper around the nav point
            // this allows me to compare the scintillas and decide what tab it came from.
            if (_tabControl.SelectedTab != null)
            { 
                Scintilla scintilla = _tabData[_tabControl.SelectedTab].Scintilla;
         //       DocumentNavigation.NavigationPont point = scintilla.DocumentNavigation.BackwardStack.Pop();
                
                if (scintilla.DocumentNavigation.CanNavigateBackward)
                {
                    
                    scintilla.DocumentNavigation.NavigateBackward();
                }
            }
             */
        }

        private void OnGotoNextCursorPosition(object sender, EventArgs e)
        {
            //!
            /*
            if (_tabControl.SelectedTab != null)
            {
                Scintilla scintilla = _tabData[_tabControl.SelectedTab].Scintilla;
               
                if (scintilla.DocumentNavigation.CanNavigateForward)
                {
                    scintilla.DocumentNavigation.NavigateForward();
                }
            }
             */
            
        }

        void OnFileListDoubleClicked(object sender, EventArgs e)
        {
            //!
            /*
            ListBox fileListBox = (ListBox)sender;

            if (fileListBox.SelectedItem == null)
            {
                return;
            }

            string name =(string) fileListBox.SelectedItem;
            TabPage t = null;
            foreach (TabPage page in _tabControl.TabPages)
            {
                if (name == page.Text)
                {
                    t = page;
                    break;
                }
            }

            if (t == null)
            {
                return;
            }

            _tabControl.SelectedTab = t;
            _tabControl.Focus();
            _tabData[_tabControl.SelectedTab].Scintilla.Focus();
             */
        }

        private void OnFunctionShortCutDblClicked(object sender, EventArgs e)
        {
            //!
            /*
            ListBox functionListBox = (ListBox)sender;

            if (functionListBox.SelectedItem == null)
            {
                return;
            }

            FunctionMetaData functionData = (FunctionMetaData)functionListBox.SelectedItem;

            _tabData[_tabControl.SelectedTab].Scintilla.GoTo.Line(functionData.StartLine);
            _tabControl.Focus();
            _tabData[_tabControl.SelectedTab].Scintilla.Focus();
             */
        }

        private void OnFormDragOver(object sender, DragEventArgs e)
        {
             if(    e.Data.GetDataPresent(DataFormats.FileDrop) == false
                ||  e.Data.GetDataPresent("FileNameW")          == false)
             {
                 return;
             }
             
             if ((e.AllowedEffect & DragDropEffects.Copy) != 0)
             {

                 string[] filePathInfo = (string[])e.Data.GetData("FileNameW", true);

                 // If there is even one correct path use it.
                 foreach (string path in filePathInfo)
                 {
                     if (path.Length == 0)
                     {
                         continue;
                     }

                     if (path.ToLower().EndsWith(".lua"))
                     {
                         e.Effect = DragDropEffects.Copy;
                     }
                 }   
             }
        }

        private void OnDragDropOnForm(object sender, DragEventArgs e)
        {
            string[] filePathInfo = (string[])e.Data.GetData(DataFormats.FileDrop);


            foreach (string path in filePathInfo)
            {
                if (path.ToLower().EndsWith(".lua"))
                {
                    LoadFileFromPath(path);
                }
            }
        }

        public delegate void LoadArgsCallback();

        public void LoadArgsCrossThread(string[] args)
        {
            if (this.InvokeRequired)
            {
                LoadArgsCallback l = delegate()
                {
                    LoadArgs(args);
                    this.BringToFront();
                    this.Focus();
                    SetForegroundWindow(this.Handle);
                };
                Invoke(l);

            }
            else
            {
                LoadArgs(args);
            }
        }

        private void LoadArgs(string[] args)
        {

            foreach (string file in args)
            {
                if (file.EndsWith(".lua") && File.Exists(file))
                {
                    LoadFileFromPath(file);
                }
            }
        }

        private void OnClickShowFunctionList(object sender, EventArgs e)
        {
            _functionPage.Show(_dockPanel);
        }

        private void OnClickAbout(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }
    }
}
