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
    public partial class FullGraphOptionsForm : Form
    {
        public FullGraphOptionsForm()
        {
            InitializeComponent();
        }

        MainForm ParentWinForm;
        ClusteringOptions Options;

        public FullGraphOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
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
                Options.MaxDistance = int.Parse(MaxDistanceTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Предельное расстояние введено некорректно.");
                return;
            }
            if (Options.MaxDistance < 0)
            {
                MessageBox.Show("Ошибка. Предельное расстояние должно быть положительным.");
                return;
            }
            ParentWinForm.SetOptions(Options);
            MessageBox.Show("Настройки сохранены.");
        }

        void ShowOptions()
        {
            MaxDistanceTB.Text = Options.MaxDistance.ToString();
        }

        private void FullGraphOptionsForm_Load(object sender, EventArgs e)
        {
            ShowOptions();
        }

    }
}
