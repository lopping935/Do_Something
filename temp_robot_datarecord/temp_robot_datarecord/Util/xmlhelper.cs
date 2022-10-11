using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GH_Utlis
{  
        /*
        class Program
        {
            static void Main(string[] args)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "XMLTest.xml";
                XMLHelper xxx = new XMLHelper(path);

                string find_value = xxx.XMLNodeSelectValue("PLC", "PLC2", "ip");
                Console.WriteLine(find_value);
                xxx.XMLNodeChangeValue("PLC", "PLC1", "port", "20000");
                xxx.XMLNodeDeleteNode("PLC", "PLC1");
                Console.ReadLine();
            }
        }
        */
        public class XMLHelper
        {
            XDocument xdoc;
            string xpath = "";
            public XMLHelper(string Xpath)
            {
                xpath = Xpath;
                xdoc = new XDocument();
                xdoc = XDocument.Load(Xpath);
            }

            /// <summary>
            /// 查询子节点的值
            /// </summary>
            /// <param name="XNode">父节点</param>
            /// <param name="XConnectName">父节点 属性Name名字</param>
            /// <param name="XChildNode">子节点</param>
            /// <returns></returns>
            public string XMLNodeSelectValue(string XNode, string XConnectName, string XChildNode)
            {
                string XMLNodeSelectValue = "";
                xdoc.Descendants(XNode).Where(b => b.Attribute("Name").Value == XConnectName).Descendants(XChildNode).Select(x => x.Value).ToList().ForEach(x => { XMLNodeSelectValue = x; });
                return XMLNodeSelectValue;
            }
            /// <summary>
            /// 改变子节点的名字
            /// </summary>
            /// <param name="XNode">父节点名</param>
            /// <param name="XConnectName">父节点 属性Name的值</param>
            /// <param name="XChildNode">子节点名字</param>
            /// <param name="XChangeValue">需改变的子节点的值</param>
            public void XMLNodeChangeValue(string XNode, string XConnectName, string XChildNode, string XChangeValue)
            {
                var quary = from c in xdoc.Descendants(XNode).Where(b => b.Attribute("Name").Value == XConnectName).Descendants(XChildNode) select c;
                quary.ToList().ForEach(it => it.Value = XChangeValue);
                xdoc.Save(xpath);
            }
            /// <summary>
            /// 删除节点
            /// </summary>
            /// <param name="XNode">父节点名字</param>
            /// <param name="XConnectName">父节点 属性Name的名字</param>
            public void XMLNodeDeleteNode(string XNode, string XConnectName)
            {
                xdoc.Descendants(XNode).Where(b => b.Attribute("Name").Value == XConnectName).Remove();
                xdoc.Save(xpath);
            }
            /// <summary>
            /// 删除节点
            /// </summary>
            /// <param name="XNode">父节点</param>
            /// <param name="XConnectName">父节点 属性Name名字</param>
            /// <param name="XChildNode">子节点</param>
            /// <param name="XChildNodeValue">子节点的值</param>
            public void XMLNodeDeleteNode(string XNode, string XConnectName, string XChildNode, string XChildNodeValue)
            {
                xdoc.Descendants(XNode).Where(b => b.Attribute("Name").Value == XConnectName).Descendants(XChildNode).Where(b => b.Value == XChildNodeValue).Remove();
                xdoc.Save(xpath);
            }

        }
}



