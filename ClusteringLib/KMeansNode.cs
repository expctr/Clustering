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
    public class KMeansNode : IKMeansNode
    {
        IClusteringNode clusteringNode;

        public KMeansNode(double[] coordinates)
        {
            clusteringNode = new ClusteringNode(coordinates);
        }

        public bool Deflected(double ConvEps)
        {
            return clusteringNode.Deflected(ConvEps);
        }

        public double[] GetCoordinates()
        {
            return clusteringNode.GetCoordinates();
        }

        public double[] GetSavedCoordinates()
        {
            return clusteringNode.GetSavedCoordinates();
        }

        public void Learn(List<Item> items)
        {
            if (items.Count == 0) return;
            // Coordinates = EuclideanGeometry.Barycentre(Item.ToDoubleArray(items));
            clusteringNode.SetCoordinates(EuclideanGeometry.Barycentre(Item.ToDoubleArray(items)));
        }

        public void RewriteSavedCoordinates()
        {
            clusteringNode.RewriteSavedCoordinates();
        }

        public void SetCoordinates(double[] coordinates)
        {
            clusteringNode.SetCoordinates(coordinates);
        }

        public void SetSavedCoordinates(double[] savedCoordinates)
        {
            clusteringNode.SetSavedCoordinates(savedCoordinates);
        }

        public double[] _Coordinates
        {
            get
            {
                return clusteringNode._Coordinates;
            }
        }
    }
}
