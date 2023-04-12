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
    public class FORELClusteringClass : IClustering
    {
        public double ReachabilityRadius;

        public event ProgressDel ProgressChanged;
        public event DebugDel debugEvent;

        private IClusteringClass clusteringClass;

        public bool StopFlag { set { clusteringClass.StopFlag = value; } get { return clusteringClass.StopFlag; } }
        public LearningMode learningMode { set { clusteringClass.learningMode = value; } get { return clusteringClass.learningMode; } }

        public FORELClusteringClass(double reachabilityRadius, List<Item> items)
        {
            clusteringClass = new ClusteringClass();

            clusteringClass.SetItems(new List<Item>()); // Items = new List<Item>();
            clusteringClass.GetItems().AddRange(items); // Items.AddRange(items);
            ReachabilityRadius = reachabilityRadius;
        }

        public void SetOptions(ClusteringOptions opt)
        {
            ReachabilityRadius = opt.ReachabilityRadius;
        }
        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ReachabilityRadius = ReachabilityRadius;
            return result;
        }

        public List<List<Item>> GetClusters()
        {
            clusteringClass.StopFlag = false; // StopFlag = false;
            if (clusteringClass.GetItems() == null || clusteringClass.GetItems().Count == 0) // if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (clusteringClass.GetItems().Count == 1) // if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(clusteringClass.GetItems()[0]); // cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            List<List<Item>> result = new List<List<Item>>();
            bool[] used = new bool[clusteringClass.GetItems().Count]; // bool[] used = new bool[Items.Count];
            IFORELNode node;
            double grabbedElements = 0;
            while (used.Contains(false))
            {
                for (int i = 0; i < clusteringClass.GetItems().Count; ++i) // for (int i = 0; i < Items.Count; ++i)
                {
                    if (used[i]) continue;
                    node = new FORELNode(clusteringClass.GetItems()[i].GetCoordinates, ReachabilityRadius); // node = new FORELNode(Items[i].GetCoordinates, ReachabilityRadius);
                    node.Grab(clusteringClass.GetItems(), used); // node.Grab(Items, used);
                    double[] oldCoord = node.GetCoordinates();
                    while (true)
                    {
                        if (clusteringClass.StopFlag) return null; // if (StopFlag) return null;
                        node.Learn();
                        if (EuclideanGeometry.Distance(oldCoord, node.GetCoordinates()) == 0)
                        {
                            break;
                        }
                        node.Grab(clusteringClass.GetItems(), used); // node.Grab(Items, used);
                        oldCoord = node.GetCoordinates();
                    }
                    List<Item> curCluster = node.Grab_Own(clusteringClass.GetItems(), used); // List<Item> curCluster = node.Grab_Own(Items, used);
                    result.Add(curCluster);
                    grabbedElements += curCluster.Count;
                    ProgressChanged(grabbedElements / clusteringClass.GetItems().Count); // ProgressChanged(grabbedElements / Items.Count);
                }
            }
            return result;
        }

        public void SetItems(List<Item> items)
        {
            clusteringClass.SetItems(items);
        }

        public void Stop()
        {
            clusteringClass.Stop();
        }
    }
}
