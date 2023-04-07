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
    public class KMeansClusteringClass : ClusteringNodeClass<KMeansNode>, IClustering
    {
        public int NodesNumber;
        public double ConvergencePrecision;
        public override event ProgressDel ProgressChanged;
        public KMeansClusteringClass(int nodesNumber, double convergencePrecision, List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            NodesNumber = nodesNumber;
            ConvergencePrecision = convergencePrecision;
        }

        public override void SetOptions(ClusteringOptions opt)
        {
            NodesNumber = opt.ClustersNumber;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ClustersNumber = NodesNumber;
            return result;
        }

        protected override void Learn()
        {
            if (learningMode == LearningMode.Start)
            {
                Tuple<double[], double[]> lims = EuclideanGeometry.MinMaxDim(Items);
                InitializeNodes(lims.Item1, lims.Item2);
            }
            for (int EpochNum = 1; ; ++EpochNum)
            {
                if (EpochNum > 1 && (StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                for (int i = 1; i <= NodesNumber; ++i)
                {
                    Dictionary<KMeansNode, List<Item>> domains = CreateDomains();
                    for (int j = 0; j < Nodes.Count; ++j)
                    {
                        Nodes[j].Learn(domains[Nodes[j]]);
                    }
                }
                ProgressChanged(EpochNum);
            }
        }
        KMeansNode InitializeNode(double[] minDin, double[] maxDim)
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
            Nodes = new List<KMeansNode>();
            for (int i = 0; i < NodesNumber; ++i)
            {
                Nodes.Add(InitializeNode(minDin, maxDim));
            }
        }
    }
}
