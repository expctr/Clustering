using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClusteringLib;
using ItemLib;
using EuclideanGeometryLib;
using AlgorithmLib;

namespace Кластеризация
{
    public partial class VisualizationForm : Form
    {
        public VisualizationForm()
        {
            InitializeComponent();
        }

        public void ApplyCenterToScreen()
        {
            CenterToScreen();
        }

        public PictureBox GetPictureBox1()
        {
            return pictureBox1;
        }

        public ToolStripButton GetViewAllPointsTSB()
        {
            return ViewAllPointsTSB;
        }
    }
}
