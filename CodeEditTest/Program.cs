using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace MonMon
{
    static class Program
    {
        static MonMonMainForm _mainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Guid guid = new Guid("{430E5900-195B-433d-8054-E8EB59C38950}");
            using (SingleInstance singleInstance = new SingleInstance(guid))
            {
                if (singleInstance.IsFirstInstance)
                {
                    singleInstance.ArgumentsReceived += new SingleInstance.ArgEvent(OnArgsRecieved);
                    singleInstance.ListenForArgumentsFromSuccessiveInstances(); 
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    TestRunner tr = new TestRunner();
                    tr.Go();
                    _mainForm = new MonMonMainForm(args);
                    Application.Run(_mainForm);
                    
                }
                else
                {
                    singleInstance.PassArgumentsToFirstInstance(args);
                }
            }

            
        }

        static void OnArgsRecieved(object sender, ArgumentsReceivedEventArgs e)
        {
            //throw new NotImplementedException();
            _mainForm.LoadArgsCrossThread(e.Args);
        }
    }
}
