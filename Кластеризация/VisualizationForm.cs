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
        public Grid grid;

        public VisualizationForm()
        {
            InitializeComponent();
            grid = new Grid(GetPictureBox1());
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

        public void ShowInfo(bool show, List<Cluster> Clusters)
        {
            grid.ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(grid.GetGraph(), false, Clusters);
            }
            else
            {
                grid.ShowPoints(grid.GetGraph(), false);
            }
            if (show)
            {
                grid.GetPB().Image = grid.GetBMP();
            }
        }

        protected void ShowCluster(Cluster cluster, bool ShowCentre, Graphics graph_,
            bool show, Grid grid)
        {
            if (cluster == null || cluster.Count == 0)
            {
                return;
            }
            List<Item> items = cluster.GetElements();

            grid.DrawPoints_Grid(Item.ToDoubleArray(items), cluster.GetElementColor, graph_, false);
            if (ShowCentre)
            {
                double[] centre = EuclideanGeometry.Barycentre(Item.ToDoubleArray(items));
                grid.DrawSquare_Grid(centre[0], centre[1], cluster.GetCentreColor, true, graph_);
            }
            if (show)
            {
                grid.GetPB().Image = grid.GetBMP();
            }
        }

        protected void ShowCentre(Cluster cluster, Graphics graph_, bool show)
        {
            if (cluster == null || cluster.Count == 0)
            {
                return;
            }
            List<Item> items = cluster.GetElements();
            double[] centre = EuclideanGeometry.Barycentre(Item.ToDoubleArray(items));
            grid.DrawSquare_Grid(centre[0], centre[1], cluster.GetCentreColor, true, graph_);
            if (show)
            {
                grid.GetPB().Image = grid.GetBMP();
            }
        }

        public void ShowClusters(Graphics graph_, bool show, List<Cluster> Clusters)
        {
            if (Clusters == null)
            {
                return;
            }
            foreach (var cluster in Clusters)
            {
                ShowCluster(cluster, false, graph_, false, grid);
            }
            foreach (var cluster in Clusters)
            {
                ShowCentre(cluster, graph_, false);
            }
            if (show)
            {
                grid.GetPB().Image = grid.GetBMP();
            }
        }
    }
}
