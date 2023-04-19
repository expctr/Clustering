using System;
using System.Windows.Forms;

namespace Кластеризация {

    public class FullGraphOptionsController {

        private FullGraphOptionsForm form;

        private FullGraphOptionsModel model;

        public FullGraphOptionsController(FullGraphOptionsForm form, FullGraphOptionsModel model) {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers() {
            form.Load += FullGraphOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }

        private void ApplyB_Click(object sender, EventArgs e) {
            try {
                model.Options.MaxDistance = int.Parse(form.GetMaxDistanceTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Предельное расстояние введено некорректно.");
                return;
            }

            if (model.Options.MaxDistance < 0) {
                MessageBox.Show("Ошибка. Предельное расстояние должно быть положительным.");
                return;
            }

            form.parentModel.SetOptions(model.Options);
            MessageBox.Show("Настройки сохранены.");
        }

        private void FullGraphOptionsForm_Load(object sender, EventArgs e) {
            form.ShowOptions(model.Options);
        }

    }

}