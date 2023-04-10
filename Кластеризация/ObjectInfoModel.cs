using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemLib;
using ClusteringLib;
using AlgorithmLib;
using EuclideanGeometryLib;

namespace Кластеризация
{
    class ObjectInfoModel
    {
        private ObjectInfoForm form;

        public Item item;

        public string[] ColsNames;

        public Cluster cluster;

        public int clusterInd;

        public ObjectInfoModel(ObjectInfoForm form, Item _item, string[] colsNames, Cluster _cluster,
            int _clusterInd)
        {
            this.form = form;
            item = _item;
            ColsNames = colsNames;
            cluster = _cluster;
            clusterInd = _clusterInd;
        }
    }
}
