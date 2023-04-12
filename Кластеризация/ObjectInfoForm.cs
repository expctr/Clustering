using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemLib;
using ClusteringLib;
using AlgorithmLib;
using EuclideanGeometryLib;

namespace Кластеризация
{
    public partial class ObjectInfoForm : Form
    {
        public ObjectInfoForm()
        {
            InitializeComponent();
        }

        public void ShowInfo(Item item, int clusterInd, Cluster cluster, string[] ColsNames)
        {
            GetObjectNameL().Text = $"Название объекта: {item.Name}";
            GetObjectIndexL().Text = $"Индекс объекта (в общем списке): {item.Index}";
            GetClusterIndexL().Text = $"Индекс кластера: {clusterInd}";
            double distance = EuclideanGeometry.Distance(item.GetCoordinates,
                cluster.GetCentre);
            GetDistanceL().Text = $"Расстояние до центра кластера: " +
                $"{distance}";
            ShowDGV(ColsNames, item);

        }

        void ShowDGV(string[] ColsNames, Item item)
        {
            GetObjectInfoDGV().ColumnCount = ColsNames.Length + 2;
            for (int i = 0; i < ColsNames.Length + 2; ++i)
            {
                GetObjectInfoDGV().Columns[i].Name = (i + 1).ToString();
            }
            string[] _ColsNames = new string[ColsNames.Length + 2];
            _ColsNames[0] = "Индекс";
            _ColsNames[1] = "Название";
            for (int i = 2; i < _ColsNames.Length; ++i)
            {
                _ColsNames[i] = ColsNames[i - 2];
            }
            GetObjectInfoDGV().Rows.Add(_ColsNames);
            GetObjectInfoDGV().Rows.Add(item.ToRow());
        }

        public Label GetObjectNameL()
        {
            return ObjectNameL;
        }

        public Label GetObjectIndexL()
        {
            return ObjectIndexL;
        }

        public Label GetClusterIndexL()
        {
            return ClusterIndexL;
        }

        public Label GetDistanceL()
        {
            return DistanceL;
        }

        public DataGridView GetObjectInfoDGV()
        {
            return ObjectInfoDGV;
        }

        public Button GetFindB()
        {
            return FindB;
        }

        public TextBox GetRadiusTB()
        {
            return RadiusTB;
        }

        public Label GetNumberOfElementsInRadius()
        {
            return NumberOfElementsInRadius;
        }

        public int GetWidth()
        {
            return Width;
        }
    }
}