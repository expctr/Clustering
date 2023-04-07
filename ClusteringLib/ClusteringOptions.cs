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
    public class ClusteringOptions
    {
        public double MaxDistance;
        public double LearningSpeed1;
        public double LearningSpeed2;
        public double ConvergencePrecision;
        public bool TimeLimitActivated;
        public double Hours, Minutes, Seconds;
        public int MaxAge;
        public int ReplicationPeriod;
        public int MaxNumberOfNeurons;
        public double ERRMN;
        public double CERR;
        public double ConvergncePrecision;
        public int ClustersNumber;
        public double DetalizationCoef;
        public double ReachabilityRadius;
        public int Threshold;
        public double SelfSimilarity;
        public enum AglomerativeClusteringDistance
        {
            SingleLink,
            CentreDistance, WardDistance
        }
        public AglomerativeClusteringDistance ACDistance;
    }
}
