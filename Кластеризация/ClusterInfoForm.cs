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

        //int Index;
        //double ADBE;
        //string[] ColsNames;
        //Cluster cluster;

        public BackgroundWorker GetBackgroundWorker1()
        {
            return backgroundWorker1;
        }

        public ProgressBar GetProgressBar1()
        {
            return progressBar1;
        }

        public Button GetFindADBEB()
        {
            return FindADBEB;
        }

        public DataGridView GetCentreCoordinatesDGV()
        {
            return CentreCoordinatesDGV;
        }

        public Label GetClusterIndexL()
        {
            return ClusterIndexL;
        }

        public Label GetSizeL()
        {
            return SizeL;
        }

        public Label GetMeanLinearDeviationL()
        {
            return MeanLinearDeviationL;
        }

        public Label GetMeanSquareDeviationL()
        {
            return MeanSquareDeviationL;
        }

        public Label GetDispersionL()
        {
            return DispersionL;
        }

        public Label GetQuartileRangeL()
        {
            return QuartileRangeL;
        }

        public Label GetADBEL()
        {
            return ADBEL;
        }

        public int GetWidth()
        {
            return Width;
        }

        public void ShowCentreCoordinates(string[] ColsNames, Cluster cluster)
        {
            GetCentreCoordinatesDGV().ColumnCount = ColsNames.Length;
            for (int i = 0; i < ColsNames.Length; ++i)
            {
                GetCentreCoordinatesDGV().Columns[i].Name = (i + 1).ToString();
            }
            GetCentreCoordinatesDGV().Rows.Add(ColsNames);
            double[] centre = cluster.GetCentre;
            string[] row = new string[centre.Length];
            for (int i = 0; i < row.Length; ++i)
            {
                row[i] = centre[i].ToString();
            }
            GetCentreCoordinatesDGV().Rows.Add(row);
            //CentreCoordinatesDGV.Rows[0].Frozen = true;
        }

        public void ShowInfo(string[] ColsNames, int Index, Cluster cluster)
        {
            GetClusterIndexL().Text = $"Кластер с индексом {Index}";
            ShowCentreCoordinates(ColsNames, cluster);
            GetSizeL().Text = $"Размер: {cluster.Count}";
            GetMeanLinearDeviationL().Text = $"Среднее линейное отклонение: " +
                $"{cluster.MeanLinearIntraclusterDeviation}";
            GetMeanSquareDeviationL().Text = $"Среднеквадратическое отклонение: " +
                $"{cluster.MeanSquareIntraclusterDeviation}";
            GetDispersionL().Text = $"Дисперсия: {cluster.Dispersion}";
            GetQuartileRangeL().Text = $"Квартильный размах: {cluster.QuartileRange}";
        }
    }
}
