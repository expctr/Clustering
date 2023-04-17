using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlgorithmLib;
using ItemLib;
using RandomAlgoLib;

namespace Кластеризация
{
    class DrawingController
    {
        private DrawingModel model;

        private DrawingForm form;

        public DrawingController(DrawingModel model, DrawingForm form)
        {
            this.model = model;
            this.form = form;
        }

        public void AddEventHandlers()
        {
            form.Load += DrawingForm_Load;
            form.GetSaveListTSB().Click += SaveListTSB_Click;
            form.GetTimer1().Interval = 1;
            form.grid.GetPB().MouseWheel += PB_MouseWheel;
            form.GetMoveTSB().Click += MoveTSB_Click;
            form.GetDrawTSSB().Click += DrawTSSB_Click;
            form.GetClickTSMI().Click += ClickTSMI_Click;
            form.GetCurveLineTSMI().Click += CurveLineTSMI_Click;
            form.GetSpraying15ptTSMI().Click += Spraying15ptTSMI_Click;
            form.GetSpraying30ptTSMI().Click += Spraying30ptTSMI_Click;
            form.GetEraseTSSB().Click += EraseTSSB_Click;
            form.GetEraserTSMI().Click += EraserTSMI_Click;
            form.GetDeleteAllTSMI().Click += DeleteAllTSMI_Click;
            form.GetShowItemsTSB().Click += ShowItemsTSB_Click;
            form.SizeChanged += Carrier_SizeChanged;
            form.GetViewAllPointsTSB().Click += ViewAllPointsTSB_Click;
            ViewAllPointsTSB_Click(new object(), new EventArgs());
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            form.Show(true);
        }

        //
        //Обработчики событий
        //

        //
        //Назад
        //
        void BackTSB_Click(object sendr, EventArgs e)
        {
            form.ParentWinfForm.Show();
            form.Close();
        }

        //
        //Сохранение списка объектов
        //
        void SaveListTSB_Click(object sender, EventArgs e)
        {
            if (form.grid.GetItems() == null || form.grid.GetItems().Count == 0)
            {
                MessageBox.Show("Ошибка. Нельзя сохранить пустой список объектов.");
                return;
            }
            form.parentModel.SetItems(form.grid.GetItems(), new string[] { "X", "Y" });
            MessageBox.Show("Список объектов сохранен.");
        }

        //
        //Режим перемещения
        //

