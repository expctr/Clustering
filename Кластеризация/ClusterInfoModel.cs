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

namespace Кластеризация
{
    class ClusterInfoModel
    {
        public ClusterInfoForm form;

        public int Index;

        public double ADBE;

        public string[] ColsNames;

        public Cluster cluster;

        public ClusterInfoModel(ClusterInfoForm form, Cluster _cluster, int index, string[] colsNames)
        {
            this.form = form;
            cluster = _cluster;
            cluster.ResetProgressChanged();
            cluster.progressChanged += (double x) =>
            {
                form.GetBackgroundWorker1().ReportProgress((int)(x * form.GetProgressBar1().Maximum));
            };
            Index = index;
            ColsNames = colsNames;
        }
    }
}
