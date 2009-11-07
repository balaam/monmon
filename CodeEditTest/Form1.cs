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
using WeifenLuo.WinFormsUI.Docking;
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
        private DeserializeDockContent _deserializeDockContent;
      
        List<CaretHistory> _caretHistory = new List<CaretHistory>();
        FindAll _findAllDialog = new FindAll();
        List<FindResult> _findResults = new List<FindResult>();
        List<CodePage> _codePageList = new List<CodePage>();

        // Helper Windows
        FunctionPage _functionPage  = new FunctionPage();
        FilePage _filePage          = new FilePage();
        OutputPage _outputPage      = new OutputPage();

        public MonMonMainForm(string[] args)
        {
            InitializeComponent();
            _deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            _codeContext = new CodeContextLua();
            FillUpRecentFilesMenu();
            recentFilesToolStripMenuItem.DropDownItemClicked += new ToolStripItemClickedEventHandler(OnOpenRecentFile);
            //!_statusLabel.Text = "Ready";
            //!_tabControl.MouseDown += new MouseEventHandler(OnMouseDownOnTab);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.OnKeyDownOnForm);

        

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(OnThreadException);
            LoadArgs(args);
            _functionPage.OnRefreshClicked += new EventHandler(Refresh_Click);
            _functionPage.OnFunctionDblClicked += new EventHandler(OnFunctionShortCutDblClicked);
            _filePage.DoubleClickFileList += new EventHandler(OnFileListDoubleClicked);
            _outputPage.OnDblClickFindResult += new EventHandler(OnDoubleClickFindResult);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
            {
                _dockPanel.LoadFromXml(configFile, _deserializeDockContent);
                _functionPage.Show(_dockPanel);
                _outputPage.Show(_dockPanel);
                _filePage.Show(_dockPanel);
            }
            else
            {
                _functionPage.Show(_dockPanel);
                _outputPage.Show(_dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);
                _filePage.Show(_dockPanel);
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(FunctionPage).ToString())
                return _functionPage;
            else if (persistString == typeof(FilePage).ToString())
                return _filePage;
            else if (persistString == typeof(OutputPage).ToString())
                return _outputPage;
            else
            {
                /*
                // DummyDoc overrides GetPersistString to add extra information into persistString.
                // Any DockContent may override this value to add any needed information for deserialization.

                string[] parsedStrings = persistString.Split(new char[] { ',' });
                if (parsedStrings.Length != 3)
                    return null;

                if (parsedStrings[0] != typeof(DummyDoc).ToString())
                    return null;

                DummyDoc dummyDoc = new DummyDoc();
                if (parsedStrings[1] != string.Empty)
                    dummyDoc.FileName = parsedStrings[1];
                if (parsedStrings[2] != string.Empty)
                    dummyDoc.Text = parsedStrings[2];

                return dummyDoc;
                 */
                return _outputPage;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            _dockPanel.SaveAsXml(configFile);
            base.OnClosing(e);
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
            FindResult findResult = _outputPage.GetSelectedFindResult();
            
            if(findResult == null)
            {
                return;
            }
            
            findResult.CodePage.Show();
            findResult.CodePage.Scintilla.GoTo.Position(findResult.Start);
        }

        // This should be more generalized
        private void OnKeyDownOnForm(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.F)
            {
               // Find all

                // preload find all with selected text
                CodePage cp = GetVisibleCodePage();
                if (cp == null)
                {
                    return; // nothing to search.
                }
           
                string selectedText = cp.Scintilla.Selection.Text;
                if (!string.IsNullOrEmpty(selectedText))
                {
                    _findAllDialog.SetDefaultSearchString(selectedText);
                }
                else
                {
                    if (System.Windows.Forms.Clipboard.ContainsText())
                    {
                        _findAllDialog.SetDefaultSearchString(System.Windows.Forms.Clipboard.GetText());
                    }
                }
                
           
               if (_findAllDialog.ShowDialog(this) == DialogResult.OK)
               {
                   FindAll(_findAllDialog.SearchString);
               }
            }
        }

        private void FindAll(string searchString)
        {
            _outputPage.Clear();
            _findResults.Clear();

            foreach (CodePage cp in _codePageList)
            {
                List<Range> finds = cp.Scintilla.FindReplace.FindAll(searchString);
                finds.ForEach(x => _findResults.Add(new FindResult(x.Start, x.End, cp)));
            }
            _outputPage.AddFindResults(_findResults.ToArray());
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
            RefreshFunctionList();
        }

        private void RefreshFunctionList()
        {
            CodePage cp = GetVisibleCodePage();

            if (cp == null)
            {
                return;
            }
            _codeContext.UpdateFunctionList(cp.Scintilla);
            _functionPage.Update(_codeContext.FunctionList);
        }

        private void OnNewClicked(object sender, EventArgs e)
        {
            CodePage cp = CreateCodePage("");
            cp.Show(_dockPanel);        
        }

        public CodePage GetVisibleCodePage()
        {
          
            foreach (CodePage cp in _codePageList)
            {
                if (cp == _dockPanel.ActiveDocumentPane.ActiveContent)
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
                _filePage.Remove(page);
            };
            page.OnModifiedFlagChanged += delegate(object sender, EventArgs e)
            {
                _filePage.RefreshItem(page);
            };
            _codePageList.Add(page);
            _filePage.Add(page);
            return page;
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
            scintilla.Indentation.TabWidth = 6;
            scintilla.PreviewKeyDown += new PreviewKeyDownEventHandler(OnPreviewScintillaKeyDown);
            scintilla.KeyUp += new KeyEventHandler(OnScintillaKeyUp);
            scintilla.Margins[0].Width = 35; // some room for line numbers
            scintilla.AutoComplete.AutoHide = false;
            scintilla.AutoComplete.AutomaticLengthEntered = true;
          
    
            scintilla.Focus();
            
            return scintilla;
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
            RefreshFunctionList();
        }



        private void OnSaveClicked(object sender, EventArgs e)
        {
            
            CodePage cp = GetVisibleCodePage();
            if (cp == null)
            {
                return;
            }

            cp.Save();
            //!_statusLabel.Text = "Saved " + _tabData[_tabControl.SelectedTab].Name;
            
        }

        private void OnSaveAllClicked(object sender, EventArgs e)
        {
            
           
            // Base save all on what the user is seeing.
            foreach (CodePage cp in _codePageList)
            {
                cp.Save();
                //!_statusLabel.Text = "Saved " + _tabData[page].Name;
            }
             
        }

        private void OnCloseFileClicked(object sender, EventArgs e)
        {
            
            CodePage cp = GetVisibleCodePage();
            if (cp == null)
            {
                return;
            }
            cp.Close();
        }

        private void OnGotoToPreviousCursorPosition(object sender, EventArgs e)
        {
            
            
            // I think this can be done by extending the navigation points
            // override the push create a wrapper around the nav point
            // this allows me to compare the scintillas and decide what tab it came from.
            CodePage cp = GetVisibleCodePage();
            if (cp != null)
            {
                Scintilla scintilla = cp.Scintilla;
         //       DocumentNavigation.NavigationPont point = scintilla.DocumentNavigation.BackwardStack.Pop();
                
                if (scintilla.DocumentNavigation.CanNavigateBackward)
                {
                    
                    scintilla.DocumentNavigation.NavigateBackward();
                }
            }
             
        }

        private void OnGotoNextCursorPosition(object sender, EventArgs e)
        {
       
            CodePage cp = GetVisibleCodePage();
            if (cp != null)
            {
                Scintilla scintilla = cp.Scintilla;
               
                if (scintilla.DocumentNavigation.CanNavigateForward)
                {
                    scintilla.DocumentNavigation.NavigateForward();
                }
            }
             
            
        }

        void OnFileListDoubleClicked(object sender, EventArgs e)
        {
            
            
            ListBox fileListBox = (ListBox)sender;

            if (fileListBox.SelectedItem == null)
            {
                return;
            }

            string name =(string) fileListBox.SelectedItem;
            CodePage cp = null;
            foreach (CodePage page in _codePageList)
            {
                if (name == page.Text)
                {
                    cp = page;
                    break;
                }
            }

            if (cp == null)
            {
                return;
            }

     
            cp.Show();
            cp.Focus();
            cp.Scintilla.Focus();
             
        }

        private void OnFunctionShortCutDblClicked(object sender, EventArgs e)
        {
            
            
            ListBox functionListBox = (ListBox)sender;

            if (functionListBox.SelectedItem == null)
            {
                return;
            }

            FunctionMetaData functionData = (FunctionMetaData)functionListBox.SelectedItem;

            CodePage cp = GetVisibleCodePage();
            if(cp == null)
            {
                return;
            }

            // Assume code page is correct for function list, this isn't guaranteed.

            cp.Scintilla.GoTo.Line(functionData.StartLine);
            cp.Focus();
            cp.Scintilla.Focus();
            
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

        private void OnClickOnShowFiles(object sender, EventArgs e)
        {
            _filePage.Show(_dockPanel);
        }

        private void OnClickOnShowOutput(object sender, EventArgs e)
        {
            _outputPage.Show(_dockPanel);
        }
    }
}
