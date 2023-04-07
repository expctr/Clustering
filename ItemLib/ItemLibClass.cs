using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ExcelLib;

namespace ItemLib
{
    public struct Item //Структура для хранения предметов (точек)
    {
        public string Name;
        public int Index; 
        readonly double[] Coordinates;
        public Item(double[] coordinates)
        {
            Coordinates = new double[coordinates.Length];
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] = coordinates[i];
            }
            Name = "";
            Index = 0;
        }
        public Item(double X, double Y)
        {
            Coordinates = new double[] { X, Y };
            Name = "";
            Index = 2;
        }
        public Item(double X, double Y, string name, int index) : this(X, Y)
        {
            Name = name;
            Index = index;
        }
        public Item(Item item)
        {
            Name = item.Name;
            Coordinates = (double[])item.GetCoordinates.Clone();
            Index = item.Index;
        }
        public Item(Item item, int index) : this(item)
        {
            Index = index;
        }
        public Item(double[] coordinates, string name, int index)
        {
            Coordinates = (double[])coordinates.Clone();
            Name = name;
            Index = index;
        }
        public double this[int i]
        {
            get
            {
                return Coordinates[i];
            }
        }

        public double[] GetCoordinates
        {
            get
            {
                double[] result = new double[Coordinates.Length];
                for (int i = 0; i < Coordinates.Length; ++i)
                {
                    result[i] = Coordinates[i];
                }
                return result;
            }
        }

        public static List<double[]> ToDoubleArray(List<Item> items)
        {
            List<double[]> result = new List<double[]>();
            foreach(var i in items)
            {
                result.Add(i.GetCoordinates);
            }
            return result;
        }

        public string[] ToRow()
        {
            string[] result = new string[Coordinates.Length + 2];
            result[0] = Index.ToString();
            result[1] = Name;
            for(int i = 0; i < Coordinates.Length; ++i)
            {
                result[i + 2] = Coordinates[i].ToString();
            }
            return result;
        }
        public Item SetIndex(int index)
        {
            return new Item(this, index);
        }
        public static void AttachIndexes(ref List<Item> items)
        {
            if (items == null) return;
            for(int i = 0; i < items.Count; ++i)
            {
                items[i] = items[i].SetIndex(i);
            }
        }
        public string Print_CSV()
        {
            string result = "";
            result += $"\"{Name}\";";
            for(int i = 0; i < Coordinates.Length; ++i)
            {
                result += $"\"{Coordinates[i]}\"";
                if(i != Coordinates.Length - 1)
                {
                    result += ";";
                }
            }
            return result;
        }
        public XmlElement Print_XML(XmlDocument xmlDoc, string[] colsNames)
        {
            XmlElement result =
                xmlDoc.CreateElement("Item");
            result.SetAttribute("Name", Name);
            for(int i = 0; i < colsNames.Length; ++i)
            {
                XmlElement cur =
                    xmlDoc.CreateElement(colsNames[i]);
                cur.InnerText = Coordinates[i].ToString();
                result.AppendChild(cur);
            }
            return result;
        }

        public static Item GetItem_XML(XmlNode node, out string[] colsNames)
        {
            string name = node.Attributes[0].InnerText;
            List<double> coordinates = new List<double>();
            List<string> colsNamesList = new List<string>();
            XmlNodeList nodeList = node.ChildNodes;
            for (int i = 0; i < nodeList.Count; ++i)
            {
                colsNamesList.Add(nodeList[i].Name);
                coordinates.Add(double.Parse(nodeList[i].InnerText));
            }
            colsNames = new string[colsNamesList.Count];
            for (int i = 0; i < colsNames.Length; ++i)
            {
                colsNames[i] = colsNamesList[i];
            }
            double[] Coordinates = new double[coordinates.Count];
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] = coordinates[i];
            }
            return new Item(Coordinates, name, 0);
        }
        public static Item GetItem_XML(XmlNode node, int colsNumber)
        {
            string name = node.Attributes[0].InnerText;
            List<double> coordinates = new List<double>();
            XmlNodeList nodeList = node.ChildNodes;
            if (nodeList.Count != colsNumber)
            {
                throw new FormatException("Недопустимый размер списка дочерних нод.");
            }
            for (int i = 0; i < nodeList.Count; ++i)
            {
                coordinates.Add(double.Parse(nodeList[i].InnerText));
            }
            double[] Coordinates = new double[coordinates.Count];
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] = coordinates[i];
            }
            return new Item(Coordinates, name, 0);
        }
        public static List<Item> GetItems_XML(XmlDocument xmlDoc, out string[] colsNames)
        {
            List<Item> result = new List<Item>();
            var childNodes = xmlDoc.ChildNodes[1].ChildNodes;
            result.Add(GetItem_XML(childNodes[0], out colsNames));
            for (int i = 1; i < childNodes.Count; ++i)
            {
                result.Add(GetItem_XML(childNodes[i], colsNames.Length));
            }
            return result;
        }
        public static Item GetItem_XLSX(Excel excel, int sheet, int rowNum, int dimension)
        {
            string name = excel.ReadCell(rowNum, 0, sheet);
            List<double> coordList = new List<double>();
            for (int i = 1; i <= dimension; ++i)
            {
                coordList.Add(double.Parse(excel.ReadCell(rowNum, i, sheet)));
            }
            double[] coordinates = new double[coordList.Count];
            for (int i = 0; i < coordinates.Length; ++i)
            {
                coordinates[i] = coordList[i];
            }
            return new Item(coordinates, name, 0);
        }
        public static void WriteItem_XLSX(Item item, Excel excel, int sheet, int rowNum)
        {
            excel.WriteCell(rowNum, 0, item.Name, sheet);
            double[] coordinates = item.GetCoordinates;
            for (int i = 1; i <= coordinates.Length; ++i)
            {
                excel.WriteCell(rowNum, i, coordinates[i - 1], sheet);
            }
        }
    }//struct Item
}
