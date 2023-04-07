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
    public abstract class ClusteringClass : IClustering
    {
        public abstract void SetOptions(ClusteringOptions opt);
        public abstract ClusteringOptions GetOptions();
        public virtual event DebugDel debugEvent;//del
        protected List<Item> Items = new List<Item>();
        public virtual void SetItems(List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            Items = new List<Item>(items);
        }
        public abstract List<List<Item>> GetClusters();
        public bool StopFlag { set; get; }
        public virtual void Stop()
        {
            StopFlag = true;
        }
        public virtual event ProgressDel ProgressChanged;
        public LearningMode learningMode { set; get; }
    }
}
