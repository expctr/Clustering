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
    public partial class KMeansOptionsForm : Form
    {
        public KMeansOptionsForm()
        {
            InitializeComponent();
        }

        public MainForm ParentWinForm;

        public MainModel parentModel;

        public KMeansOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            // Options = ParentWinForm.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            // ApplyB.Click += ApplyB_Click;
        }

        public void ShowOptions(ClusteringOptions clusteringOptions)
        {
            GetNodesNumberTB().Text = clusteringOptions.ClustersNumber.ToString();
            if (clusteringOptions.TimeLimitActivated)
            {
                GetHoursTB().Text = clusteringOptions.Hours.ToString();
                GetMinutesTB().Text = clusteringOptions.Minutes.ToString();
                GetSecondsTB().Text = clusteringOptions.Seconds.ToString();
            }
        }

        public TextBox GetNodesNumberTB()
        {
            return NodesNumberTB;
        }

        public TextBox GetHoursTB()
        {
            return HoursTB;
        }

        public TextBox GetMinutesTB()
        {
            return MinutesTB;
        }

        public TextBox GetSecondsTB()
        {
            return SecondsTB;
        }

        public Button GetApplyB()
        {
            return ApplyB;
        }
    }
}
