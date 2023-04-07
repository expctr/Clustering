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
    public partial class ClusterizationParameterOptionsForm : Form
    {
        public ClusterizationParameterOptionsForm()
        {
            InitializeComponent();
            CCPChLB.CheckOnClick = true;
        }

        MainForm ParentWinForm;
        string[] OriginalColsNames;
        ClusteringLib.ClusteringParameterOptions CPOptions;

        public ClusterizationParameterOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
        {
            SizeChanged += ChooseClusterizationParameterForm_SizeChanged;
            ParentWinForm = parentWinForm;
            CPOptions =
                ParentWinForm.GetClusterizationParameterOptions();
            OriginalColsNames = ParentWinForm.GetOriginalColsNames();
            ApplyB.Enabled = ApplyEnabled;
            ApplyB.Click += ApplyB_Click;
            ShowClusteringParameterWeight.Click += ShowClusteringParameterWeight_Click;
        }

        private void ClusterizationParameterOptionsForm_Load(object sender, EventArgs e)
        {
            ShowInfo();
        }

        void ShowInfo()
        {
            CCPChLB.Items.Clear();
            if (OriginalColsNames == null)
            {
                return;
            }
            CCPChLB.Items.AddRange(OriginalColsNames);
            for (int i = 0; i < CPOptions.ChosenClusterizationParameter.Length; ++i)
            {
                CCPChLB.SetItemChecked(i, CPOptions.ChosenClusterizationParameter[i]);
            }
            NormalizeChB.Checked = CPOptions.Normalize;
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CPOptions.ChosenClusterizationParameter.Length; ++i)
            {
                CPOptions.ChosenClusterizationParameter[i] = false;
            }
            var checkedIndices = CCPChLB.CheckedIndices;
            foreach (var index in checkedIndices)
            {
                CPOptions.ChosenClusterizationParameter[(int)index] = true;
            }
            if (!CPOptions.ChosenClusterizationParameter.Contains(true))
            {
                MessageBox.Show("Ошибка. Необходимо выбрать хотя бы один параметр " +
                    "кластеризации.");
                return;
            }
            CPOptions.Normalize = NormalizeChB.Checked;
            if (ChangeWeightChB.Checked)
            {
                int ind = ClusteringParameterIndex(ClusteringParameterNameTB.Text);
                if (ind == -1)
                {
                    MessageBox.Show($"Параметра с названием \"{ClusteringParameterNameTB.Text}\" " +
                        $"не найдено.");
                    return;
                }
                double weight;
                try
                {
                    weight = double.Parse(ClusteringParameterWeightTB.Text);
                }
                catch
                {
                    MessageBox.Show("Ошибка. Вес параметра кластеризации введен " +
                        "некорректно.");
                    return;
                }
                CPOptions.DimensionalWeights[ind] = weight;
            }
            ParentWinForm.SetClusterizationParameterOptions(CPOptions);
            MessageBox.Show("Настройки сохранены.");
        }

        void ChooseClusterizationParameterForm_SizeChanged(object sender, EventArgs e)
        {
            ApplyB.Location = new Point(12, Height - 8 - 12 - 23 - 32);
            ApplyB.Width = Width - 2 * 8 - 2 * 12;
            NormalizeChB.Location = new Point(12, ApplyB.Location.Y - 23);
            ChangeWeightChB.Location = new Point(12, NormalizeChB.Location.Y - 23);
            ClusteringParameterWeightL.Location = new Point(8, ChangeWeightChB.Location.Y - 15);
            ClusteringParameterWeightTB.Location = new Point(210, ChangeWeightChB.Location.Y - 19);
            ShowClusteringParameterWeight.Width = ApplyB.Width;
            ShowClusteringParameterWeight.Location = new Point(12, ClusteringParameterWeightL.Location.Y - 38);
            ClusteringParameterNameL.Location = new Point(8, ShowClusteringParameterWeight.Location.Y - 22);
            ClusteringParameterNameTB.Location = new Point(210, ShowClusteringParameterWeight.Location.Y - 26);
            CCPChLB.Width = Width - 2 * 8 - 2 * 12;
            CCPChLB.Height = Height - 215;
        }

        int ClusteringParameterIndex(string clusteringParameterName)
        {
            for (int i = 0; i < OriginalColsNames.Length; ++i)
            {
                if (OriginalColsNames[i] == clusteringParameterName)
                {
                    return i;
                }
            }
            return -1;
        }

        private void ShowClusteringParameterWeight_Click(object sender, EventArgs e)
        {
            string clusteringParameterName = ClusteringParameterNameTB.Text;
            int ind = ClusteringParameterIndex(clusteringParameterName);
            if (ind != -1)
            {
                MessageBox.Show($"Вес параметра \"{clusteringParameterName}\" " +
                    $"равен {CPOptions.DimensionalWeights[ind]}.");
            }
            else
            {
                MessageBox.Show($"Параметра с названием \"{clusteringParameterName}\" " +
                    $"не найдено.");
            }
        }
    }
}
