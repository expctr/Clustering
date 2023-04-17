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
    class MSTOptionsController
    {
        MSTOptionsForm form;

        MSTOptionsModel model;

        public MSTOptionsController(MSTOptionsForm form, MSTOptionsModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.Load += MSTOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                model.Options.ClustersNumber = int.Parse(form.GetClustersNumberTB().Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Число кластеров введено некорректно.");
                return;
            }
            if (model.Options.ClustersNumber < 1)
            {
                MessageBox.Show("Ошибка. Число кластеров должно быть положительным.");
                return;
            }
            form.parentModel.SetOptions(model.Options);
            MessageBox.Show("Настройки сохранены.");
        }

        private void MSTOptionsForm_Load(object sender, EventArgs e)
        {
            form.ShowOptions(model.Options);
        }
    }
}
