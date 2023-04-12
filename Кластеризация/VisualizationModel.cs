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
    class VisualizationModel
    {
        private VisualizationForm form;

        public List<Item> Items;

        public List<Cluster> Clusters;

        public double targetX, targetY;

        public bool InMove;

        public Grid grid;

        public VisualizationModel(VisualizationForm form)
        {
            this.form = form;
            grid = new Grid(form.GetPictureBox1());
        }

        public void SetInfo(List<Item> items, List<Cluster> clusters, bool copy)
        {
            if (items == null)
            {
                Items = null;
            }
            else
            {
                if (copy)
                {
                    Items = new List<Item>(items);
                }
                else
                {
                    Items = items;
                }
            }
            if (clusters == null)
            {
                Clusters = null;
            }
            else
            {
                if (copy)
                {
                    Clusters = new List<Cluster>(clusters);
                }
                else
                {
                    Clusters = clusters;
                }
            }
        }
    }
}
