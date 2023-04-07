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
    public partial class KMeansOptionsForm : Form
    {
        public KMeansOptionsForm()
        {
            InitializeComponent();
        }
        MainForm ParentWinForm;
        ClusteringOptions Options;

        public KMeansOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
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
                Options.ClustersNumber = int.Parse(NodesNumberTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Предельное число кластеров введено некорректно.");
                return;
            }
            if (Options.ClustersNumber < 1)
            {
                MessageBox.Show("Ошибка. Предельное число кластеров должно быть положительным.");
                return;
            }
            HoursTB.Text.Trim(); MinutesTB.Text.Trim(); SecondsTB.Text.Trim();
            if (HoursTB.Text == "" && MinutesTB.Text == "" && SecondsTB.Text == "")
            {
                Options.TimeLimitActivated = false;
            }
            else
            {
                Options.TimeLimitActivated = true;
                try
                {
                    Options.Hours = double.Parse(HoursTB.Text);
                    Options.TimeLimitActivated = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка. Часовая составляющая предельной продолжительности кластеризации" +
                        "введена некорректно.");
                    return;
                }
                if (Options.Hours < 0)
                {
                    MessageBox.Show("Ошибка. Часовая составляющая предельной продолжительности кластеризации не" +
                        "может быть отрицательной.");
                    return;
                }
                try
                {
                    Options.Minutes = double.Parse(MinutesTB.Text);
                    Options.TimeLimitActivated = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка. Минутная составляющая предельной продолжительности кластеризации" +
                        "введена некорректно.");
                }
                if (Options.Minutes < 0)
                {
                    MessageBox.Show("Ошибка. Минутная составляющая предельной продолжительности кластеризации не" +
                        "может быть отрицательной.");
                    return;
                }
                try
                {
                    Options.Seconds = double.Parse(SecondsTB.Text);
                    Options.TimeLimitActivated = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка. Секундная составляющая предельной продолжительности кластеризации" +
                        "введена некорректно");
                }
                if (Options.Seconds < 0)
                {
                    MessageBox.Show("Ошибка. Секундная составляющая предельной продолжительности кластеризации" +
                        "введена некорректно.");
                    return;
                }
            }
            ParentWinForm.SetOptions(Options);
            MessageBox.Show("Настройки сохранены.");
        }

        private void KMeansOptionsForm_Load(object sender, EventArgs e)
        {
            Options = ParentWinForm.GetOptions();
            ShowOptions();
        }

        public void ShowOptions()
        {
            NodesNumberTB.Text = Options.ClustersNumber.ToString();
            if (Options.TimeLimitActivated)
            {
                HoursTB.Text = Options.Hours.ToString();
                MinutesTB.Text = Options.Minutes.ToString();
                SecondsTB.Text = Options.Seconds.ToString();
            }
        }
    }
}
