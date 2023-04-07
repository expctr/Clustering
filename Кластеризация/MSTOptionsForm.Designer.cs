namespace Кластеризация
{
    partial class MSTOptionsForm
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
            this.ClustersNumberTB = new System.Windows.Forms.TextBox();
            this.ClustersNumberL = new System.Windows.Forms.Label();
            this.MSTL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 51);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(201, 23);
            this.ApplyB.TabIndex = 7;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // ClustersNumberTB
            // 
            this.ClustersNumberTB.Location = new System.Drawing.Point(116, 25);
            this.ClustersNumberTB.Name = "ClustersNumberTB";
            this.ClustersNumberTB.Size = new System.Drawing.Size(100, 20);
            this.ClustersNumberTB.TabIndex = 6;
            // 
            // ClustersNumberL
            // 
            this.ClustersNumberL.AutoSize = true;
            this.ClustersNumberL.Location = new System.Drawing.Point(12, 28);
            this.ClustersNumberL.Name = "ClustersNumberL";
            this.ClustersNumberL.Size = new System.Drawing.Size(98, 13);
            this.ClustersNumberL.TabIndex = 5;
            this.ClustersNumberL.Text = "Число кластеров:";
            // 
            // MSTL
            // 
            this.MSTL.AutoSize = true;
            this.MSTL.Location = new System.Drawing.Point(12, 9);
            this.MSTL.Name = "MSTL";
            this.MSTL.Size = new System.Drawing.Size(167, 13);
            this.MSTL.TabIndex = 4;
            this.MSTL.Text = "Минимальное остовное дерево";
            // 
            // MSTOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 92);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.ClustersNumberTB);
            this.Controls.Add(this.ClustersNumberL);
            this.Controls.Add(this.MSTL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(250, 131);
            this.MinimumSize = new System.Drawing.Size(250, 131);
            this.Name = "MSTOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.MSTOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.TextBox ClustersNumberTB;
        private System.Windows.Forms.Label ClustersNumberL;
        private System.Windows.Forms.Label MSTL;
    }
}