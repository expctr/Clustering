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
    class ClusterizationParameterOptionsModel
    {
        ClusterizationParameterOptionsForm form;

        public string[] OriginalColsNames;

        public ClusteringParameterOptions CPOptions;

        public ClusterizationParameterOptionsModel(ClusterizationParameterOptionsForm form)
        {
            this.form = form;
            CPOptions = form.ParentWinForm.GetClusterizationParameterOptions();
            OriginalColsNames = form.ParentWinForm.GetOriginalColsNames();
        }
    }
}
