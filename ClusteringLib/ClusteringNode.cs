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
    public class ClusteringNode : IClusteringNode
    {
        protected double[] Coordinates;

        protected double[] SavedCoordinates;
        public ClusteringNode(double[] coordinates)
        {
            Coordinates = (double[])coordinates.Clone();
            SavedCoordinates = null;
        }
        public void RewriteSavedCoordinates()
        {
            SavedCoordinates = (double[])Coordinates.Clone();
        }

        public bool Deflected(double ConvEps)
        {
            if (SavedCoordinates == null) return true;
            return EuclideanGeometry.Distance(Coordinates, SavedCoordinates) > ConvEps;
        }

        public double[] GetCoordinates()
        {
            double[] result = new double[Coordinates.Length];
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                result[i] = Coordinates[i];
            }
            return result;
        }

        public double[] _Coordinates
        {
            get
            {
                return Coordinates;
            }
        }

        public void SetCoordinates(double[] coordinates)
        {
            Coordinates = coordinates;
        }

        public void SetSavedCoordinates(double[] savedCoordinates)
        {
            SavedCoordinates = savedCoordinates;
        }

        public double[] GetSavedCoordinates()
        {
            double[] result = new double[SavedCoordinates.Length];
            for (int i = 0; i < SavedCoordinates.Length; ++i)
            {
                result[i] = SavedCoordinates[i];
            }
            return result;
        }
    }
}