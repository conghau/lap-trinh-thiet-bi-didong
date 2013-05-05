using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AiThongMinhHonLop5.HighScore
{
    public class HighScore
    {
        public static void AddItem(string fileName, string rootName, string elementName, List<XmlElement> xmlElements)
        {
            try
            {
                var myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

                if (!myIsolatedStorage.FileExists(fileName))
                {
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(fileName, FileMode.Create, myIsolatedStorage))
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;
                        using (XmlWriter writer = XmlWriter.Create(isoStream, settings))
                        {
                            writer.WriteStartElement(rootName);

                            writer.WriteStartElement(elementName, "");

                            foreach (var element in xmlElements)
                            {
                                writer.WriteStartElement(element.Name, "");
                                writer.WriteString(element.Value);
                                writer.WriteEndElement();
                            }

                            writer.WriteEndElement();

                            writer.WriteEndDocument();
                            writer.Flush();
                        }
                    }
                }
                else
                {
                    XDocument loadedData;
                    using (Stream stream = myIsolatedStorage.OpenFile(fileName, FileMode.Open, FileAccess.ReadWrite))
                    {
                        loadedData = XDocument.Load(stream);
                        var fav = new XElement(elementName, "");

                        foreach (var element in xmlElements)
                        {
                            var elm = new XElement(element.Name, element.Value);
                            fav.Add(elm);
                        }


                        var root = loadedData.Element(rootName);
                        var rows = root.Descendants(elementName);

                        if (rows.Count() > 0)
                        {
                            var lastRow = rows.Last();
                            lastRow.AddAfterSelf(fav);
                        }
                        else
                        {
                            root.AddFirst(fav);
                        }
                    }

                    // Save To History.xml File 
                    using (IsolatedStorageFileStream myStream = new IsolatedStorageFileStream(fileName, FileMode.Create, myIsolatedStorage))
                    {
                        loadedData.Save(myStream);
                        myStream.Close();
                    }
                }

                //MessageBox.Show("Save successfully!", "Save", MessageBoxButton.OK);
                //return lastId;
            }
            catch
            {
                // MessageBox.Show("Can't save your template.", "Save", MessageBoxButton.OK);
            }
        }
    }
    public class XmlElement
    {
        public XmlElement()
        {

        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}
