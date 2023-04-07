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
    public partial class GNGOptionsForm : Form
    {
        public GNGOptionsForm()
        {
            InitializeComponent();
        }
        MainForm ParentWinForm;
        ClusteringOptions Options;

        public GNGOptionsForm(MainForm parentWinForm, bool ApplyEnabled) : this()
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
                Options.LearningSpeed1 = double.Parse(WinnerLearningSpeedTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-победителя введена некорректно.");
                return;
            }
            if (Options.LearningSpeed1 < 0)
            {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-победителя не может быть отрицательной.");
                return;
            }
            try
            {
                Options.LearningSpeed2 = double.Parse(NeighbourLearningSpeedTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-спутника введена некорректно.");
                return;
            }
            if (Options.LearningSpeed2 < 0)
            {
                MessageBox.Show("Ошибка. Скорость обучения нейрона-спутника не может быть отрицательной.");
                return;
            }
            try
            {
                Options.MaxAge = int.Parse(MaxAgeTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Предельный возраст нейронной связи введен некорректно.");
                return;
            }
            if (Options.MaxAge < 0)
            {
                MessageBox.Show("Ошибка. Предельный возраст нейронной связи не может быть отрицательным.");
                return;
            }
            try
            {
                Options.ReplicationPeriod = int.Parse(ReplicationPeriodTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Период репликации введен некорректно.");
                return;
            }
            if (Options.ReplicationPeriod < 0)
            {
                MessageBox.Show("Ошибка. Период репликации не может быть отрицательным.");
                return;
            }
            try
            {
                Options.MaxNumberOfNeurons = int.Parse(MaxNumberOfNeuronsTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Предельное число нейронов введено некорректно.");
                return;
            }
            if (Options.MaxNumberOfNeurons < 0)
            {
                MessageBox.Show("Ошибка. Предельное число нейронов не может быть отрицательным");
                return;
            }
            try
            {
                Options.ERRMN = double.Parse(ErrorReductionRatioOfMultiplyingNeuronTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Коэффициент снижения ошибки размножающегося нейрона введен " +
                    "некорректно.");
                return;
            }
            if (Options.ERRMN < 0)
            {
                MessageBox.Show("Ошибка. Коэффициент снижения ошибки размножающегося нейрона не " +
                    "может быть отрицательным.");
                return;
            }
            try
            {
                Options.CERR = double.Parse(CommonErrorReductionRationTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Общий коэффициент снижения ошибки введен некорректно.");
            }
            if (Options.CERR < 0)
            {
                MessageBox.Show("Ошибка. Общий коэффициент снижения ошибки не может быть отрицательным.");
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
                MessageBox.Show("Ошибка. Точнеость сходимости не может быть отрицательна.");
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
                }
                catch
                {
                    MessageBox.Show("Ошибка. Число часов введено некорректно.");
                    return;
                }
                if (Options.Hours < 0)
                {
                    MessageBox.Show("Ошибка. Число часов не может быть отрицательным.");
                    return;
                }
                try
                {
                    Options.Minutes = double.Parse(MinutesTB.Text);
                }
                catch
                {
                    MessageBox.Show("Ошибка. Число часов минут некорректно.");
                    return;
                }
                if (Options.Minutes < 0)
                {
                    MessageBox.Show("Ошибка. Число минут не может быть отрицательным.");
                    return;
                }
                try
                {
                    Options.Seconds = double.Parse(SecondsTB.Text);
                }
                catch
                {
                    MessageBox.Show("Ошибка. Число секунд введено некорректно.");
                    return;
                }
                if (Options.Seconds < 0)
                {
                    MessageBox.Show("Ошибка. Число секунд не может быть отрицательным.");
                    return;
                }
            }
            ParentWinForm.SetOptions(Options);
            MessageBox.Show("Настройки сохранены.");
        }

        private void GNGOptionsForm_Load(object sender, EventArgs e)
        {
            ShowOptions();
        }

        void ShowOptions()
        {
            WinnerLearningSpeedTB.Text = Options.LearningSpeed1.ToString();
            NeighbourLearningSpeedTB.Text = Options.LearningSpeed2.ToString();
            MaxAgeTB.Text = Options.MaxAge.ToString();
            ReplicationPeriodTB.Text = Options.ReplicationPeriod.ToString();
            MaxNumberOfNeuronsTB.Text = Options.MaxNumberOfNeurons.ToString();
            ErrorReductionRatioOfMultiplyingNeuronTB.Text = Options.ERRMN.ToString();
            CommonErrorReductionRationTB.Text = Options.CERR.ToString();
            ConvergencePrecisionTB.Text = Options.ConvergencePrecision.ToString();
            if (Options.TimeLimitActivated)
            {
                HoursTB.Text = Options.Hours.ToString();
                MinutesTB.Text = Options.Minutes.ToString();
                SecondsTB.Text = Options.Seconds.ToString();
            }
        }

    }
}
