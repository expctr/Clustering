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
    interface IFORELNode
    {
        void Grab(List<Item> items, bool[] used);

        double[] GetCoordinates();

        void Learn();

        List<Item> Grab_Own(List<Item> items, bool[] used);
    }
}
