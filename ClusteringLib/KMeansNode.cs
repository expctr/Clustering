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
    public class KMeansNode : ClusteringNode
    {
        public KMeansNode(double[] coordinates) : base(coordinates)
        {

        }
        public void Learn(List<Item> items)
        {
            if (items.Count == 0) return;
            Coordinates = EuclideanGeometry.Barycentre(Item.ToDoubleArray(items));
        }
    }
}
