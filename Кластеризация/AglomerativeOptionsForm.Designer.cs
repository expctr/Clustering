namespace Кластеризация
{
    partial class AglomerativeOptionsForm
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
            this.WardDistanceChB = new System.Windows.Forms.CheckBox();
            this.CentreDistanceChB = new System.Windows.Forms.CheckBox();
            this.SingleLinkDistanceChB = new System.Windows.Forms.CheckBox();
            this.ApplyB = new System.Windows.Forms.Button();
            this.DetalizationCoefTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WardDistanceChB
            // 
            this.WardDistanceChB.AutoSize = true;
            this.WardDistanceChB.Location = new System.Drawing.Point(15, 97);
            this.WardDistanceChB.Name = "WardDistanceChB";
            this.WardDistanceChB.Size = new System.Drawing.Size(121, 17);
            this.WardDistanceChB.TabIndex = 13;
            this.WardDistanceChB.Text = "Расстояние Уорда";
            this.WardDistanceChB.UseVisualStyleBackColor = true;
            // 
            // CentreDistanceChB
            // 
            this.CentreDistanceChB.AutoSize = true;
            this.CentreDistanceChB.Location = new System.Drawing.Point(15, 74);
            this.CentreDistanceChB.Name = "CentreDistanceChB";
            this.CentreDistanceChB.Size = new System.Drawing.Size(174, 17);
            this.CentreDistanceChB.TabIndex = 12;
            this.CentreDistanceChB.Text = "Расстояние между центрами";
            this.CentreDistanceChB.UseVisualStyleBackColor = true;
            // 
            // SingleLinkDistanceChB
            // 
            this.SingleLinkDistanceChB.AutoSize = true;
            this.SingleLinkDistanceChB.Location = new System.Drawing.Point(15, 51);
            this.SingleLinkDistanceChB.Name = "SingleLinkDistanceChB";
            this.SingleLinkDistanceChB.Size = new System.Drawing.Size(191, 17);
            this.SingleLinkDistanceChB.TabIndex = 11;
            this.SingleLinkDistanceChB.Text = "Расстояние ближайшего соседа";
            this.SingleLinkDistanceChB.UseVisualStyleBackColor = true;
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 120);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(275, 23);
            this.ApplyB.TabIndex = 10;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // DetalizationCoefTB
            // 
            this.DetalizationCoefTB.Location = new System.Drawing.Point(190, 25);
            this.DetalizationCoefTB.Name = "DetalizationCoefTB";
            this.DetalizationCoefTB.Size = new System.Drawing.Size(100, 20);
            this.DetalizationCoefTB.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Детализирующий коэффициент:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Агломеративная кластеризация";
            // 
            // AglomerativeOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 155);
            this.Controls.Add(this.WardDistanceChB);
            this.Controls.Add(this.CentreDistanceChB);
            this.Controls.Add(this.SingleLinkDistanceChB);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.DetalizationCoefTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(314, 194);
            this.MinimumSize = new System.Drawing.Size(314, 194);
            this.Name = "AglomerativeOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox WardDistanceChB;
        private System.Windows.Forms.CheckBox CentreDistanceChB;
        private System.Windows.Forms.CheckBox SingleLinkDistanceChB;
        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.TextBox DetalizationCoefTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}