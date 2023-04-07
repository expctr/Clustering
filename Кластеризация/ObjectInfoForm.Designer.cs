namespace Кластеризация
{
    partial class ObjectInfoForm
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
            this.ObjectIndexL = new System.Windows.Forms.Label();
            this.ObjectNameL = new System.Windows.Forms.Label();
            this.ObjectInfoDGV = new System.Windows.Forms.DataGridView();
            this.FindB = new System.Windows.Forms.Button();
            this.NumberOfElementsInRadius = new System.Windows.Forms.Label();
            this.RadiusTB = new System.Windows.Forms.TextBox();
            this.RadiusS = new System.Windows.Forms.Label();
            this.DistanceL = new System.Windows.Forms.Label();
            this.ClusterIndexL = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectInfoDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // ObjectIndexL
            // 
            this.ObjectIndexL.AutoSize = true;
            this.ObjectIndexL.Location = new System.Drawing.Point(12, 31);
            this.ObjectIndexL.Name = "ObjectIndexL";
            this.ObjectIndexL.Size = new System.Drawing.Size(185, 13);
            this.ObjectIndexL.TabIndex = 18;
            this.ObjectIndexL.Text = "Индекс объекта (в общем списке):";
            // 
            // ObjectNameL
            // 
            this.ObjectNameL.AutoSize = true;
            this.ObjectNameL.Location = new System.Drawing.Point(12, 9);
            this.ObjectNameL.Name = "ObjectNameL";
            this.ObjectNameL.Size = new System.Drawing.Size(105, 13);
            this.ObjectNameL.TabIndex = 17;
            this.ObjectNameL.Text = "Название объекта:";
            // 
            // ObjectInfoDGV
            // 
            this.ObjectInfoDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ObjectInfoDGV.Location = new System.Drawing.Point(12, 47);
            this.ObjectInfoDGV.Name = "ObjectInfoDGV";
            this.ObjectInfoDGV.Size = new System.Drawing.Size(275, 82);
            this.ObjectInfoDGV.TabIndex = 16;
            // 
            // FindB
            // 
            this.FindB.Location = new System.Drawing.Point(11, 226);
            this.FindB.Name = "FindB";
            this.FindB.Size = new System.Drawing.Size(170, 23);
            this.FindB.TabIndex = 15;
            this.FindB.Text = "Найти";
            this.FindB.UseVisualStyleBackColor = true;
            // 
            // NumberOfElementsInRadius
            // 
            this.NumberOfElementsInRadius.AutoSize = true;
            this.NumberOfElementsInRadius.Location = new System.Drawing.Point(9, 184);
            this.NumberOfElementsInRadius.Name = "NumberOfElementsInRadius";
            this.NumberOfElementsInRadius.Size = new System.Drawing.Size(130, 39);
            this.NumberOfElementsInRadius.TabIndex = 14;
            this.NumberOfElementsInRadius.Text = "Число элементов около\r\nобъекта в указанном \r\nрадиусе:";
            // 
            // RadiusTB
            // 
            this.RadiusTB.Location = new System.Drawing.Point(105, 161);
            this.RadiusTB.Name = "RadiusTB";
            this.RadiusTB.Size = new System.Drawing.Size(77, 20);
            this.RadiusTB.TabIndex = 13;
            // 
            // RadiusS
            // 
            this.RadiusS.AutoSize = true;
            this.RadiusS.Location = new System.Drawing.Point(9, 164);
            this.RadiusS.Name = "RadiusS";
            this.RadiusS.Size = new System.Drawing.Size(90, 13);
            this.RadiusS.TabIndex = 12;
            this.RadiusS.Text = "Введите радиус:";
            // 
            // DistanceL
            // 
            this.DistanceL.AutoSize = true;
            this.DistanceL.Location = new System.Drawing.Point(9, 145);
            this.DistanceL.Name = "DistanceL";
            this.DistanceL.Size = new System.Drawing.Size(173, 13);
            this.DistanceL.TabIndex = 11;
            this.DistanceL.Text = "Расстояние до центра кластера:";
            // 
            // ClusterIndexL
            // 
            this.ClusterIndexL.AutoSize = true;
            this.ClusterIndexL.Location = new System.Drawing.Point(9, 132);
            this.ClusterIndexL.Name = "ClusterIndexL";
            this.ClusterIndexL.Size = new System.Drawing.Size(98, 13);
            this.ClusterIndexL.TabIndex = 10;
            this.ClusterIndexL.Text = "Индекс кластера:";
            // 
            // ObjectInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 260);
            this.Controls.Add(this.ObjectIndexL);
            this.Controls.Add(this.ObjectNameL);
            this.Controls.Add(this.ObjectInfoDGV);
            this.Controls.Add(this.FindB);
            this.Controls.Add(this.NumberOfElementsInRadius);
            this.Controls.Add(this.RadiusTB);
            this.Controls.Add(this.RadiusS);
            this.Controls.Add(this.DistanceL);
            this.Controls.Add(this.ClusterIndexL);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(315, 299);
            this.MinimumSize = new System.Drawing.Size(315, 299);
            this.Name = "ObjectInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Информацмя об объекте";
            this.Load += new System.EventHandler(this.ObjectInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectInfoDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ObjectIndexL;
        private System.Windows.Forms.Label ObjectNameL;
        private System.Windows.Forms.DataGridView ObjectInfoDGV;
        private System.Windows.Forms.Button FindB;
        private System.Windows.Forms.Label NumberOfElementsInRadius;
        private System.Windows.Forms.TextBox RadiusTB;
        private System.Windows.Forms.Label RadiusS;
        private System.Windows.Forms.Label DistanceL;
        private System.Windows.Forms.Label ClusterIndexL;
    }
}