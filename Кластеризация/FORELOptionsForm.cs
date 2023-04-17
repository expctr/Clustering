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
    public partial class FORELOptionsForm : Form
    {
        public FORELOptionsForm()
        {
            InitializeComponent();
        }

        MainForm ParentWinForm;

        public MainModel parentModel;

        ClusteringOptions Options;

        public FORELOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            // Options = parentModel.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            // ApplyB.Click += ApplyB_Click;
        }

        public void ShowOptions(ClusteringOptions clusteringOptions)
        {
            ReachabilityRadiusTB.Text = clusteringOptions.ReachabilityRadius.ToString();
        }

        public Button GetApplyB()
        {
            return ApplyB;
        }
        
        
        public TextBox GetReachabilityRadiusTB()
        {
            return ReachabilityRadiusTB;
        }
    }
}