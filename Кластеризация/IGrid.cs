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
    interface IGrid
    {
        void SetItems(List<Item> items);

        List<Item> GetItems();

        PictureBox GetPB();

        void ShowGrid(Graphics graph_, bool show);

        Graphics GetGraph();

        void SetGrpah(Graphics graph);

        void Show(Graphics graph_, bool show);

        void DrawPoints_Grid(List<double[]> points, Color color, Graphics graph_, bool show);

        void DrawSquare_Grid(double Xgrid, double Ygrid, Color color, bool InverseCentre, Graphics graph_,
            int aPB = 8);

        Bitmap GetBMP();

        void SetBMP(Bitmap bmp);

        void SetOffsetX(double _offsetX);

        void SetOffsetY(double _offsetY);

        double GetOffsetX();

        double GetOffsetY();

        double XtoGrid(double eX);

        double YtoGrid(double eY);

        void SetScaleX(double _scaleX);

        void SetScaleY(double _scaleY);

        double GetScaleX();

        double GetScaleY();
    }
}
