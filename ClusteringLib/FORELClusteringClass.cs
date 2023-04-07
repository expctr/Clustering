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
    public class FORELClusteringClass : ClusteringClass
    {
        public double ReachabilityRadius;
        public override event ProgressDel ProgressChanged;
        public FORELClusteringClass(double reachabilityRadius, List<Item> items)
        {
            Items = new List<Item>();
            Items.AddRange(items);
            ReachabilityRadius = reachabilityRadius;
        }

        public override void SetOptions(ClusteringOptions opt)
        {
            ReachabilityRadius = opt.ReachabilityRadius;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ReachabilityRadius = ReachabilityRadius;
            return result;
        }

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
            List<List<Item>> result = new List<List<Item>>();
            bool[] used = new bool[Items.Count];
            FORELNode node;
            double grabbedElements = 0;
            while (used.Contains(false))
            {
                for (int i = 0; i < Items.Count; ++i)
                {
                    if (used[i]) continue;
                    node = new FORELNode(Items[i].GetCoordinates, ReachabilityRadius);
                    node.Grab(Items, used);
                    double[] oldCoord = node.GetCoordinates();
                    while (true)
                    {
                        if (StopFlag) return null;
                        node.Learn();
                        if (EuclideanGeometry.Distance(oldCoord, node.GetCoordinates()) == 0)
                        {
                            break;
                        }
                        node.Grab(Items, used);
                        oldCoord = node.GetCoordinates();
                    }
                    List<Item> curCluster = node.Grab_Own(Items, used);
                    result.Add(curCluster);
                    grabbedElements += curCluster.Count;
                    ProgressChanged(grabbedElements / Items.Count);
                }
            }
            return result;
        }
    }
}
