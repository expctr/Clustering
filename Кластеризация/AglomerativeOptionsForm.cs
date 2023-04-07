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
    public partial class AglomerativeOptionsForm : Form
    {
        public AglomerativeOptionsForm()
        {
            InitializeComponent();
        }

        MainForm ParentWinForm;
        ClusteringOptions Options;

        public AglomerativeOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            Options = ParentWinForm.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            ApplyB.Click += ApplyB_Click;
            SingleLinkDistanceChB.CheckedChanged += 
                SingleLinkDistanceChB_CheckedChanged;
            CentreDistanceChB.Click +=
                CentreDistanceChB_CheckedChanged;
            WardDistanceChB.Click +=
                WardDistanceChB_CheckedChanged;
            Load += AglomerativeOptionsForm_Load;
        }

        private void AglomerativeOptionsForm_Load(object sender, EventArgs e)
        {
            ShowOptions();
        }

        public void ShowOptions()
        {
            DetalizationCoefTB.Text = Options.DetalizationCoef.ToString();
            switch (Options.ACDistance)
            {
                case ClusteringOptions.AglomerativeClusteringDistance.SingleLink:
                    SingleLinkDistanceChB.Checked = true;
                    break;
                case ClusteringOptions.AglomerativeClusteringDistance.CentreDistance:
                    CentreDistanceChB.Checked = true;
                    break;
                case ClusteringOptions.AglomerativeClusteringDistance.WardDistance:
                    WardDistanceChB.Checked = true;
                    break;
            }
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                Options.DetalizationCoef = double.Parse(DetalizationCoefTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Детализирующий коэффициент введен некорректно.");
                return;
            }
            if (Options.DetalizationCoef < 0)
            {
                MessageBox.Show("Ошибка. Детализирующий коэффициент должен быть положительным.");
                return;
            }

            if (SingleLinkDistanceChB.Checked)
            {
                Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.SingleLink;
            }
            if (CentreDistanceChB.Checked)
            {
                Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.CentreDistance;
            }
            if (WardDistanceChB.Checked)
            {
                Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.WardDistance;
            }
            ParentWinForm.SetOptions(Options);
            MessageBox.Show("Сохранение настроек успешно.");
        }

        void CheckSmth()
        {
            if (!(SingleLinkDistanceChB.Checked == CentreDistanceChB.Checked ==
                WardDistanceChB.Checked == false))
            {
                return;
            }
            switch (Options.ACDistance)
            {
                case ClusteringOptions.AglomerativeClusteringDistance.SingleLink:
                    SingleLinkDistanceChB.Checked = true;
                    break;
                case ClusteringOptions.AglomerativeClusteringDistance.CentreDistance:
                    CentreDistanceChB.Checked = true;
                    break;
                case ClusteringOptions.AglomerativeClusteringDistance.WardDistance:
                    WardDistanceChB.Checked = true;
                    break;
            }
        }
        private void SingleLinkDistanceChB_CheckedChanged(object sender, EventArgs e)
        {
            CheckSmth();
            if (SingleLinkDistanceChB.Checked)
            {
                Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.SingleLink;
                CentreDistanceChB.Checked = false;
                WardDistanceChB.Checked = false;
            }
        }

        private void CentreDistanceChB_CheckedChanged(object sender, EventArgs e)
        {
            CheckSmth();
            if (CentreDistanceChB.Checked)
            {
                Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.CentreDistance;
                SingleLinkDistanceChB.Checked = false;
                WardDistanceChB.Checked = false;
            }
        }

        private void WardDistanceChB_CheckedChanged(object sender, EventArgs e)
        {
            CheckSmth();
            if (WardDistanceChB.Checked)
            {
                Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.WardDistance;
                SingleLinkDistanceChB.Checked = false;
                CentreDistanceChB.Checked = false;
            }
        }
    }
}
