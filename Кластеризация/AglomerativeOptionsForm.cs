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

    public partial class AglomerativeOptionsForm : Form {

        public AglomerativeOptionsForm() {
            InitializeComponent();
        }

        MainForm ParentWinForm;

        public MainModel parentModel;

        public AglomerativeOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this() {
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            ApplyB.Enabled = ApplyEnabled;
        }


        public Button GetApplyB() {
            return ApplyB;
        }

        public TextBox GetDetalizationCoefTB() {
            return DetalizationCoefTB;
        }

        public CheckBox GetSingleLinkDistanceChB() {
            return SingleLinkDistanceChB;
        }

        public CheckBox GetCentreDistanceChB() {
            return CentreDistanceChB;
        }

        public CheckBox GetWardDistanceChB() {
            return WardDistanceChB;
        }

        public void ShowOptions(ClusteringOptions clusteringOptions) {
            DetalizationCoefTB.Text = clusteringOptions.DetalizationCoef.ToString();
            switch (clusteringOptions.ACDistance) {
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

    }

}