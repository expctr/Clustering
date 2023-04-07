namespace Кластеризация
{
    partial class KMeansOptionsForm
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
            this.NodesNumberTB = new System.Windows.Forms.TextBox();
            this.SecondsTB = new System.Windows.Forms.TextBox();
            this.HoursTB = new System.Windows.Forms.TextBox();
            this.SecondsL = new System.Windows.Forms.Label();
            this.MinutesL = new System.Windows.Forms.Label();
            this.MinutesTB = new System.Windows.Forms.TextBox();
            this.HoursL = new System.Windows.Forms.Label();
            this.TimeLimitL = new System.Windows.Forms.Label();
            this.NodesNumberL = new System.Windows.Forms.Label();
            this.KMeansClusteringL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 96);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(230, 23);
            this.ApplyB.TabIndex = 32;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // NodesNumberTB
            // 
            this.NodesNumberTB.Location = new System.Drawing.Point(178, 28);
            this.NodesNumberTB.Name = "NodesNumberTB";
            this.NodesNumberTB.Size = new System.Drawing.Size(67, 20);
            this.NodesNumberTB.TabIndex = 31;
            // 
            // SecondsTB
            // 
            this.SecondsTB.Location = new System.Drawing.Point(224, 70);
            this.SecondsTB.Name = "SecondsTB";
            this.SecondsTB.Size = new System.Drawing.Size(21, 20);
            this.SecondsTB.TabIndex = 30;
            // 
            // HoursTB
            // 
            this.HoursTB.Location = new System.Drawing.Point(55, 70);
            this.HoursTB.Name = "HoursTB";
            this.HoursTB.Size = new System.Drawing.Size(21, 20);
            this.HoursTB.TabIndex = 29;
            // 
            // SecondsL
            // 
            this.SecondsL.AutoSize = true;
            this.SecondsL.Location = new System.Drawing.Point(164, 73);
            this.SecondsL.Name = "SecondsL";
            this.SecondsL.Size = new System.Drawing.Size(54, 13);
            this.SecondsL.TabIndex = 28;
            this.SecondsL.Text = "Секунды:";
            // 
            // MinutesL
            // 
            this.MinutesL.AutoSize = true;
            this.MinutesL.Location = new System.Drawing.Point(82, 73);
            this.MinutesL.Name = "MinutesL";
            this.MinutesL.Size = new System.Drawing.Size(49, 13);
            this.MinutesL.TabIndex = 27;
            this.MinutesL.Text = "Минуты:";
            // 
            // MinutesTB
            // 
            this.MinutesTB.Location = new System.Drawing.Point(137, 70);
            this.MinutesTB.Name = "MinutesTB";
            this.MinutesTB.Size = new System.Drawing.Size(21, 20);
            this.MinutesTB.TabIndex = 26;
            // 
            // HoursL
            // 
            this.HoursL.AutoSize = true;
            this.HoursL.Location = new System.Drawing.Point(12, 73);
            this.HoursL.Name = "HoursL";
            this.HoursL.Size = new System.Drawing.Size(38, 13);
            this.HoursL.TabIndex = 25;
            this.HoursL.Text = "Часы:";
            // 
            // TimeLimitL
            // 
            this.TimeLimitL.AutoSize = true;
            this.TimeLimitL.Location = new System.Drawing.Point(12, 51);
            this.TimeLimitL.Name = "TimeLimitL";
            this.TimeLimitL.Size = new System.Drawing.Size(225, 13);
            this.TimeLimitL.TabIndex = 24;
            this.TimeLimitL.Text = "Предельная длительность кластеризации:";
            // 
            // NodesNumberL
            // 
            this.NodesNumberL.AutoSize = true;
            this.NodesNumberL.Location = new System.Drawing.Point(12, 31);
            this.NodesNumberL.Name = "NodesNumberL";
            this.NodesNumberL.Size = new System.Drawing.Size(160, 13);
            this.NodesNumberL.TabIndex = 23;
            this.NodesNumberL.Text = "Предельное число кластеров:";
            // 
            // KMeansClusteringL
            // 
            this.KMeansClusteringL.AutoSize = true;
            this.KMeansClusteringL.Location = new System.Drawing.Point(12, 9);
            this.KMeansClusteringL.Name = "KMeansClusteringL";
            this.KMeansClusteringL.Size = new System.Drawing.Size(65, 13);
            this.KMeansClusteringL.TabIndex = 22;
            this.KMeansClusteringL.Text = "К - Средних";
            // 
            // KMeansOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 133);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.NodesNumberTB);
            this.Controls.Add(this.SecondsTB);
            this.Controls.Add(this.HoursTB);
            this.Controls.Add(this.SecondsL);
            this.Controls.Add(this.MinutesL);
            this.Controls.Add(this.MinutesTB);
            this.Controls.Add(this.HoursL);
            this.Controls.Add(this.TimeLimitL);
            this.Controls.Add(this.NodesNumberL);
            this.Controls.Add(this.KMeansClusteringL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(274, 172);
            this.MinimumSize = new System.Drawing.Size(274, 172);
            this.Name = "KMeansOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.KMeansOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.TextBox NodesNumberTB;
        private System.Windows.Forms.TextBox SecondsTB;
        private System.Windows.Forms.TextBox HoursTB;
        private System.Windows.Forms.Label SecondsL;
        private System.Windows.Forms.Label MinutesL;
        private System.Windows.Forms.TextBox MinutesTB;
        private System.Windows.Forms.Label HoursL;
        private System.Windows.Forms.Label TimeLimitL;
        private System.Windows.Forms.Label NodesNumberL;
        private System.Windows.Forms.Label KMeansClusteringL;
    }
}