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
    public class MinimumSpanningTreeMaster
    {
        public bool StopFlag;
        public event ProgressDel ProgressChanged;
        public delegate double distanceDel<T>(int a, int b, List<T> set);
        public void Clear()
        {
            ProgressChanged = null;
        }
        public List<List<int>> CreateMinimumSpanningTree<T>(List<T> set,
            distanceDel<T> distance)//построение минимального остовного дерева
        {
            bool[] visited = new bool[set.Count];
            Tuple<double, int>[] table = new Tuple<double, int>[set.Count()];
            table[0] = new Tuple<double, int>(0, -1);
            for (int i = 1; i < table.Length; ++i)//инициализация таблицы
            {
                if (StopFlag) return new List<List<int>>();
                table[i] = new Tuple<double, int>(double.PositiveInfinity, -1);
            }
            int FindInd()
            {
                int resultInd = -1;
                for (int i = 0; i < visited.Length; ++i)
                {
                    if (!visited[i])
                    {
                        resultInd = i;
                        break;
                    }
                }
                if (resultInd == -1)
                {
                    return -1;
                }
                double resultLambda = table[resultInd].Item1;
                for (int i = 0; i < table.Length; ++i)
                {
                    if (visited[i]) continue;
                    if (table[i].Item1 < resultLambda)
                    {
                        resultInd = i;
                        resultLambda = table[i].Item1;
                    }
                }
                return resultInd;
            }
            int curInd;
            double buildingTableCurProgress = 0;
            double buildingTableTotalProgress = set.Count;
            while (-1 != (curInd = FindInd()))//заполнение таблицы
            {
                for (int i = 0; i < table.Length; ++i)
                {
                    if (StopFlag) return new List<List<int>>();
                    if (visited[i]) continue;
                    double curDist = distance(curInd, i, set);
                    if (table[i].Item1 > curDist)
                    {
                        table[i] = new Tuple<double, int>(curDist, curInd);
                    }
                }
                visited[curInd] = true;
                ProgressChanged(++buildingTableCurProgress / buildingTableTotalProgress);
            }
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < table.Length; ++i)
            {
                if (StopFlag) return new List<List<int>>();
                result.Add(new List<int>());
            }
            for (int i = 1; i < table.Length; ++i)//извлечение ответа из таблицы
            {
                if (StopFlag) return new List<List<int>>();
                result[i].Add(table[i].Item2);
                result[table[i].Item2].Add(i);
            }
            return result;
        }
    }
}
