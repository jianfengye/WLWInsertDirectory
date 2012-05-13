using System;
using System.Collections.Generic;
using System.Text;
using WindowsLive.Writer.Api;
using System.Windows.Forms;
using System.Xml;
using System.IO;


namespace InsertDire
{
    [WriterPlugin("01B35DA5-1A9A-4335-8851-71A38C2B0B63", "Insert Dire",
    PublisherUrl = "http://www.cnblogs.com/yjf512/",
    Description = "Insert Dire.")]
    [InsertableContentSourceAttribute("Insert Dire", SidebarText = "Insert Dire")]
    public class Class1 : ContentSource
    {
        public override DialogResult CreateContent(IWin32Window dialogOwner, ref string content) 
        {
            content = this.InsertDirectory(content);
            return DialogResult.OK;
        }

        private string InsertDirectory(string content)
        {
            content = "<div>" + content + "</div>";
            XmlDocument doc = new XmlDocument();
            XmlElement directory = doc.CreateElement("div");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(content);
            XmlNodeList nodelist = xmlDoc.GetElementsByTagName("h1");
            int sectionNum = 1;
            foreach (XmlElement item in nodelist)
            {
                XmlElement ahref = doc.CreateElement("a");
                ahref.SetAttribute("href", "#section" + sectionNum);
                ahref.InnerText = item.InnerText;

                XmlElement pNode = doc.CreateElement("p");
                pNode.AppendChild(ahref);
                directory.AppendChild(pNode);

                XmlElement alink = xmlDoc.CreateElement("a");
                alink.SetAttribute("name", "section" + sectionNum);
                item.AppendChild(alink);

                sectionNum++;
            };
            string direcotryStr = directory.InnerXml;
            return direcotryStr + xmlDoc.DocumentElement.InnerXml;
        }
    }
}
