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
    public class MinimumSpanningTreeClusteringClass : GraphClusteringClass
    {
        public override event ProgressDel ProgressChanged;
        public int ClustersNumber;
        public MinimumSpanningTreeClusteringClass(int clustersNumber, List<Item> items)
        {
            ClustersNumber = clustersNumber;
            Items = items;
        }
        public override void SetOptions(ClusteringOptions opt)
        {
            ClustersNumber = opt.ClustersNumber;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ClustersNumber = ClustersNumber;
            return result;
        }
        public override void Report(double x)
        {
            ProgressChanged(x);
        }
        public override List<List<int>> SplitGraph(List<List<int>> graph)
        {
            List<Tuple<int, int, double>> edges = new List<Tuple<int, int, double>>();
            double cur4 = 0;
            double total4 = 0;
            graph.ForEach(x => total4 += x.Count);
            for (int i = 0; i < graph.Count; ++i)
            {
                for (int j = 0; j < graph[i].Count; ++j)
                {
                    if (StopFlag) return null;
                    if (i >= graph[i][j]) continue;
                    edges.Add(new Tuple<int, int, double>(i, graph[i][j], Distances(i, graph[i][j])));
                }
                ++cur4;
                ProgressChanged((3.0 / 6) + (1.0 / 6) * cur4 / total4);
            }
            edges.Sort((edge1, edge2) => -edge1.Item3.CompareTo(edge2.Item3));
            List<List<int>> result = new List<List<int>>();
            foreach (var i in graph)
            {
                if (StopFlag) return null;
                result.Add(new List<int>(i));
            }
            for (int i = 0; i < ClustersNumber - 1 && i < edges.Count; ++i)
            {
                if (StopFlag)
                {

                    return new List<List<int>>();
                }
                result[edges[i].Item1].Remove(edges[i].Item2);
                result[edges[i].Item2].Remove(edges[i].Item1);
            }
            return result;
        }
    }
}
