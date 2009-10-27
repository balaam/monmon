using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MonMon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            TestRunner tr = new TestRunner();
            tr.Go();
            Application.Run(new Form1(args));
        }
    }
}
