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
    public class ClusteringClass : IClusteringClass
    {
        public event DebugDel debugEvent;

        protected List<Item> Items = new List<Item>();

        public void SetItems(List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            Items = new List<Item>(items);
        }

        public List<Item> GetItems()
        {
            return Items;
        }

        public bool StopFlag { set; get; }

        public void Stop()
        {
            StopFlag = true;
        }

        public event ProgressDel ProgressChanged;

        public LearningMode learningMode { set; get; }
    }
}
