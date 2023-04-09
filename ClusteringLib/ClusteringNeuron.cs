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
    public class ClusteringNeuron : ClusteringNode, IClusteringNode
    {
        protected double LearningSpeed;
        public ClusteringNeuron(double[] coordinates, double learningSpeed) : base(coordinates)
        {
            LearningSpeed = learningSpeed;
            SavedCoordinates = null;
        }
        public ClusteringNeuron(double[] coordinates) : base(coordinates)
        {
            SavedCoordinates = null;
        }
        public void Learn(double[] point)
        {
            double[] offset = EuclideanGeometry.VectorSubtraction(point, Coordinates);
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] = Coordinates[i] + LearningSpeed * offset[i];
            }
        }
        public void Learn(double[] point, double learningSpeed)
        {
            double[] offset = EuclideanGeometry.VectorSubtraction(point, Coordinates);
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] = Coordinates[i] + learningSpeed * offset[i];
            }
        }
    }
}