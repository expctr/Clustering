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
    class KMeansOptionsController
    {
        KMeansOptionsForm form;

        KMeansOptionsModel model;

        public KMeansOptionsController(KMeansOptionsForm form, KMeansOptionsModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.Load += KMeansOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                model.Options.ClustersNumber = int.Parse(form.GetNodesNumberTB().Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Предельное число кластеров введено некорректно.");
                return;
            }
            if (model.Options.ClustersNumber < 1)
            {
                MessageBox.Show("Ошибка. Предельное число кластеров должно быть положительным.");
                return;
            }
            form.GetHoursTB().Text.Trim(); form.GetMinutesTB().Text.Trim(); form.GetSecondsTB().Text.Trim();
            if (form.GetHoursTB().Text == "" && form.GetMinutesTB().Text == "" && form.GetSecondsTB().Text == "")
            {
                model.Options.TimeLimitActivated = false;
            }
            else
            {
                model.Options.TimeLimitActivated = true;
                try
                {
                    model.Options.Hours = double.Parse(form.GetHoursTB().Text);
                    model.Options.TimeLimitActivated = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка. Часовая составляющая предельной продолжительности кластеризации" +
                        "введена некорректно.");
                    return;
                }
                if (model.Options.Hours < 0)
                {
                    MessageBox.Show("Ошибка. Часовая составляющая предельной продолжительности кластеризации не" +
                        "может быть отрицательной.");
                    return;
                }
                try
                {
                    model.Options.Minutes = double.Parse(form.GetMinutesTB().Text);
                    model.Options.TimeLimitActivated = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка. Минутная составляющая предельной продолжительности кластеризации" +
                        "введена некорректно.");
                }
                if (model.Options.Minutes < 0)
                {
                    MessageBox.Show("Ошибка. Минутная составляющая предельной продолжительности кластеризации не" +
                        "может быть отрицательной.");
                    return;
                }
                try
                {
                    model.Options.Seconds = double.Parse(form.GetSecondsTB().Text);
                    model.Options.TimeLimitActivated = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка. Секундная составляющая предельной продолжительности кластеризации" +
                        "введена некорректно");
                }
                if (model.Options.Seconds < 0)
                {
                    MessageBox.Show("Ошибка. Секундная составляющая предельной продолжительности кластеризации" +
                        "введена некорректно.");
                    return;
                }
            }
            form.ParentWinForm.SetOptions(model.Options);
            MessageBox.Show("Настройки сохранены.");
        }

        private void KMeansOptionsForm_Load(object sender, EventArgs e)
        {
            model.Options = form.ParentWinForm.GetOptions();
            form.ShowOptions(model.Options);
        }
    }
}
