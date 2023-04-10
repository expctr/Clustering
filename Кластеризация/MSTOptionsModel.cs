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
    class MSTOptionsModel
    {
        MSTOptionsForm form;

        public ClusteringOptions Options;

        public MSTOptionsModel(MSTOptionsForm form)
        {
            this.form = form;
            Options = form.ParentWinForm.GetOptions();
        }
    }
}
