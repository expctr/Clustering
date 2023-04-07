using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLib;
using EuclideanGeometryLib;
using RandomAlgoLib;
using AlgorithmLib;
using System.Drawing;
using System.Threading;

namespace ClusteringLib
{
    public class Cluster
    {
        List<Item> Elements;
        Color ElementColor;
        Color CentreColor;
        public Cluster(List<Item> elements)
        {
            Elements = new List<Item>();
            foreach (var element in elements)
            {
                Elements.Add(new Item(element));
            }
            RandomAlgo.RandomColor(170, out ElementColor, out CentreColor);
        }
        public static List<Cluster> CreateClusters(List<List<Item>> input)
        {
            if (input == null) return null;
            List<Cluster> result = new List<Cluster>();
            foreach (var i in input)
            {
                result.Add(new Cluster(i));
            }
            return result;
        }
        public List<Item> GetElements()
        {
            List<Item> result = new List<Item>();
            foreach (var el in Elements)
            {
                result.Add(new Item(el));
            }
            return result;
        }
        public double[] GetCentre
        {
            get
            {
                return EuclideanGeometry.Barycentre(Item.ToDoubleArray(Elements));
            }
        }
        public Color GetElementColor
        {
            get
            {
                return ElementColor;
            }
        }
        public Color GetCentreColor
        {
            get
            {
                return CentreColor;
            }
        }
        public int Count
        {
            get
            {
                return Elements.Count;
            }
        }

        public Item this[int ind]
        {
            get
            {
                return Elements[ind];
            }
        }
        //
        //Описательные характеристики
        //
        public double MeanLinearIntraclusterDeviation
        {
            get
            {
                int count = Count;
                double[] centre = GetCentre;
                double result = 0;
                foreach (var el in Elements)
                {
                    result += EuclideanGeometry.Distance(el.GetCoordinates, centre) / count;
                }
                return result;
            }
        }
        public double Dispersion
        {
            get
            {
                double[] centre = GetCentre;
                double countSqrt = Math.Sqrt((double)Count);
                double result = 0;
                foreach (var el in Elements)
                {
                    result += Math.Pow(EuclideanGeometry.Distance(el.GetCoordinates, centre) /
                        countSqrt, 2);
                }
                return result;
            }
        }
        public double MeanSquareIntraclusterDeviation
        {
            get
            {
                return Math.Sqrt(Dispersion);
            }
        }
        public double QuartileRange
        {
            get
            {
                double[] centre = GetCentre;
                List<Item> elements = GetElements();
                elements.Sort((el1, el2) =>
                {
                    return EuclideanGeometry.Distance(el1.GetCoordinates,
                        centre).CompareTo(EuclideanGeometry.Distance(el2.GetCoordinates,
                        centre));
                });
                int ind = elements.Count / 2 + (elements.Count % 2 == 0 ? 0 : 1);
                return EuclideanGeometry.Distance(elements[ind - 1].GetCoordinates, centre);
            }
        }
        public event Action<double> progressChanged;
        public void ResetProgressChanged()
        {
            progressChanged = null;
        }
        public double AverageDistanceBetweenElements
        {
            get
            {
                double result = 0;
                double cur = 0;
                double total = (double)Count / 2 * (Count - 1);
                for (int i = 0; i < Count; ++i)
                {
                    for (int j = 0; j < Count; ++j)
                    {
                        if (i <= j) continue;
                        result += EuclideanGeometry.Distance(Elements[i].GetCoordinates,
                            Elements[j].GetCoordinates) / total;
                        progressChanged(++cur / total);
                    }
                }
                return result;
            }
        }
        public int FindIndex_Name(string name)
        {
            return Elements.FindIndex(el => el.Name == name);
        }
        public int FindIndex_Index(int index)
        {
            return Elements.FindIndex(el => el.Index == index);
        }
        public static int FindCluster_Name(List<Cluster> clusters, string name)
        {
            for (int i = 0; i < clusters.Count; ++i)
            {
                if (clusters[i].FindIndex_Name(name) != -1)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int FindCluster_Index(List<Cluster> clusters, int index)
        {
            for (int i = 0; i < clusters.Count; ++i)
            {
                if (clusters[i].FindIndex_Index(index) != -1)
                {
                    return i;
                }
            }
            return -1;
        }
        public int CountInRadius(double[] point, double radius)
        {
            int result = 0;
            Elements.ForEach(el =>
            {
                if (EuclideanGeometry.Distance(el.GetCoordinates, point) <= radius)
                    result += 1;
            });
            return result;
        }
        delegate double clusterDel(Cluster cluster);
        static double IntraclusterDeviation_Total(List<Cluster> clusters, clusterDel mean)
        {
            double result = 0;
            clusters.ForEach(cluster => { result += mean(cluster); });
            return result;
        }
        public static double MeanLinearIntraclusterDeviation_Total(List<Cluster> clusters)
        {
            return IntraclusterDeviation_Total(clusters, cluster =>
            cluster.MeanLinearIntraclusterDeviation);
        }
        public static double MeanSquareIntraclusterDeviation_Total(List<Cluster> clusters)
        {
            return IntraclusterDeviation_Total(clusters, cluster =>
            cluster.MeanSquareIntraclusterDeviation);
        }
        public static double[] ClustersBarycentre(List<Cluster> clusters)
        {
            List<double[]> centres = new List<double[]>();
            List<double> masses = new List<double>();
            clusters.ForEach(cluster => {
                masses.Add(cluster.Count);
                centres.Add(cluster.GetCentre);
            });
            return EuclideanGeometry.Barycentre(centres, masses);
        }
        public static double LinearInterclusterDeviation_Total(List<Cluster> clusters)
        {
            double result = 0;
            double[] barycentre = ClustersBarycentre(clusters);
            clusters.ForEach(cluster =>
            {
                result +=
                    EuclideanGeometry.Distance(cluster.GetCentre, barycentre);
            });
            return result;
        }
        public static double MeanSquareInterclusterDeviation(List<Cluster> clusters)
        {
            double result = 0;
            double[] barycentre = ClustersBarycentre(clusters);
            double sqrtCount = Math.Sqrt(clusters.Count);
            clusters.ForEach(cluster =>
            {
                result += Math.Pow(EuclideanGeometry.Distance(cluster.GetCentre,
                    barycentre) / sqrtCount, 2);
            });
            return Math.Sqrt(result);
        }

    }
}
