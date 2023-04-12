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

namespace Кластеризация
{
    class DBSCANOptionsController
    {
        DBSCANOptionsForm form;

        DBSCANOptionsModel model;

        public DBSCANOptionsController(DBSCANOptionsForm form, DBSCANOptionsModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHadlers()
        {
            form.Load += DBSCANOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }

        private void DBSCANOptionsForm_Load(object sender, EventArgs e)
        {
            form.ShowOptions(model.Options);
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                model.Options.ReachabilityRadius = double.Parse(form.GetReachabilityRadiusTB().Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Радиус достижимости введен некорректно.");
                return;
            }
            if (model.Options.ReachabilityRadius < 0)
            {
                MessageBox.Show("Ошибка. Радиус достижимости не может быть отрицательным.");
                return;
            }
            try
            {
                model.Options.Threshold = int.Parse(form.GetThresholdTB().Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Порог кучности введен некорректно.");
                return;
            }
            if (model.Options.Threshold < 0)
            {
                MessageBox.Show("Ошибка. Порог кучности не может быть отрицательным.");
                return;
            }
            form.ParentWinForm.SetOptions(model.Options);
            MessageBox.Show("Настройки сохранены.");
        }
    }
}
