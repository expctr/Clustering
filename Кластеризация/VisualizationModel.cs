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
    public class VisualizationModel
    {
        private VisualizationForm form;

        public List<Item> Items;

        public List<Cluster> Clusters;

        public double targetX, targetY;

        public bool InMove;

        public VisualizationModel(VisualizationForm form)
        {
            this.form = form;
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

        public void Boundaries(out double lowerX, out double lowerY, out double upperX,
            out double upperY)
        {
            if (form.grid.GetItems() == null || form.grid.GetItems().Count == 0)
            {
                lowerX = lowerY = upperX = upperY = 0;
                return;
            }
            lowerX =
                Algorithm.FindMin(form.grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            lowerY =
                Algorithm.FindMin(form.grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
            upperX =
                Algorithm.FindMax(form.grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            upperY =
                Algorithm.FindMax(form.grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
        }
    }
}
