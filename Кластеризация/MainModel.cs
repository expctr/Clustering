using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemLib;
using AlgorithmLib;
using ClusteringLib;
using System.Diagnostics;
using System.IO;
using System.Xml;
using ExcelLib;

namespace Кластеризация
{
    public class MainModel
    {
        MainForm form;

        public enum ClusteringAlgorithm
        {
            SOM, GNG, KMeans, Aglomerative, DBSCAN, AffinityPropagation,
            FOREL, MST, FullGraph, NULL
        };

        ClusteringParameterOptions CPOptions = new ClusteringParameterOptions();

        List<Item> OriginalItems = new List<Item>();

        public List<Item> Items = new List<Item>();

        public List<Cluster> Clusters = new List<Cluster>();

        public TimeLimitMaster timeLimitMaster;

        public ClusteringAlgorithm clusteringAlgorithm = ClusteringAlgorithm.NULL;

        public IClustering clusteringClass;

        public Stopwatch Watch = new Stopwatch();

        public bool Continuable;

        string[] OriginalColsNames;

        public string[] ColsNames;

        public MainModel(MainForm form)
        {
            this.form = form;
            timeLimitMaster = new TimeLimitMaster(true, 0, 0, 10);
        }

        //
        //Установить список объектов
        //

        public void SetItems(List<Item> items, string[] colsNames)
        {
            if (items == null || items.Count == 0 || colsNames == null ||
                colsNames.Length == 0)
            {
                return;
            }
            //
            OriginalColsNames = colsNames;
            ColsNames = (string[])OriginalColsNames.Clone();
            //
            OriginalItems = items;
            //
            CPOptions.ChosenClusterizationParameter = new bool[ColsNames.Length];
            for (int i = 0; i < CPOptions.ChosenClusterizationParameter.Length; ++i)
            {
                CPOptions.ChosenClusterizationParameter[i] = true;
            }
            //
            Item.AttachIndexes(ref OriginalItems);
            CPOptions.DimensionalWeights = new double[Dimension];
            for (int i = 0; i < Dimension; ++i)
            {
                CPOptions.DimensionalWeights[i] = 1;
            }
            CPOptions.Normalize = false;
            Items = new List<Item>(OriginalItems);
            clusteringClass.SetItems(Items);
            form.SecondState(ref Clusters, Dimension);
            form.ShowObjectListBClickEventHandler(Dimension, ColsNames, Items);
        }

        //
        //Настройки
        //

        public void SetOptions(ClusteringOptions opt)
        {
            form.GetContinueClusterizationTSMI().Enabled = false;
            clusteringClass.SetOptions(opt);
            timeLimitMaster.SetOptions(opt);
        }

        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = clusteringClass.GetOptions();
            timeLimitMaster.AddOptions(result);
            return result;
        }

        //
        //Выбор параметров кластеризации
        //

        public void SetClusterizationParameterOptions(ClusteringParameterOptions
            _CPOptions)
        {
            CPOptions = _CPOptions;
            Transform();
        }

        public ClusteringParameterOptions GetClusterizationParameterOptions()
        {
            ClusteringParameterOptions result = new ClusteringParameterOptions();
            result.ChosenClusterizationParameter = (bool[])CPOptions.ChosenClusterizationParameter.Clone();
            result.DimensionalWeights = (double[])CPOptions.DimensionalWeights.Clone();
            result.Normalize = CPOptions.Normalize;
            return result;
        }

        public string[] GetOriginalColsNames()
        {
            return (string[])OriginalColsNames.Clone();
        }

        Item TransformItem(Item item, bool[] chosenCoord,
            double[] dimensionalWeights)
        {
            List<double> NewCoordinatesList = new List<double>();
            for (int i = 0; i < chosenCoord.Length; ++i)
            {
                if (chosenCoord[i])
                {
                    NewCoordinatesList.Add(item[i] * dimensionalWeights[i]);
                }
            }
            double[] NewCoordinates = new double[NewCoordinatesList.Count];
            for (int i = 0; i < NewCoordinatesList.Count; ++i)
            {
                NewCoordinates[i] = NewCoordinatesList[i];
            }
            return new Item(NewCoordinates, item.Name, item.Index);
        }

