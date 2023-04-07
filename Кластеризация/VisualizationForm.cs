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
        VisualizationViewMaster ViewMaster;
        List<Item> Items;
        List<Cluster> Clusters;
        public void SetInfo(List<Item> items, List<Cluster> clusters, bool copy)
        {
            if (items == null)
            {
                items = null;
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
                clusters = null;
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
        private void VisualizationForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            ViewMaster = new VisualizationViewMaster(this, pictureBox1, Items, Clusters,
                ViewAllPointsTSB);
            ViewMaster.ShowInfo(true);
            MouseClick += VisualizationForm_MouseClick;//del
        }

        //
        //
        void VisualizationForm_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show($"{e.X}, {e.Y}");
        }
        //
        //

    }

    class VisualizationViewMaster : Grid
    {
        VisualizationForm Carrier;
        List<Cluster> Clusters;
        double targetX, targetY;
        bool InMove;
        ToolStripButton ViewAllPointsTSB;
        public VisualizationViewMaster(VisualizationForm carrier, PictureBox pb,
            List<Item> items, List<Cluster> clusters,
            ToolStripButton viewAllPointsTSB) : base(pb)
        {
            Carrier = carrier;
            Carrier.SizeChanged += Carrier_SizeChanged;
            PB.MouseWheel += PB_MouseWheel;
            PB.MouseDown += PB_MouseDown;
            PB.MouseMove += PB_MoveMouseMove;
            PB.MouseUp += PB_MouseUp;
            Items = items;
            Clusters = clusters;
            ViewAllPointsTSB = viewAllPointsTSB;
            ViewAllPointsTSB.Click += ViewAllPointsTSB_Click;
            ViewAllPointsTSB_Click(new object(), new EventArgs());
        }
        public void ShowInfo(bool show)
        {
            ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(graph, false);
            }
            else
            {
                ShowPoints(graph, false);
            }
            if (show)
            {
                PB.Image = bmp;
            }
        }
        //
        protected void ShowCluster(Cluster cluster, bool ShowCentre, Graphics graph_, bool show)
        {
            if (cluster == null || cluster.Count == 0)
            {
                return;
            }
            List<Item> items = cluster.GetElements();

            DrawPoints_Grid(Item.ToDoubleArray(items), cluster.GetElementColor, graph_, false);
            if (ShowCentre)
            {
                double[] centre = EuclideanGeometry.Barycentre(Item.ToDoubleArray(items));
                DrawSquare_Grid(centre[0], centre[1], cluster.GetCentreColor, true, graph_);
            }
            if (show)
            {
                PB.Image = bmp;
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
            DrawSquare_Grid(centre[0], centre[1], cluster.GetCentreColor, true, graph_);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        protected void ShowClusters(Graphics graph_, bool show)
        {
            if (Clusters == null)
            {
                return;
            }
            foreach (var cluster in Clusters)
            {
                ShowCluster(cluster, false, graph_, false);
            }
            foreach (var cluster in Clusters)
            {
                ShowCentre(cluster, graph_, false);
            }
            if (show)
            {
                PB.Image = bmp;
            }
        }

        void Boundaries(out double lowerX, out double lowerY, out double upperX,
            out double upperY)
        {
            if (Items == null || Items.Count == 0)
            {
                lowerX = lowerY = upperX = upperY = 0;
                return;
            }
            lowerX =
                Algorithm.FindMin(Items, (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            lowerY =
                Algorithm.FindMin(Items, (item1, item2) => item1[1].CompareTo(item2[1]))[1];
            upperX =
                Algorithm.FindMax(Items, (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            upperY =
                Algorithm.FindMax(Items, (item1, item2) => item1[1].CompareTo(item2[1]))[1];
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
            InMove = true;
            targetX = e.X;
            targetY = e.Y;
        }
        void PB_MouseUp(object sender, MouseEventArgs e) //Регистрирует снятия зажатия с мыши
        {
            InMove = false;
        }
        void PB_MoveMouseMove(object sender, MouseEventArgs e) //При перемещении курсора перемещается и плоскость
        {
            if (!InMove) return;
            SetOffsetX(offsetX - (e.X - targetX));
            SetOffsetY(offsetY + e.Y - targetY);
            targetX = e.X;
            targetY = e.Y;
            ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(graph, false);
            }
            else
            {
                ShowPoints(graph, false);
            }
            PB.Image = bmp;
        }
        //
        //Масштабирование
        //
        void PB_MouseWheel(object sender, MouseEventArgs e) //Управление масштабированием. Этот обработчик является общим для всех режимов
        {
            double X0 = XtoGrid(e.X);
            double Y0 = YtoGrid(e.Y);
            if (e.Delta > 0)
            {
                SetScaleX(scaleX * 0.95);
                SetScaleY(scaleY * 0.95);
            }
            else
            {
                SetScaleX(scaleX * 1.05);
                SetScaleY(scaleY * 1.05);
            }
            SetOffsetX(X0 / scaleX - e.X);
            SetOffsetY(Y0 / scaleY + e.Y);
            ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(graph, false);
            }
            else
            {
                ShowPoints(graph, false);
            }
            PB.Image = bmp;
        }
        //
        //Изменение размеров формы
        //
        void Carrier_SizeChanged(object sender, EventArgs e)
        {
            PB.Width = Carrier.Width - 2 * 8 - 2 * 12;
            PB.Height = Carrier.Height - 8 - 12 - PB.Location.Y - 32;
            bmp = new Bitmap(PB.Width, PB.Height);
            graph = Graphics.FromImage(bmp);
            //MessageBox.Show("SizeChanged");//del
            ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(graph, false);
            }
            else
            {
                ShowPoints(graph, false);
            }
            PB.Image = bmp;
        }
        //
        //Разное
        //
        void ViewAllPointsTSB_Click(object sender, EventArgs e)
        {
            if (Items == null || Items.Count == 0)
            {
                return;
            }
            double x_min, y_min, x_max, y_max;
            Boundaries(out x_min, out y_min, out x_max, out y_max);
            double w = x_max - x_min;
            double h = y_max - y_min;
            double scale = Algorithm.Max(w / (0.9 * PB.Width), h / (0.9 * PB.Height));
            SetScaleX(scale);
            SetScaleY(scale);
            SetOffsetX((x_max + x_min) / (2 * scaleX) - PB.Width / 2);
            SetOffsetY((y_max + y_min) / (2 * scaleY) + PB.Height / 2);
            ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(graph, false);
            }
            else
            {
                ShowPoints(graph, false);
            }
            PB.Image = bmp;
        }
    }

}
