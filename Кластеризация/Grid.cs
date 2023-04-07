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
    class Grid //Реализует координатную евклидову плоскость, способную содержать точки
    {
        protected Bitmap bmp;
        protected PictureBox PB;
        protected int minXpb = 50, minYpb = 50;
        protected Graphics graph;
        protected double scaleX;
        protected double scaleY;
        protected double offsetX;
        protected double offsetY;
        protected double minScaleX = 1.0 / 1000000000;
        protected double minScaleY = 1.0 / 1000000000;
        protected double maxScaleX = 1000000000;
        protected double maxScaleY = 1000000000;
        protected double minOffsetX = double.MinValue / 10;
        protected double minOffsetY = double.MinValue / 10;
        protected double maxOffsetX = double.MaxValue / 10;
        protected double maxOffsetY = double.MaxValue / 10;
        public Grid(PictureBox pb)
        {
            PB = pb;
            SetOffsetX(-PB.Width / 2);
            SetOffsetY(PB.Height / 2);
            SetScaleX(1.0 / 16);
            SetScaleY(1.0 / 16);
            bmp = new Bitmap(PB.Width, PB.Height);
            graph = Graphics.FromImage(bmp);
            graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
        protected void SetOffsetX(double _offsetX)
        {
            if(_offsetX < minOffsetX)
            {
                offsetX = minOffsetX;
                return;
            }
            if(_offsetX > maxOffsetX)
            {
                offsetX = maxOffsetX;
                return;
            }
            offsetX = _offsetX;
        }
        protected void SetOffsetY(double _offsetY)
        {
            if (_offsetY < minOffsetY)
            {
                offsetY = minOffsetY;
                return;
            }
            if (_offsetY > maxOffsetY)
            {
                offsetY = maxOffsetY;
                return;
            }
            offsetY = _offsetY;
        }
        protected void SetScaleX(double _scaleX)
        {
            if(_scaleX < minScaleX)
            {
                scaleX = minScaleX;
                return;
            }
            if(_scaleX > maxScaleX)
            {
                scaleX = maxScaleX;
                return;
            }
            scaleX = _scaleX;
        }
        protected void SetScaleY(double _scaleY)
        {
            if (_scaleY < minScaleY)
            {
                scaleY = minScaleY;
                return;
            }
            if (_scaleY > maxScaleY)
            {
                scaleY = maxScaleY;
                return;
            }
            scaleY = _scaleY;
        }
        //Преобразования координат
        protected double XtoGrid(double eX)
        {
            return (offsetX + eX) * scaleX;
        }
        protected double YtoGrid(double eY)
        {
            return (offsetY - eY) * scaleY;
        }
        protected double XtoPB(double X)
        {
            return (X / scaleX - offsetX);
        }
        protected double YtoPB(double Y)
        {
            return -(Y / scaleY - offsetY);
        }

        protected static int IntegerPart(double x) //Целая часть числа
        {
            if (x >= 0) return (int)x;
            if (x == (int)x) return (int)x;
            return (int)x - 1;
        }

        protected static double Step(double x)//Шаг координатной решетки
        {
            double pow = IntegerPart(Math.Log10(x));
            double x1 = x / Math.Pow(10, pow);
            if (x1 <= 1) return 1 * Math.Pow(10, pow);
            if (x1 <= 2) return 2 * Math.Pow(10, pow);
            if (x1 <= 5) return 5 * Math.Pow(10, pow);
            return 10 * Math.Pow(10, pow);
        }
        //Рисование
        protected void DrawHLine(double y, Pen pen, Graphics graph_)
        {
            graph_.DrawLine(pen, 0, (float)YtoPB(y), PB.Width, (float)YtoPB(y));
        }
        protected void DrawVLine(double x, Pen pen, Graphics graph_)
        {
            graph_.DrawLine(pen, (float)XtoPB(x), 0, (float)XtoPB(x), PB.Height);
        }
        protected void DrawString(string s, Font font, Brush brush, double x, double y, Graphics graph_)
        {
            graph_.DrawString(s, font, brush, (float)XtoPB(x), (float)YtoPB(y));
        }
        protected void DrawPointPB(double Xpb, double Ypb, Color color, Graphics graph_, bool show, int radPB = 3)
        {
            graph_.FillEllipse(new Pen(color).Brush, (float)(Xpb - radPB), (float)(Ypb - radPB),
                2 * radPB, 2 * radPB);
            graph_.DrawEllipse(new Pen(Color.Black), (float)(Xpb - radPB), (float)(Ypb - radPB),
                2 * radPB, 2 * radPB);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        protected void DrawPointPB(double Xpb, double Ypb, Color color, bool show, int radPB = 3)
        {
            graph.FillEllipse(new Pen(color).Brush, (float)(Xpb - radPB), (float)(Ypb - radPB),
                2 * radPB, 2 * radPB);
            graph.DrawEllipse(new Pen(Color.Black), (float)(Xpb - radPB), (float)(Ypb - radPB),
                2 * radPB, 2 * radPB);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        protected void DrawPointGrid(double Xgrid, double Ygrid, Color color, Graphics graph_, bool show, int radPB = 3)
        {
            DrawPointPB(XtoPB(Xgrid), YtoPB(Ygrid), color, graph_, false, radPB);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        protected void DrawPoints_Grid(List<double[]> points, Color color, Graphics graph_, bool show)
        {
            foreach (var point in points)
            {
                DrawPointGrid(point[0], point[1], color, graph_, false);
            }
            if (show)
            {
                PB.Image = bmp;
            }
        }
        protected void DrawLine_PB(double eX1, double eY1, double eX2, double eY2, Graphics graph_)
        {
            graph_.DrawLine(new Pen(Color.Black), (float)eX1, (float)eY1, (float)eX2, (float)eY2);
        }
        protected void DrawLine_Grid(double X1grid, double Y1grid, double X2grid, double Y2grid, Graphics graph_)
        {
            DrawLine_PB(XtoPB(X1grid), YtoPB(Y1grid), XtoPB(X2grid), YtoPB(Y2grid), graph_);
        }
        protected void DrawSquare_PB(double Xpb, double Ypb, Color color, Graphics graph_, int aPB = 16)
        {
            graph_.FillRectangle(new Pen(color).Brush, (float)(Xpb - aPB / 2), (float)(Ypb - aPB / 2), aPB, aPB);
            graph_.DrawRectangle(new Pen(Color.Black), (float)(Xpb - aPB / 2),
                (float)(Ypb - aPB / 2), aPB, aPB);
        }
        protected void DrawSquare_PB(double Xpb, double Ypb, Color color, bool InverseContourColor, Graphics graph_,
            int aPB = 16)
        {
            graph_.FillRectangle(new Pen(color).Brush, (float)(Xpb - aPB / 2), (float)(Ypb - aPB / 2), aPB, aPB);
            Color ContourColor;
            if (InverseContourColor)
            {
                ContourColor = Color.FromArgb(255, 255 - color.R, 255 - color.G, 255 - color.B);
            }
            else
            {
                ContourColor = Color.Black;
            }
            graph_.DrawRectangle(new Pen(ContourColor), (float)(Xpb - aPB / 2),
                (float)(Ypb - aPB / 2), aPB, aPB);
        }
        protected void DrawSquare_Grid(double Xgrid, double Ygrid, Color color, Graphics graph_, int aPB = 8)
        {
            DrawSquare_PB(XtoPB(Xgrid), YtoPB(Ygrid), color, graph_, aPB);
        }
        protected void DrawSquare_Grid(double Xgrid, double Ygrid, Color color, bool InverseCentre, Graphics graph_,
            int aPB = 8)
        {
            DrawSquare_PB(XtoPB(Xgrid), YtoPB(Ygrid), color, InverseCentre, graph_, aPB);
        }
        public void ShowGrid(Graphics graph_, bool show) //Отображает координатную плоскость
        {
            graph_.Clear(Color.White);
            double y1, y3;
            y1 = YtoGrid(0);
            y3 = YtoGrid(PB.Height);
            double StepY = Step(minYpb * scaleY) / 5;
            int ky = IntegerPart(y3 / StepY) + 1;
            for (; ky * StepY < y1; ++ky)
            {
                DrawHLine(ky * StepY, new Pen(Color.Silver), graph_);
            }
            double x1, x2;
            x1 = XtoGrid(0);
            x2 = XtoGrid(PB.Width);
            double StepX = Step(minXpb * scaleX) / 5;
            int kx = IntegerPart(x1 / StepX) + 1;
            for (; kx * StepX < x2; ++kx)
            {
                DrawVLine(kx * StepX, new Pen(Color.Silver), graph_);
            }
            StepY = Step(minYpb * scaleY);
            ky = IntegerPart(y3 / StepY) + 1;
            for (; ky * StepY < y1; ++ky)
            {
                DrawHLine(ky * StepY, new Pen(SystemColors.ControlDarkDark), graph_);
                graph_.DrawString((ky * StepY).ToString(), new Font("Calibri", 12),
                    new Pen(SystemColors.ControlDarkDark).Brush, 0, (float)YtoPB(ky * StepY));
            }
            StepX = Step(minXpb * scaleX);
            kx = IntegerPart(x1 / StepX) + 1;
            for (; kx * StepX < x2; ++kx)
            {
                DrawVLine(kx * StepX, new Pen(SystemColors.ControlDarkDark), graph_);
                graph_.DrawString((kx * StepX).ToString(), new Font("Calibri", 12),
                    new Pen(SystemColors.ControlDarkDark).Brush, (float)XtoPB(kx * StepX), 0);
            }
            DrawHLine(0, new Pen(Color.Black, 2), graph_);
            DrawVLine(0, new Pen(Color.Black, 2), graph_);
            graph_.DrawLine(new Pen(Color.Black), 0, 0, PB.Width - 1, 0);
            graph_.DrawLine(new Pen(Color.Black), PB.Width - 1, 0, PB.Width - 1, PB.Height - 1);
            graph_.DrawLine(new Pen(Color.Black), PB.Width - 1, PB.Height - 1, 0, PB.Height - 1);
            graph_.DrawLine(new Pen(Color.Black), 0, PB.Height - 1, 0, 0);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        public void ShowGrid(bool show) //Отображает координатную плоскость
        {
            ShowGrid(graph, show);
        }
        public void ShowPoints(Graphics graph_, bool show) //Отображает точки
        {
            if (Items == null) return;
            foreach (var i in Items)
            {
                DrawPointGrid(i[0], i[1], Color.Gray, graph_, false);
            }
            if (show)
            {
                PB.Image = bmp;
            }
        }
        public void Show(Graphics graph_, bool show) //Отображает координатную плоскость и точки
        {
            ShowGrid(graph_, false);
            ShowPoints(graph_, false);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        public void Show(bool show) //Отображает координатную плоскость и точки
        {
            ShowGrid(graph, false);
            ShowPoints(graph, false);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        protected List<Item> Items = new List<Item>(); //Список точек
        protected Item CreateItem_PB(double eX, double eY) //Создает 2D экземпляр структуры Item по координатам на PB
        {
            return new Item(XtoGrid(eX), YtoGrid(eY), $"Объект {Items.Count + 1}", 3);
        }
        protected void AddItem_PB(double eX, double eY, Graphics graph_, bool show)
        {
            Items.Add(CreateItem_PB(eX, eY));
            DrawPointPB(eX, eY, Color.Gray, graph_, false);
            if (show)
            {
                PB.Image = bmp;
            }
        }
        protected void AddItem_PB(double eX, double eY, bool show)
        {
            Items.Add(CreateItem_PB(eX, eY));
            DrawPointPB(eX, eY, Color.Gray, false);
            if (show)
            {
                PB.Image = bmp;
            }
        }
    }//class Grid

    class ClusteringViewMaster : Grid
    {
        protected Form Carrier;
        protected List<Cluster> Clusters = null;
        public ClusteringViewMaster(PictureBox pb, Form carrier, List<Item> items) : base(pb)
        {
            Carrier = carrier;
            Items = items;
            PB.MouseDown += PB_MouseDown;
            PB.MouseUp += PB_MouseUp;
            PB.MouseMove += PB_MoveMouseMove;
            PB.MouseWheel += PB_MouseWheel;
            Carrier.SizeChanged += Carrier_SizeChanged;

            showDelegate += delegate ()
            {
                if (Clusters != null)
                {
                    ShowGrid(graph, false);
                    ShowClusters(graph, true);
                }
                else
                {
                    Show(graph, true);
                }
            };
        }
        //
        //Рисование
        //
        protected void ShowCluster(Cluster cluster, bool ShowCentre, Graphics graph_, bool show)
        {
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
        protected void ShowClusters(Graphics graph_, bool show)
        {
            foreach (var cluster in Clusters)
            {
                ShowCluster(cluster, true, graph_, false);
            }
            if (show)
            {
                PB.Image = bmp;
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
            SetOffsetX(offsetX - (e.X - targetX));
            SetOffsetY(offsetY + e.Y - targetY);
            showDelegate();
            targetX = e.X;
            targetY = e.Y;
        }
        //
        //Масштабирование
        //
        protected void PB_MouseWheel(object sender, MouseEventArgs e) //Управление масштабированием. Этот обработчик является общим для всех режимов
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
            showDelegate();
        }

        //
        //Изменение размеров формы
        //
        protected void Carrier_SizeChanged(object sender, EventArgs e)
        {
            PB.Width = Carrier.Width - PB.Location.X - 28;
            PB.Height = Carrier.Height - 78;
            try
            {
                bmp = new Bitmap(PB.Width, PB.Height);
                graph = Graphics.FromImage(bmp);
                showDelegate();
            }
            catch
            {

            }
        }
    }//class ClusteringViewMaster

    class TimeLimitMaster
    {
        public bool TimeLimitActivated;
        public double Hours, Minutes, Seconds;
        double timeLimit_Seconds;
        Stopwatch watch;
        Action Finish;
        public TimeLimitMaster(bool timeLimitActivated, double hours, double minutes, double seconds)
        {
            TimeLimitActivated = timeLimitActivated;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            watch = new Stopwatch();
        }

        public void SetOptions(ClusteringOptions opt)
        {
            TimeLimitActivated = opt.TimeLimitActivated;
            Hours = opt.Hours;
            Minutes = opt.Minutes;
            Seconds = opt.Seconds;
        }
        public void AddOptions(ClusteringOptions opt)
        {
            opt.TimeLimitActivated = TimeLimitActivated;
            opt.Hours = Hours;
            opt.Minutes = Minutes;
            opt.Seconds = Seconds;
        }

        public double TimeLimit_Seconds
        {
            get {
                return Hours * 3600 + Minutes * 60 + Seconds;
            }
        }
        public void SetTimeLimit(double hours, double minutes, double seconds)
        {
            Hours = hours; Minutes = minutes; Seconds = seconds;
        }
        public void SetFinish(Action finish)
        {
            Finish = null;
            Finish += finish;
        }
        public void Activate()
        {
            watch.Restart();
            timeLimit_Seconds = TimeLimit_Seconds;
        }

        public void Check()
        {
            if (!TimeLimitActivated) return;
            if (watch.Elapsed.Seconds > TimeLimit_Seconds)
            {
                watch.Reset();
                Finish();
            }
        }
    }//class SOMTimeLimitMaster

}