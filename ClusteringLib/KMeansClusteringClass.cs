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
    public class KMeansClusteringClass : IClustering
    {
        public int NodesNumber;

        public double ConvergencePrecision;

        public event ProgressDel ProgressChanged;
        public event DebugDel debugEvent;

        IClusteringNodeClass clusteringNodeClass;

        private List<IKMeansNode> Nodes = new List<IKMeansNode>();

        public bool StopFlag { set { clusteringNodeClass.StopFlag = value; } get { return clusteringNodeClass.StopFlag; } }
        public LearningMode learningMode { set { clusteringNodeClass.learningMode = value; } get { return clusteringNodeClass.learningMode; } }

        public KMeansClusteringClass(int nodesNumber, double convergencePrecision, List<Item> items)
        {
            clusteringNodeClass = new ClusteringNodeClass();

            if (items == null)
            {
               clusteringNodeClass.SetItems(new List<Item>());  // Items = new List<Item>();
            }
            else
            {
                clusteringNodeClass.SetItems(new List<Item>(items)); // Items = new List<Item>(items);
            }
            NodesNumber = nodesNumber;
            ConvergencePrecision = convergencePrecision;
        }

        public void SetOptions(ClusteringOptions opt)
        {
            NodesNumber = opt.ClustersNumber;
        }
        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ClustersNumber = NodesNumber;
            return result;
        }

        protected void Learn()
        {
            if (clusteringNodeClass.learningMode == LearningMode.Start) // if (learningMode == LearningMode.Start)
            {
                Tuple<double[], double[]> lims = EuclideanGeometry.MinMaxDim(clusteringNodeClass.GetItems()); // Tuple<double[], double[]> lims = EuclideanGeometry.MinMaxDim(Items);
                InitializeNodes(lims.Item1, lims.Item2);
            }
            for (int EpochNum = 1; ; ++EpochNum)
            {
                if (EpochNum > 1 && (clusteringNodeClass.StopFlag || clusteringNodeClass.Converged(ConvergencePrecision, Nodes))) // if (EpochNum > 1 && (StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                for (int i = 1; i <= NodesNumber; ++i)
                {
                    Dictionary<IKMeansNode, List<Item>> domains = clusteringNodeClass.CreateDomains(Nodes); // Dictionary<KMeansNode, List<Item>> domains = CreateDomains();
                    for (int j = 0; j < Nodes.Count; ++j)
                    {
                        Nodes[j].Learn(domains[Nodes[j]]);
                    }
                }
                ProgressChanged(EpochNum);
            }
        }
        IKMeansNode InitializeNode(double[] minDin, double[] maxDim)
        {
            double[] coord = new double[minDin.Length];
            for (int i = 0; i < coord.Length; ++i)
            {
                coord[i] = RandomAlgo.RandomNumber(minDin[i], maxDim[i]);
            }
            return new KMeansNode(coord);
        }
        void InitializeNodes(double[] minDin, double[] maxDim)
        {
            Nodes = new List<IKMeansNode>();
            for (int i = 0; i < NodesNumber; ++i)
            {
                Nodes.Add(InitializeNode(minDin, maxDim));
            }
        }

        public void SetItems(List<Item> items)
        {
            clusteringNodeClass.SetItems(items);
        }

        public List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (clusteringNodeClass.GetItems() == null || clusteringNodeClass.GetItems().Count == 0) // if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (clusteringNodeClass.GetItems().Count == 1) // if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(clusteringNodeClass.GetItems()[0]); // cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            Learn();
            List<List<Item>> Clusters = new List<List<Item>>();
            for (int i = 0; i < Nodes.Count; ++i)
            {
                Clusters.Add(new List<Item>());
            }
            foreach (var item in clusteringNodeClass.GetItems()) // foreach (var item in Items)
            {
                Clusters[clusteringNodeClass.Winner(item, Nodes)].Add(item); // Clusters[Winner(item)].Add(item);
            }
            Clusters.RemoveAll(cluster => cluster.Count == 0);
            return Clusters;
        }

        public void Stop()
        {
            clusteringNodeClass.Stop();
        }
    }
}