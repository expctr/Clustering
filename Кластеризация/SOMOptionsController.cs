using System;
using System.Windows.Forms;

namespace Кластеризация {

    public class SOMOptionsController {

        SOMOptionsForm form;

        SOMOptionsModel model;

        public SOMOptionsController(SOMOptionsForm form, SOMOptionsModel model) {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers() {
            form.Load += SOMOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }

        private void SOMOptionsForm_Load(object sender, EventArgs e) {
            form.ShowOptions(model.Options);
        }

        private void ApplyB_Click(object sender, EventArgs e) {
            try {
                model.Options.MaxDistance = double.Parse(form.GetMaxDistanceTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Предельное расстояние введено некорректно.");
                return;
            }

            if (model.Options.MaxDistance < 0) {
                MessageBox.Show("Ошибка. Предельное расстояние не может быть отрицательным.");
                return;
            }

            try {
                model.Options.LearningSpeed1 = double.Parse(form.GetLearningSpeedTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Скорость обучения введена некорректно.");
                return;
            }

            if (model.Options.LearningSpeed1 < 0) {
                MessageBox.Show("Ошибка. Скорость обучения не может быть отрицательной");
                return;
            }

            try {
                model.Options.ConvergencePrecision = double.Parse(form.GetConvergencePrecisionTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Точность сходимости введена некорректно.");
                return;
            }

            if (model.Options.ConvergencePrecision < 0) {
                MessageBox.Show("Ошибка. Точность сходимости не может быть отрицательной.");
                return;
            }

            form.GetHoursTB().Text = form.GetHoursTB().Text.Trim();

            form.GetMinutesTB().Text = form.GetMinutesTB().Text.Trim();

            form.GetSecondsTB().Text = form.GetSecondsTB().Text.Trim();
            if (form.GetHoursTB().Text == "" && form.GetMinutesTB().Text == "" && form.GetSecondsTB().Text == "") {
                model.Options.TimeLimitActivated = false;
            }
            else {
                model.Options.TimeLimitActivated = true;
                try {
                    model.Options.Hours = double.Parse(form.GetHoursTB().Text);
                    model.Options.TimeLimitActivated = true;
                }
                catch {
                    MessageBox.Show("Ошибка. Часовая составляющая предельной продолжительности кластеризации" +
                                    "введена некорректно.");
                    return;
                }

                if (model.Options.Hours < 0) {
                    MessageBox.Show("Ошибка. Часовая составляющая предельной продолжительности кластеризации не" +
                                    "может быть отрицательной.");
                    return;
                }

                try {
                    model.Options.Minutes = double.Parse(form.GetMinutesTB().Text);
                    model.Options.TimeLimitActivated = true;
                }
                catch {
                    MessageBox.Show("Ошибка. Минутная составляющая предельной продолжительности кластеризации" +
                                    "введена некорректно.");
                }

                if (model.Options.Minutes < 0) {
                    MessageBox.Show("Ошибка. Минутная составляющая предельной продолжительности кластеризации не" +
                                    "может быть отрицательной.");
                    return;
                }

                try {
                    model.Options.Seconds = double.Parse(form.GetSecondsTB().Text);
                    model.Options.TimeLimitActivated = true;
                }
                catch {
                    MessageBox.Show("Ошибка. Секундная составляющая предельной продолжительности кластеризации" +
                                    "введена некорректно");
                }

                if (model.Options.Seconds < 0) {
                    MessageBox.Show("Ошибка. Секундная составляющая предельной продолжительности кластеризации" +
                                    "введена некорректно.");
                    return;
                }
            }

            form.parentModel.SetOptions(model.Options);
            MessageBox.Show("Настройки сохранены.");
        }

    }

}