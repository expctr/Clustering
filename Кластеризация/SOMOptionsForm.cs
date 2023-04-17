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

namespace Кластеризация {

    public partial class SOMOptionsForm : Form {

        public SOMOptionsForm() {
            InitializeComponent();
        }

        MainForm ParentWinForm;

        public MainModel parentModel;


        public SOMOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this() {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            ApplyB.Enabled = ApplyEnabled;
        }

        public void ShowOptions(ClusteringOptions clusteringOptions) {
            MaxDistanceTB.Text = clusteringOptions.MaxDistance.ToString();
            LearningSpeedTB.Text = clusteringOptions.LearningSpeed1.ToString();
            ConvergencePrecisionTB.Text = clusteringOptions.ConvergencePrecision.ToString();
            if (clusteringOptions.TimeLimitActivated) {
                HoursTB.Text = clusteringOptions.Hours.ToString();
                MinutesTB.Text = clusteringOptions.Minutes.ToString();
                SecondsTB.Text = clusteringOptions.Seconds.ToString();
            }
        }

        public Button GetApplyB() {
            return ApplyB;
        }

        public TextBox GetHoursTB() {
            return HoursTB;
        }

        public TextBox GetMinutesTB() {
            return MinutesTB;
        }

        public TextBox GetSecondsTB() {
            return SecondsTB;
        }

        public TextBox GetMaxDistanceTB() {
            return MaxDistanceTB;
        }

        public TextBox GetLearningSpeedTB() {
            return LearningSpeedTB;
        }

        public TextBox GetConvergencePrecisionTB() {
            return ConvergencePrecisionTB;
        }

    }

}