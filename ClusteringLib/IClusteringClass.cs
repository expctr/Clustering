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
    interface IClusteringClass
    {
        void SetItems(List<Item> items);

        List<Item> GetItems();

        bool StopFlag { set; get; }

        void Stop();

        LearningMode learningMode { set; get; }
    }
}
