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
    public class ClusteringNeuron : IClusteringNode, IClusteringNeuron
    {
        IClusteringNode clusteringNode;

        protected double LearningSpeed;

        public ClusteringNeuron(double[] coordinates, double learningSpeed)
        {
            clusteringNode = new ClusteringNode(coordinates);

            LearningSpeed = learningSpeed;
            clusteringNode.SetSavedCoordinates(null); // SavedCoordinates = null;
        }

        public ClusteringNeuron(double[] coordinates)
        {
            clusteringNode = new ClusteringNode(coordinates);
            // SavedCoordinates = null;
            clusteringNode.SetSavedCoordinates(null);
        }

        public void Learn(double[] point)
        {
            double[] offset = EuclideanGeometry.VectorSubtraction(point, clusteringNode.GetCoordinates());
            for (int i = 0; i < clusteringNode.GetCoordinates().Length; ++i)
            {
                clusteringNode.GetCoordinates()[i] = clusteringNode.GetCoordinates()[i] + LearningSpeed * offset[i];
            }
        }
        public void Learn(double[] point, double learningSpeed)
        {
            double[] offset = EuclideanGeometry.VectorSubtraction(point, clusteringNode.GetCoordinates());
            for (int i = 0; i < clusteringNode.GetCoordinates().Length; ++i)
            {
                clusteringNode.GetCoordinates()[i] = clusteringNode.GetCoordinates()[i] + learningSpeed * offset[i];
            }
        }

        public void SetCoordinates(double[] coordinates)
        {
            clusteringNode.SetCoordinates(coordinates);
        }

        public double[] GetCoordinates()
        {
            return clusteringNode.GetCoordinates();
        }

        public void SetSavedCoordinates(double[] savedCoordinates)
        {
            clusteringNode.SetSavedCoordinates(savedCoordinates);
        }

        public double[] GetSavedCoordinates()
        {
            return clusteringNode.GetSavedCoordinates();
        }

        public bool Deflected(double ConvEps)
        {
            return clusteringNode.Deflected(ConvEps);
        }

        public void RewriteSavedCoordinates()
        {
            clusteringNode.RewriteSavedCoordinates();
        }
    }
}