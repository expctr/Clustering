namespace Кластеризация
{
    partial class SOMOptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ApplyB = new System.Windows.Forms.Button();
            this.SelfOrganisingKohonenNetworkL = new System.Windows.Forms.Label();
            this.SecondsTB = new System.Windows.Forms.TextBox();
            this.HoursTB = new System.Windows.Forms.TextBox();
            this.SecondsL = new System.Windows.Forms.Label();
            this.MinutesL = new System.Windows.Forms.Label();
            this.MinutesTB = new System.Windows.Forms.TextBox();
            this.HoursL = new System.Windows.Forms.Label();
            this.TimeLimitL = new System.Windows.Forms.Label();
            this.ConvergencePrecisionL = new System.Windows.Forms.Label();
            this.ConvergencePrecisionTB = new System.Windows.Forms.TextBox();
            this.LearningSpeedTB = new System.Windows.Forms.TextBox();
            this.LearningSpeedL = new System.Windows.Forms.Label();
            this.MaxDistanceTB = new System.Windows.Forms.TextBox();
            this.MaxDistanceL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 154);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(230, 23);
            this.ApplyB.TabIndex = 29;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // SelfOrganisingKohonenNetworkL
            // 
            this.SelfOrganisingKohonenNetworkL.AutoSize = true;
            this.SelfOrganisingKohonenNetworkL.Location = new System.Drawing.Point(12, 9);
            this.SelfOrganisingKohonenNetworkL.Name = "SelfOrganisingKohonenNetworkL";
            this.SelfOrganisingKohonenNetworkL.Size = new System.Drawing.Size(198, 13);
            this.SelfOrganisingKohonenNetworkL.TabIndex = 28;
            this.SelfOrganisingKohonenNetworkL.Text = "Самоорганизующаяся сеть Кохонена";
            // 
            // SecondsTB
            // 
            this.SecondsTB.Location = new System.Drawing.Point(224, 128);
            this.SecondsTB.Name = "SecondsTB";
            this.SecondsTB.Size = new System.Drawing.Size(21, 20);
            this.SecondsTB.TabIndex = 27;
            // 
            // HoursTB
            // 
            this.HoursTB.Location = new System.Drawing.Point(55, 128);
            this.HoursTB.Name = "HoursTB";
            this.HoursTB.Size = new System.Drawing.Size(21, 20);
            this.HoursTB.TabIndex = 26;
            // 
            // SecondsL
            // 
            this.SecondsL.AutoSize = true;
            this.SecondsL.Location = new System.Drawing.Point(164, 131);
            this.SecondsL.Name = "SecondsL";
            this.SecondsL.Size = new System.Drawing.Size(54, 13);
            this.SecondsL.TabIndex = 25;
            this.SecondsL.Text = "Секунды:";
            // 
            // MinutesL
            // 
            this.MinutesL.AutoSize = true;
            this.MinutesL.Location = new System.Drawing.Point(82, 131);
            this.MinutesL.Name = "MinutesL";
            this.MinutesL.Size = new System.Drawing.Size(49, 13);
            this.MinutesL.TabIndex = 24;
            this.MinutesL.Text = "Минуты:";
            // 
            // MinutesTB
            // 
            this.MinutesTB.Location = new System.Drawing.Point(137, 128);
            this.MinutesTB.Name = "MinutesTB";
            this.MinutesTB.Size = new System.Drawing.Size(21, 20);
            this.MinutesTB.TabIndex = 23;
            // 
            // HoursL
            // 
            this.HoursL.AutoSize = true;
            this.HoursL.Location = new System.Drawing.Point(12, 131);
            this.HoursL.Name = "HoursL";
            this.HoursL.Size = new System.Drawing.Size(38, 13);
            this.HoursL.TabIndex = 22;
            this.HoursL.Text = "Часы:";
            // 
            // TimeLimitL
            // 
            this.TimeLimitL.AutoSize = true;
            this.TimeLimitL.Location = new System.Drawing.Point(12, 109);
            this.TimeLimitL.Name = "TimeLimitL";
            this.TimeLimitL.Size = new System.Drawing.Size(225, 13);
            this.TimeLimitL.TabIndex = 21;
            this.TimeLimitL.Text = "Предельная длительность кластеризации:";
            // 
            // ConvergencePrecisionL
            // 
            this.ConvergencePrecisionL.AutoSize = true;
            this.ConvergencePrecisionL.Location = new System.Drawing.Point(12, 89);
            this.ConvergencePrecisionL.Name = "ConvergencePrecisionL";
            this.ConvergencePrecisionL.Size = new System.Drawing.Size(119, 13);
            this.ConvergencePrecisionL.TabIndex = 20;
            this.ConvergencePrecisionL.Text = "Точночть сходимости:";
            // 
            // ConvergencePrecisionTB
            // 
            this.ConvergencePrecisionTB.Location = new System.Drawing.Point(152, 86);
            this.ConvergencePrecisionTB.Name = "ConvergencePrecisionTB";
            this.ConvergencePrecisionTB.Size = new System.Drawing.Size(93, 20);
            this.ConvergencePrecisionTB.TabIndex = 19;
            // 
            // LearningSpeedTB
            // 
            this.LearningSpeedTB.Location = new System.Drawing.Point(152, 60);
            this.LearningSpeedTB.Name = "LearningSpeedTB";
            this.LearningSpeedTB.Size = new System.Drawing.Size(93, 20);
            this.LearningSpeedTB.TabIndex = 18;
            // 
            // LearningSpeedL
            // 
            this.LearningSpeedL.AutoSize = true;
            this.LearningSpeedL.Location = new System.Drawing.Point(12, 63);
            this.LearningSpeedL.Name = "LearningSpeedL";
            this.LearningSpeedL.Size = new System.Drawing.Size(107, 13);
            this.LearningSpeedL.TabIndex = 17;
            this.LearningSpeedL.Text = "Скорость обучения:";
            // 
            // MaxDistanceTB
            // 
            this.MaxDistanceTB.Location = new System.Drawing.Point(152, 34);
            this.MaxDistanceTB.Name = "MaxDistanceTB";
            this.MaxDistanceTB.Size = new System.Drawing.Size(93, 20);
            this.MaxDistanceTB.TabIndex = 16;
            // 
            // MaxDistanceL
            // 
            this.MaxDistanceL.AutoSize = true;
            this.MaxDistanceL.Location = new System.Drawing.Point(12, 37);
            this.MaxDistanceL.Name = "MaxDistanceL";
            this.MaxDistanceL.Size = new System.Drawing.Size(134, 13);
            this.MaxDistanceL.TabIndex = 15;
            this.MaxDistanceL.Text = "Предельное расстояние:";
            // 
            // SOMOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 190);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.SelfOrganisingKohonenNetworkL);
            this.Controls.Add(this.SecondsTB);
            this.Controls.Add(this.HoursTB);
            this.Controls.Add(this.SecondsL);
            this.Controls.Add(this.MinutesL);
            this.Controls.Add(this.MinutesTB);
            this.Controls.Add(this.HoursL);
            this.Controls.Add(this.TimeLimitL);
            this.Controls.Add(this.ConvergencePrecisionL);
            this.Controls.Add(this.ConvergencePrecisionTB);
            this.Controls.Add(this.LearningSpeedTB);
            this.Controls.Add(this.LearningSpeedL);
            this.Controls.Add(this.MaxDistanceTB);
            this.Controls.Add(this.MaxDistanceL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(278, 229);
            this.MinimumSize = new System.Drawing.Size(278, 229);
            this.Name = "SOMOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SOMOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.Label SelfOrganisingKohonenNetworkL;
        private System.Windows.Forms.TextBox SecondsTB;
        private System.Windows.Forms.TextBox HoursTB;
        private System.Windows.Forms.Label SecondsL;
        private System.Windows.Forms.Label MinutesL;
        private System.Windows.Forms.TextBox MinutesTB;
        private System.Windows.Forms.Label HoursL;
        private System.Windows.Forms.Label TimeLimitL;
        private System.Windows.Forms.Label ConvergencePrecisionL;
        private System.Windows.Forms.TextBox ConvergencePrecisionTB;
        private System.Windows.Forms.TextBox LearningSpeedTB;
        private System.Windows.Forms.Label LearningSpeedL;
        private System.Windows.Forms.TextBox MaxDistanceTB;
        private System.Windows.Forms.Label MaxDistanceL;
    }
}