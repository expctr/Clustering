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
    class AffinityPropagationOptionsController
    {
        private AffinityPropagationOptionsForm form;

        private AffinityPropagationOptionsModel model;

        public AffinityPropagationOptionsController(AffinityPropagationOptionsForm form,
            AffinityPropagationOptionsModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.Load += AffinityPropagationOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }

        private void AffinityPropagationOptionsForm_Load(object sender, EventArgs e)
        {
            ShowOptions();
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                model.Options.SelfSimilarity = double.Parse(form.GetSelfSimilarityTB().Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Коэффициент самоподобия введено некорректно.");
                return;
            }
            if (model.Options.SelfSimilarity > 0)
            {
                MessageBox.Show("Ошибка. Коэффициент самоподобия не может быть положительным.");
                return;
            }
            try
            {
                model.Options.ConvergencePrecision = double.Parse(form.GetConvergencePrecisionTB().Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Точность сходимости введена некорректно.");
                return;
            }
            if (model.Options.ConvergencePrecision < 0)
            {
                MessageBox.Show("Ошибка. Точность сходимости не может быть отрицательной.");
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

        public void ShowOptions()
        {
            form.GetSelfSimilarityTB().Text = model.Options.SelfSimilarity.ToString();
            form.GetConvergencePrecisionTB().Text = model.Options.ConvergencePrecision.ToString();
            if (model.Options.TimeLimitActivated)
            {
                form.GetHoursTB().Text = model.Options.Hours.ToString();
                form.GetMinutesTB().Text = model.Options.Minutes.ToString();
                form.GetSecondsTB().Text = model.Options.Seconds.ToString();
            }
        }
    }
}
