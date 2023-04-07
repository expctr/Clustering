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

        Item item;
        string[] ColsNames;
        Cluster cluster;
        int clusterInd;

        public ObjectInfoForm(Item _item, string[] colsNames, Cluster _cluster,
            int _clusterInd) : this()
        {
            item = _item;
            ColsNames = colsNames;
            cluster = _cluster;
            clusterInd = _clusterInd;
            FindB.Click += FindB_Click;
        }

        private void ObjectInfoForm_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }

        void ShowInfo()
        {
            ObjectNameL.Text = $"Название объекта: {item.Name}";
            ObjectIndexL.Text = $"Индекс объекта (в общем списке): {item.Index}";
            ClusterIndexL.Text = $"Индекс кластера: {clusterInd}";
            double distance = EuclideanGeometry.Distance(item.GetCoordinates,
                cluster.GetCentre);
            DistanceL.Text = $"Расстояние до центра кластера: " +
                $"{distance}";
            ShowDGV();

        }

        void ShowDGV()
        {
            ObjectInfoDGV.ColumnCount = ColsNames.Length + 2;
            for (int i = 0; i < ColsNames.Length + 2; ++i)
            {
                ObjectInfoDGV.Columns[i].Name = (i + 1).ToString();
            }
            string[] _ColsNames = new string[ColsNames.Length + 2];
            _ColsNames[0] = "Индекс";
            _ColsNames[1] = "Название";
            for (int i = 2; i < _ColsNames.Length; ++i)
            {
                _ColsNames[i] = ColsNames[i - 2];
            }
            ObjectInfoDGV.Rows.Add(_ColsNames);
            ObjectInfoDGV.Rows.Add(item.ToRow());
        }

        private void FindB_Click(object sender, EventArgs e)
        {
            FindB.Enabled = false;
            double radius = 0;
            try
            {
                radius = double.Parse(RadiusTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Радиус введен некорректно.");
                FindB.Enabled = true;
                return;
            }
            if (radius < 0)
            {
                MessageBox.Show("Ошибка. Радиус не может быть отрицательным");
                FindB.Enabled = true;
                return;
            }
            int number = cluster.CountInRadius(item.GetCoordinates, radius);
            NumberOfElementsInRadius.Text = "Число элементов около\n" +
                "объекта в указанном\n" +
                $"радиусе: {number - 1}";
            FindB.Enabled = true;
        }

        private void ObjectInfoForm_SizeChanged(object sender, EventArgs e)
        {
            ObjectInfoDGV.Width = Width - 2 * 8 - 2 * 12;
        }
    }
}