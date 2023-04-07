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
    public partial class MSTOptionsForm : Form
    {
        public MSTOptionsForm()
        {
            InitializeComponent();
        }

        MainForm ParentWinForm;
        ClusteringOptions Options;

        public MSTOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            Options = ParentWinForm.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            ApplyB.Click += ApplyB_Click;
        }

        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                Options.ClustersNumber = int.Parse(ClustersNumberTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Число кластеров введено некорректно.");
                return;
            }
            if (Options.ClustersNumber < 1)
            {
                MessageBox.Show("Ошибка. Число кластеров должно быть положительным.");
                return;
            }
            ParentWinForm.SetOptions(Options);
            MessageBox.Show("Настройки сохранены.");
        }

        void ShowOptions()
        {
            ClustersNumberTB.Text = Options.ClustersNumber.ToString();
        }

        private void MSTOptionsForm_Load(object sender, EventArgs e)
        {
            ShowOptions();
        }
    }
}
