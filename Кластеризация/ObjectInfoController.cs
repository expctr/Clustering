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
    class ObjectInfoController
    {
        private ObjectInfoForm form;

        private ObjectInfoModel model;

        public ObjectInfoController(ObjectInfoForm form, ObjectInfoModel model)
        {
            this.form = form;
            this.model = model;
        }

        private void ObjectInfoForm_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }

        public void AddEventHandlers()
        {
            form.Load += ObjectInfoForm_Load;
            form.GetFindB().Click += FindB_Click;
            form.SizeChanged += ObjectInfoForm_SizeChanged;
        }

        void ShowInfo()
        {
            form.GetObjectNameL().Text = $"Название объекта: {model.item.Name}";
            form.GetObjectIndexL().Text = $"Индекс объекта (в общем списке): {model.item.Index}";
            form.GetClusterIndexL().Text = $"Индекс кластера: {model.clusterInd}";
            double distance = EuclideanGeometry.Distance(model.item.GetCoordinates,
                model.cluster.GetCentre);
            form.GetDistanceL().Text = $"Расстояние до центра кластера: " +
                $"{distance}";
            ShowDGV();

        }

        void ShowDGV()
        {
            form.GetObjectInfoDGV().ColumnCount = model.ColsNames.Length + 2;
            for (int i = 0; i < model.ColsNames.Length + 2; ++i)
            {
                form.GetObjectInfoDGV().Columns[i].Name = (i + 1).ToString();
            }
            string[] _ColsNames = new string[model.ColsNames.Length + 2];
            _ColsNames[0] = "Индекс";
            _ColsNames[1] = "Название";
            for (int i = 2; i < _ColsNames.Length; ++i)
            {
                _ColsNames[i] = model.ColsNames[i - 2];
            }
            form.GetObjectInfoDGV().Rows.Add(_ColsNames);
            form.GetObjectInfoDGV().Rows.Add(model.item.ToRow());
        }

        private void FindB_Click(object sender, EventArgs e)
        {
            form.GetFindB().Enabled = false;
            double radius = 0;
            try
            {
                radius = double.Parse(form.GetRadiusTB().Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Радиус введен некорректно.");
                form.GetFindB().Enabled = true;
                return;
            }
            if (radius < 0)
            {
                MessageBox.Show("Ошибка. Радиус не может быть отрицательным");
                form.GetFindB().Enabled = true;
                return;
            }
            int number = model.cluster.CountInRadius(model.item.GetCoordinates, radius);
            form.GetNumberOfElementsInRadius().Text = "Число элементов около\n" +
                "объекта в указанном\n" +
                $"радиусе: {number - 1}";
            form.GetFindB().Enabled = true;
        }

        private void ObjectInfoForm_SizeChanged(object sender, EventArgs e)
        {
            form.GetObjectInfoDGV().Width = form.GetWidth() - 2 * 8 - 2 * 12;
        }
    }
}