        List<Item> TransformItems(List<Item> items, bool[] chosenCoord,
            double[] dimensionalWeights, bool normalize)
        {
            List<Item> result = new List<Item>();
            foreach (var item in items)
            {
                result.Add(TransformItem(item, chosenCoord, dimensionalWeights));
            }
            if (normalize)
            {
                List<double[]> coordList = new List<double[]>();
                foreach (var item in result)
                {
                    coordList.Add(item.GetCoordinates);
                }
                coordList = EuclideanGeometryLib.EuclideanGeometry.Normalize(coordList);
                for (int i = 0; i < result.Count; ++i)
                {
                    result[i] = new Item(coordList[i], result[i].Name, result[i].Index);
                }
            }
            return result;
        }

        string[] TransformColsNames(string[] colsNames, bool[] chosenCoord)
        {
            List<string> NewColsNamesList = new List<string>();
            for (int i = 0; i < chosenCoord.Length; ++i)
            {
                if (chosenCoord[i])
                {
                    NewColsNamesList.Add(colsNames[i]);
                }
            }
            string[] result = new string[NewColsNamesList.Count];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = NewColsNamesList[i];
            }
            return result;
        }

        void Transform()
        {
            Items = TransformItems(OriginalItems, CPOptions.ChosenClusterizationParameter,
                CPOptions.DimensionalWeights, CPOptions.Normalize);
            ColsNames = TransformColsNames(OriginalColsNames, CPOptions.ChosenClusterizationParameter);
            clusteringClass.SetItems(Items);
            Clusters = new List<Cluster>();
            form.ClustersUpdated(ref Clusters, Dimension, ColsNames);
            form.ShowObjectListBClickEventHandler(Dimension, ColsNames, Items);
        }

        public int Dimension
        {
            get
            {
                if (ColsNames == null)
                {
                    return 0;
                }
                return ColsNames.Length;
            }
        }

        //
        //Сохранение файла
        //
        public List<string> PrintCluster_CSV(Cluster cluster, bool quotes)
        {
            List<string> result = new List<string>();
            result.Add(Algorithm.ToString_CSV(ColsNames, quotes));
            List<Item> items = cluster.GetElements();
            foreach (var item in items)
            {
                result.Add(item.Print_CSV());
            }
            return result;
        }

        public List<string> PrintClusters_CSV()
        {
            List<string> result = new List<string>();
            result.AddRange(PrintCluster_CSV(Clusters[0], false));
            for (int i = 1; i < Clusters.Count; ++i)
            {
                result.AddRange(PrintCluster_CSV(Clusters[i], true));
            }
            return result;
        }

        public XmlDocument PrintClusterXML(Cluster cluster)
        {
            List<Item> items = cluster.GetElements();
            XmlDocument result = new XmlDocument();
            XmlDeclaration xmlDecl = result.CreateXmlDeclaration("1.0", "utf-8", null);
            result.AppendChild(xmlDecl);
            XmlElement clusterElement =
                result.CreateElement("Cluster");
            for (int i = 0; i < cluster.Count; ++i)
            {
                clusterElement.AppendChild(
                    cluster[i].Print_XML(result,
                    ColsNames));
            }
            result.AppendChild(clusterElement);
            return result;
        }

        XmlElement PrintClusterXML(Cluster cluster, XmlDocument xmlDoc)
        {
            List<Item> items = cluster.GetElements();
            XmlElement result = xmlDoc.CreateElement("Cluster");
            for (int i = 0; i < cluster.Count; ++i)
            {
                result.AppendChild(
                    cluster[i].Print_XML(xmlDoc,
                    ColsNames));
            }
            return result;
        }

        public XmlDocument PrintClustersXML()
        {
            XmlDocument result = new XmlDocument();
            XmlDeclaration xmlDecl = result.CreateXmlDeclaration("1.0", "utf-8", null);
            result.AppendChild(xmlDecl);
            XmlElement clustersElement = result.CreateElement("Clusters");
            foreach (var cluster in Clusters)
            {
                clustersElement.AppendChild(PrintClusterXML(cluster, result));
            }
            result.AppendChild(clustersElement);
            return result;
        }
    }
}
