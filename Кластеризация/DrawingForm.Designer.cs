namespace Кластеризация
{
    partial class DrawingForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.MoveTSB = new System.Windows.Forms.ToolStripButton();
            this.DrawTSSB = new System.Windows.Forms.ToolStripSplitButton();
            this.ClickTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.CurveLineTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SprayingTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.Spraying15ptTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.Spraying30ptTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.EraseTSSB = new System.Windows.Forms.ToolStripSplitButton();
            this.EraserTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteAllTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowItemsTSB = new System.Windows.Forms.ToolStripButton();
            this.ViewAllPointsTSB = new System.Windows.Forms.ToolStripButton();
            this.SaveListTSB = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MoveTSB,
            this.DrawTSSB,
            this.EraseTSSB,
            this.ShowItemsTSB,
            this.ViewAllPointsTSB,
            this.SaveListTSB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(797, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MoveTSB
            // 
            this.MoveTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveTSB.Image = ((System.Drawing.Image)(resources.GetObject("MoveTSB.Image")));
            this.MoveTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveTSB.Name = "MoveTSB";
            this.MoveTSB.Size = new System.Drawing.Size(23, 22);
            this.MoveTSB.Text = "Перемещение";
            // 
            // DrawTSSB
            // 
            this.DrawTSSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DrawTSSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClickTSMI,
            this.CurveLineTSMI,
            this.SprayingTSMI});
            this.DrawTSSB.Image = ((System.Drawing.Image)(resources.GetObject("DrawTSSB.Image")));
            this.DrawTSSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DrawTSSB.Name = "DrawTSSB";
            this.DrawTSSB.Size = new System.Drawing.Size(32, 22);
            this.DrawTSSB.Text = "Рисование";
            // 
            // ClickTSMI
            // 
            this.ClickTSMI.Name = "ClickTSMI";
            this.ClickTSMI.Size = new System.Drawing.Size(150, 22);
            this.ClickTSMI.Text = "Клик";
            // 
            // CurveLineTSMI
            // 
            this.CurveLineTSMI.Name = "CurveLineTSMI";
            this.CurveLineTSMI.Size = new System.Drawing.Size(150, 22);
            this.CurveLineTSMI.Text = "Кривая линия";
            // 
            // SprayingTSMI
            // 
            this.SprayingTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Spraying15ptTSMI,
            this.Spraying30ptTSMI});
            this.SprayingTSMI.Name = "SprayingTSMI";
            this.SprayingTSMI.Size = new System.Drawing.Size(150, 22);
            this.SprayingTSMI.Text = "Распыление";
            // 
            // Spraying15ptTSMI
            // 
            this.Spraying15ptTSMI.Name = "Spraying15ptTSMI";
            this.Spraying15ptTSMI.Size = new System.Drawing.Size(97, 22);
            this.Spraying15ptTSMI.Text = "15pt";
            // 
            // Spraying30ptTSMI
            // 
            this.Spraying30ptTSMI.Name = "Spraying30ptTSMI";
            this.Spraying30ptTSMI.Size = new System.Drawing.Size(97, 22);
            this.Spraying30ptTSMI.Text = "30pt";
            // 
            // EraseTSSB
            // 
            this.EraseTSSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EraseTSSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EraserTSMI,
            this.DeleteAllTSMI});
            this.EraseTSSB.Image = ((System.Drawing.Image)(resources.GetObject("EraseTSSB.Image")));
            this.EraseTSSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EraseTSSB.Name = "EraseTSSB";
            this.EraseTSSB.Size = new System.Drawing.Size(32, 22);
            this.EraseTSSB.Text = "Стереть";
            // 
            // EraserTSMI
            // 
            this.EraserTSMI.Name = "EraserTSMI";
            this.EraserTSMI.Size = new System.Drawing.Size(173, 22);
            this.EraserTSMI.Text = "Ластик";
            // 
            // DeleteAllTSMI
            // 
            this.DeleteAllTSMI.Name = "DeleteAllTSMI";
            this.DeleteAllTSMI.Size = new System.Drawing.Size(173, 22);
            this.DeleteAllTSMI.Text = "Стереть все точки";
            // 
            // ShowItemsTSB
            // 
            this.ShowItemsTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShowItemsTSB.Image = ((System.Drawing.Image)(resources.GetObject("ShowItemsTSB.Image")));
            this.ShowItemsTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowItemsTSB.Name = "ShowItemsTSB";
            this.ShowItemsTSB.Size = new System.Drawing.Size(23, 22);
            this.ShowItemsTSB.Text = "Список предметов";
            // 
            // ViewAllPointsTSB
            // 
            this.ViewAllPointsTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewAllPointsTSB.Image = ((System.Drawing.Image)(resources.GetObject("ViewAllPointsTSB.Image")));
            this.ViewAllPointsTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewAllPointsTSB.Name = "ViewAllPointsTSB";
            this.ViewAllPointsTSB.Size = new System.Drawing.Size(23, 22);
            this.ViewAllPointsTSB.Text = "Показать все точки";
            // 
            // SaveListTSB
            // 
            this.SaveListTSB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.SaveListTSB.Image = ((System.Drawing.Image)(resources.GetObject("SaveListTSB.Image")));
            this.SaveListTSB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveListTSB.Name = "SaveListTSB";
            this.SaveListTSB.Size = new System.Drawing.Size(69, 22);
            this.SaveListTSB.Text = "Сохранить";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(776, 410);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 453);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pictureBox1);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "DrawingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рисование списка объектов";
            this.Load += new System.EventHandler(this.DrawingForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton MoveTSB;
        private System.Windows.Forms.ToolStripSplitButton DrawTSSB;
        private System.Windows.Forms.ToolStripMenuItem ClickTSMI;
        private System.Windows.Forms.ToolStripMenuItem CurveLineTSMI;
        private System.Windows.Forms.ToolStripMenuItem SprayingTSMI;
        private System.Windows.Forms.ToolStripMenuItem Spraying15ptTSMI;
        private System.Windows.Forms.ToolStripMenuItem Spraying30ptTSMI;
        private System.Windows.Forms.ToolStripSplitButton EraseTSSB;
        private System.Windows.Forms.ToolStripMenuItem EraserTSMI;
        private System.Windows.Forms.ToolStripMenuItem DeleteAllTSMI;
        private System.Windows.Forms.ToolStripButton ShowItemsTSB;
        private System.Windows.Forms.ToolStripButton ViewAllPointsTSB;
        private System.Windows.Forms.ToolStripButton SaveListTSB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}