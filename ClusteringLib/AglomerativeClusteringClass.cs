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
    public class AglomerativeClusteringClass : IClustering
    {
        public event DebugDel debugEvent;
        List<Item> Items;
        public double DetalizationCoef;
        public ClusteringOptions.AglomerativeClusteringDistance ACDistance;
        MinimumSpanningTreeMaster MSTMaster;
        AglomerativeNode DendraRoot = null;
        List<List<AglomerativeNode>> Levels = null;
        public bool StopFlag { set; get; }
        public void Stop()
        {
            StopFlag = true;
        }
        public event ProgressDel ProgressChanged;
        //
        double CDpart = 25.0 / 30, CMSTpart = 1.0 / 4,
            CELpart = 1.0 / 4, SELpart = 1.0 / 4,
            CD1part = 1.0 / 4;
        double SDpart = 4.0 / 30;
        double ECpart = 1.0 / 30,
            MInANpart = 1.0 / 2, EANpart = 1.0 / 2;
        double CurProgress = 0;
        void Report(double x)
        {
            ProgressChanged(CurProgress + x);
        }
        //
        public LearningMode learningMode { set; get; }//костыль
        public AglomerativeClusteringClass(double detalizationCoef,
            ClusteringOptions.AglomerativeClusteringDistance _ACDistance,
            List<Item> items)
        {
            DetalizationCoef = detalizationCoef;
            ACDistance = _ACDistance;
            Items = new List<Item>();
            Items.AddRange(items);
        }

        public void SetOptions(ClusteringOptions opt)
        {
            if (ACDistance != opt.ACDistance)
            {
                DendraRoot = null;
                Levels = null;
            }
            DetalizationCoef = opt.DetalizationCoef;
            ACDistance = opt.ACDistance;
        }
        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.DetalizationCoef = DetalizationCoef;
            result.ACDistance = ACDistance;
            return result;
        }

        public void SetItems(List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            DendraRoot = null;
            Levels = null;
        }
        Tuple<AglomerativeNode, AglomerativeNode> NodeKey(int i, int j, List<AglomerativeNode> nodes)
        {
            return new Tuple<AglomerativeNode, AglomerativeNode>(nodes[i], nodes[j]);
        }
        Tuple<AglomerativeNode, AglomerativeNode> NodeKey(AglomerativeNode first, AglomerativeNode second)
        {
            return new Tuple<AglomerativeNode, AglomerativeNode>(first, second);
        }

        List<AglomerativeNode> PrimaryNodes()
        {
            List<AglomerativeNode> result = new List<AglomerativeNode>();
            for (int i = 0; i < Items.Count; ++i)
            {
                result.Add(new AglomerativeNode(i, Items, i.ToString()));
            }
            return result;
        }

        Dictionary<AglomerativeNode, List<AglomerativeNode>>
            CreateMinimumSpanningTree(out List<AglomerativeNode> _primaryNodes)
        {
            MSTMaster = new MinimumSpanningTreeMaster();
            MSTMaster.ProgressChanged += (double x) =>
            {
                Report(x * CMSTpart * CDpart);
            };
            List<List<int>> mst_ind = MSTMaster.CreateMinimumSpanningTree(Items,
                (a, b, items) => {
                    if (a == b) return double.PositiveInfinity;
                    return EuclideanGeometry.Distance(items[a].GetCoordinates,
                        items[b].GetCoordinates);
                });
            _primaryNodes = PrimaryNodes();
            Dictionary<AglomerativeNode, List<AglomerativeNode>> result =
                new Dictionary<AglomerativeNode, List<AglomerativeNode>>();
            for (int i = 0; i < mst_ind.Count; ++i)//преобразование списка смежности из списка индексов в список ссылок
            {
                result[_primaryNodes[i]] = new List<AglomerativeNode>();
                for (int j = 0; j < mst_ind[i].Count; ++j)
                {
                    result[_primaryNodes[i]].Add(_primaryNodes[mst_ind[i][j]]);
                }
            }
            return result;
        }

        AglomerativeNode CreateDendrogramm_SingleLinkDistance()
        {
            List<AglomerativeNode> primaryNodes;
            Dictionary<AglomerativeNode, List<AglomerativeNode>> mst =
                CreateMinimumSpanningTree(out primaryNodes);//минимальное остовное дерево изначальных агломеративных нод
            CurProgress += CMSTpart * CDpart;
            List<AglomerativeEdge> edges =
                new List<AglomerativeEdge>();//список ребер минимального остовного дерева (ребра не должны повторяться)
            //сформируем edjes обходом в ширину
            Dictionary<AglomerativeNode, bool> visited =
                new Dictionary<AglomerativeNode, bool>();//словарь посещенности
            foreach (var node in primaryNodes)
            {
                visited[node] = false;
            }
            List<AglomerativeNode> curLayer = new List<AglomerativeNode>();
            curLayer.Add(primaryNodes[0]);
            List<AglomerativeNode> nextLayer;
            double buildingEdgesListCurProgress = 0;
            double buildingEdgesListTotalProgress = Items.Count - 1;
            while (curLayer.Count != 0)//обход в ширину
            {
                nextLayer = new List<AglomerativeNode>();
                foreach (var nodeI in curLayer)//получаем ребра с помощью текущего слоя
                {
                    foreach (var nodeJ in mst[nodeI])//обходим смежные ноды с текущей нодой текущего слоя
                    {
                        if (StopFlag)
                        {
                            return null;
                        }
                        if (visited[nodeJ]) continue;
                        edges.Add(new AglomerativeEdge(
                            nodeI, nodeJ, EuclideanGeometry.Distance(nodeI.GetItems[0].GetCoordinates,
                            nodeJ.GetItems[0].GetCoordinates)));
                        nextLayer.Add(nodeJ);
                    }
                    visited[nodeI] = true;
                }
                Report(CELpart * CDpart * ((buildingEdgesListCurProgress += curLayer.Count) / buildingEdgesListTotalProgress));
                curLayer = nextLayer;
            }
            CurProgress += CELpart * CDpart;
            edges.Sort((edge1, edge2) => -edge1.Distance.CompareTo(edge2.Distance));
            Report(SELpart * CDpart);
            CurProgress += SELpart * CDpart;
            double CD1_CurProgress = 0;
            double CD1_TotalProgress = edges.Count;
            while (edges.Count > 0)//процесс построения дендрограммы
            {
                if (StopFlag)
                {
                    return null;
                }
                AglomerativeEdge curEdge =
                    edges[edges.Count - 1];//текущее кратчайшее ребро
                AglomerativeNode node1 = curEdge.Node1;
                AglomerativeNode node2 = curEdge.Node2;
                AglomerativeNode newNode = new AglomerativeNode(node1, node2,
                    curEdge.Distance, Items);
                edges.RemoveAt(edges.Count - 1);
                for (int i = 0; i < edges.Count; ++i)//перенаправление ребер в списке
                {
                    if (StopFlag)
                    {
                        return null;
                    }
                    edges[i].Redirect(node1, node2, newNode);
                }
                //Стянем две вершины в одну
                List<AglomerativeNode> newNeighbours =
                    new List<AglomerativeNode>();
                newNeighbours.AddRange(mst[node1]);
                newNeighbours.AddRange(mst[node2]);
                newNeighbours.Remove(node1);
                newNeighbours.Remove(node2);
                mst.Remove(node1);
                mst.Remove(node2);
                mst[newNode] = newNeighbours;
                Report(CD1part * CDpart * (++CD1_CurProgress / CD1_TotalProgress));
            }
            var keys = mst.Keys;
            return keys.ElementAt(0);
        }//AglomerativeNode CreateDendrogramm_SingleLinkDistance()

        double CentreDistance(AglomerativeNode node1, AglomerativeNode node2)
        {
            return EuclideanGeometry.Distance(node1.GetCentre, node2.GetCentre);
        }

        double WardDistance(AglomerativeNode node1, AglomerativeNode node2)
        {
            return (node1.Count * node2.Count / (node1.Count + node1.Count)) *
                CentreDistance(node1, node2);
        }

        delegate double AglomerativeDistance(AglomerativeNode node1,
            AglomerativeNode node2);
        AglomerativeNode CreateDendrogramm(AglomerativeDistance distance)
        {
            List<AglomerativeNode> _ANNodes = PrimaryNodes();//инициализация списка агломеративных нод
            MSTMaster = new MinimumSpanningTreeMaster();
            double CD_CurProgress = 0;
            double CD_TotalProgress = 2 * Items.Count - 1;
            while (_ANNodes.Count > 1)//процесс слияния агломеративных нод
            {
                if (StopFlag)
                {
                    return null;
                }
                MSTMaster.Clear();
                MSTMaster.ProgressChanged += (double x) => { };
                //Найдем ближайшие агломеративные ноды для слияния
                List<List<int>> mst = MSTMaster.CreateMinimumSpanningTree(_ANNodes,
                    (a, b, nodes) => {
                        if (a == b) return double.PositiveInfinity;
                        return distance(nodes[a], nodes[b]);
                    });//минимальное ребро гарантированно входит в минимальное остовное дерево
                int targetI = -1, targetJ = -1;
                double minDist = double.PositiveInfinity;
                for (int i = 0; i < mst.Count; ++i)
                {
                    foreach (var ind in mst[i])
                    {
                        if (StopFlag)
                        {
                            return null;
                        }
                        double curDist = distance(_ANNodes[i], _ANNodes[ind]);
                        if (curDist < minDist)
                        {
                            minDist = curDist;
                            targetI = i;
                            targetJ = ind;
                        }
                    }
                }
                AglomerativeNode newNode = new AglomerativeNode(
                    _ANNodes[targetI], _ANNodes[targetJ], CentreDistance(
                        _ANNodes[targetI], _ANNodes[targetJ]), Items);
                AglomerativeNode nodeI = _ANNodes[targetI];
                AglomerativeNode nodeJ = _ANNodes[targetJ];
                _ANNodes.Remove(nodeI);
                _ANNodes.Remove(nodeJ);
                _ANNodes.Add(newNode);
                Report(CDpart * (++CD_CurProgress / CD_TotalProgress));
            }
            return _ANNodes[0];
        }

        double SD_CurProgress;
        double SD_TotalProgress;
        public void SplitDendrogramm(AglomerativeNode node, List<List<AglomerativeNode>> levels)//распределяем ноды дендрограммы на уровни
        {
            if (StopFlag)
            {
                return;
            }
            if (node == null)
            {
                return;
            }
            levels[node.GetLevel].Add(node);
            Report(SDpart * (++SD_CurProgress / SD_TotalProgress));
            SplitDendrogramm(node.SubNode1, levels);
            SplitDendrogramm(node.SubNode2, levels);
        }
        public List<List<Item>> GetClusters()
        {
            StopFlag = false;
            CurProgress = 0;
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
            if (DendraRoot == null)
            {
                switch (ACDistance)
                {
                    case ClusteringOptions.AglomerativeClusteringDistance.SingleLink:
                        DendraRoot = CreateDendrogramm_SingleLinkDistance();
                        break;
                    case ClusteringOptions.AglomerativeClusteringDistance.CentreDistance:
                        DendraRoot = CreateDendrogramm(CentreDistance);
                        break;
                    case ClusteringOptions.AglomerativeClusteringDistance.WardDistance:
                        DendraRoot = CreateDendrogramm(WardDistance);
                        break;
                }
            }
            CurProgress = CDpart;
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            if (Levels == null)
            {
                Levels = new List<List<AglomerativeNode>>();
                for (int i = 0; i <= DendraRoot.GetLevel; ++i)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    Levels.Add(new List<AglomerativeNode>());
                }
                SD_CurProgress = 0;
                SD_TotalProgress = 2 * Items.Count - 1;
                SplitDendrogramm(DendraRoot, Levels);
            }
            else
            {
                for (int i = 1; i < Levels.Count; ++i)//разблокируем все ноды
                {
                    for (int j = 0; j < Levels[i].Count; ++j)
                    {
                        if (StopFlag)
                        {
                            return new List<List<Item>>();
                        }
                        Levels[i][j].Locked = false;
                    }
                }
            }
            CurProgress += SDpart;
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            double EC_CurProgress = 0;
            double EC_TotalProgress = 2 * (Levels.Count - 1);
            for (int i = 1; i < Levels.Count; ++i)//помечаем ноды со слишком большим собственным расстоянием
            {
                for (int j = 0; j < Levels[i].Count; ++j)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    AglomerativeNode curNode = Levels[i][j];
                    if (curNode.GetDistance > DetalizationCoef)
                    {
                        curNode.Locked = true;
                    }
                }
                Report(MInANpart * ECpart * (++EC_CurProgress / EC_TotalProgress));
            }
            for (int i = 1; i < Levels.Count; ++i)//помечаем ноды, у которых помечен хотя бы 1 потомок
            {
                for (int j = 0; j < Levels[i].Count; ++j)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    AglomerativeNode curNode = Levels[i][j];
                    if (curNode.SubNode1.Locked || curNode.SubNode2.Locked)
                    {
                        curNode.Locked = true;
                    }
                }
                Report(MInANpart * ECpart * (++EC_CurProgress / EC_TotalProgress));
            }
            CurProgress += MInANpart * ECpart;
            List<AglomerativeNode> CompletedNodes = new List<AglomerativeNode>();
            if (!Levels[Levels.Count - 1][0].Locked)
            {
                CompletedNodes.Add(Levels[Levels.Count - 1][0]);
            }
            double EAN_CurProgress = 0;
            double EAN_TotalProgress = Levels.Count - 1;
            for (int i = 1; i < Levels.Count; ++i)//извлекаем непомеченные ноды, они задают искомую кластеризацию
            {
                if (StopFlag)
                {
                    return new List<List<Item>>();
                }
                for (int j = 0; j < Levels[i].Count; ++j)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    AglomerativeNode curNode = Levels[i][j];
                    if (!curNode.Locked) continue;
                    if (!curNode.SubNode1.Locked) CompletedNodes.Add(curNode.SubNode1);
                    if (!curNode.SubNode2.Locked) CompletedNodes.Add(curNode.SubNode2);
                }
                Report(EANpart * ECpart * (++EAN_CurProgress / EAN_TotalProgress));
            }
            List<List<Item>> result = new List<List<Item>>();
            foreach (var node in CompletedNodes)
            {
                if (StopFlag)
                {
                    return new List<List<Item>>();
                }
                result.Add(node.GetItems);
            }
            return result;
        }
    }
}
