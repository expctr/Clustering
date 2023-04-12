using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Кластеризация
{
    class VisualizationController
    {
        private VisualizationForm form;

        private VisualizationModel model;

        public VisualizationController(VisualizationForm form, VisualizationModel model)
        {
            this.form = form;
            this.model = model;
        }
    }
}
