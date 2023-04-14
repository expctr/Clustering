using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Кластеризация
{
    class MainController
    {
        MainForm form;

        MainModel model;

        public MainController(MainForm form, MainModel model)
        {
            this.form = form;
            this.model = model;
        }
    }
}
