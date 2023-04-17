using System;
using System.Windows.Forms;

namespace Кластеризация {

    public class GNGOptionsController {

        private GNGOptionsForm form;

        private GNGOptionsModel model;

        public GNGOptionsController(GNGOptionsForm form, GNGOptionsModel model) {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers() {
            form.Load += GNGOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }


        private void GNGOptionsForm_Load(object sender, EventArgs e) {
            form.ShowOptions(model.Options);
        }

        private void ApplyB_Click(object sender, EventArgs e) {
            try {
                model.Options.LearningSpeed1 = double.Parse(form.GetWinnerLearningSpeedTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-победителя введена некорректно.");
                return;
            }

            if (model.Options.LearningSpeed1 < 0) {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-победителя не может быть отрицательной.");
                return;
            }

            try {
                model.Options.LearningSpeed2 = double.Parse(form.GetNeighbourLearningSpeedTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-спутника введена некорректно.");
                return;
            }

            if (model.Options.LearningSpeed2 < 0) {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-спутника не может быть отрицательной.");
                return;
            }

            try {
                model.Options.MaxAge = int.Parse(form.GetMaxAgeTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Предельный возраст нейронной связи введен некорректно.");
                return;
            }

            if (model.Options.MaxAge < 0) {
                MessageBox.Show("Ошибка. Предельный возраст нейронной связи не может быть отрицательным.");
                return;
            }

            try {
                model.Options.ReplicationPeriod = int.Parse(form.GetReplicationPeriodTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Период репликации введен некорректно.");
                return;
            }

            if (model.Options.ReplicationPeriod < 0) {
                MessageBox.Show("Ошибка. Период репликации не может быть отрицательным.");
                return;
            }

            try {
                model.Options.MaxNumberOfNeurons = int.Parse(form.GetMaxNumberOfNeuronsTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Предельное число нейронов введено некорректно.");
                return;
            }

            if (model.Options.MaxNumberOfNeurons < 0) {
                MessageBox.Show("Ошибка. Предельное число нейронов не может быть отрицательным");
                return;
            }

            try {
                model.Options.ERRMN = double.Parse(form.GetErrorReductionRatioOfMultiplyingNeuronTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Коэффициент снижения ошибки размножающегося нейрона введен " +
                                "некорректно.");
                return;
            }

            if (model.Options.ERRMN < 0) {
                MessageBox.Show("Ошибка. Коэффициент снижения ошибки размножающегося нейрона не " +
                                "может быть отрицательным.");
                return;
            }

            try {
                model.Options.CERR = double.Parse(form.GetCommonErrorReductionRationTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Общий коэффициент снижения ошибки введен некорректно.");
            }

            if (model.Options.CERR < 0) {
                MessageBox.Show("Ошибка. Общий коэффициент снижения ошибки не может быть отрицательным.");
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
                MessageBox.Show("Ошибка. Точнеость сходимости не может быть отрицательна.");
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
                }
                catch {
                    MessageBox.Show("Ошибка. Число часов введено некорректно.");
                    return;
                }

                if (model.Options.Hours < 0) {
                    MessageBox.Show("Ошибка. Число часов не может быть отрицательным.");
                    return;
                }

                try {
                    model.Options.Minutes = double.Parse(form.GetMinutesTB().Text);
                }
                catch {
                    MessageBox.Show("Ошибка. Число часов минут некорректно.");
                    return;
                }

                if (model.Options.Minutes < 0) {
                    MessageBox.Show("Ошибка. Число минут не может быть отрицательным.");
                    return;
                }

                try {
                    model.Options.Seconds = double.Parse(form.GetSecondsTB().Text);
                }
                catch {
                    MessageBox.Show("Ошибка. Число секунд введено некорректно.");
                    return;
                }

                if (model.Options.Seconds < 0) {
                    MessageBox.Show("Ошибка. Число секунд не может быть отрицательным.");
                    return;
                }
            }

            form.parentModel.SetOptions(model.Options);
            MessageBox.Show("Настройки сохранены.");
        }

    }

}