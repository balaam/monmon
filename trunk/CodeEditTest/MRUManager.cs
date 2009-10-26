using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

// Taken from http://www.eggheadcafe.com/tutorials/aspnet/a6ec88d4-212f-45be-ad94-716ee0c10901/using-isolated-storage-st.aspx
// With help from http://blogs.msdn.com/dmahugh/archive/2006/12/14/finding-windowsbase-dll.aspx
namespace MonMon.Utilities
{
   
    /// <summary>
    /// MRU Mangaer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MRUManager<T>
    {
        /// <summary>
        /// 
        /// </summary>
        private string _listName;
        /// <summary>
        /// 
        /// </summary>
        private int _limit;
        /// <summary>
        /// 
        /// </summary>
        private ObservableCollection<T> _list;

        /// <summary>
        /// Occurs when [list changed].
        /// </summary>
        public event EventHandler ListChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MRUManager<T>"/> class.
        /// </summary>
        /// <param name="listName">Name of the list.</param>
        /// <param name="limit">The limit.</param>
        public MRUManager(string listName, int limit)
        {
            
            if (String.IsNullOrEmpty(listName))
            {
                throw new ArgumentOutOfRangeException("listName", "name cannot be null or empty");
            }

            if (limit <= 0)
            {
                throw new ArgumentOutOfRangeException("limit", "limit must be greater than zero.");
            }

            _listName = listName;
            _limit = limit;
            LoadFromDisk();
        }


        /// <summary>
        /// Gets the name of the list.
        /// </summary>
        /// <value>The name of the list.</value>
        public string ListName
        {
            get { return _listName; }
        }

        /// <summary>
        /// Gets the limit.
        /// </summary>
        /// <value>The limit.</value>
        public int Limit
        {
            get { return _limit; }
        }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>The list.</value>
        public ReadOnlyCollection<T> List
        {
            get { return new ReadOnlyCollection<T>(_list); }
        }

        public void Add(T item)
        {
            if (_list.Contains(item))
            {
                _list.Remove(item);
            }
            _list.Insert(0, item);
            RemoveExtraItems();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }

        /// <summary>
        /// Saves to disk.
        /// </summary>
        private void SaveToDisk()
        {
            try
            {
                // Scope = User &   Assembly.
                // store is a container of streams of the IsolatedStorageFile class.
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    string fileName = "mruList_" + ListName;
                    IsolatedStorageFileStream stream;
                    if (store.GetFileNames(fileName).Length > 0)
                    {
                        stream = new IsolatedStorageFileStream(fileName, FileMode.Truncate, FileAccess.Write, store);
                    }
                    else
                    {
                        stream = new IsolatedStorageFileStream(fileName, FileMode.Create, FileAccess.Write, store);
                    }
                    using (stream)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                        serializer.Serialize(stream, _list);
                    }
                }
            }
            catch (Exception e)
            {
                //log "Unable to save MRU list to disk." along with e.
                System.Console.Error.WriteLine(e);
            }
        }

        /// <summary>
        /// Loads from disk.
        /// </summary>
        private void LoadFromDisk()
        {
            ObservableCollection<T> list = null;
            try
            {
                //C:\Documents and Settings\[username]\Local Settings\Application Data\IsolatedStorage
                using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForAssembly())
                {
                    string fileName = "mruList_" + ListName;
                    if (store.GetFileNames(fileName).Length > 0)
                    {
                        using (IsolatedStorageFileStream stream = new
                            IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, store))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<T>));
                            list = serializer.Deserialize(stream) as ObservableCollection<T>;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Log : "Unable to load MRU list from disk." along with e.
                System.Console.Error.WriteLine(e);
            }
            if (list == null) list = new ObservableCollection<T>();
            _list = list;
            _list.CollectionChanged
                += new NotifyCollectionChangedEventHandler(CollectionChangedHandler);
            RemoveExtraItems();
        }

        /// <summary>
        /// Removes the extra items.
        /// </summary>
        private void RemoveExtraItems()
        {
            if (_list.Count > Limit)
            {
                for (int x = Limit; x < _list.Count; x++)
                {
                    _list.RemoveAt(x);
                }
            }
        }

        /// <summary>
        /// Collections the changed handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            SaveToDisk();
            if (ListChanged != null)
            {
                ListChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}