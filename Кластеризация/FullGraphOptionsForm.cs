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
    public partial class FullGraphOptionsForm : Form
    {
        public FullGraphOptionsForm()
        {
            InitializeComponent();
        }

        MainForm ParentWinForm;

        public MainModel parentModel;

        public FullGraphOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            // Options = parentModel.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            // ApplyB.Click += ApplyB_Click;
        }
        
        public Button GetApplyB() {
            return ApplyB;
        }

        public TextBox GetMaxDistanceTB() {
            return MaxDistanceTB;
        }
        
        public void ShowOptions(ClusteringOptions clusteringOptions)
        {
            MaxDistanceTB.Text = clusteringOptions.MaxDistance.ToString();
        }

    }
}
