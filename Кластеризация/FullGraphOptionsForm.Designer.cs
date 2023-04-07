namespace Кластеризация
{
    partial class FullGraphOptionsForm
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
            this.MaxDistanceTB = new System.Windows.Forms.TextBox();
            this.MaxDistanceL = new System.Windows.Forms.Label();
            this.FullGraphL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 51);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(237, 23);
            this.ApplyB.TabIndex = 7;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // MaxDistanceTB
            // 
            this.MaxDistanceTB.Location = new System.Drawing.Point(152, 25);
            this.MaxDistanceTB.Name = "MaxDistanceTB";
            this.MaxDistanceTB.Size = new System.Drawing.Size(100, 20);
            this.MaxDistanceTB.TabIndex = 6;
            // 
            // MaxDistanceL
            // 
            this.MaxDistanceL.AutoSize = true;
            this.MaxDistanceL.Location = new System.Drawing.Point(12, 28);
            this.MaxDistanceL.Name = "MaxDistanceL";
            this.MaxDistanceL.Size = new System.Drawing.Size(134, 13);
            this.MaxDistanceL.TabIndex = 5;
            this.MaxDistanceL.Text = "Предельное расстояние:";
            // 
            // FullGraphL
            // 
            this.FullGraphL.AutoSize = true;
            this.FullGraphL.Location = new System.Drawing.Point(12, 9);
            this.FullGraphL.Name = "FullGraphL";
            this.FullGraphL.Size = new System.Drawing.Size(170, 13);
            this.FullGraphL.TabIndex = 4;
            this.FullGraphL.Text = "Кластеризация полным графом";
            // 
            // FullGraphOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 90);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.MaxDistanceTB);
            this.Controls.Add(this.MaxDistanceL);
            this.Controls.Add(this.FullGraphL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(287, 129);
            this.MinimumSize = new System.Drawing.Size(287, 129);
            this.Name = "FullGraphOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.FullGraphOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.TextBox MaxDistanceTB;
        private System.Windows.Forms.Label MaxDistanceL;
        private System.Windows.Forms.Label FullGraphL;
    }
}