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

        public Grid grid;

        public DrawingForm()
        {
            InitializeComponent();

            grid = new Grid(pictureBox1);
            grid.SetItems(Items);
        }

        public DrawingForm(MainForm parentWinForm) : this()
        {
            ParentWinfForm = parentWinForm;
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

        public ToolStripButton GetSaveListTSB()
        {
            return SaveListTSB;
        }

        public Timer GetTimer1()
        {
            return timer1;
        }

        public ToolStripButton GetMoveTSB()
        {
            return MoveTSB;
        }

        public ToolStripSplitButton GetDrawTSSB()
        {
            return DrawTSSB;
        }

        public ToolStripMenuItem GetClickTSMI()
        {
            return ClickTSMI;
        }

        public ToolStripMenuItem GetCurveLineTSMI()
        {
            return CurveLineTSMI;
        }

        public ToolStripMenuItem GetSpraying15ptTSMI()
        {
            return Spraying15ptTSMI;
        }

        public ToolStripMenuItem GetSpraying30ptTSMI()
        {
            return Spraying30ptTSMI;
        }

        public ToolStripSplitButton GetEraseTSSB()
        {
            return EraseTSSB;
        }

        public ToolStripMenuItem GetEraserTSMI()
        {
            return EraserTSMI;
        }

        public ToolStripMenuItem GetDeleteAllTSMI()
        {
            return DeleteAllTSMI;
        }

        public ToolStripButton GetShowItemsTSB()
        {
            return ShowItemsTSB;
        }

        public ToolStripButton GetViewAllPointsTSB()
        {
            return ViewAllPointsTSB;
        }
    }
}
