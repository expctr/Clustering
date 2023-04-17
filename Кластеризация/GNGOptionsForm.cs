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

    public partial class GNGOptionsForm : Form {

        public GNGOptionsForm() {
            InitializeComponent();
        }

        MainForm ParentWinForm;

        public MainModel parentModel;

        public GNGOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this() {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            // Options = parentModel.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            // ApplyB.Click += ApplyB_Click;
        }

        public Button GetApplyB() {
            return ApplyB;
        }


        public TextBox GetWinnerLearningSpeedTB() {
            return WinnerLearningSpeedTB;
        }

        public TextBox GetNeighbourLearningSpeedTB() {
            return NeighbourLearningSpeedTB;
        }

        public TextBox GetMaxAgeTB() {
            return MaxAgeTB;
        }

        public TextBox GetReplicationPeriodTB() {
            return ReplicationPeriodTB;
        }

        public TextBox GetMaxNumberOfNeuronsTB() {
            return MaxNumberOfNeuronsTB;
        }

        public TextBox GetErrorReductionRatioOfMultiplyingNeuronTB() {
            return ErrorReductionRatioOfMultiplyingNeuronTB;
        }

        public TextBox GetCommonErrorReductionRationTB() {
            return CommonErrorReductionRationTB;
        }

        public TextBox GetConvergencePrecisionTB() {
            return ConvergencePrecisionTB;
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

        public void ShowOptions(ClusteringOptions clusteringOptions) {
            WinnerLearningSpeedTB.Text = clusteringOptions.LearningSpeed1.ToString();
            NeighbourLearningSpeedTB.Text = clusteringOptions.LearningSpeed2.ToString();
            MaxAgeTB.Text = clusteringOptions.MaxAge.ToString();
            ReplicationPeriodTB.Text = clusteringOptions.ReplicationPeriod.ToString();
            MaxNumberOfNeuronsTB.Text = clusteringOptions.MaxNumberOfNeurons.ToString();
            ErrorReductionRatioOfMultiplyingNeuronTB.Text = clusteringOptions.ERRMN.ToString();
            CommonErrorReductionRationTB.Text = clusteringOptions.CERR.ToString();
            ConvergencePrecisionTB.Text = clusteringOptions.ConvergencePrecision.ToString();
            if (clusteringOptions.TimeLimitActivated) {
                HoursTB.Text = clusteringOptions.Hours.ToString();
                MinutesTB.Text = clusteringOptions.Minutes.ToString();
                SecondsTB.Text = clusteringOptions.Seconds.ToString();
            }
        }

    }

}