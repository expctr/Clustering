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
    class DBSCANOptionsModel
    {
        DBSCANOptionsForm form;

        public ClusteringOptions Options;

        public DBSCANOptionsModel(DBSCANOptionsForm form, ClusteringOptions options)
        {
            this.form = form;
            Options = options;
        }
    }
}
