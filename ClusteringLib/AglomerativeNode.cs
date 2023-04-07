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
    public class AglomerativeNode
    {
        public bool Locked = false;
        bool Completed;
        int Level;
        List<int> Indexes;
        double[] Centre;
        public AglomerativeNode SubNode1, SubNode2;
        double Distance;
        List<Item> Items;
        public string ID;
        public AglomerativeNode(int ind, List<Item> items, string _ID)
        {
            Completed = false;
            Level = 0;
            Indexes = new List<int>(new int[] { ind });
            Centre = items[ind].GetCoordinates;
            SubNode1 = null;
            SubNode2 = null;
            Distance = 0;
            Items = items;
            ID = _ID;
        }
        public AglomerativeNode(AglomerativeNode first, AglomerativeNode second, double distance, List<Item> items)
        {
            Completed = false;
            Level = first.GetLevel > second.GetLevel ? first.GetLevel + 1 : second.GetLevel + 1;
            Indexes = new List<int>();
            Indexes.AddRange(first.GetIndexes);
            Indexes.AddRange(second.GetIndexes);
            Centre = EuclideanGeometry.Barycentre(first.GetCentre, first.Weight, second.GetCentre,
                second.Weight);
            SubNode1 = first;
            SubNode2 = second;
            Distance = distance;
            Items = items;
            ID = $"{first.ID} {second.ID}";
        }
        public static double Distance_SingleLink(AglomerativeNode node1, AglomerativeNode node2)
        {
            return EuclideanGeometry.Distance_SingleLink(node1.GetCoordinates(), node2.GetCoordinates());
        }
        public List<double[]> GetCoordinates()
        {
            List<double[]> result = new List<double[]>();
            foreach (int ind in Indexes)
            {
                result.Add(Items[ind].GetCoordinates);
            }
            return result;
        }
        public int GetLevel
        {
            get
            {
                return Level;
            }
        }
        public double[] GetCentre
        {
            get
            {
                return (double[])Centre.Clone();
            }
        }
        public int Weight
        {
            get
            {
                return Indexes.Count;
            }
        }
        public List<int> GetIndexes
        {
            get
            {
                List<int> result = new List<int>();
                result.AddRange(Indexes);
                return result;
            }
        }
        public bool IsCompleted
        {
            get
            {
                return Completed;
            }
        }
        public void Complete()
        {
            Completed = true;
        }
        public double GetDistance
        {
            get
            {
                return Distance;
            }
        }
        public List<Item> GetItems
        {
            get
            {
                List<Item> result = new List<Item>();
                foreach (var ind in Indexes)
                {
                    result.Add(Items[ind]);
                }
                return result;
            }
        }
        public Item this[int ind]
        {
            get
            {
                return Items[ind];
            }
        }
        public int Count
        {
            get
            {
                return Indexes.Count;
            }
        }
    }
}
