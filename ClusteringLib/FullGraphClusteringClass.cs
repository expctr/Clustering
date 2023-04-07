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
    public class FullGraphClusteringClass : GraphClusteringClass
    {
        public override event ProgressDel ProgressChanged;
        public double MaxDistance;
        public FullGraphClusteringClass(double maxDistance, List<Item> items)
        {
            MaxDistance = maxDistance;
            Items = items;
        }
        public override void SetOptions(ClusteringOptions opt)
        {
            MaxDistance = opt.MaxDistance;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.MaxDistance = MaxDistance;
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
            edges.RemoveAll(x => x.Item3 >= MaxDistance);
            List<List<int>> result = new List<List<int>>();
            foreach (var i in graph)
            {
                if (StopFlag) return null;
                result.Add(new List<int>());
            }
            double cur = 0;
            double total = edges.Count;
            foreach (var edge in edges)
            {
                //if (edge.Item3 <= MaxDistance) continue;
                result[edge.Item1].Add(edge.Item2);
                result[edge.Item2].Add(edge.Item1);
                ++cur;
                ProgressChanged((4.0 / 6) + (1.0 / 6) * cur / total);
            }
            return result;
        }
    }
}