        void MoveTSB_Click(object sender, EventArgs e) //Активирует режим перемещения
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            form.grid.GetPB().MouseDown += PB_MouseDown;
            form.grid.GetPB().MouseUp += PB_MouseUp;
            form.grid.GetPB().MouseMove += PB_MoveMouseMove;
        }

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
            form.grid.Show(true);
            model.targetX = e.X;
            model.targetY = e.Y;
        }

        //
        //Режим рисования
        //

        void ClickTSMI_Click(object sender, EventArgs e) //Активирует режим рисования точек двойным кликом
        {
            PB_EventRefresh();
            form.grid.GetPB().MouseClick += PB_MouseClick;
        }

        void DrawTSSB_Click(object sender, EventArgs e) //Активирует режим рисования кривой линии
        {
            CurveLineTSMI_Click(sender, e);
        }

        void PB_MouseClick(object sender, MouseEventArgs e) //Добавляет в список новый предмет, согласно расположению курсора в момент двойного клика
        {
            if (form.grid.GetItems().Count < 5000)
            {
                form.grid.AddItem_PB(e.X, e.Y, true);
            }
        }

        void CurveLineTSMI_Click(object sender, EventArgs e) //Активирует режим рисования кривой линии
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            form.grid.GetPB().MouseDown += PB_TimerEnableMouseDown;
            form.grid.GetPB().MouseUp += PB_TimerUnenableMouseUp;
            form.GetTimer1().Tick += timer1_CurveLineTick;
        }

        void PB_TimerEnableMouseDown(object sender, EventArgs e)
        {
            form.GetTimer1().Enabled = true;
        }

        void PB_TimerUnenableMouseUp(object sender, EventArgs e)
        {
            form.GetTimer1().Enabled = false;
        }

        void timer1_CurveLineTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - form.Location.X - form.grid.GetPB().Location.X - 8;
            double eY = Cursor.Position.Y - form.Location.Y - form.grid.GetPB().Location.Y - 31;
            if (form.grid.GetItems().Count < 5000)
            {
                form.grid.AddItem_PB(eX, eY, true);
            }
        }

        void Spraying15ptTSMI_Click(object sender, EventArgs e) //Активирует режим распыления на 15pt
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            form.GetTimer1().Interval = 20;
            form.grid.GetPB().MouseDown += PB_TimerEnableMouseDown;
            form.grid.GetPB().MouseUp += PB_TimerUnenableMouseUp;
            form.GetTimer1().Tick += timer1_Spraying15ptTick;
        }

        void Spraying15ptInArea_PB(double eX, double eY)
        {
            if (form.grid.GetItems().Count < 4996)
            {
                model.AddItemInArea_PB(eX + 7.5, eY + 7.5, eX, eY, true);
                model.AddItemInArea_PB(eX, eY + 7.5, eX - 7.5, eY, true);
                model.AddItemInArea_PB(eX + 7.5, eY, eX, eY - 7.5, true);
                model.AddItemInArea_PB(eX, eY, eX - 7.5, eY - 7.5, true);
            }
        }

        void timer1_Spraying15ptTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - form.Location.X - form.grid.GetPB().Location.X - 8;
            double eY = Cursor.Position.Y - form.Location.Y - form.grid.GetPB().Location.Y - 31;
            Spraying15ptInArea_PB(eX, eY);
        }

        void Spraying30ptTSMI_Click(object sender, EventArgs e) //Активирует режим распыления на 30pt
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            form.GetTimer1().Interval = 70;
            form.grid.GetPB().MouseDown += PB_TimerEnableMouseDown;
            form.grid.GetPB().MouseUp += PB_TimerUnenableMouseUp;
            form.GetTimer1().Tick += timer1_Spraying30ptTick;
        }

        void timer1_Spraying30ptTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - form.Location.X - form.grid.GetPB().Location.X - 8;
            double eY = Cursor.Position.Y - form.Location.Y - form.grid.GetPB().Location.Y - 31;
            Spraying15ptInArea_PB(eX + 7.5, eY + 7.5);
            Spraying15ptInArea_PB(eX - 7.5, eY + 7.5);
            Spraying15ptInArea_PB(eX + 7.5, eY - 7.5);
            Spraying15ptInArea_PB(eX - 7.5, eY - 7.5);
        }

        //
        //Режим стирания
        //

        void EraserTSMI_Click(object sender, EventArgs e) //Активирует режим стирания
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            form.grid.GetPB().MouseDown += PB_MouseDown;
            form.grid.GetPB().MouseUp += PB_MouseUp;
            form.grid.GetPB().MouseMove += PB_EraserMouseMove;
            form.grid.GetPB().MouseLeave += PB_EraserMouseLeave;
        }

        void EraseTSSB_Click(object sender, EventArgs e)
        {
            EraserTSMI_Click(sender, e);
        }

        void PB_EraserMouseMove(object sender, MouseEventArgs e)
        {
            if (model.InMove)
                model.DeleteItemsInArea_PB(e.X - 10, e.Y - 10, e.X + 10, e.Y + 10);
            form.grid.ShowGrid(false);
            form.grid.ShowPoints(form.grid.GetGraph(), false);
            form.grid.GetPB().Image = form.grid.GetBMP();
            form.DrawEraser(e.X, e.Y);
        }

        void PB_EraserMouseLeave(object sender, EventArgs e)
        {
            form.grid.Show(true);
        }

        void DeleteAllTSMI_Click(object sender, EventArgs e)
        {
            form.grid.GetItems().Clear();
            form.grid.Show(true);
        }

        //
        //Отображение предметов
        //

        void ShowItemsTSB_Click(object sender, EventArgs e)
        {
            InfoForm fm2 = new InfoForm();
            InfoModel infoModel = new InfoModel(fm2);
            InfoController infoController = new InfoController(fm2, infoModel);
            infoController.AddEventHandlers();
            fm2.dataGridView1.ColumnCount = 4;
            fm2.dataGridView1.Columns[0].Name = "Индекс";
            fm2.dataGridView1.Columns[1].Name = "Название";
            fm2.dataGridView1.Columns[2].Name = "X";
            fm2.dataGridView1.Columns[3].Name = "Y";
            for (int i = 0; i < form.grid.GetItems().Count; ++i)
            {
                fm2.dataGridView1.Rows.Add(model.Item2DToRow(i));
            }
            fm2.ShowDialog();
        }

        //
        //Изменение размеров формы и элементов
        //

        void Carrier_SizeChanged(object sender, EventArgs e)
        {
            form.grid.GetPB().Width = form.Width - form.grid.GetPB().Location.X - 28;
            form.grid.GetPB().Height = form.Height - 78;
            try
            {
                form.grid.SetBMP(new Bitmap(form.grid.GetPB().Width, form.grid.GetPB().Height));
                form.grid.SetGrpah(Graphics.FromImage(form.grid.GetBMP()));
                form.grid.Show(true);
            }
            catch
            {

            }
        }

        //
        //Разное
        //

        void PB_EventRefresh() //Очищает события  PB от не общих обработчиков
        {
            form.grid.GetPB().MouseUp -= PB_MouseUp;
            form.grid.GetPB().MouseUp -= PB_TimerUnenableMouseUp;
            form.grid.GetPB().MouseDown -= PB_MouseDown;
            form.grid.GetPB().MouseDown -= PB_TimerEnableMouseDown;
            form.grid.GetPB().MouseClick -= PB_MouseClick;
            form.grid.GetPB().MouseMove -= PB_MoveMouseMove;
            form.grid.GetPB().MouseMove -= PB_EraserMouseMove;
            form.grid.GetPB().MouseLeave -= PB_EraserMouseLeave;
        }

        void timer1_EventRefresh() //Очищает события timer1 от не общих обработчиков
        {
            form.GetTimer1().Tick -= timer1_CurveLineTick;
            form.GetTimer1().Tick -= timer1_Spraying15ptTick;
            form.GetTimer1().Tick -= timer1_Spraying30ptTick;
        }

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
            form.grid.Show(true);
        }

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
            form.grid.Show(true);
        }
    }
}
