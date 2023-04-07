namespace Кластеризация
{
    partial class AffinityPropagationOptionsForm
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
            this.ConvergencePrecisionTB = new System.Windows.Forms.TextBox();
            this.SelfSimilarityTB = new System.Windows.Forms.TextBox();
            this.ApplyB = new System.Windows.Forms.Button();
            this.SecondsTB = new System.Windows.Forms.TextBox();
            this.HoursTB = new System.Windows.Forms.TextBox();
            this.SecondsL = new System.Windows.Forms.Label();
            this.MinutesL = new System.Windows.Forms.Label();
            this.MinutesTB = new System.Windows.Forms.TextBox();
            this.HoursL = new System.Windows.Forms.Label();
            this.TimeLimitL = new System.Windows.Forms.Label();
            this.ConvergencePrecisionL = new System.Windows.Forms.Label();
            this.SelfSimilarityL = new System.Windows.Forms.Label();
            this.AffinityPropagationL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConvergencePrecisionTB
            // 
            this.ConvergencePrecisionTB.Location = new System.Drawing.Point(169, 51);
            this.ConvergencePrecisionTB.Name = "ConvergencePrecisionTB";
            this.ConvergencePrecisionTB.Size = new System.Drawing.Size(76, 20);
            this.ConvergencePrecisionTB.TabIndex = 37;
            // 
            // SelfSimilarityTB
            // 
            this.SelfSimilarityTB.Location = new System.Drawing.Point(169, 25);
            this.SelfSimilarityTB.Name = "SelfSimilarityTB";
            this.SelfSimilarityTB.Size = new System.Drawing.Size(76, 20);
            this.SelfSimilarityTB.TabIndex = 36;
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 119);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(230, 23);
            this.ApplyB.TabIndex = 35;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // SecondsTB
            // 
            this.SecondsTB.Location = new System.Drawing.Point(224, 93);
            this.SecondsTB.Name = "SecondsTB";
            this.SecondsTB.Size = new System.Drawing.Size(21, 20);
            this.SecondsTB.TabIndex = 34;
            // 
            // HoursTB
            // 
            this.HoursTB.Location = new System.Drawing.Point(55, 93);
            this.HoursTB.Name = "HoursTB";
            this.HoursTB.Size = new System.Drawing.Size(21, 20);
            this.HoursTB.TabIndex = 33;
            // 
            // SecondsL
            // 
            this.SecondsL.AutoSize = true;
            this.SecondsL.Location = new System.Drawing.Point(164, 96);
            this.SecondsL.Name = "SecondsL";
            this.SecondsL.Size = new System.Drawing.Size(54, 13);
            this.SecondsL.TabIndex = 32;
            this.SecondsL.Text = "Секунды:";
            // 
            // MinutesL
            // 
            this.MinutesL.AutoSize = true;
            this.MinutesL.Location = new System.Drawing.Point(82, 96);
            this.MinutesL.Name = "MinutesL";
            this.MinutesL.Size = new System.Drawing.Size(49, 13);
            this.MinutesL.TabIndex = 31;
            this.MinutesL.Text = "Минуты:";
            // 
            // MinutesTB
            // 
            this.MinutesTB.Location = new System.Drawing.Point(137, 93);
            this.MinutesTB.Name = "MinutesTB";
            this.MinutesTB.Size = new System.Drawing.Size(21, 20);
            this.MinutesTB.TabIndex = 30;
            // 
            // HoursL
            // 
            this.HoursL.AutoSize = true;
            this.HoursL.Location = new System.Drawing.Point(12, 96);
            this.HoursL.Name = "HoursL";
            this.HoursL.Size = new System.Drawing.Size(38, 13);
            this.HoursL.TabIndex = 29;
            this.HoursL.Text = "Часы:";
            // 
            // TimeLimitL
            // 
            this.TimeLimitL.AutoSize = true;
            this.TimeLimitL.Location = new System.Drawing.Point(12, 74);
            this.TimeLimitL.Name = "TimeLimitL";
            this.TimeLimitL.Size = new System.Drawing.Size(225, 13);
            this.TimeLimitL.TabIndex = 28;
            this.TimeLimitL.Text = "Предельная длительность кластеризации:";
            // 
            // ConvergencePrecisionL
            // 
            this.ConvergencePrecisionL.AutoSize = true;
            this.ConvergencePrecisionL.Location = new System.Drawing.Point(12, 54);
            this.ConvergencePrecisionL.Name = "ConvergencePrecisionL";
            this.ConvergencePrecisionL.Size = new System.Drawing.Size(120, 13);
            this.ConvergencePrecisionL.TabIndex = 27;
            this.ConvergencePrecisionL.Text = "Точность сходимости:";
            // 
            // SelfSimilarityL
            // 
            this.SelfSimilarityL.AutoSize = true;
            this.SelfSimilarityL.Location = new System.Drawing.Point(12, 28);
            this.SelfSimilarityL.Name = "SelfSimilarityL";
            this.SelfSimilarityL.Size = new System.Drawing.Size(151, 13);
            this.SelfSimilarityL.TabIndex = 26;
            this.SelfSimilarityL.Text = "Коэффициент самоподобия:";
            // 
            // AffinityPropagationL
            // 
            this.AffinityPropagationL.AutoSize = true;
            this.AffinityPropagationL.Location = new System.Drawing.Point(12, 9);
            this.AffinityPropagationL.Name = "AffinityPropagationL";
            this.AffinityPropagationL.Size = new System.Drawing.Size(98, 13);
            this.AffinityPropagationL.TabIndex = 25;
            this.AffinityPropagationL.Text = "Affinity Propagation";
            // 
            // AffinityPropagationOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 160);
            this.Controls.Add(this.ConvergencePrecisionTB);
            this.Controls.Add(this.SelfSimilarityTB);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.SecondsTB);
            this.Controls.Add(this.HoursTB);
            this.Controls.Add(this.SecondsL);
            this.Controls.Add(this.MinutesL);
            this.Controls.Add(this.MinutesTB);
            this.Controls.Add(this.HoursL);
            this.Controls.Add(this.TimeLimitL);
            this.Controls.Add(this.ConvergencePrecisionL);
            this.Controls.Add(this.SelfSimilarityL);
            this.Controls.Add(this.AffinityPropagationL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(278, 199);
            this.MinimumSize = new System.Drawing.Size(278, 199);
            this.Name = "AffinityPropagationOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.AffinityPropagationOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ConvergencePrecisionTB;
        private System.Windows.Forms.TextBox SelfSimilarityTB;
        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.TextBox SecondsTB;
        private System.Windows.Forms.TextBox HoursTB;
        private System.Windows.Forms.Label SecondsL;
        private System.Windows.Forms.Label MinutesL;
        private System.Windows.Forms.TextBox MinutesTB;
        private System.Windows.Forms.Label HoursL;
        private System.Windows.Forms.Label TimeLimitL;
        private System.Windows.Forms.Label ConvergencePrecisionL;
        private System.Windows.Forms.Label SelfSimilarityL;
        private System.Windows.Forms.Label AffinityPropagationL;
    }
}