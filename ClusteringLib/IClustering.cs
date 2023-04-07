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
    public interface IClustering
    {
        void SetItems(List<Item> items);
        List<List<Item>> GetClusters();
        bool StopFlag
        {
            set;
            get;
        }
        void Stop();
        event ProgressDel ProgressChanged;
        LearningMode learningMode { set; get; }
        void SetOptions(ClusteringOptions opt);
        ClusteringOptions GetOptions();
        event DebugDel debugEvent;//del
    }
}
