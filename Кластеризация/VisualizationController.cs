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
            form.grid.GetPB().MouseWheel += PB_MouseWheel;
            form.grid.GetPB().MouseDown += PB_MouseDown;
            form.grid.GetPB().MouseMove += PB_MoveMouseMove;
            form.grid.GetPB().MouseUp += PB_MouseUp;
            form.GetViewAllPointsTSB().Click += ViewAllPointsTSB_Click;
            ViewAllPointsTSB_Click(new object(), new EventArgs());
        }

        private void VisualizationForm_Load(object sender, EventArgs e)
        {
            form.ApplyCenterToScreen();

            //ViewMaster = new VisualizationViewMaster(this, pictureBox1, Items, Clusters,
            //    ViewAllPointsTSB);
            // ViewMaster.ShowInfo(true);

            form.grid = new Grid(form.GetPictureBox1());
            form.grid.SetItems(model.Items);
            form.ShowInfo(true, model.Clusters);
        }

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
            form.grid.SetOffsetX(form.grid.GetOffsetX() - (e.X - model.targetX));
            form.grid.SetOffsetY(form.grid.GetOffsetY() + e.Y - model.targetY);
            model.targetX = e.X;
            model.targetY = e.Y;
            form.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                form.ShowClusters(form.grid.GetGraph(), false, model.Clusters);
            }
            else
            {
                form.grid.ShowPoints(form.grid.GetGraph(), false);
            }
            form.grid.GetPB().Image = form.grid.GetBMP();
        }

        //
        //Масштабирование
        //

        void PB_MouseWheel(object sender, MouseEventArgs e) //Управление масштабированием. Этот обработчик является общим для всех режимов
        {
            double X0 = form.grid.XtoGrid(e.X);
            double Y0 = form.grid.YtoGrid(e.Y);
            if (e.Delta > 0)
            {
                form.grid.SetScaleX(form.grid.GetScaleX() * 0.95);
                form.grid.SetScaleY(form.grid.GetScaleY() * 0.95);
            }
            else
            {
                form.grid.SetScaleX(form.grid.GetScaleX() * 1.05);
                form.grid.SetScaleY(form.grid.GetScaleY() * 1.05);
            }
            form.grid.SetOffsetX(X0 / form.grid.GetScaleX() - e.X);
            form.grid.SetOffsetY(Y0 / form.grid.GetScaleY() + e.Y);
            form.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                form.ShowClusters(form.grid.GetGraph(), false, model.Clusters);
            }
            else
            {
                form.grid.ShowPoints(form.grid.GetGraph(), false);
            }
            form.grid.GetPB().Image = form.grid.GetBMP();
        }

        //
        //Изменение размеров формы
        //

        void Carrier_SizeChanged(object sender, EventArgs e)
        {
            form.grid.GetPB().Width = form.Width - 2 * 8 - 2 * 12;
            form.grid.GetPB().Height = form.Height - 8 - 12 - form.grid.GetPB().Location.Y - 32;
            form.grid.SetBMP(new Bitmap(form.grid.GetPB().Width, form.grid.GetPB().Height));
            form.grid.SetGrpah(Graphics.FromImage(form.grid.GetBMP()));
            //MessageBox.Show("SizeChanged");//del
            form.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                form.ShowClusters(form.grid.GetGraph(), false, model.Clusters);
            }
            else
            {
                form.grid.ShowPoints(form.grid.GetGraph(), false);
            }
            form.grid.GetPB().Image = form.grid.GetBMP();
        }

        //
        //Разное
        //

        void ViewAllPointsTSB_Click(object sender, EventArgs e)
        {
            if (form.grid.GetItems() == null || form.grid.GetItems().Count == 0)
            {
                return;
            }
            double x_min, y_min, x_max, y_max;
            model.Boundaries(out x_min, out y_min, out x_max, out y_max);
            double w = x_max - x_min;
            double h = y_max - y_min;
            double scale = Algorithm.Max(w / (0.9 * form.grid.GetPB().Width), h / (0.9 * form.grid.GetPB().Height));
            form.grid.SetScaleX(scale);
            form.grid.SetScaleY(scale);
            form.grid.SetOffsetX((x_max + x_min) / (2 * form.grid.GetScaleX()) - form.grid.GetPB().Width / 2);
            form.grid.SetOffsetY((y_max + y_min) / (2 * form.grid.GetScaleY()) + form.grid.GetPB().Height / 2);
            form.grid.ShowGrid(false);
            if (model.Clusters != null && model.Clusters.Count > 0)
            {
                form.ShowClusters(form.grid.GetGraph(), false, model.Clusters);
            }
            else
            {
                form.grid.ShowPoints(form.grid.GetGraph(), false);
            }
            form.grid.GetPB().Image = form.grid.GetBMP();
        }
    }
}
