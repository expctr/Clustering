namespace Кластеризация
{
    partial class ClusterizationParameterOptionsForm
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
            this.ChangeWeightChB = new System.Windows.Forms.CheckBox();
            this.ShowClusteringParameterWeight = new System.Windows.Forms.Button();
            this.NormalizeChB = new System.Windows.Forms.CheckBox();
            this.ClusteringParameterWeightL = new System.Windows.Forms.Label();
            this.ClusteringParameterWeightTB = new System.Windows.Forms.TextBox();
            this.ClusteringParameterNameTB = new System.Windows.Forms.TextBox();
            this.ClusteringParameterNameL = new System.Windows.Forms.Label();
            this.ApplyB = new System.Windows.Forms.Button();
            this.CCPChLB = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // ChangeWeightChB
            // 
            this.ChangeWeightChB.AutoSize = true;
            this.ChangeWeightChB.Location = new System.Drawing.Point(12, 171);
            this.ChangeWeightChB.Name = "ChangeWeightChB";
            this.ChangeWeightChB.Size = new System.Drawing.Size(194, 17);
            this.ChangeWeightChB.TabIndex = 17;
            this.ChangeWeightChB.Text = "Изменить весовой коэффициент";
            this.ChangeWeightChB.UseVisualStyleBackColor = true;
            // 
            // ShowClusteringParameterWeight
            // 
            this.ShowClusteringParameterWeight.Location = new System.Drawing.Point(12, 123);
            this.ShowClusteringParameterWeight.Name = "ShowClusteringParameterWeight";
            this.ShowClusteringParameterWeight.Size = new System.Drawing.Size(300, 23);
            this.ShowClusteringParameterWeight.TabIndex = 16;
            this.ShowClusteringParameterWeight.Text = "Отобразить вес данного параметра";
            this.ShowClusteringParameterWeight.UseVisualStyleBackColor = true;
            // 
            // NormalizeChB
            // 
            this.NormalizeChB.AutoSize = true;
            this.NormalizeChB.Location = new System.Drawing.Point(12, 194);
            this.NormalizeChB.Name = "NormalizeChB";
            this.NormalizeChB.Size = new System.Drawing.Size(102, 17);
            this.NormalizeChB.TabIndex = 15;
            this.NormalizeChB.Text = "Нормализация";
            this.NormalizeChB.UseVisualStyleBackColor = true;
            // 
            // ClusteringParameterWeightL
            // 
            this.ClusteringParameterWeightL.AutoSize = true;
            this.ClusteringParameterWeightL.Location = new System.Drawing.Point(9, 155);
            this.ClusteringParameterWeightL.Name = "ClusteringParameterWeightL";
            this.ClusteringParameterWeightL.Size = new System.Drawing.Size(125, 13);
            this.ClusteringParameterWeightL.TabIndex = 14;
            this.ClusteringParameterWeightL.Text = "Весовой коэффициент:";
            // 
            // ClusteringParameterWeightTB
            // 
            this.ClusteringParameterWeightTB.Location = new System.Drawing.Point(213, 152);
            this.ClusteringParameterWeightTB.Name = "ClusteringParameterWeightTB";
            this.ClusteringParameterWeightTB.Size = new System.Drawing.Size(99, 20);
            this.ClusteringParameterWeightTB.TabIndex = 13;
            // 
            // ClusteringParameterNameTB
            // 
            this.ClusteringParameterNameTB.Location = new System.Drawing.Point(213, 97);
            this.ClusteringParameterNameTB.Name = "ClusteringParameterNameTB";
            this.ClusteringParameterNameTB.Size = new System.Drawing.Size(99, 20);
            this.ClusteringParameterNameTB.TabIndex = 12;
            // 
            // ClusteringParameterNameL
            // 
            this.ClusteringParameterNameL.AutoSize = true;
            this.ClusteringParameterNameL.Location = new System.Drawing.Point(9, 100);
            this.ClusteringParameterNameL.Name = "ClusteringParameterNameL";
            this.ClusteringParameterNameL.Size = new System.Drawing.Size(198, 13);
            this.ClusteringParameterNameL.TabIndex = 11;
            this.ClusteringParameterNameL.Text = "Название параметра кластеризации:";
            // 
            // ApplyB
            // 
            this.ApplyB.Location = new System.Drawing.Point(12, 217);
            this.ApplyB.Name = "ApplyB";
            this.ApplyB.Size = new System.Drawing.Size(300, 23);
            this.ApplyB.TabIndex = 10;
            this.ApplyB.Text = "Применить";
            this.ApplyB.UseVisualStyleBackColor = true;
            // 
            // CCPChLB
            // 
            this.CCPChLB.FormattingEnabled = true;
            this.CCPChLB.Location = new System.Drawing.Point(12, 12);
            this.CCPChLB.Name = "CCPChLB";
            this.CCPChLB.Size = new System.Drawing.Size(300, 79);
            this.CCPChLB.TabIndex = 9;
            // 
            // ClusterizationParameterOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 253);
            this.Controls.Add(this.ChangeWeightChB);
            this.Controls.Add(this.ShowClusteringParameterWeight);
            this.Controls.Add(this.NormalizeChB);
            this.Controls.Add(this.ClusteringParameterWeightL);
            this.Controls.Add(this.ClusteringParameterWeightTB);
            this.Controls.Add(this.ClusteringParameterNameTB);
            this.Controls.Add(this.ClusteringParameterNameL);
            this.Controls.Add(this.ApplyB);
            this.Controls.Add(this.CCPChLB);
            this.MinimumSize = new System.Drawing.Size(340, 292);
            this.Name = "ClusterizationParameterOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор параметров кластеризации";
            this.Load += new System.EventHandler(this.ClusterizationParameterOptionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChangeWeightChB;
        private System.Windows.Forms.Button ShowClusteringParameterWeight;
        private System.Windows.Forms.CheckBox NormalizeChB;
        private System.Windows.Forms.Label ClusteringParameterWeightL;
        private System.Windows.Forms.TextBox ClusteringParameterWeightTB;
        private System.Windows.Forms.TextBox ClusteringParameterNameTB;
        private System.Windows.Forms.Label ClusteringParameterNameL;
        private System.Windows.Forms.Button ApplyB;
        private System.Windows.Forms.CheckedListBox CCPChLB;
    }
}