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

        public int ClusteringParameterIndex(string clusteringParameterName)
        {
            for (int i = 0; i < OriginalColsNames.Length; ++i)
            {
                if (OriginalColsNames[i] == clusteringParameterName)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
