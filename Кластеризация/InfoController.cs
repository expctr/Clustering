using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Кластеризация
{
    class InfoController
    {
        private InfoForm form;

        private InfoModel model;

        public InfoController(InfoForm form, InfoModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.Resize += InfoForm_Resize;
        }

        private void InfoForm_Resize(object sender, EventArgs e)
        {
            form.GetDataGridView1().Width = form.GetWidth() - 2 * 8 - 2 * 12;
            form.GetDataGridView1().Height = form.GetHeight() - 32 - 2 * 8 - 12;
        }
    }
}
