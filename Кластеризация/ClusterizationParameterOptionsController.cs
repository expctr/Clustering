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
    class ClusterizationParameterOptionsController
    {
        ClusterizationParameterOptionsForm form;

        ClusterizationParameterOptionsModel model;

        public ClusterizationParameterOptionsController(ClusterizationParameterOptionsForm form,
            ClusterizationParameterOptionsModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.Load += ClusterizationParameterOptionsForm_Load;
            form.GetApplyB().Click += ApplyB_Click;
            form.SizeChanged += ChooseClusterizationParameterForm_SizeChanged;
            form.GetShowClusteringParameterWeight().Click += ShowClusteringParameterWeight_Click;
        }

        private void ClusterizationParameterOptionsForm_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }

        void ShowInfo()
        {
            form.GetCCPChLB().Items.Clear();
            if (model.OriginalColsNames == null)
            {
                return;
            }
            form.GetCCPChLB().Items.AddRange(model.OriginalColsNames);
            for (int i = 0; i < model.CPOptions.ChosenClusterizationParameter.Length; ++i)
            {
                form.GetCCPChLB().SetItemChecked(i, model.CPOptions.ChosenClusterizationParameter[i]);
            }
            form.GetNormalizeChB().Checked = model.CPOptions.Normalize;
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < model.CPOptions.ChosenClusterizationParameter.Length; ++i)
            {
                model.CPOptions.ChosenClusterizationParameter[i] = false;
            }
            var checkedIndices = form.GetCCPChLB().CheckedIndices;
            foreach (var index in checkedIndices)
            {
                model.CPOptions.ChosenClusterizationParameter[(int)index] = true;
            }
            if (!model.CPOptions.ChosenClusterizationParameter.Contains(true))
            {
                MessageBox.Show("Ошибка. Необходимо выбрать хотя бы один параметр " +
                    "кластеризации.");
                return;
            }
            model.CPOptions.Normalize = form.GetNormalizeChB().Checked;
            if (form.GetChangeWeightChB().Checked)
            {
                int ind = ClusteringParameterIndex(form.GetClusteringParameterNameTB().Text);
                if (ind == -1)
                {
                    MessageBox.Show($"Параметра с названием \"{form.GetClusteringParameterNameTB().Text}\" " +
                        $"не найдено.");
                    return;
                }
                double weight;
                try
                {
                    weight = double.Parse(form.GetClusteringParameterWeightTB().Text);
                }
                catch
                {
                    MessageBox.Show("Ошибка. Вес параметра кластеризации введен " +
                        "некорректно.");
                    return;
                }
                model.CPOptions.DimensionalWeights[ind] = weight;
            }
            form.ParentWinForm.SetClusterizationParameterOptions(model.CPOptions);
            MessageBox.Show("Настройки сохранены.");
        }

        void ChooseClusterizationParameterForm_SizeChanged(object sender, EventArgs e)
        {
            form.GetApplyB().Location = new Point(12, form.GetHeight() - 8 - 12 - 23 - 32);
            form.GetApplyB().Width = form.GetWidth() - 2 * 8 - 2 * 12;
            form.GetNormalizeChB().Location = new Point(12, form.GetApplyB().Location.Y - 23);
            form.GetChangeWeightChB().Location = new Point(12, form.GetNormalizeChB().Location.Y - 23);
            form.GetClusteringParameterWeightL().Location = new Point(8, form.GetChangeWeightChB().Location.Y - 15);
            form.GetClusteringParameterWeightTB().Location = new Point(210, form.GetChangeWeightChB().Location.Y - 19);
            form.GetShowClusteringParameterWeight().Width = form.GetApplyB().Width;
            form.GetShowClusteringParameterWeight().Location = new Point(12, form.GetClusteringParameterWeightL().Location.Y - 38);
            form.GetClusteringParameterNameL().Location = new Point(8, form.GetShowClusteringParameterWeight().Location.Y - 22);
            form.GetClusteringParameterNameTB().Location = new Point(210, form.GetShowClusteringParameterWeight().Location.Y - 26);
            form.GetCCPChLB().Width = form.GetWidth() - 2 * 8 - 2 * 12;
            form.GetCCPChLB().Height = form.GetHeight() - 215;
        }

        int ClusteringParameterIndex(string clusteringParameterName)
        {
            for (int i = 0; i < model.OriginalColsNames.Length; ++i)
            {
                if (model.OriginalColsNames[i] == clusteringParameterName)
                {
                    return i;
                }
            }
            return -1;
        }

        private void ShowClusteringParameterWeight_Click(object sender, EventArgs e)
        {
            string clusteringParameterName = form.GetClusteringParameterNameTB().Text;
            int ind = ClusteringParameterIndex(clusteringParameterName);
            if (ind != -1)
            {
                MessageBox.Show($"Вес параметра \"{clusteringParameterName}\" " +
                    $"равен {model.CPOptions.DimensionalWeights[ind]}.");
            }
            else
            {
                MessageBox.Show($"Параметра с названием \"{clusteringParameterName}\" " +
                    $"не найдено.");
            }
        }
    }
}
