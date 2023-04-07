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
    public partial class SOMOptionsForm : Form
    {
        public SOMOptionsForm()
        {
            InitializeComponent();
        }
        MainForm ParentWinForm;
        ClusteringOptions Options;

        public SOMOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
        {
            ParentWinForm = parentWinForm;
            Options = ParentWinForm.GetOptions();
            ApplyB.Enabled = ApplyEnabled;
            ApplyB.Click += ApplyB_Click;
        }

        void ShowOptions()
        {
            MaxDistanceTB.Text = Options.MaxDistance.ToString();
            LearningSpeedTB.Text = Options.LearningSpeed1.ToString();
            ConvergencePrecisionTB.Text = Options.ConvergencePrecision.ToString();
            if (Options.TimeLimitActivated)
            {
                HoursTB.Text = Options.Hours.ToString();
                MinutesTB.Text = Options.Minutes.ToString();
                SecondsTB.Text = Options.Seconds.ToString();
            }
        }
        private void SOMOptionsForm_Load(object sender, EventArgs e)
        {
            ShowOptions();
        }
        private void ApplyB_Click(object sender, EventArgs e)
        {
            try
            {
                Options.MaxDistance = double.Parse(MaxDistanceTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Предельное расстояние введено некорректно.");
                return;
            }
            if (Options.MaxDistance < 0)
            {
                MessageBox.Show("Ошибка. Предельное расстояние не может быть отрицательным.");
                return;
            }
            try
            {
                Options.LearningSpeed1 = double.Parse(LearningSpeedTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Скорость обучения введена некорректно.");
                return;
            }
            if (Options.LearningSpeed1 < 0)
            {
                MessageBox.Show("Ошибка. Скорость обучения не может быть отрицательной");
                return;
            }
            try
            {
                Options.ConvergencePrecision = double.Parse(ConvergencePrecisionTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Точность сходимости введена некорректно.");
                return;
            }
            if (Options.ConvergencePrecision < 0)
            {
                MessageBox.Show("Ошибка. Точность сходимости не может быть отрицательной.");
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
    }
}

