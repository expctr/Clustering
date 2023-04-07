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
    public abstract class ClusteringNodeClass<Node> : ClusteringClass where Node : ClusteringNode
    {
        protected List<Node> Nodes = new List<Node>();
        protected int Winner(Item item)
        {
            double[] itemCoordinates = item.GetCoordinates;
            double MinDistance = EuclideanGeometry.Distance(Nodes[0].GetCoordinates(), itemCoordinates);
            int result = 0;
            for (int i = 1; i < Nodes.Count; ++i)
            {
                double CurDistance = EuclideanGeometry.Distance(Nodes[i].GetCoordinates(), itemCoordinates);
                if (CurDistance < MinDistance)
                {
                    MinDistance = CurDistance;
                    result = i;
                }
            }
            return result;
        }
        protected int Winner(Item item, out double distance)
        {
            double[] itemCoordinates = item.GetCoordinates;
            double MinDistance = EuclideanGeometry.Distance(Nodes[0].GetCoordinates(), itemCoordinates);
            int result = 0;
            for (int i = 1; i < Nodes.Count; ++i)
            {
                double CurDistance = EuclideanGeometry.Distance(Nodes[i].GetCoordinates(), itemCoordinates);
                if (CurDistance < MinDistance)
                {
                    MinDistance = CurDistance;
                    result = i;
                }
            }
            distance = MinDistance;
            return result;
        }
        protected abstract void Learn();
        public override List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
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
            foreach (var item in Items)
            {
                Clusters[Winner(item)].Add(item);
            }
            Clusters.RemoveAll(cluster => cluster.Count == 0);
            return Clusters;
        }
        public Dictionary<Node, List<Item>> CreateDomains()
        {
            Dictionary<Node, List<Item>> result = new Dictionary<Node, List<Item>>();
            foreach (var node in Nodes)
            {
                result[node] = new List<Item>();
            }
            foreach (var item in Items)
            {
                result[Nodes[Winner(item)]].Add(item);
            }
            return result;
        }
        public List<double[]> GetNodesCoordinates()
        {
            List<double[]> result = new List<double[]>();
            foreach (var neuron in Nodes)
            {
                result.Add(neuron.GetCoordinates());
            }
            return result;
        }
        protected bool Converged(double ConvEps)
        {
            for (int i = 0; i < Nodes.Count; ++i)
            {
                if (Nodes[i].Deflected(ConvEps))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
