using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Vision_Utlis
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
        //11:03
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
        /// 查询子节点的值
        /// </summary>
        /// <param name="XNode">父节点</param>
        /// <param name="XAttribute">父节点 属性名</param>
        /// <param name="XConnectName">父节点属性值</param>
        /// <param name="XChildNode">子节点</param>
        /// <returns></returns>
        public string XMLNodeSelectValue(string XNode, string XAttribute, string XConnectName, string XChildNode)
        {
            string XMLNodeSelectValue = "";
            xdoc.Descendants(XNode).Where(b => b.Attribute(XAttribute).Value == XConnectName).Descendants(XChildNode).Select(x => x.Value).ToList().ForEach(x => { XMLNodeSelectValue = x; });
            return XMLNodeSelectValue;
        }
        /// <summary>
        /// 查询某属性的节点值
        /// </summary>
        /// <param name="XNode">节点</param>
        /// <param name="XAttribute">属性名</param>
        /// <param name="XConnectName">属性值</param>
        /// <returns></returns>
        public string XMLNodeSelectValue_1(string XNode, string XAttribute, string XConnectName)
        {
            string XMLNodeSelectValue = "";
            xdoc.Descendants(XNode).Where(b => b.Attribute(XAttribute).Value == XConnectName).Select(x => x.Value).ToList().ForEach(x => { XMLNodeSelectValue = x; });
            return XMLNodeSelectValue;
        }
        /// <summary>
        /// 某属性节点的子节点数
        /// </summary>
        /// <param name="XNode">父节点</param>
        /// <param name="XAttribute">某属性名</param>
        /// <param name="XConnectName">某属性值</param>
        /// <returns></returns>
        public int XMLNodeCount(string XNode, string XAttribute, string XConnectName)
        {
            int Count = 0;
            xdoc.Descendants(XNode).Where(b => b.Attribute(XAttribute).Value == XConnectName).Descendants().ToList().ForEach(x => { Count++; });
            return Count;
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
        /// 查询节点某属性 
        /// </summary>
        /// <param name="XNode">查询节点的名称</param>
        /// <param name="Attribute">所需查询的属性</param>
        public string XMLAttibuteValue(string XNode, string Attribute)
        {
            return xdoc.Descendants(XNode).Attributes(Attribute).First().Value.ToString();
        }
        /// <summary>
        /// 根据 Attribute1查找XNode的 Attribute值
        /// </summary>
        /// <param name="XNode">节点</param>
        /// <param name="Attribute1">已知属性名</param>
        /// <param name="Attribute1Value">已知属性值</param>
        /// <param name="Attribute">查询的属性名</param>
        /// <returns></returns>
        public string XMLAttibute1forAttributeValue(string XNode, string Attribute1, string Attribute1Value, string Attribute)
        {
            return xdoc.Descendants(XNode).Where(b => b.Attribute(Attribute1).Value == Attribute1Value).Attributes(Attribute).First().Value.ToString();
        }
        /// <summary>
        /// 某节点的某属性值
        /// </summary>
        /// <param name="XNode">某节点</param>
        /// <param name="Attribute">某属性名</param>
        /// <returns></returns>
        public List<string> XMLAttibuteValue_1(string XNode, string Attribute)
        {
            List<string> XMLAttibuteValue = new List<string>();
            xdoc.Descendants(XNode).Attributes(Attribute).ToList().ForEach(x => XMLAttibuteValue.Add(x.Value));
            return XMLAttibuteValue;
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



