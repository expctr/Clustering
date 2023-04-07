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
    public abstract class GraphClusteringClass : ClusteringClass
    {
        public object Cluster;
        List<List<int>> minimumSpanningTree = null;
        MinimumSpanningTreeMaster MSTMaster = new MinimumSpanningTreeMaster();
        public override event ProgressDel ProgressChanged;
        public override event DebugDel debugEvent;

        public abstract void Report(double x);

        public abstract List<List<int>> SplitGraph(List<List<int>> graph);

        public override void Stop()
        {
            StopFlag = true;
            MSTMaster.StopFlag = true;
            minimumSpanningTree = null;
        }

        public override void SetItems(List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            minimumSpanningTree = null;
        }

        protected double Distances(int a, int b)
        {
            if (a == b)
            {
                return double.PositiveInfinity;
            }
            return EuclideanGeometry.Distance(Items[a].GetCoordinates,
                Items[b].GetCoordinates);
        }

        public List<List<int>> GetCompenents(List<List<int>> graph)
        {
            if (StopFlag)
            {
                return new List<List<int>>();
            }
            List<List<int>> result = new List<List<int>>();//искомый список компонент
            bool[] visited = new bool[graph.Count];//массив, содержащий информацию о посещенных вершинах
            for (int i = 0; i < Items.Count; ++i)
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
                double total6 = Items.Count;
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
                    bool[] added = new bool[Items.Count];//этот массив нужен, чтобы никакая вершина не вошла в новый слой дважды
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
        }//List<List<int>> GetCompenents(List<List<int>> graph)
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
            if (minimumSpanningTree == null || minimumSpanningTree.Count == 0)
            {
                MSTMaster = new MinimumSpanningTreeMaster();
                MSTMaster.ProgressChanged += (double x) => {
                    Report(x);
                };
                minimumSpanningTree = MSTMaster.CreateMinimumSpanningTree(Items,
                    (a, b, items) => {
                        if (a == b) return double.PositiveInfinity;
                        return EuclideanGeometry.Distance(items[a].GetCoordinates,
                            items[b].GetCoordinates);
                    });
            }
            if (StopFlag)
            {
                return new List<List<Item>>();
            }

            List<List<int>> SplittedGraph = SplitGraph(minimumSpanningTree);
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            List<List<int>> Componets = GetCompenents(SplittedGraph);
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
                    result[result.Count - 1].Add(Items[ind]);
                }
            }
            return result;
        }//List<List<Item>> GetClusters()
    }
}
