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
        public MainForm ParentWinfForm;

        List<Item> Items;

        Grid grid;

        public DrawingForm()
        {
            InitializeComponent();

            grid = new Grid(pictureBox1);
            grid.SetItems(Items);
            SaveListTSB.Click += SaveListTSB_Click;
            timer1.Interval = 1;
            grid.GetPB().MouseWheel += PB_MouseWheel;
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
            SizeChanged += Carrier_SizeChanged;
            ViewAllPointsTSB.Click += ViewAllPointsTSB_Click;
            ViewAllPointsTSB_Click(new object(), new EventArgs());
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {
            Show(true);
        }

        public void SetItems(List<Item> items, bool clone)
        {
            if (items == null)
            {
                Items = null;
                grid.SetItems(Items);
                return;
            }
            if (clone)
            {
                Items = new List<Item>(items);
            }
            Items = items;
            grid.SetItems(Items);
        }

        public void Show(bool show)
        {
            grid.Show(show);
        }

        public void DrawEraser(double eX, double eY, int a = 20) //Рисует ластик там, где расположен курсор
        {
            grid.GetGraph().FillRectangle(new Pen(Color.LightGray).Brush, (float)(eX - 10), (float)(eY - 10), 20, 20);
            grid.GetGraph().DrawRectangle(new Pen(Color.DarkGray), (float)(eX - 10), (float)(eY - 10), 20, 20);
            grid.GetPB().Image = grid.GetBMP();
        }

        void AddItemInArea_PB(double x1, double y1, double x3, double y3, bool show)
        {
            double X = RandomAlgo.RandomNumber(x1, x3);
            double Y = RandomAlgo.RandomNumber(y1, y3);
            if (grid.GetItems().Count < 5000)
            {
                grid.AddItem_PB(X, Y, false);
            }
            if (show)
            {
                grid.GetPB().Image = grid.GetBMP();
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
            grid.GetItems().RemoveAll(x => InRange(x1, x3, grid.XtoPB(x[0])) &&
            InRange(y1, y3, grid.YtoPB(x[1])));
        }

        string[] Item2DToRow(int ind)
        {
            string[] result = new string[4];
            result[0] = ind.ToString();
            result[1] = grid.GetItems()[ind].Name;
            result[2] = grid.GetItems()[ind][0].ToString();
            result[3] = grid.GetItems()[ind][1].ToString();
            return result;
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
        //Обработчики событий
        //

        //
        //Назад
        //
        void BackTSB_Click(object sendr, EventArgs e)
        {
            ParentWinfForm.Show();
            Close();
        }

        //
        //Сохранение списка объектов
        //
        void SaveListTSB_Click(object sender, EventArgs e)
        {
            if (grid.GetItems() == null || grid.GetItems().Count == 0)
            {
                MessageBox.Show("Ошибка. Нельзя сохранить пустой список объектов.");
                return;
            }
            ParentWinfForm.SetItems(grid.GetItems(), new string[] { "X", "Y" });
            MessageBox.Show("Список объектов сохранен.");
        }

        //
        //Режим перемещения
        //

        void MoveTSB_Click(object sender, EventArgs e) //Активирует режим перемещения
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            grid.GetPB().MouseDown += PB_MouseDown;
            grid.GetPB().MouseUp += PB_MouseUp;
            grid.GetPB().MouseMove += PB_MoveMouseMove;
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
            grid.SetOffsetX(grid.GetOffsetX() - (e.X - targetX));
            grid.SetOffsetY(grid.GetOffsetY() + e.Y - targetY);
            grid.Show(true);
            targetX = e.X;
            targetY = e.Y;
        }

        //
        //Режим рисования
        //

        void ClickTSMI_Click(object sender, EventArgs e) //Активирует режим рисования точек двойным кликом
        {
            PB_EventRefresh();
            grid.GetPB().MouseClick += PB_MouseClick;
        }

        void DrawTSSB_Click(object sender, EventArgs e) //Активирует режим рисования кривой линии
        {
            CurveLineTSMI_Click(sender, e);
        }

        void PB_MouseClick(object sender, MouseEventArgs e) //Добавляет в список новый предмет, согласно расположению курсора в момент двойного клика
        {
            if (grid.GetItems().Count < 5000)
            {
                grid.AddItem_PB(e.X, e.Y, true);
            }
        }

        void CurveLineTSMI_Click(object sender, EventArgs e) //Активирует режим рисования кривой линии
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            grid.GetPB().MouseDown += PB_TimerEnableMouseDown;
            grid.GetPB().MouseUp += PB_TimerUnenableMouseUp;
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
            double eX = Cursor.Position.X - Location.X - grid.GetPB().Location.X - 8;
            double eY = Cursor.Position.Y - Location.Y - grid.GetPB().Location.Y - 31;
            if (grid.GetItems().Count < 5000)
            {
                grid.AddItem_PB(eX, eY, true);
            }
        }

        void Spraying15ptTSMI_Click(object sender, EventArgs e) //Активирует режим распыления на 15pt
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            timer1.Interval = 20;
            grid.GetPB().MouseDown += PB_TimerEnableMouseDown;
            grid.GetPB().MouseUp += PB_TimerUnenableMouseUp;
            timer1.Tick += timer1_Spraying15ptTick;
        }

        void Spraying15ptInArea_PB(double eX, double eY)
        {
            if (grid.GetItems().Count < 4996)
            {
                AddItemInArea_PB(eX + 7.5, eY + 7.5, eX, eY, true);
                AddItemInArea_PB(eX, eY + 7.5, eX - 7.5, eY, true);
                AddItemInArea_PB(eX + 7.5, eY, eX, eY - 7.5, true);
                AddItemInArea_PB(eX, eY, eX - 7.5, eY - 7.5, true);
            }
        }

        void timer1_Spraying15ptTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - Location.X - grid.GetPB().Location.X - 8;
            double eY = Cursor.Position.Y - Location.Y - grid.GetPB().Location.Y - 31;
            Spraying15ptInArea_PB(eX, eY);
        }

        void Spraying30ptTSMI_Click(object sender, EventArgs e) //Активирует режим распыления на 30pt
        {
            PB_EventRefresh();
            timer1_EventRefresh();
            timer1.Interval = 70;
            grid.GetPB().MouseDown += PB_TimerEnableMouseDown;
            grid.GetPB().MouseUp += PB_TimerUnenableMouseUp;
            timer1.Tick += timer1_Spraying30ptTick;
        }

        void timer1_Spraying30ptTick(object sender, EventArgs e)
        {
            double eX = Cursor.Position.X - Location.X - grid.GetPB().Location.X - 8;
            double eY = Cursor.Position.Y - Location.Y - grid.GetPB().Location.Y - 31;
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
            grid.GetPB().MouseDown += PB_MouseDown;
            grid.GetPB().MouseUp += PB_MouseUp;
            grid.GetPB().MouseMove += PB_EraserMouseMove;
            grid.GetPB().MouseLeave += PB_EraserMouseLeave;
        }

        void EraseTSSB_Click(object sender, EventArgs e)
        {
            EraserTSMI_Click(sender, e);
        }

        void PB_EraserMouseMove(object sender, MouseEventArgs e)
        {
            if (InMove)
                DeleteItemsInArea_PB(e.X - 10, e.Y - 10, e.X + 10, e.Y + 10);
            grid.ShowGrid(false);
            grid.ShowPoints(grid.GetGraph(), false);
            grid.GetPB().Image = grid.GetBMP();
            DrawEraser(e.X, e.Y);
        }

        void PB_EraserMouseLeave(object sender, EventArgs e)
        {
            grid.Show(true);
        }

        void DeleteAllTSMI_Click(object sender, EventArgs e)
        {
            grid.GetItems().Clear();
            grid.Show(true);
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
            for (int i = 0; i < grid.GetItems().Count; ++i)
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
            grid.GetPB().Width = Width - grid.GetPB().Location.X - 28;
            grid.GetPB().Height = Height - 78;
            try
            {
                grid.SetBMP(new Bitmap(grid.GetPB().Width, grid.GetPB().Height));
                grid.SetGrpah(Graphics.FromImage(grid.GetBMP()));
                grid.Show(true);
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
            grid.GetPB().MouseUp -= PB_MouseUp;
            grid.GetPB().MouseUp -= PB_TimerUnenableMouseUp;
            grid.GetPB().MouseDown -= PB_MouseDown;
            grid.GetPB().MouseDown -= PB_TimerEnableMouseDown;
            grid.GetPB().MouseClick -= PB_MouseClick;
            grid.GetPB().MouseMove -= PB_MoveMouseMove;
            grid.GetPB().MouseMove -= PB_EraserMouseMove;
            grid.GetPB().MouseLeave -= PB_EraserMouseLeave;
        }

        void timer1_EventRefresh() //Очищает события timer1 от не общих обработчиков
        {
            timer1.Tick -= timer1_CurveLineTick;
            timer1.Tick -= timer1_Spraying15ptTick;
            timer1.Tick -= timer1_Spraying30ptTick;
        }

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
            grid.Show(true);
        }

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
            grid.Show(true);
        }
    }
}
