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
    public class DBSCANClusteringClass : IClustering
    {
        public double ReachabilityRadius;

        public int Threshold;

        public event DebugDel debugEvent;
        
        double CGpart = 35.0 / 40;

        double SRpart = 4.0 / 40;

        double CCpart = 1.0 / 40;

        double CurProgress = 0;

        IClusteringClass clusteringClass;

        public bool StopFlag { set { clusteringClass.StopFlag = value; } get { return clusteringClass.StopFlag; } }
        public LearningMode learningMode { set { clusteringClass.learningMode = value; } get { return clusteringClass.learningMode; } }

        void Report(double x)
        {
            ProgressChanged(x + CurProgress);
        }
        //
        public DBSCANClusteringClass(double reachabilityRadius, int threshold, List<Item> items)
        {
            clusteringClass = new ClusteringClass();
            ReachabilityRadius = reachabilityRadius;
            Threshold = threshold;
            clusteringClass.SetItems(items); // Items = items;
        }

        public void SetOptions(ClusteringOptions opt)
        {
            ReachabilityRadius = opt.ReachabilityRadius;
            Threshold = opt.Threshold;
        }
        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ReachabilityRadius = ReachabilityRadius;
            result.Threshold = Threshold;
            return result;
        }

        public event ProgressDel ProgressChanged;
        //public LearningMode learningMode { set; get; }
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
            // bool[] visited = new bool[Items.Count];//информация о посещенных вершинах
            bool[] visited = new bool[clusteringClass.GetItems().Count];
            // List<int>[] graph = new List<int>[Items.Count];//список смежности
            List<int>[] graph = new List<int>[clusteringClass.GetItems().Count];
            for (int i = 0; i < graph.Length; ++i)
            {
                graph[i] = new List<int>();
            }
            //int total = Items.Count * (Items.Count + 1) / 2;
            //double cur = 0;
            double CG_CurProgress = 0;
            double CG_TotalProgress = clusteringClass.GetItems().Count * clusteringClass.GetItems().Count;
            for (int i = 0; i < clusteringClass.GetItems().Count; ++i)//построение графа
            {
                for (int j = 0; j < clusteringClass.GetItems().Count; ++j)
                {
                    Report(CGpart * (++CG_CurProgress / CG_TotalProgress));
                    if (j > i) continue;
                    if (EuclideanGeometry.Distance(clusteringClass.GetItems()[i].GetCoordinates, clusteringClass.GetItems()[j].GetCoordinates)
                        <= ReachabilityRadius)
                    {
                        if (clusteringClass.StopFlag) return null;
                        graph[i].Add(j);
                        graph[j].Add(i);
                    }
                }
            }
            CurProgress = CGpart;
            double SR_CurProgress = 0;
            double SR_TotalProgress = clusteringClass.GetItems().Count;
            bool[] IsRoot = new bool[clusteringClass.GetItems().Count];
            for (int i = 0; i < clusteringClass.GetItems().Count; ++i)//выявление корневых вершин
            {
                if (graph[i].Count >= Threshold)
                {
                    if (clusteringClass.StopFlag) return null;
                    IsRoot[i] = true;
                }
                Report(SRpart * (++SR_CurProgress / SR_TotalProgress));
            }
            CurProgress += SRpart;
            double CC_CurProgress = 0;
            double CC_TotalProgress = clusteringClass.GetItems().Count;
            for (int i = 0; i < clusteringClass.GetItems().Count; ++i)//
            {
                if (visited[i]) continue;
                if (!IsRoot[i]) continue;
                List<int> curCluster = new List<int>();
                List<int> curLayer = new List<int>();
                curLayer.Add(i);
                while (curLayer.Count > 0)//формирование кластера как компоненту связности через обход в глубину
                {
                    foreach (var ind in curLayer)//извлечение вершин из текущего слоя обхода для текущего кластера
                    {
                        if (clusteringClass.StopFlag) return new List<List<Item>>();
                        if (visited[ind]) continue;
                        visited[ind] = true;
                        curCluster.Add(ind);
                        Report(CCpart * (++CC_CurProgress / CC_TotalProgress));
                    }
                    List<int> nextLayer = new List<int>();
                    bool[] added = new bool[clusteringClass.GetItems().Count];
                    foreach (var ind in curLayer)//формирование следующего слоя обхода
                    {
                        if (IsRoot[ind])
                        {
                            foreach (var _ind in graph[ind])
                            {
                                if (clusteringClass.StopFlag) return null;
                                if (!visited[_ind] && !added[_ind])
                                {
                                    nextLayer.Add(_ind);
                                    added[_ind] = true;
                                }
                            }
                        }
                    }
                    curLayer = nextLayer;
                }
                List<Item> _curCluster = new List<Item>();
                foreach (var ind in curCluster)
                {
                    if (clusteringClass.StopFlag) return new List<List<Item>>();
                    _curCluster.Add(clusteringClass.GetItems()[ind]);
                }
                result.Add(_curCluster);
            }
            for (int i = 0; i < clusteringClass.GetItems().Count; ++i)//формирование одноэлементных кластеров шума
            {
                if (!visited[i])
                {
                    if (clusteringClass.StopFlag) return new List<List<Item>>();
                    result.Add(new List<Item>(new Item[] { clusteringClass.GetItems()[i] }));
                    Report(CCpart * (++CC_CurProgress / CC_TotalProgress));
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
