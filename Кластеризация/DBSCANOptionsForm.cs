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
    public partial class DBSCANOptionsForm : Form
    {
        public DBSCANOptionsForm()
        {
            InitializeComponent();
        }

        public MainForm ParentWinForm;

        public MainModel parentModel;

        public DBSCANOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            // Options = ParentWinForm.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            // ApplyB.Click += ApplyB_Click;
        }

        public void ShowOptions(ClusteringOptions clusteringOptions)
        {
            GetReachabilityRadiusTB().Text = clusteringOptions.ReachabilityRadius.ToString();
            GetThresholdTB().Text = clusteringOptions.Threshold.ToString();
        }

        public TextBox GetReachabilityRadiusTB()
        {
            return ReachabilityRadiusTB;
        }

        public TextBox GetThresholdTB()
        {
            return ThresholdTB;
        }

        public Button GetApplyB()
        {
            return ApplyB;
        }
    }
}

