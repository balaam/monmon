﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Einfall.Editor.Lua;

namespace MonMon
{
    public class SettingsReader
    {
        public SettingsReader()
        {
        }

        public Dictionary<string, List<CompleteData>> Read()
        {
            string filePath = "default.xml";
            var autocompleteTable = new Dictionary<string, List<CompleteData>>();
            if (!File.Exists(filePath))
            {
                return autocompleteTable;
            }
            
            using (var reader = XmlReader.Create(new FileStream(filePath, FileMode.Open)))
            {
              
                string currentName = "";

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "parent")
                        {
                            reader.MoveToAttribute("name");
                            currentName = reader.ReadContentAsString();
                            autocompleteTable.Add(currentName, new List<CompleteData>());
                        }
                        else if (reader.Name == "child")
                        {
                            CompleteData childData = new CompleteData();
                            // Get data add it to the autocomplete table
                            reader.Read();
                            while (reader.Name != "child")
                            {
                                switch (reader.Name)
                                {
                                    case "name":
                                        {
                                            childData.Name = reader.ReadElementContentAsString();
                                        } break;
                                    default:
                                        {
                                            break;
                                        }
                                }
                                reader.Read();
                            }
                            autocompleteTable[currentName].Add(childData);

                        }
                    }
                    else
                    {
                        if (reader.Name == "parent")
                        {
                            currentName = "";
                        }
                    }
                }
            }

            return autocompleteTable;

           
        }


    }
}
