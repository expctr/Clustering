namespace Кластеризация
{
    partial class FORELOptionsForm
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
            this.ReachabilityRadiusTB = new System.Windows.Forms.TextBox();
            this.ReachabilityRadiusL = new System.Windows.Forms.Label();
            this.FORELL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(15, 51);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(226, 23);
            this.ApplyB.TabIndex = 7;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // ReachabilityRadiusTB
            // 
            this.ReachabilityRadiusTB.Location = new System.Drawing.Point(141, 25);
            this.ReachabilityRadiusTB.Name = "ReachabilityRadiusTB";
            this.ReachabilityRadiusTB.Size = new System.Drawing.Size(100, 20);
            this.ReachabilityRadiusTB.TabIndex = 6;
            // 
            // ReachabilityRadiusL
            // 
            this.ReachabilityRadiusL.AutoSize = true;
            this.ReachabilityRadiusL.Location = new System.Drawing.Point(12, 28);
            this.ReachabilityRadiusL.Name = "ReachabilityRadiusL";
            this.ReachabilityRadiusL.Size = new System.Drawing.Size(123, 13);
            this.ReachabilityRadiusL.TabIndex = 5;
            this.ReachabilityRadiusL.Text = "Радиус достижимости:";
            // 
            // FORELL
            // 
            this.FORELL.AutoSize = true;
            this.FORELL.Location = new System.Drawing.Point(12, 9);
            this.FORELL.Name = "FORELL";
            this.FORELL.Size = new System.Drawing.Size(48, 13);
            this.FORELL.TabIndex = 4;
            this.FORELL.Text = "ФОРЭЛ";
            // 
            // FORELOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 88);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.ReachabilityRadiusTB);
            this.Controls.Add(this.ReachabilityRadiusL);
            this.Controls.Add(this.FORELL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(272, 127);
            this.MinimumSize = new System.Drawing.Size(272, 127);
            this.Name = "FORELOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.FORELOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.TextBox ReachabilityRadiusTB;
        private System.Windows.Forms.Label ReachabilityRadiusL;
        private System.Windows.Forms.Label FORELL;
    }
}