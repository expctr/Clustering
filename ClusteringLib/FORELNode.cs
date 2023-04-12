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
    public class FORELNode : IFORELNode
    {
        IClusteringNode clusteringNode;

        double Range;

        List<Item> Domain = new List<Item>();

        public FORELNode(double[] coordinates, double range)
        {
            clusteringNode = new ClusteringNode(coordinates);
            Range = range;
        }
        public void Grab(List<Item> items, bool[] used)
        {
            Domain = new List<Item>();
            for (int i = 0; i < items.Count; ++i)
            {
                if (used[i]) continue;
                if (EuclideanGeometry.Distance(items[i].GetCoordinates, clusteringNode.GetCoordinates()) <= Range)
                {
                    Domain.Add(items[i]);
                }
            }
        }
        public List<Item> Grab_Own(List<Item> items, bool[] used)
        {
            List<int> DomainInd = new List<int>();
            for (int i = 0; i < items.Count; ++i)
            {
                if (used[i]) continue;
                if (EuclideanGeometry.Distance(items[i].GetCoordinates, clusteringNode.GetCoordinates()) <= Range)
                {
                    DomainInd.Add(i);
                }
            }
            foreach (var ind in DomainInd)
            {
                used[ind] = true;
            }
            List<Item> result = new List<Item>();
            foreach (var ind in DomainInd)
            {
                result.Add(items[ind]);
            }
            return result;
        }
        public void Learn()
        {
            // Coordinates = EuclideanGeometry.Barycentre(Item.ToDoubleArray(Domain));
            clusteringNode.SetCoordinates(EuclideanGeometry.Barycentre(Item.ToDoubleArray(Domain)));
        }

        public double[] GetCoordinates()
        {
            return clusteringNode.GetCoordinates();
        }
    }
}
