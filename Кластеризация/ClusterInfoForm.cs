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
    }
}
