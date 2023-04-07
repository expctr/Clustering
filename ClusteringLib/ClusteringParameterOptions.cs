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
    public class ClusteringParameterOptions
    {
        public bool[] ChosenClusterizationParameter;
        public double[] DimensionalWeights;
        public bool Normalize;
    }
}
