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
    class ClusterInfoController
    {
        private ClusterInfoForm form;

        private ClusterInfoModel model;

        public ClusterInfoController(ClusterInfoForm form, ClusterInfoModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.Load += ClusterInfoForm_Load;
            form.SizeChanged += ClusterInfoForm_SizeChanged;
            form.GetFindADBEB().Click += FindADBEB_Click;
            form.GetBackgroundWorker1().DoWork +=
                backgroundWorker1_DoWork;
            form.GetBackgroundWorker1().ProgressChanged +=
                backgroundWorker1_ProgressChanged;
            form.GetBackgroundWorker1().RunWorkerCompleted +=
                backgroundWorker1_RunWorkerCompleted;
        }

        private void ClusterInfoForm_Load(object sender, EventArgs e)
        {
            form.ShowInfo(model.ColsNames, model.Index, model.cluster);
        }

        private void FindADBEB_Click(object sender, EventArgs e)
        {
            form.GetFindADBEB().Enabled = false;
            form.GetBackgroundWorker1().RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            model.ADBE = model.cluster.AverageDistanceBetweenElements;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            form.GetProgressBar1().Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            form.GetProgressBar1().Value = 0;
            form.GetADBEL().Text = $"Среднее расстояние между \nэлементами: {model.ADBE}";
            form.GetFindADBEB().Enabled = true;
        }

        private void ClusterInfoForm_SizeChanged(object sender, EventArgs e)
        {
            form.GetCentreCoordinatesDGV().Width = form.GetWidth() - 2 * 8 - 2 * 12;
        }
    }
}
