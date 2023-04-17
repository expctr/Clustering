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
    public partial class AffinityPropagationOptionsForm : Form
    {
        public AffinityPropagationOptionsForm()
        {
            InitializeComponent();
        }

        public MainForm ParentWinForm;

        public MainModel parentModel;

        public AffinityPropagationOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            // Options = ParentWinForm.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
        }

        public void ShowOptions(ClusteringOptions options)
        {
            GetSelfSimilarityTB().Text = options.SelfSimilarity.ToString();
            GetConvergencePrecisionTB().Text = options.ConvergencePrecision.ToString();
            if (options.TimeLimitActivated)
            {
                GetHoursTB().Text = options.Hours.ToString();
                GetMinutesTB().Text = options.Minutes.ToString();
                GetSecondsTB().Text = options.Seconds.ToString();
            }
        }

        public TextBox GetSelfSimilarityTB()
        {
            return SelfSimilarityTB;
        }

        public TextBox GetConvergencePrecisionTB()
        {
            return ConvergencePrecisionTB;
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
