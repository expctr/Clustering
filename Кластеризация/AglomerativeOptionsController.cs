using System;
using System.Windows.Forms;
using ClusteringLib;

namespace Кластеризация {

    public class AglomerativeOptionsController {

        private AglomerativeOptionsForm form;

        private AglomerativeOptionsModel model;

        public AglomerativeOptionsController(AglomerativeOptionsForm form, AglomerativeOptionsModel model) {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers() {
            form.Load += AglomerativeOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
            form.GetCentreDistanceChB().Click +=
                CentreDistanceChB_CheckedChanged;
            form.GetWardDistanceChB().Click +=
                WardDistanceChB_CheckedChanged;
            form.GetSingleLinkDistanceChB().CheckedChanged +=
                SingleLinkDistanceChB_CheckedChanged;
        }

        private void AglomerativeOptionsForm_Load(object sender, EventArgs e) {
            form.ShowOptions(model.Options);
        }

        private void ApplyB_Click(object sender, EventArgs e) {
            try {
                model.Options.DetalizationCoef = double.Parse(form.GetDetalizationCoefTB().Text);
            }
            catch {
                MessageBox.Show("Ошибка. Детализирующий коэффициент введен некорректно.");
                return;
            }

            if (model.Options.DetalizationCoef < 0) {
                MessageBox.Show("Ошибка. Детализирующий коэффициент должен быть положительным.");
                return;
            }

            if (form.GetSingleLinkDistanceChB().Checked) {
                model.Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.SingleLink;
            }

            if (form.GetCentreDistanceChB().Checked) {
                model.Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.CentreDistance;
            }

            if (form.GetWardDistanceChB().Checked) {
                model.Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.WardDistance;
            }

            form.parentModel.SetOptions(model.Options);
            MessageBox.Show("Сохранение настроек успешно.");
        }

        void CheckSmth() {
            if (form.GetSingleLinkDistanceChB().Checked == form.GetCentreDistanceChB().Checked ==
                form.GetWardDistanceChB().Checked) {
                return;
            }

            switch (model.Options.ACDistance) {
                case ClusteringOptions.AglomerativeClusteringDistance.SingleLink:
                    form.GetSingleLinkDistanceChB().Checked = true;
                    break;
                case ClusteringOptions.AglomerativeClusteringDistance.CentreDistance:
                    form.GetCentreDistanceChB().Checked = true;
                    break;
                case ClusteringOptions.AglomerativeClusteringDistance.WardDistance:
                    form.GetWardDistanceChB().Checked = true;
                    break;
            }
        }


        private void CentreDistanceChB_CheckedChanged(object sender, EventArgs e) {
            CheckSmth();
            if (form.GetCentreDistanceChB().Checked) {
                model.Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.CentreDistance;
                form.GetSingleLinkDistanceChB().Checked = false;
                form.GetWardDistanceChB().Checked = false;
            }
        }

        private void WardDistanceChB_CheckedChanged(object sender, EventArgs e) {
            CheckSmth();
            if (form.GetWardDistanceChB().Checked) {
                model.Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.WardDistance;
                form.GetSingleLinkDistanceChB().Checked = false;
                form.GetCentreDistanceChB().Checked = false;
            }
        }

        private void SingleLinkDistanceChB_CheckedChanged(object sender, EventArgs e) {
            CheckSmth();
            if (form.GetSingleLinkDistanceChB().Checked) {
                model.Options.ACDistance = ClusteringOptions.AglomerativeClusteringDistance.SingleLink;
                form.GetCentreDistanceChB().Checked = false;
                form.GetWardDistanceChB().Checked = false;
            }
        }

    }

}