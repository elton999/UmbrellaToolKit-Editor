using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using Microsoft.Win32;

namespace Umbrella_Location_Editor
{
    public class FIleXML
    {

        public XmlDocument doc = new XmlDocument();
        XmlTextReader reader;
        string curlFile;

        public void CreateNewFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Umbrella Location File (*.UmbrellaLocation)|*.UmbrellaLocation";
            if (saveFileDialog.ShowDialog() == true)
            {
                //File.WriteAllText(saveFileDialog.FileName, "location");
                this.curlFile = saveFileDialog.FileName;
                
                File.AppendAllText(this.curlFile, "location");
                doc.AppendChild(doc.CreateElement("location"));
            }
        }

        public void SaveFile()
        {
            doc.Save(this.curlFile);
        }

        private void CreateList(string Node, string Type)
        {
            if (doc.GetElementsByTagName(Node).Count < 1)
            {
                XmlElement elem = doc.CreateElement(Node);
                elem.SetAttribute("type", Type);
                doc.GetElementsByTagName("location")[0].AppendChild(elem);
            }

            XmlNodeList ElementNode = doc.GetElementsByTagName(Node);
            ElementNode[0].RemoveAll();
            XmlAttribute attr = doc.CreateAttribute("type");
            attr.Value = Type;
            ElementNode[0].Attributes.SetNamedItem(attr);
        }

        public void AddItemString(string Node, List<string> ContentList)
        {
            this.CreateList(Node, "string");

            XmlNodeList ElementNode = doc.GetElementsByTagName(Node);

            foreach (string content in ContentList)
            {
                XmlElement NewElement = doc.CreateElement("item");
                NewElement.InnerText = content;
                ElementNode[0].AppendChild(NewElement);
            }
        }

        public List<string> getItemsString(string Node)
        {
            List<string> ListReturn = new List<string>();

            XmlNodeList ElementNode = doc.GetElementsByTagName(Node);
            if (ElementNode.Count > 0 && ElementNode[0].Attributes[0].Value == "string" && ElementNode[0].HasChildNodes)
            {
                foreach (XmlNode xxNode in ElementNode[0].ChildNodes) ListReturn.Add(xxNode.InnerText);
            }

            return ListReturn;
        }
    }
}
