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
    class AglomerativeEdge
    {
        public AglomerativeNode Node1;
        public AglomerativeNode Node2;
        public readonly double Distance;
        public AglomerativeEdge(AglomerativeNode node1, AglomerativeNode node2,
            double distance)
        {
            Node1 = node1;
            Node2 = node2;
            Distance = distance;
        }
        public void Redirect(AglomerativeNode oldNode1, AglomerativeNode oldNode2, AglomerativeNode newNode)
        {
            if (Node1 == oldNode1 || Node1 == oldNode2)
            {
                Node1 = newNode;
            }
            if (Node2 == oldNode1 || Node2 == oldNode2)
            {
                Node2 = newNode;
            }
        }
    }
}
