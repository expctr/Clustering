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
    class VisualizationController
    {
        private VisualizationForm form;

        private VisualizationModel model;

        public VisualizationController(VisualizationForm form, VisualizationModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.Load += VisualizationForm_Load;
            form.SizeChanged += Carrier_SizeChanged;
            model.grid.GetPB().MouseWheel += PB_MouseWheel;
            model.grid.GetPB().MouseDown += PB_MouseDown;
            model.grid.GetPB().MouseMove += PB_MoveMouseMove;
            model.grid.GetPB().MouseUp += PB_MouseUp;
            form.GetViewAllPointsTSB().Click += ViewAllPointsTSB_Click;
            ViewAllPointsTSB_Click(new object(), new EventArgs());
        }

        private void VisualizationForm_Load(object sender, EventArgs e)
        {
            form.ApplyCenterToScreen();

            //ViewMaster = new VisualizationViewMaster(this, pictureBox1, Items, Clusters,
            //    ViewAllPointsTSB);
            // ViewMaster.ShowInfo(true);

            model.grid = new Grid(form.GetPictureBox1());
            model.grid.SetItems(model.Items);
            ShowInfo(true);
        }

        public void ShowInfo(bool show)
        {
            model.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                ShowClusters(model.grid.GetGraph(), false);
            }
            else
            {
                model.grid.ShowPoints(model.grid.GetGraph(), false);
            }
            if (show)
            {
                model.grid.GetPB().Image = model.grid.GetBMP();
            }
        }

        protected void ShowCluster(Cluster cluster, bool ShowCentre, Graphics graph_, bool show)
        {
            if (cluster == null || cluster.Count == 0)
            {
                return;
            }
            List<Item> items = cluster.GetElements();

            model.grid.DrawPoints_Grid(Item.ToDoubleArray(items), cluster.GetElementColor, graph_, false);
            if (ShowCentre)
            {
                double[] centre = EuclideanGeometry.Barycentre(Item.ToDoubleArray(items));
                model.grid.DrawSquare_Grid(centre[0], centre[1], cluster.GetCentreColor, true, graph_);
            }
            if (show)
            {
                model.grid.GetPB().Image = model.grid.GetBMP();
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
            ////
            //double radPB = cluster.QuartileRange / scaleX;
            //double xPB = XtoPB(centre[0]);
            //double yPB = YtoPB(centre[1]);
            //graph.DrawEllipse(new Pen(Color.Black), (float)(xPB - radPB), 
            //    (float)(yPB - radPB),
            //    (float)radPB * 2, (float)radPB * 2);
            ////
            model.grid.DrawSquare_Grid(centre[0], centre[1], cluster.GetCentreColor, true, graph_);
            if (show)
            {
                model.grid.GetPB().Image = model.grid.GetBMP();
            }
        }

        protected void ShowClusters(Graphics graph_, bool show)
        {
            if (model.Clusters == null)
            {
                return;
            }
            foreach (var cluster in model.Clusters)
            {
                ShowCluster(cluster, false, graph_, false);
            }
            foreach (var cluster in model.Clusters)
            {
                ShowCentre(cluster, graph_, false);
            }
            if (show)
            {
                model.grid.GetPB().Image = model.grid.GetBMP();
            }
        }

        void Boundaries(out double lowerX, out double lowerY, out double upperX,
            out double upperY)
        {
            if (model.grid.GetItems() == null || model.grid.GetItems().Count == 0)
            {
                lowerX = lowerY = upperX = upperY = 0;
                return;
            }
            lowerX =
                Algorithm.FindMin(model.grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            lowerY =
                Algorithm.FindMin(model.grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
            upperX =
                Algorithm.FindMax(model.grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            upperY =
                Algorithm.FindMax(model.grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
        }

        //

        //
        //Обработчики событий
        //

        //
        //Перемещение плоскости
        //
        void PB_MouseDown(object sender, MouseEventArgs e) //Регистрирует зажатие мыши
        {
            model.InMove = true;
            model.targetX = e.X;
            model.targetY = e.Y;
        }

        void PB_MouseUp(object sender, MouseEventArgs e) //Регистрирует снятия зажатия с мыши
        {
            model.InMove = false;
        }

        void PB_MoveMouseMove(object sender, MouseEventArgs e) //При перемещении курсора перемещается и плоскость
        {
            if (!model.InMove) return;
            model.grid.SetOffsetX(model.grid.GetOffsetX() - (e.X - model.targetX));
            model.grid.SetOffsetY(model.grid.GetOffsetY() + e.Y - model.targetY);
            model.targetX = e.X;
            model.targetY = e.Y;
            model.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                ShowClusters(model.grid.GetGraph(), false);
            }
            else
            {
                model.grid.ShowPoints(model.grid.GetGraph(), false);
            }
            model.grid.GetPB().Image = model.grid.GetBMP();
        }

        //
        //Масштабирование
        //

        void PB_MouseWheel(object sender, MouseEventArgs e) //Управление масштабированием. Этот обработчик является общим для всех режимов
        {
            double X0 = model.grid.XtoGrid(e.X);
            double Y0 = model.grid.YtoGrid(e.Y);
            if (e.Delta > 0)
            {
                model.grid.SetScaleX(model.grid.GetScaleX() * 0.95);
                model.grid.SetScaleY(model.grid.GetScaleY() * 0.95);
            }
            else
            {
                model.grid.SetScaleX(model.grid.GetScaleX() * 1.05);
                model.grid.SetScaleY(model.grid.GetScaleY() * 1.05);
            }
            model.grid.SetOffsetX(X0 / model.grid.GetScaleX() - e.X);
            model.grid.SetOffsetY(Y0 / model.grid.GetScaleY() + e.Y);
            model.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                ShowClusters(model.grid.GetGraph(), false);
            }
            else
            {
                model.grid.ShowPoints(model.grid.GetGraph(), false);
            }
            model.grid.GetPB().Image = model.grid.GetBMP();
        }

        //
        //Изменение размеров формы
        //

        void Carrier_SizeChanged(object sender, EventArgs e)
        {
            model.grid.GetPB().Width = form.Width - 2 * 8 - 2 * 12;
            model.grid.GetPB().Height = form.Height - 8 - 12 - model.grid.GetPB().Location.Y - 32;
            model.grid.SetBMP(new Bitmap(model.grid.GetPB().Width, model.grid.GetPB().Height));
            model.grid.SetGrpah(Graphics.FromImage(model.grid.GetBMP()));
            //MessageBox.Show("SizeChanged");//del
            model.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                ShowClusters(model.grid.GetGraph(), false);
            }
            else
            {
                model.grid.ShowPoints(model.grid.GetGraph(), false);
            }
            model.grid.GetPB().Image = model.grid.GetBMP();
        }

        //
        //Разное
        //

        void ViewAllPointsTSB_Click(object sender, EventArgs e)
        {
            if (model.grid.GetItems() == null || model.grid.GetItems().Count == 0)
            {
                return;
            }
            double x_min, y_min, x_max, y_max;
            Boundaries(out x_min, out y_min, out x_max, out y_max);
            double w = x_max - x_min;
            double h = y_max - y_min;
            double scale = Algorithm.Max(w / (0.9 * model.grid.GetPB().Width), h / (0.9 * model.grid.GetPB().Height));
            model.grid.SetScaleX(scale);
            model.grid.SetScaleY(scale);
            model.grid.SetOffsetX((x_max + x_min) / (2 * model.grid.GetScaleX()) - model.grid.GetPB().Width / 2);
            model.grid.SetOffsetY((y_max + y_min) / (2 * model.grid.GetScaleY()) + model.grid.GetPB().Height / 2);
            model.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                ShowClusters(model.grid.GetGraph(), false);
            }
            else
            {
                model.grid.ShowPoints(model.grid.GetGraph(), false);
            }
            model.grid.GetPB().Image = model.grid.GetBMP();
        }
    }
}
