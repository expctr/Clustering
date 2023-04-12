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

        // VisualizationViewMaster ViewMaster;

        List<Item> Items;

        List<Cluster> Clusters;

        double targetX, targetY;

        bool InMove;

        Grid grid;

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

            //ViewMaster = new VisualizationViewMaster(this, pictureBox1, Items, Clusters,
            //    ViewAllPointsTSB);
            // ViewMaster.ShowInfo(true);

            grid = new Grid(pictureBox1);

            SizeChanged += Carrier_SizeChanged;
            grid.GetPB().MouseWheel += PB_MouseWheel;
            grid.GetPB().MouseDown += PB_MouseDown;
            grid.GetPB().MouseMove += PB_MoveMouseMove;
            grid.GetPB().MouseUp += PB_MouseUp;
            grid.SetItems(Items);
            ViewAllPointsTSB.Click += ViewAllPointsTSB_Click;
            ViewAllPointsTSB_Click(new object(), new EventArgs());
        }

        public void ShowInfo(bool show)
        {
            grid.ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(grid.GetGraph(), false);
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

        protected void ShowCluster(Cluster cluster, bool ShowCentre, Graphics graph_, bool show)
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
            ////
            //double radPB = cluster.QuartileRange / scaleX;
            //double xPB = XtoPB(centre[0]);
            //double yPB = YtoPB(centre[1]);
            //graph.DrawEllipse(new Pen(Color.Black), (float)(xPB - radPB), 
            //    (float)(yPB - radPB),
            //    (float)radPB * 2, (float)radPB * 2);
            ////
            grid.DrawSquare_Grid(centre[0], centre[1], cluster.GetCentreColor, true, graph_);
            if (show)
            {
                grid.GetPB().Image = grid.GetBMP();
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
                grid.GetPB().Image = grid.GetBMP();
            }
        }

        void Boundaries(out double lowerX, out double lowerY, out double upperX,
            out double upperY)
        {
            if (grid.GetItems() == null || grid.GetItems().Count == 0)
            {
                lowerX = lowerY = upperX = upperY = 0;
                return;
            }
            lowerX =
                Algorithm.FindMin(grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            lowerY =
                Algorithm.FindMin(grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
            upperX =
                Algorithm.FindMax(grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            upperY =
                Algorithm.FindMax(grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
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
            grid.SetOffsetX(grid.GetOffsetX() - (e.X - targetX));
            grid.SetOffsetY(grid.GetOffsetY() + e.Y - targetY);
            targetX = e.X;
            targetY = e.Y;
            grid.ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(grid.GetGraph(), false);
            }
            else
            {
                grid.ShowPoints(grid.GetGraph(), false);
            }
            grid.GetPB().Image = grid.GetBMP();
        }

        //
        //Масштабирование
        //

        void PB_MouseWheel(object sender, MouseEventArgs e) //Управление масштабированием. Этот обработчик является общим для всех режимов
        {
            double X0 = grid.XtoGrid(e.X);
            double Y0 = grid.YtoGrid(e.Y);
            if (e.Delta > 0)
            {
                grid.SetScaleX(grid.GetScaleX() * 0.95);
                grid.SetScaleY(grid.GetScaleY() * 0.95);
            }
            else
            {
                grid.SetScaleX(grid.GetScaleX() * 1.05);
                grid.SetScaleY(grid.GetScaleY() * 1.05);
            }
            grid.SetOffsetX(X0 / grid.GetScaleX() - e.X);
            grid.SetOffsetY(Y0 / grid.GetScaleY() + e.Y);
            grid.ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(grid.GetGraph(), false);
            }
            else
            {
                grid.ShowPoints(grid.GetGraph(), false);
            }
            grid.GetPB().Image = grid.GetBMP();
        }

        //
        //Изменение размеров формы
        //

        void Carrier_SizeChanged(object sender, EventArgs e)
        {
            grid.GetPB().Width = Width - 2 * 8 - 2 * 12;
            grid.GetPB().Height = Height - 8 - 12 - grid.GetPB().Location.Y - 32;
            grid.SetBMP(new Bitmap(grid.GetPB().Width, grid.GetPB().Height));
            grid.SetGrpah(Graphics.FromImage(grid.GetBMP()));
            //MessageBox.Show("SizeChanged");//del
            grid.ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(grid.GetGraph(), false);
            }
            else
            {
                grid.ShowPoints(grid.GetGraph(), false);
            }
            grid.GetPB().Image = grid.GetBMP();
        }

        //
        //Разное
        //

        void ViewAllPointsTSB_Click(object sender, EventArgs e)
        {
            if (grid.GetItems() == null || grid.GetItems().Count == 0)
            {
                return;
            }
            double x_min, y_min, x_max, y_max;
            Boundaries(out x_min, out y_min, out x_max, out y_max);
            double w = x_max - x_min;
            double h = y_max - y_min;
            double scale = Algorithm.Max(w / (0.9 * grid.GetPB().Width), h / (0.9 * grid.GetPB().Height));
            grid.SetScaleX(scale);
            grid.SetScaleY(scale);
            grid.SetOffsetX((x_max + x_min) / (2 * grid.GetScaleX()) - grid.GetPB().Width / 2);
            grid.SetOffsetY((y_max + y_min) / (2 * grid.GetScaleY()) + grid.GetPB().Height / 2);
            grid.ShowGrid(false);
            if (Clusters != null && Clusters.Count > 0)
            {
                ShowClusters(grid.GetGraph(), false);
            }
            else
            {
                grid.ShowPoints(grid.GetGraph(), false);
            }
            grid.GetPB().Image = grid.GetBMP();
        }
    }
}
