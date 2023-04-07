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
    public partial class DrawingForm : Form
    {
        public DrawingForm()
        {
            InitializeComponent();
        }
        public MainForm ParentWinfForm;
        DrawingFormViewMaster ViewMaster;
        List<Item> Items;
        private void DrawingForm_Load(object sender, EventArgs e)
        {
            ViewMaster = new DrawingFormViewMaster(Items, pictureBox1, SaveListTSB, MoveTSB,
                DrawTSSB, ClickTSMI, CurveLineTSMI, Spraying15ptTSMI, Spraying30ptTSMI, timer1,
                this, EraseTSSB, EraserTSMI, DeleteAllTSMI, ShowItemsTSB, ViewAllPointsTSB);
            ViewMaster.Show(true);
        }
        public void SetItems(List<Item> items, bool clone)
        {
            if (items == null)
            {
                Items = null;
                return;
            }
            if (clone)
            {
                Items = new List<Item>(items);
            }
            Items = items;
        }
    }

    class DrawingFormViewMaster : Grid //Класс реализует координатную евклидову плоскость и взаимодействует с элементами пользовательского интерфейса
    {
        ToolStripButton SaveListTSB;
        ToolStripButton MoveTSB;
        ToolStripSplitButton DrawTSSB;
        ToolStripMenuItem ClickTSMI, CurveLineTSMI, Spraying15ptTSMI, Spraying30ptTSMI;
        ToolStripSplitButton EraseTSSB;
        ToolStripMenuItem EraserTSMI;
        ToolStripMenuItem DeleteAllTSMI;
        Timer timer1;
        DrawingForm Carrier;
        ToolStripButton ShowItemsTSB;
        ToolStripButton ViewAllPointsTSB;
        public DrawingFormViewMaster(List<Item> items, PictureBox pb, ToolStripButton saveListTSB,
            ToolStripButton moveTSB, ToolStripSplitButton drawTSSB,
            ToolStripMenuItem сlickTSMI, ToolStripMenuItem curveLineTSMI,
            ToolStripMenuItem spraying15ptTSMI, ToolStripMenuItem spraying30ptTSMI,
            Timer _timer1, DrawingForm carrier, ToolStripSplitButton eraseTSSB,
            ToolStripMenuItem eraserTSMI, ToolStripMenuItem deleteAllTSMI,
            ToolStripButton showItemsTSB, ToolStripButton viewAllPointsTSB) : base(pb)
        {
            Items = items;
            SaveListTSB = saveListTSB;
            SaveListTSB.Click += SaveListTSB_Click;
            MoveTSB = moveTSB;
            DrawTSSB = drawTSSB;
            ClickTSMI = сlickTSMI;
            CurveLineTSMI = curveLineTSMI;
            Spraying15ptTSMI = spraying15ptTSMI;
            Spraying30ptTSMI = spraying30ptTSMI;
            timer1 = _timer1;
            EraseTSSB = eraseTSSB;
            EraserTSMI = eraserTSMI;
            timer1.Interval = 1;
            Carrier = carrier;
            DeleteAllTSMI = deleteAllTSMI;
            ShowItemsTSB = showItemsTSB;
            PB.MouseWheel += PB_MouseWheel;
            MoveTSB.Click += MoveTSB_Click;
            DrawTSSB.Click += DrawTSSB_Click;
            ClickTSMI.Click += ClickTSMI_Click;
            CurveLineTSMI.Click += CurveLineTSMI_Click;
            Spraying15ptTSMI.Click += Spraying15ptTSMI_Click;
            Spraying30ptTSMI.Click += Spraying30ptTSMI_Click;
            EraseTSSB.Click += EraseTSSB_Click;
            EraserTSMI.Click += EraserTSMI_Click;
            DeleteAllTSMI.Click += DeleteAllTSMI_Click;
            ShowItemsTSB.Click += ShowItemsTSB_Click;
            Carrier.SizeChanged += Carrier_SizeChanged;
            ViewAllPointsTSB = viewAllPointsTSB;
            ViewAllPointsTSB.Click += ViewAllPointsTSB_Click;
            ViewAllPointsTSB_Click(new object(), new EventArgs());
        }

        public void DrawEraser(double eX, double eY, int a = 20) //Рисует ластик там, где расположен курсор
        {
            graph.FillRectangle(new Pen(Color.LightGray).Brush, (float)(eX - 10), (float)(eY - 10), 20, 20);
            graph.DrawRectangle(new Pen(Color.DarkGray), (float)(eX - 10), (float)(eY - 10), 20, 20);
            PB.Image = bmp;
        }

        void AddItemInArea_PB(double x1, double y1, double x3, double y3, bool show)
        {
            double X = RandomAlgo.RandomNumber(x1, x3);
            double Y = RandomAlgo.RandomNumber(y1, y3);
            if (Items.Count < 5000)
            {
                AddItem_PB(X, Y, false);
            }
            if (show)
            {
                PB.Image = bmp;
            }
        }
        bool InRange(double a, double b, double x)
        {
            if (a > b)
            {
                double buf = b;
                b = a;
                a = buf;
            }
            return a <= x && x <= b;
        }
        void DeleteItemsInArea_PB(double x1, double y1, double x3, double y3)
        {
            Items.RemoveAll(x => InRange(x1, x3, XtoPB(x[0])) &&
            InRange(y1, y3, YtoPB(x[1])));
        }
        string[] Item2DToRow(int ind)
        {
            string[] result = new string[4];
            result[0] = ind.ToString();
            result[1] = Items[ind].Name;
            result[2] = Items[ind][0].ToString();
            result[3] = Items[ind][1].ToString();
            return result;
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
        //Обработчики событий
        //

        //
        //Назад
        //
        void BackTSB_Click(object sendr, EventArgs e)
        {
            Carrier.ParentWinfForm.Show();
            Carrier.Close();
        }

        //
        //Сохранение списка объектов
        //
        void SaveListTSB_Click(object sender, EventArgs e)
        {
            if (Items == null || Items.Count == 0)
            {
                MessageBox.Show("Ошибка. Нельзя сохранить пустой список объектов.");
                return;
            }
            Carrier.ParentWinfForm.SetItems(Items, new string[] { "X", "Y" });
            MessageBox.Show("Список объектов сохранен.");
        }
        //
        //Режим перемещения
        //
        void MoveTSB_Click(object sender, EventArgs e) //Активирует режим перемещения
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            PB.MouseDown += PB_MouseDown;
            PB.MouseUp += PB_MouseUp;
            PB.MouseMove += PB_MoveMouseMove;
        }
        bool InMove; //Мышь зажата
        double targetX, targetY; //Запоминание положения мыши в некоторый момент в координатах PB
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
            Show(true);
            targetX = e.X;
            targetY = e.Y;
        }
        //
        //Режим рисования
        //
        void ClickTSMI_Click(object sender, EventArgs e) //Активирует режим рисования точек двойным кликом
        {
            PB_EventRefresh();
            PB.MouseClick += PB_MouseClick;
        }
        void DrawTSSB_Click(object sender, EventArgs e) //Активирует режим рисования кривой линии
        {
            CurveLineTSMI_Click(sender, e);
        }
        void PB_MouseClick(object sender, MouseEventArgs e) //Добавляет в список новый предмет, согласно расположению курсора в момент двойного клика
        {
            if (Items.Count < 5000)
            {
                AddItem_PB(e.X, e.Y, true);
            }
        }
        void CurveLineTSMI_Click(object sender, EventArgs e) //Активирует режим рисования кривой линии
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            PB.MouseDown += PB_TimerEnableMouseDown;
            PB.MouseUp += PB_TimerUnenableMouseUp;
            timer1.Tick += timer1_CurveLineTick;
        }
        void PB_TimerEnableMouseDown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        void PB_TimerUnenableMouseUp(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        void timer1_CurveLineTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - Carrier.Location.X - PB.Location.X - 8;
            double eY = Cursor.Position.Y - Carrier.Location.Y - PB.Location.Y - 31;
            if (Items.Count < 5000)
            {
                AddItem_PB(eX, eY, true);
            }
        }
        void Spraying15ptTSMI_Click(object sender, EventArgs e) //Активирует режим распыления на 15pt
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            timer1.Interval = 20;
            PB.MouseDown += PB_TimerEnableMouseDown;
            PB.MouseUp += PB_TimerUnenableMouseUp;
            timer1.Tick += timer1_Spraying15ptTick;
        }
        void Spraying15ptInArea_PB(double eX, double eY)
        {
            if (Items.Count < 4996)
            {
                AddItemInArea_PB(eX + 7.5, eY + 7.5, eX, eY, true);
                AddItemInArea_PB(eX, eY + 7.5, eX - 7.5, eY, true);
                AddItemInArea_PB(eX + 7.5, eY, eX, eY - 7.5, true);
                AddItemInArea_PB(eX, eY, eX - 7.5, eY - 7.5, true);
            }
        }
        void timer1_Spraying15ptTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - Carrier.Location.X - PB.Location.X - 8;
            double eY = Cursor.Position.Y - Carrier.Location.Y - PB.Location.Y - 31;
            Spraying15ptInArea_PB(eX, eY);
        }
        void Spraying30ptTSMI_Click(object sender, EventArgs e) //Активирует режим распыления на 30pt
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            timer1.Interval = 70;
            PB.MouseDown += PB_TimerEnableMouseDown;
            PB.MouseUp += PB_TimerUnenableMouseUp;
            timer1.Tick += timer1_Spraying30ptTick;
        }
        void timer1_Spraying30ptTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - Carrier.Location.X - PB.Location.X - 8;
            double eY = Cursor.Position.Y - Carrier.Location.Y - PB.Location.Y - 31;
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
            PB.MouseDown += PB_MouseDown;
            PB.MouseUp += PB_MouseUp;
            PB.MouseMove += PB_EraserMouseMove;
            PB.MouseLeave += PB_EraserMouseLeave;
        }
        void EraseTSSB_Click(object sender, EventArgs e)
        {
            EraserTSMI_Click(sender, e);
        }
        void PB_EraserMouseMove(object sender, MouseEventArgs e)
        {
            if (InMove)
                DeleteItemsInArea_PB(e.X - 10, e.Y - 10, e.X + 10, e.Y + 10);
            ShowGrid(false);
            ShowPoints(graph, false);
            PB.Image = bmp;
            DrawEraser(e.X, e.Y);
        }
        void PB_EraserMouseLeave(object sender, EventArgs e)
        {
            Show(true);
        }
        void DeleteAllTSMI_Click(object sender, EventArgs e)
        {
            Items.Clear();
            Show(true);
        }
        //
        //Отображение предметов
        //

        void ShowItemsTSB_Click(object sender, EventArgs e)
        {
            InfoForm fm2 = new InfoForm();
            fm2.dataGridView1.ColumnCount = 4;
            fm2.dataGridView1.Columns[0].Name = "Индекс";
            fm2.dataGridView1.Columns[1].Name = "Название";
            fm2.dataGridView1.Columns[2].Name = "X";
            fm2.dataGridView1.Columns[3].Name = "Y";
            for (int i = 0; i < Items.Count; ++i)
            {
                fm2.dataGridView1.Rows.Add(Item2DToRow(i));
            }
            fm2.ShowDialog();
        }

        //
        //Изменение размеров формы и элементов
        //
        void Carrier_SizeChanged(object sender, EventArgs e)
        {
            PB.Width = Carrier.Width - PB.Location.X - 28;
            PB.Height = Carrier.Height - 78;
            try
            {
                bmp = new Bitmap(PB.Width, PB.Height);
                graph = Graphics.FromImage(bmp);
                Show(true);
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
            PB.MouseUp -= PB_MouseUp;
            PB.MouseUp -= PB_TimerUnenableMouseUp;
            PB.MouseDown -= PB_MouseDown;
            PB.MouseDown -= PB_TimerEnableMouseDown;
            PB.MouseClick -= PB_MouseClick;
            PB.MouseMove -= PB_MoveMouseMove;
            PB.MouseMove -= PB_EraserMouseMove;
            PB.MouseLeave -= PB_EraserMouseLeave;
        }
        void timer1_EventRefresh() //Очищает события timer1 от не общих обработчиков
        {
            timer1.Tick -= timer1_CurveLineTick;
            timer1.Tick -= timer1_Spraying15ptTick;
            timer1.Tick -= timer1_Spraying30ptTick;
        }

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
            Show(true);
        }

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
            Show(true);
        }

    }//class DrawingFormViewMaster

}
