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
    interface IClusteringNodeClass
    {
        LearningMode learningMode { set; get; }

        bool StopFlag { set; get; }

        void SetItems(List<Item> items);

        List<Item> GetItems();

        bool Converged<Node>(double ConvEps, List<Node> Nodes) where Node : IClusteringNode;

        int Winner<Node>(Item item, out double distance, List<Node> Nodes) where Node : IClusteringNode;

        int Winner<Node>(Item item, List<Node> Nodes) where Node : IClusteringNode;

        Dictionary<Node, List<Item>> CreateDomains<Node>(List<Node> Nodes) where Node : IClusteringNode;

        void Stop();
    }
}
