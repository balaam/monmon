using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Pipes;
using System.IO;

namespace MonMon
{
    public class SingleInstance : IDisposable
    {
        bool    _ownsMutex;
        Mutex   _mutex;
        Guid    _guid;
        public delegate void ArgEvent(object sender, ArgumentsReceivedEventArgs e);
        public event ArgEvent ArgumentsReceived;

        public Boolean IsFirstInstance
        {
            get 
            { 
                return _ownsMutex; 
            } 
        }

        public SingleInstance(Guid guid)
        {
            _guid = guid;
            _mutex = new Mutex(true, guid.ToString(), out _ownsMutex); 
        }

        public void PassArgumentsToFirstInstance(String[] args)
        {
            try
            {
                using (NamedPipeClientStream client = new NamedPipeClientStream(_guid.ToString()))
                {
                    using (StreamWriter writer = new StreamWriter(client))
                    {
                        client.Connect(200);
                        foreach (String argument in args)
                        {
                            writer.WriteLine(argument);
                        }
                    }
                }
            }
            catch (TimeoutException)
            { } //Couldn't connect to server
            catch (IOException)
            { } //Pipe was broken

        }

        public void CallOnArgumentsReceived(object state)
        {
            if (ArgumentsReceived != null)
            {
                ArgumentsReceived(this, new ArgumentsReceivedEventArgs((string[])state));
            }
        }

        public void ListenForArguments(object state)
        {
            try
            {
                using (NamedPipeServerStream server = new NamedPipeServerStream(_guid.ToString()))
                using (StreamReader reader = new StreamReader(server))
                {
                    server.WaitForConnection();
                    List<String> arguments = new List<String>();
                    while (server.IsConnected)
                    {
                        string line = reader.ReadLine();
                        if (line != null && line.Length > 0)
                        {
                            arguments.Add(line);
                        }
                    }

                    ThreadPool.QueueUserWorkItem(new WaitCallback(CallOnArgumentsReceived), arguments.ToArray());
                }
            }
            catch (IOException)
            { } //Pipe was broken
            finally
            {
                ListenForArguments(null);
            }

        }

        public void ListenForArgumentsFromSuccessiveInstances()
        {
            if (!IsFirstInstance)
            {
                throw new InvalidOperationException("This is not the first instance.");
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(ListenForArguments));
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_mutex != null && _ownsMutex)
            {
                _mutex.ReleaseMutex();
                _mutex = null;
            }
        }

        #endregion
    }
}
