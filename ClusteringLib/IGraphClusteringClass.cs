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
    interface IGraphClusteringClass
    {
        LearningMode learningMode { set; get; }

        void SetItems(List<Item> items);

        List<Item> GetItems();

        bool StopFlag { set; get; }

        double Distances(int a, int b);

        void Stop();

        void SetMinimumSpanningTree(List<List<int>> minimumSpanningTree);

        List<List<int>> GetMinimumSpanningTree();

        void SetMSTMaster(MinimumSpanningTreeMaster minimumSpanningTreeMaster);

        MinimumSpanningTreeMaster GetMSTMaster();
    }
}
