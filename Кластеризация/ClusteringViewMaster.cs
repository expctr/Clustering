using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ItemLib;
using EuclideanGeometryLib;
using AlgorithmLib;
using System.Diagnostics;
using ClusteringLib;


namespace Кластеризация
{
    class ClusteringViewMaster
    {
        protected Form Carrier;

        protected List<Cluster> Clusters = null;

        private IGrid grid;

        public ClusteringViewMaster(PictureBox pb, Form carrier, List<Item> items)
        {
            grid = new Grid(pb);

            Carrier = carrier;
            grid.SetItems(items); // Items = items;
            grid.GetPB().MouseDown += PB_MouseDown;// PB.MouseDown += PB_MouseDown;
            grid.GetPB().MouseUp += PB_MouseUp; // PB.MouseUp += PB_MouseUp;
            grid.GetPB().MouseMove += PB_MoveMouseMove; // PB.MouseMove += PB_MoveMouseMove;
            grid.GetPB().MouseWheel += PB_MouseWheel; // PB.MouseWheel += PB_MouseWheel;
            Carrier.SizeChanged += Carrier_SizeChanged;

            showDelegate += delegate ()
            {
                if (Clusters != null)
                {
                    grid.ShowGrid(grid.GetGraph(), false);
                    ShowClusters(grid.GetGraph(), true);
                }
                else
                {
                    grid.Show(grid.GetGraph(), true);
                }
            };
        }
        //
        //Рисование
        //
        protected void ShowCluster(Cluster cluster, bool ShowCentre, Graphics graph_, bool show)
        {
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
        protected void ShowClusters(Graphics graph_, bool show)
        {
            foreach (var cluster in Clusters)
            {
                ShowCluster(cluster, true, graph_, false);
            }
            if (show)
            {
                grid.GetPB().Image = grid.GetBMP();
            }
        }
        //
        //Обработчики событий
        //

        //
        //Режим перемещения
        //
        protected delegate void ShowDelegate();
        protected ShowDelegate showDelegate;
        protected bool InMove; //Мышь зажата
        protected double targetX, targetY; //Запоминание положения мыши в некоторый момент в координатах PB
        protected void PB_MouseDown(object sender, MouseEventArgs e) //Регистрирует зажатие мыши
        {
            InMove = true;
            targetX = e.X;
            targetY = e.Y;
        }
        protected void PB_MouseUp(object sender, MouseEventArgs e) //Регистрирует снятия зажатия с мыши
        {
            InMove = false;
        }
        protected void PB_MoveMouseMove(object sender, MouseEventArgs e) //При перемещении курсора перемещается и плоскость
        {
            if (!InMove) return;
            grid.SetOffsetX(grid.GetOffsetX() - (e.X - targetX));
            grid.SetOffsetY(grid.GetOffsetY() + e.Y - targetY);
            showDelegate();
            targetX = e.X;
            targetY = e.Y;
        }
        //
        //Масштабирование
        //
        protected void PB_MouseWheel(object sender, MouseEventArgs e) //Управление масштабированием. Этот обработчик является общим для всех режимов
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
            showDelegate();
        }

        //
        //Изменение размеров формы
        //
        protected void Carrier_SizeChanged(object sender, EventArgs e)
        {
            grid.GetPB().Width = Carrier.Width - grid.GetPB().Location.X - 28;
            grid.GetPB().Height = Carrier.Height - 78;
            try
            {
                grid.SetBMP(new Bitmap(grid.GetPB().Width, grid.GetPB().Height)); // bmp = new Bitmap(PB.Width, PB.Height);
                grid.SetGrpah(Graphics.FromImage(grid.GetBMP())); // graph = Graphics.FromImage(bmp);
                showDelegate();
            }
            catch
            {

            }
        }
    }//class ClusteringViewMaster
}
