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
    public class GraphClusteringClass : IGraphClusteringClass
    {
        public object Cluster;

        List<List<int>> minimumSpanningTree = null;

        MinimumSpanningTreeMaster MSTMaster = new MinimumSpanningTreeMaster();

        public event ProgressDel ProgressChanged;

        public event DebugDel debugEvent;

        IClusteringClass clusteringClass;

        public LearningMode learningMode { set { clusteringClass.learningMode = value; } get { return clusteringClass.learningMode; } }
        public bool StopFlag { set { clusteringClass.StopFlag = value; } get { return clusteringClass.StopFlag; } }

        public GraphClusteringClass()
        {
            clusteringClass = new ClusteringClass();
        }

        public void Stop()
        {
            clusteringClass.Stop(); // StopFlag = true;
            MSTMaster.StopFlag = true;
            minimumSpanningTree = null;
        }

        public void SetItems(List<Item> items)
        {
            if (items == null)
            {
                // Items = new List<Item>();
                clusteringClass.SetItems(new List<Item>());
            }
            else
            {
                // Items = new List<Item>(items);
                clusteringClass.SetItems(new List<Item>(items));
            }
            minimumSpanningTree = null;
        }

        public List<Item> GetItems()
        {
            // return Items;
            return clusteringClass.GetItems();
        }

        public double Distances(int a, int b)
        {
            if (a == b)
            {
                return double.PositiveInfinity;
            }
            return EuclideanGeometry.Distance(clusteringClass.GetItems()[a].GetCoordinates,
                clusteringClass.GetItems()[b].GetCoordinates);
        }

        public void SetMinimumSpanningTree(List<List<int>> minimumSpanningTree)
        {
            this.minimumSpanningTree = minimumSpanningTree;
        }

        public List<List<int>> GetMinimumSpanningTree()
        {
            return minimumSpanningTree;
        }

        public void SetMSTMaster(MinimumSpanningTreeMaster minimumSpanningTreeMaster)
        {
            MSTMaster = minimumSpanningTreeMaster;
        }

        public MinimumSpanningTreeMaster GetMSTMaster()
        {
            return MSTMaster;
        }
    }
}
