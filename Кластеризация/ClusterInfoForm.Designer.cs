namespace Кластеризация
{
    partial class ClusterInfoForm
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
            this.FindADBEB = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ADBEL = new System.Windows.Forms.Label();
            this.QuartileRangeL = new System.Windows.Forms.Label();
            this.DispersionL = new System.Windows.Forms.Label();
            this.MeanSquareDeviationL = new System.Windows.Forms.Label();
            this.MeanLinearDeviationL = new System.Windows.Forms.Label();
            this.SizeL = new System.Windows.Forms.Label();
            this.CentreCoordinatesL = new System.Windows.Forms.Label();
            this.CentreCoordinatesDGV = new System.Windows.Forms.DataGridView();
            this.ClusterIndexL = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.CentreCoordinatesDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // FindADBEB
            // 
            this.FindADBEB.Location = new System.Drawing.Point(16, 288);
            this.FindADBEB.Name = "FindADBEB";
            this.FindADBEB.Size = new System.Drawing.Size(214, 23);
            this.FindADBEB.TabIndex = 21;
            this.FindADBEB.Text = "Найти";
            this.FindADBEB.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 317);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(214, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // ADBEL
            // 
            this.ADBEL.AutoSize = true;
            this.ADBEL.Location = new System.Drawing.Point(13, 259);
            this.ADBEL.Name = "ADBEL";
            this.ADBEL.Size = new System.Drawing.Size(151, 26);
            this.ADBEL.TabIndex = 19;
            this.ADBEL.Text = "Среднее расстояние между \r\nэлементами:";
            // 
            // QuartileRangeL
            // 
            this.QuartileRangeL.AutoSize = true;
            this.QuartileRangeL.Location = new System.Drawing.Point(13, 237);
            this.QuartileRangeL.Name = "QuartileRangeL";
            this.QuartileRangeL.Size = new System.Drawing.Size(118, 13);
            this.QuartileRangeL.TabIndex = 18;
            this.QuartileRangeL.Text = "Квартильный размах:";
            // 
            // DispersionL
            // 
            this.DispersionL.AutoSize = true;
            this.DispersionL.Location = new System.Drawing.Point(13, 215);
            this.DispersionL.Name = "DispersionL";
            this.DispersionL.Size = new System.Drawing.Size(67, 13);
            this.DispersionL.TabIndex = 17;
            this.DispersionL.Text = "Дисперсия:";
            // 
            // MeanSquareDeviationL
            // 
            this.MeanSquareDeviationL.AutoSize = true;
            this.MeanSquareDeviationL.Location = new System.Drawing.Point(13, 193);
            this.MeanSquareDeviationL.Name = "MeanSquareDeviationL";
            this.MeanSquareDeviationL.Size = new System.Drawing.Size(191, 13);
            this.MeanSquareDeviationL.TabIndex = 16;
            this.MeanSquareDeviationL.Text = "Среднеквадратическое отклонение:";
            // 
            // MeanLinearDeviationL
            // 
            this.MeanLinearDeviationL.AutoSize = true;
            this.MeanLinearDeviationL.Location = new System.Drawing.Point(13, 171);
            this.MeanLinearDeviationL.Name = "MeanLinearDeviationL";
            this.MeanLinearDeviationL.Size = new System.Drawing.Size(166, 13);
            this.MeanLinearDeviationL.TabIndex = 15;
            this.MeanLinearDeviationL.Text = "Среднее линейное отклонение:";
            // 
            // SizeL
            // 
            this.SizeL.AutoSize = true;
            this.SizeL.Location = new System.Drawing.Point(13, 149);
            this.SizeL.Name = "SizeL";
            this.SizeL.Size = new System.Drawing.Size(49, 13);
            this.SizeL.TabIndex = 14;
            this.SizeL.Text = "Размер:";
            // 
            // CentreCoordinatesL
            // 
            this.CentreCoordinatesL.AutoSize = true;
            this.CentreCoordinatesL.Location = new System.Drawing.Point(10, 37);
            this.CentreCoordinatesL.Name = "CentreCoordinatesL";
            this.CentreCoordinatesL.Size = new System.Drawing.Size(110, 13);
            this.CentreCoordinatesL.TabIndex = 13;
            this.CentreCoordinatesL.Text = "Координаты центра:";
            // 
            // CentreCoordinatesDGV
            // 
            this.CentreCoordinatesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CentreCoordinatesDGV.Location = new System.Drawing.Point(13, 53);
            this.CentreCoordinatesDGV.Name = "CentreCoordinatesDGV";
            this.CentreCoordinatesDGV.Size = new System.Drawing.Size(275, 93);
            this.CentreCoordinatesDGV.TabIndex = 12;
            // 
            // ClusterIndexL
            // 
            this.ClusterIndexL.AutoSize = true;
            this.ClusterIndexL.Location = new System.Drawing.Point(10, 11);
            this.ClusterIndexL.Name = "ClusterIndexL";
            this.ClusterIndexL.Size = new System.Drawing.Size(111, 13);
            this.ClusterIndexL.TabIndex = 11;
            this.ClusterIndexL.Text = "Кластер с индексом";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // ClusterInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 350);
            this.Controls.Add(this.FindADBEB);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ADBEL);
            this.Controls.Add(this.QuartileRangeL);
            this.Controls.Add(this.DispersionL);
            this.Controls.Add(this.MeanSquareDeviationL);
            this.Controls.Add(this.MeanLinearDeviationL);
            this.Controls.Add(this.SizeL);
            this.Controls.Add(this.CentreCoordinatesL);
            this.Controls.Add(this.CentreCoordinatesDGV);
            this.Controls.Add(this.ClusterIndexL);
            this.MinimumSize = new System.Drawing.Size(315, 389);
            this.Name = "ClusterInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информация о кластере";
            this.Load += new System.EventHandler(this.ClusterInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CentreCoordinatesDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindADBEB;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label ADBEL;
        private System.Windows.Forms.Label QuartileRangeL;
        private System.Windows.Forms.Label DispersionL;
        private System.Windows.Forms.Label MeanSquareDeviationL;
        private System.Windows.Forms.Label MeanLinearDeviationL;
        private System.Windows.Forms.Label SizeL;
        private System.Windows.Forms.Label CentreCoordinatesL;
        private System.Windows.Forms.DataGridView CentreCoordinatesDGV;
        private System.Windows.Forms.Label ClusterIndexL;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}