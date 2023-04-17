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
    public class AffinityPropagationOptionsModel
    {
        private AffinityPropagationOptionsForm form;

        public ClusteringOptions Options;

        public AffinityPropagationOptionsModel(AffinityPropagationOptionsForm form, MainModel mainModel)
        {
            this.form = form;
            Options = mainModel.GetOptions();
        }
    }
}
