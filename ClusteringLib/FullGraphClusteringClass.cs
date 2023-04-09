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
    public class FullGraphClusteringClass : IClustering
    {
        public event ProgressDel ProgressChanged;
        public event DebugDel debugEvent;

        public double MaxDistance;

        IGraphClusteringClass graphClusteringClass;

        public bool StopFlag { set { graphClusteringClass.StopFlag = value; } get { return graphClusteringClass.StopFlag; } }
        public LearningMode learningMode { set { graphClusteringClass.learningMode = value; } get { return graphClusteringClass.learningMode; } }

        public FullGraphClusteringClass(double maxDistance, List<Item> items)
        {
            graphClusteringClass = new GraphClusteringClass();

            MaxDistance = maxDistance;
            graphClusteringClass.SetItems(items); // Items = items;
        }
        public void SetOptions(ClusteringOptions opt)
        {
            MaxDistance = opt.MaxDistance;
        }
        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.MaxDistance = MaxDistance;
            return result;
        }
        public void Report(double x)
        {
            ProgressChanged(x);
        }
        public List<List<int>> SplitGraph(List<List<int>> graph)
        {
            List<Tuple<int, int, double>> edges = new List<Tuple<int, int, double>>();
            double cur4 = 0;
            double total4 = 0;
            graph.ForEach(x => total4 += x.Count);
            for (int i = 0; i < graph.Count; ++i)
            {
                for (int j = 0; j < graph[i].Count; ++j)
                {
                    if (graphClusteringClass.StopFlag) return null; // if (StopFlag) return null;
                    if (i >= graph[i][j]) continue;
                    edges.Add(new Tuple<int, int, double>(i, graph[i][j], graphClusteringClass.Distances(i, graph[i][j])));
                }
                ++cur4;
                ProgressChanged((3.0 / 6) + (1.0 / 6) * cur4 / total4);
            }
            edges.Sort((edge1, edge2) => -edge1.Item3.CompareTo(edge2.Item3));
            edges.RemoveAll(x => x.Item3 >= MaxDistance);
            List<List<int>> result = new List<List<int>>();
            foreach (var i in graph)
            {
                if (graphClusteringClass.StopFlag) return null;
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

        public void SetItems(List<Item> items)
        {
            graphClusteringClass.SetItems(items);
        }

        public List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (graphClusteringClass.GetItems() == null || graphClusteringClass.GetItems().Count == 0)
            {
                return new List<List<Item>>();
            }
            if (graphClusteringClass.GetItems().Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(graphClusteringClass.GetItems()[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            if (graphClusteringClass.GetMinimumSpanningTree() == null || graphClusteringClass.GetMinimumSpanningTree().Count == 0)
            {
                // MSTMaster = new MinimumSpanningTreeMaster();
                graphClusteringClass.SetMSTMaster(new MinimumSpanningTreeMaster());
                // MSTMaster.ProgressChanged += (double x) =>
                graphClusteringClass.GetMSTMaster().ProgressChanged += (double x) =>
                {
                    Report(x);
                };
                //minimumSpanningTree = MSTMaster.CreateMinimumSpanningTree(Items,
                //    (a, b, items) =>
                //    {
                //        if (a == b) return double.PositiveInfinity;
                //        return EuclideanGeometry.Distance(items[a].GetCoordinates,
                //            items[b].GetCoordinates);
                //    });
                graphClusteringClass.SetMinimumSpanningTree(
                    graphClusteringClass.GetMSTMaster().CreateMinimumSpanningTree(graphClusteringClass.GetItems(), // minimumSpanningTree = MSTMaster.CreateMinimumSpanningTree(Items
                        (a, b, items) =>
                        {
                            if (a == b) return double.PositiveInfinity;
                            return EuclideanGeometry.Distance(items[a].GetCoordinates,
                                items[b].GetCoordinates);
                        }));
            }
            if (StopFlag)
            {
                return new List<List<Item>>();
            }

            List<List<int>> SplittedGraph = SplitGraph(graphClusteringClass.GetMinimumSpanningTree()); // List<List<int>> SplittedGraph = SplitGraph(minimumSpanningTree);
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            List<List<int>> Componets = GetComponents(SplittedGraph);
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            List<List<Item>> result = new List<List<Item>>();
            foreach (var component in Componets)
            {
                if (StopFlag)
                {
                    return new List<List<Item>>();
                }
                result.Add(new List<Item>());
                foreach (var ind in component)
                {
                    if (StopFlag) return null;
                    result[result.Count - 1].Add(graphClusteringClass.GetItems()[ind]); // result[result.Count - 1].Add(Items[ind]);
                }
            }
            return result;
        }

        public void Stop()
        {
            graphClusteringClass.Stop();
        }

        public List<List<int>> GetComponents(List<List<int>> graph)
        {
            if (StopFlag)
            {
                return new List<List<int>>();
            }
            List<List<int>> result = new List<List<int>>();//искомый список компонент
            bool[] visited = new bool[graph.Count];//массив, содержащий информацию о посещенных вершинах
            for (int i = 0; i < graphClusteringClass.GetItems().Count; ++i) // for (int i = 0; i < Items.Count; ++i)
            {
                if (StopFlag)
                {
                    return new List<List<int>>();
                };
                if (visited[i]) continue; //если вершина посещена, то не следует снова искать ее компоненту связности//here
                List<int> curComponent = new List<int>();//текущая компонента
                List<int> curLayer = new List<int>();//текущий слой вершин обхода
                curLayer.Add(i);//первый слой состоит из вершины, с которой начинается обход
                double cur6 = 0;
                double total6 = graphClusteringClass.GetItems().Count; // double total6 = Items.Count;
                while (curLayer.Count > 0)//пока текущий слой обхода непуст
                {
                    if (StopFlag)
                    {
                        return new List<List<int>>();
                    };
                    foreach (var ind in curLayer)//относим к текущей компоненте вершины текущего слоя
                    {
                        if (StopFlag)
                        {
                            return new List<List<int>>();
                        };
                        if (visited[ind]) continue;
                        visited[ind] = true;
                        curComponent.Add(ind);
                    }
                    List<int> nextLayer = new List<int>();//список вершин следующего слоя
                    // bool[] added = new bool[Items.Count];//этот массив нужен, чтобы никакая вершина не вошла в новый слой дважды
                    bool[] added = new bool[graphClusteringClass.GetItems().Count];
                    foreach (var ind in curLayer)
                    {
                        foreach (var _ind in graph[ind])//для каждой вершины текущего слоя обходим список смежных с ней вершин
                        {
                            if (StopFlag)
                            {
                                return new List<List<int>>();
                            };
                            if (!visited[_ind] && !added[_ind])
                            {
                                nextLayer.Add(_ind);
                                added[_ind] = true;
                            }
                        }
                    }
                    curLayer = nextLayer;
                }
                result.Add(curComponent);
                cur6 += curComponent.Count;
                Report((5.0 / 6) + (1.0 / 6) * cur6 / total6);
            }
            return result;
        }
    }
}
