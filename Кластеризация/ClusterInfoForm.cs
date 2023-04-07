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
    public partial class ClusterInfoForm : Form
    {
        public ClusterInfoForm()
        {
            InitializeComponent();
        }

        int Index;
        double ADBE;
        string[] ColsNames;
        Cluster cluster;

        public ClusterInfoForm(Cluster _cluster, int index, string[] colsNames) : this()
        {
            cluster = _cluster;
            cluster.ResetProgressChanged();
            cluster.progressChanged += (double x) =>
            {
                backgroundWorker1.ReportProgress((int)(x * progressBar1.Maximum));
            };
            Index = index;
            ColsNames = colsNames;
        }

        private void ClusterInfoForm_Load(object sender, EventArgs e)
        {
            ShowInfo();
            FindADBEB.Click += FindADBEB_Click;
            backgroundWorker1.DoWork +=
                backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged +=
                backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted +=
                backgroundWorker1_RunWorkerCompleted;
        }

        void ShowCentreCoordinates()
        {
            CentreCoordinatesDGV.ColumnCount = ColsNames.Length;
            for (int i = 0; i < ColsNames.Length; ++i)
            {
                CentreCoordinatesDGV.Columns[i].Name = (i + 1).ToString();
            }
            CentreCoordinatesDGV.Rows.Add(ColsNames);
            double[] centre = cluster.GetCentre;
            string[] row = new string[centre.Length];
            for (int i = 0; i < row.Length; ++i)
            {
                row[i] = centre[i].ToString();
            }
            CentreCoordinatesDGV.Rows.Add(row);
            //CentreCoordinatesDGV.Rows[0].Frozen = true;
        }

        void ShowInfo()
        {
            ClusterIndexL.Text = $"Кластер с индексом {Index}";
            ShowCentreCoordinates();
            SizeL.Text = $"Размер: {cluster.Count}";
            MeanLinearDeviationL.Text = $"Среднее линейное отклонение: " +
                $"{cluster.MeanLinearIntraclusterDeviation}";
            MeanSquareDeviationL.Text = $"Среднеквадратическое отклонение: " +
                $"{cluster.MeanSquareIntraclusterDeviation}";
            DispersionL.Text = $"Дисперсия: {cluster.Dispersion}";
            QuartileRangeL.Text = $"Квартильный размах: {cluster.QuartileRange}";
        }

        private void FindADBEB_Click(object sender, EventArgs e)
        {
            FindADBEB.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ADBE = cluster.AverageDistanceBetweenElements;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            ADBEL.Text = $"Среднее расстояние между \nэлементами: {ADBE}";
            FindADBEB.Enabled = true;
        }

        private void ClusterInfoForm_SizeChanged(object sender, EventArgs e)
        {
            CentreCoordinatesDGV.Width = Width - 2 * 8 - 2 * 12;
        }
    }
}
