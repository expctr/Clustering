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

        MainForm ParentWinForm;
        ClusteringOptions Options;

        public DBSCANOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            Options = ParentWinForm.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            ApplyB.Click += ApplyB_Click;
        }

        private void DBSCANOptionsForm_Load(object sender, EventArgs e)
        {
            ShowOptions();
        }

        public void ShowOptions()
        {
            ReachabilityRadiusTB.Text = Options.ReachabilityRadius.ToString();
            ThresholdTB.Text = Options.Threshold.ToString();
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                Options.ReachabilityRadius = double.Parse(ReachabilityRadiusTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Радиус достижимости введен некорректно.");
                return;
            }
            if (Options.ReachabilityRadius < 0)
            {
                MessageBox.Show("Ошибка. Радиус достижимости не может быть отрицательным.");
                return;
            }
            try
            {
                Options.Threshold = int.Parse(ThresholdTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Порог кучности введен некорректно.");
                return;
            }
            if (Options.Threshold < 0)
            {
                MessageBox.Show("Ошибка. Порог кучности не может быть отрицательным.");
                return;
            }
            ParentWinForm.SetOptions(Options);
            MessageBox.Show("Настройки сохранены.");
        }
    }
}

