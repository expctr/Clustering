namespace Кластеризация
{
    partial class DBSCANOptionsForm
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
            this.ThresholdTB = new System.Windows.Forms.TextBox();
            this.ReachabilityRadiusTB = new System.Windows.Forms.TextBox();
            this.ThresholdL = new System.Windows.Forms.Label();
            this.ReachabilityRadiusL = new System.Windows.Forms.Label();
            this.DBSCANClusterizationL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 77);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(233, 23);
            this.ApplyB.TabIndex = 11;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // ThresholdTB
            // 
            this.ThresholdTB.Location = new System.Drawing.Point(148, 51);
            this.ThresholdTB.Name = "ThresholdTB";
            this.ThresholdTB.Size = new System.Drawing.Size(100, 20);
            this.ThresholdTB.TabIndex = 10;
            // 
            // ReachabilityRadiusTB
            // 
            this.ReachabilityRadiusTB.Location = new System.Drawing.Point(148, 25);
            this.ReachabilityRadiusTB.Name = "ReachabilityRadiusTB";
            this.ReachabilityRadiusTB.Size = new System.Drawing.Size(100, 20);
            this.ReachabilityRadiusTB.TabIndex = 9;
            // 
            // ThresholdL
            // 
            this.ThresholdL.AutoSize = true;
            this.ThresholdL.Location = new System.Drawing.Point(12, 54);
            this.ThresholdL.Name = "ThresholdL";
            this.ThresholdL.Size = new System.Drawing.Size(89, 13);
            this.ThresholdL.TabIndex = 8;
            this.ThresholdL.Text = "Порог кучности:";
            // 
            // ReachabilityRadiusL
            // 
            this.ReachabilityRadiusL.AutoSize = true;
            this.ReachabilityRadiusL.Location = new System.Drawing.Point(12, 28);
            this.ReachabilityRadiusL.Name = "ReachabilityRadiusL";
            this.ReachabilityRadiusL.Size = new System.Drawing.Size(123, 13);
            this.ReachabilityRadiusL.TabIndex = 7;
            this.ReachabilityRadiusL.Text = "Радиус достижимости:";
            // 
            // DBSCANClusterizationL
            // 
            this.DBSCANClusterizationL.AutoSize = true;
            this.DBSCANClusterizationL.Location = new System.Drawing.Point(12, 9);
            this.DBSCANClusterizationL.Name = "DBSCANClusterizationL";
            this.DBSCANClusterizationL.Size = new System.Drawing.Size(103, 13);
            this.DBSCANClusterizationL.TabIndex = 6;
            this.DBSCANClusterizationL.Text = "Алгоритм DBSCAN";
            // 
            // DBSCANOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 119);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.ThresholdTB);
            this.Controls.Add(this.ReachabilityRadiusTB);
            this.Controls.Add(this.ThresholdL);
            this.Controls.Add(this.ReachabilityRadiusL);
            this.Controls.Add(this.DBSCANClusterizationL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(283, 158);
            this.MinimumSize = new System.Drawing.Size(283, 158);
            this.Name = "DBSCANOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.DBSCANOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.TextBox ThresholdTB;
        private System.Windows.Forms.TextBox ReachabilityRadiusTB;
        private System.Windows.Forms.Label ThresholdL;
        private System.Windows.Forms.Label ReachabilityRadiusL;
        private System.Windows.Forms.Label DBSCANClusterizationL;
    }
}