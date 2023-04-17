using System;
using System.Windows.Forms;

namespace Кластеризация {

    public class FORELOptionsController {

        private FORELOptionsForm form;

        private FORELOptionsModel model;

        public FORELOptionsController(FORELOptionsForm form, FORELOptionsModel model) {
            this.form = form;
            this.model = model;
        }
        
        public void AddEventHandlers()
        {
            form.Load += FORELOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
        }
        
        private void FORELOptionsForm_Load(object sender, EventArgs e)
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
            form.parentModel.SetOptions(model.Options);
            MessageBox.Show("Настройки сохранены.");
        }

    }

}