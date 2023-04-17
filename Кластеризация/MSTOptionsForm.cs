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
    public partial class MSTOptionsForm : Form
    {
        public MSTOptionsForm()
        {
            InitializeComponent();
        }

        public MainForm ParentWinForm;

        public MainModel parentModel;

        public MSTOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            // Options = ParentWinForm.GetOptions();
            this.parentModel = parentModel;
            ApplyB.Enabled = ApplyEnabled;
            // ApplyB.Click += ApplyB_Click;
        }

        public void ShowOptions(ClusteringOptions clusteringOptions)
        {
            GetClustersNumberTB().Text = clusteringOptions.ClustersNumber.ToString();
        }

        public TextBox GetClustersNumberTB()
        {
            return ClustersNumberTB;
        }

        public Button GetApplyB()
        {
            return ApplyB;
        }
    }
}
