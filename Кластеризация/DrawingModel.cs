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
    class DrawingModel
    {
        DrawingForm form;

        List<Item> Items;

        public bool InMove; //Мышь зажата

        public double targetX, targetY; //Запоминание положения мыши в некоторый момент в координатах PB

        public DrawingModel(DrawingForm form)
        {
            this.form = form;
        }

        public void SetItems(List<Item> items, bool clone)
        {
            if (items == null)
            {
                Items = null;
                form.grid.SetItems(Items);
                return;
            }
            if (clone)
            {
                Items = new List<Item>(items);
            }
            Items = items;
            form.grid.SetItems(Items);
        }

        public void AddItemInArea_PB(double x1, double y1, double x3, double y3, bool show)
        {
            double X = RandomAlgo.RandomNumber(x1, x3);
            double Y = RandomAlgo.RandomNumber(y1, y3);
            if (form.grid.GetItems().Count < 5000)
            {
                form.grid.AddItem_PB(X, Y, false);
            }
            if (show)
            {
                form.grid.GetPB().Image = form.grid.GetBMP();
            }
        }

        public bool InRange(double a, double b, double x)
        {
            if (a > b)
            {
                double buf = b;
                b = a;
                a = buf;
            }
            return a <= x && x <= b;
        }

        public void DeleteItemsInArea_PB(double x1, double y1, double x3, double y3)
        {
            form.grid.GetItems().RemoveAll(x => InRange(x1, x3, form.grid.XtoPB(x[0])) &&
            InRange(y1, y3, form.grid.YtoPB(x[1])));
        }

        public string[] Item2DToRow(int ind)
        {
            string[] result = new string[4];
            result[0] = ind.ToString();
            result[1] = form.grid.GetItems()[ind].Name;
            result[2] = form.grid.GetItems()[ind][0].ToString();
            result[3] = form.grid.GetItems()[ind][1].ToString();
            return result;
        }

        public void Boundaries(out double lowerX, out double lowerY, out double upperX,
            out double upperY)
        {
            if (form.grid.GetItems() == null || form.grid.GetItems().Count == 0)
            {
                lowerX = lowerY = upperX = upperY = 0;
                return;
            }
            lowerX =
                Algorithm.FindMin(form.grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            lowerY =
                Algorithm.FindMin(form.grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
            upperX =
                Algorithm.FindMax(form.grid.GetItems(), (item1, item2) => item1[0].CompareTo(item2[0]))[0];
            upperY =
                Algorithm.FindMax(form.grid.GetItems(), (item1, item2) => item1[1].CompareTo(item2[1]))[1];
        }
    }
}
