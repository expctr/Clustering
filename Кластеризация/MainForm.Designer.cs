namespace Кластеризация
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MeanSquareInterclusterDeviationTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DataGridViewL = new System.Windows.Forms.Label();
            this.LinearInterclusterDeviatioin_TotalTB = new System.Windows.Forms.TextBox();
            this.MeanSquareIntraclusterDeviation_TotalTB = new System.Windows.Forms.TextBox();
            this.MeanLinearIntraclusterDeviation_TotalTB = new System.Windows.Forms.TextBox();
            this.ClustersNumberTB = new System.Windows.Forms.TextBox();
            this.FindClusterIndexB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FindClusterNameB = new System.Windows.Forms.Button();
            this.ObjectIDTB = new System.Windows.Forms.TextBox();
            this.ClusterInfoB = new System.Windows.Forms.Button();
            this.ClustersNumberNUD = new System.Windows.Forms.NumericUpDown();
            this.ShowClusterL = new System.Windows.Forms.Label();
            this.ClustersNumberL = new System.Windows.Forms.Label();
            this.ShowObjectListB = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.EpochNumTSTB = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ClusterizationTSPB = new System.Windows.Forms.ToolStripProgressBar();
            this.ClusterizationStatusL = new System.Windows.Forms.ToolStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ObjectListTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadLObjectListTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadObjectListCSVTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadObjectListXMLTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DownloadObjectListXLSXTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DrawObjectListTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllClustersTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllClustersCSVTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllClustersXMLTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAllClustersXLSXTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveShownClusterTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveShownClusterCSVTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveShownClusterXMLTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveShownClusterXLSXTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.AlgorithmOptionsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClusterizationParameterOptionsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.VisualizationTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClusterizationTSDDB = new System.Windows.Forms.ToolStripDropDownButton();
            this.StartClusterizationTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ContinueClusterizationTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.FinishClusterizationTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClusteringAlgorithmTSMI = new System.Windows.Forms.ToolStripDropDownButton();
            this.SOMTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.GNGTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.KMeansTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.AglomerativeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DBSCANTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.AffinityPropagationTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.FORELTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.MinimumSpanningTreeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.FullGraphTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClusteringAlgorithmTSL = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClustersNumberNUD)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MeanSquareInterclusterDeviationTB
            // 
            this.MeanSquareInterclusterDeviationTB.Location = new System.Drawing.Point(243, 260);
            this.MeanSquareInterclusterDeviationTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MeanSquareInterclusterDeviationTB.Name = "MeanSquareInterclusterDeviationTB";
            this.MeanSquareInterclusterDeviationTB.ReadOnly = true;
            this.MeanSquareInterclusterDeviationTB.Size = new System.Drawing.Size(132, 22);
            this.MeanSquareInterclusterDeviationTB.TabIndex = 86;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 252);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 34);
            this.label3.TabIndex = 85;
            this.label3.Text = "Среднеквадратическое \r\nмежкластерное отклонение:";
            // 
            // DataGridViewL
            // 
            this.DataGridViewL.AutoSize = true;
            this.DataGridViewL.Location = new System.Drawing.Point(384, 108);
            this.DataGridViewL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DataGridViewL.Name = "DataGridViewL";
            this.DataGridViewL.Size = new System.Drawing.Size(83, 17);
            this.DataGridViewL.TabIndex = 84;
            this.DataGridViewL.Text = "Отображен";
            // 
            // LinearInterclusterDeviatioin_TotalTB
            // 
            this.LinearInterclusterDeviatioin_TotalTB.Location = new System.Drawing.Point(243, 220);
            this.LinearInterclusterDeviatioin_TotalTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LinearInterclusterDeviatioin_TotalTB.Name = "LinearInterclusterDeviatioin_TotalTB";
            this.LinearInterclusterDeviatioin_TotalTB.ReadOnly = true;
            this.LinearInterclusterDeviatioin_TotalTB.Size = new System.Drawing.Size(132, 22);
            this.LinearInterclusterDeviatioin_TotalTB.TabIndex = 83;
            // 
            // MeanSquareIntraclusterDeviation_TotalTB
            // 
            this.MeanSquareIntraclusterDeviation_TotalTB.Location = new System.Drawing.Point(243, 181);
            this.MeanSquareIntraclusterDeviation_TotalTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MeanSquareIntraclusterDeviation_TotalTB.Name = "MeanSquareIntraclusterDeviation_TotalTB";
            this.MeanSquareIntraclusterDeviation_TotalTB.ReadOnly = true;
            this.MeanSquareIntraclusterDeviation_TotalTB.Size = new System.Drawing.Size(132, 22);
            this.MeanSquareIntraclusterDeviation_TotalTB.TabIndex = 82;
            // 
            // MeanLinearIntraclusterDeviation_TotalTB
            // 
            this.MeanLinearIntraclusterDeviation_TotalTB.Location = new System.Drawing.Point(243, 142);
            this.MeanLinearIntraclusterDeviation_TotalTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MeanLinearIntraclusterDeviation_TotalTB.Name = "MeanLinearIntraclusterDeviation_TotalTB";
            this.MeanLinearIntraclusterDeviation_TotalTB.ReadOnly = true;
            this.MeanLinearIntraclusterDeviation_TotalTB.Size = new System.Drawing.Size(132, 22);
            this.MeanLinearIntraclusterDeviation_TotalTB.TabIndex = 81;
            // 
            // ClustersNumberTB
            // 
            this.ClustersNumberTB.Location = new System.Drawing.Point(315, 105);
            this.ClustersNumberTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClustersNumberTB.Name = "ClustersNumberTB";
            this.ClustersNumberTB.ReadOnly = true;
            this.ClustersNumberTB.Size = new System.Drawing.Size(60, 22);
            this.ClustersNumberTB.TabIndex = 80;
            // 
            // FindClusterIndexB
            // 
            this.FindClusterIndexB.Location = new System.Drawing.Point(12, 449);
            this.FindClusterIndexB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FindClusterIndexB.Name = "FindClusterIndexB";
            this.FindClusterIndexB.Size = new System.Drawing.Size(364, 28);
            this.FindClusterIndexB.TabIndex = 79;
            this.FindClusterIndexB.Text = "Указан индекс";
            this.FindClusterIndexB.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 362);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 17);
            this.label2.TabIndex = 78;
            this.label2.Text = "Информация об объекте";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 213);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(197, 34);
            this.label17.TabIndex = 77;
            this.label17.Text = "Суммарное линейное\r\nмежкластерное отклонение:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 174);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(226, 34);
            this.label14.TabIndex = 76;
            this.label14.Text = "Суммарное среднеквадратичное\r\nвнутрикластерное отклонение:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 134);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(216, 34);
            this.label11.TabIndex = 75;
            this.label11.Text = "Суммарное средниленейное\r\nвнутрикластерное отклонение:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(384, 142);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(667, 336);
            this.dataGridView1.TabIndex = 74;
            // 
            // FindClusterNameB
            // 
            this.FindClusterNameB.Location = new System.Drawing.Point(12, 414);
            this.FindClusterNameB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FindClusterNameB.Name = "FindClusterNameB";
            this.FindClusterNameB.Size = new System.Drawing.Size(364, 28);
            this.FindClusterNameB.TabIndex = 73;
            this.FindClusterNameB.Text = "Указано название";
            this.FindClusterNameB.UseVisualStyleBackColor = true;
            // 
            // ObjectIDTB
            // 
            this.ObjectIDTB.Location = new System.Drawing.Point(12, 382);
            this.ObjectIDTB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ObjectIDTB.Name = "ObjectIDTB";
            this.ObjectIDTB.Size = new System.Drawing.Size(363, 22);
            this.ObjectIDTB.TabIndex = 72;
            // 
            // ClusterInfoB
            // 
            this.ClusterInfoB.Location = new System.Drawing.Point(12, 324);
            this.ClusterInfoB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClusterInfoB.Name = "ClusterInfoB";
            this.ClusterInfoB.Size = new System.Drawing.Size(364, 28);
            this.ClusterInfoB.TabIndex = 71;
            this.ClusterInfoB.Text = "Информация об отображенном кластере";
            this.ClusterInfoB.UseVisualStyleBackColor = true;
            // 
            // ClustersNumberNUD
            // 
            this.ClustersNumberNUD.Location = new System.Drawing.Point(243, 292);
            this.ClustersNumberNUD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClustersNumberNUD.Name = "ClustersNumberNUD";
            this.ClustersNumberNUD.Size = new System.Drawing.Size(133, 22);
            this.ClustersNumberNUD.TabIndex = 70;
            // 
            // ShowClusterL
            // 
            this.ShowClusterL.AutoSize = true;
            this.ShowClusterL.Location = new System.Drawing.Point(8, 294);
            this.ShowClusterL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ShowClusterL.Name = "ShowClusterL";
            this.ShowClusterL.Size = new System.Drawing.Size(226, 17);
            this.ShowClusterL.TabIndex = 69;
            this.ShowClusterL.Text = "Отобразить кластер с индексом:";
            // 
            // ClustersNumberL
            // 
            this.ClustersNumberL.AutoSize = true;
            this.ClustersNumberL.Location = new System.Drawing.Point(176, 108);
            this.ClustersNumberL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ClustersNumberL.Name = "ClustersNumberL";
            this.ClustersNumberL.Size = new System.Drawing.Size(125, 17);
            this.ClustersNumberL.TabIndex = 68;
            this.ClustersNumberL.Text = "Число кластеров:";
            // 
            // ShowObjectListB
            // 
            this.ShowObjectListB.Location = new System.Drawing.Point(12, 102);
            this.ShowObjectListB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ShowObjectListB.Name = "ShowObjectListB";
            this.ShowObjectListB.Size = new System.Drawing.Size(156, 28);
            this.ShowObjectListB.TabIndex = 67;
            this.ShowObjectListB.Text = "Список объектов";
            this.ShowObjectListB.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.EpochNumTSTB,
            this.toolStripLabel2,
            this.ClusterizationTSPB,
            this.ClusterizationStatusL});
            this.toolStrip2.Location = new System.Drawing.Point(0, 61);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1067, 31);
            this.toolStrip2.TabIndex = 66;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(191, 28);
            this.toolStripLabel1.Text = "Эпох обучения пройдено:";
            // 
            // EpochNumTSTB
            // 
            this.EpochNumTSTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.EpochNumTSTB.Name = "EpochNumTSTB";
            this.EpochNumTSTB.ReadOnly = true;
            this.EpochNumTSTB.Size = new System.Drawing.Size(132, 31);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(78, 28);
            this.toolStripLabel2.Text = "Прогресс:";
            // 
            // ClusterizationTSPB
            // 
            this.ClusterizationTSPB.Name = "ClusterizationTSPB";
            this.ClusterizationTSPB.Size = new System.Drawing.Size(267, 28);
            // 
            // ClusterizationStatusL
            // 
            this.ClusterizationStatusL.Name = "ClusterizationStatusL";
            this.ClusterizationStatusL.Size = new System.Drawing.Size(160, 28);
            this.ClusterizationStatusL.Text = "Статус кластеризации";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ObjectListTSMI,
            this.сохранитьToolStripMenuItem,
            this.OptionsTSMI,
            this.VisualizationTSMI});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 30);
            this.menuStrip1.TabIndex = 64;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ObjectListTSMI
            // 
            this.ObjectListTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadLObjectListTSMI,
            this.DrawObjectListTSMI});
            this.ObjectListTSMI.Name = "ObjectListTSMI";
            this.ObjectListTSMI.Size = new System.Drawing.Size(142, 26);
            this.ObjectListTSMI.Text = "Список объектов";
            // 
            // DownloadLObjectListTSMI
            // 
            this.DownloadLObjectListTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadObjectListCSVTSMI,
            this.DownloadObjectListXMLTSMI,
            this.DownloadObjectListXLSXTSMI});
            this.DownloadLObjectListTSMI.Name = "DownloadLObjectListTSMI";
            this.DownloadLObjectListTSMI.Size = new System.Drawing.Size(175, 26);
            this.DownloadLObjectListTSMI.Text = "Скачать";
            // 
            // DownloadObjectListCSVTSMI
            // 
            this.DownloadObjectListCSVTSMI.Name = "DownloadObjectListCSVTSMI";
            this.DownloadObjectListCSVTSMI.Size = new System.Drawing.Size(119, 26);
            this.DownloadObjectListCSVTSMI.Text = ".csv";
            // 
            // DownloadObjectListXMLTSMI
            // 
            this.DownloadObjectListXMLTSMI.Name = "DownloadObjectListXMLTSMI";
            this.DownloadObjectListXMLTSMI.Size = new System.Drawing.Size(119, 26);
            this.DownloadObjectListXMLTSMI.Text = ".xml";
            // 
            // DownloadObjectListXLSXTSMI
            // 
            this.DownloadObjectListXLSXTSMI.Name = "DownloadObjectListXLSXTSMI";
            this.DownloadObjectListXLSXTSMI.Size = new System.Drawing.Size(119, 26);
            this.DownloadObjectListXLSXTSMI.Text = ".xlsx";
            // 
            // DrawObjectListTSMI
            // 
            this.DrawObjectListTSMI.Name = "DrawObjectListTSMI";
            this.DrawObjectListTSMI.Size = new System.Drawing.Size(175, 26);
            this.DrawObjectListTSMI.Text = "Нарисовать";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveAllClustersTSMI,
            this.SaveShownClusterTSMI});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(97, 26);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // SaveAllClustersTSMI
            // 
            this.SaveAllClustersTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveAllClustersCSVTSMI,
            this.SaveAllClustersXMLTSMI,
            this.SaveAllClustersXLSXTSMI});
            this.SaveAllClustersTSMI.Name = "SaveAllClustersTSMI";
            this.SaveAllClustersTSMI.Size = new System.Drawing.Size(258, 26);
            this.SaveAllClustersTSMI.Text = "Все кластеры";
            // 
            // SaveAllClustersCSVTSMI
            // 
            this.SaveAllClustersCSVTSMI.Name = "SaveAllClustersCSVTSMI";
            this.SaveAllClustersCSVTSMI.Size = new System.Drawing.Size(119, 26);
            this.SaveAllClustersCSVTSMI.Text = ".csv";
            // 
            // SaveAllClustersXMLTSMI
            // 
            this.SaveAllClustersXMLTSMI.Name = "SaveAllClustersXMLTSMI";
            this.SaveAllClustersXMLTSMI.Size = new System.Drawing.Size(119, 26);
            this.SaveAllClustersXMLTSMI.Text = ".xml";
            // 
            // SaveAllClustersXLSXTSMI
            // 
            this.SaveAllClustersXLSXTSMI.Name = "SaveAllClustersXLSXTSMI";
            this.SaveAllClustersXLSXTSMI.Size = new System.Drawing.Size(119, 26);
            this.SaveAllClustersXLSXTSMI.Text = ".xlsx";
            // 
            // SaveShownClusterTSMI
            // 
            this.SaveShownClusterTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveShownClusterCSVTSMI,
            this.SaveShownClusterXMLTSMI,
            this.SaveShownClusterXLSXTSMI});
            this.SaveShownClusterTSMI.Name = "SaveShownClusterTSMI";
            this.SaveShownClusterTSMI.Size = new System.Drawing.Size(258, 26);
            this.SaveShownClusterTSMI.Text = "Отображенный кластер";
            // 
            // SaveShownClusterCSVTSMI
            // 
            this.SaveShownClusterCSVTSMI.Name = "SaveShownClusterCSVTSMI";
            this.SaveShownClusterCSVTSMI.Size = new System.Drawing.Size(119, 26);
            this.SaveShownClusterCSVTSMI.Text = ".csv";
            // 
            // SaveShownClusterXMLTSMI
            // 
            this.SaveShownClusterXMLTSMI.Name = "SaveShownClusterXMLTSMI";
            this.SaveShownClusterXMLTSMI.Size = new System.Drawing.Size(119, 26);
            this.SaveShownClusterXMLTSMI.Text = ".xml";
            // 
            // SaveShownClusterXLSXTSMI
            // 
            this.SaveShownClusterXLSXTSMI.Name = "SaveShownClusterXLSXTSMI";
            this.SaveShownClusterXLSXTSMI.Size = new System.Drawing.Size(119, 26);
            this.SaveShownClusterXLSXTSMI.Text = ".xlsx";
            // 
            // OptionsTSMI
            // 
            this.OptionsTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AlgorithmOptionsTSMI,
            this.ClusterizationParameterOptionsTSMI});
            this.OptionsTSMI.Name = "OptionsTSMI";
            this.OptionsTSMI.Size = new System.Drawing.Size(98, 26);
            this.OptionsTSMI.Text = "Настройки";
            // 
            // AlgorithmOptionsTSMI
            // 
            this.AlgorithmOptionsTSMI.Name = "AlgorithmOptionsTSMI";
            this.AlgorithmOptionsTSMI.Size = new System.Drawing.Size(364, 26);
            this.AlgorithmOptionsTSMI.Text = "Настройки алгоритма";
            // 
            // ClusterizationParameterOptionsTSMI
            // 
            this.ClusterizationParameterOptionsTSMI.Name = "ClusterizationParameterOptionsTSMI";
            this.ClusterizationParameterOptionsTSMI.Size = new System.Drawing.Size(364, 26);
            this.ClusterizationParameterOptionsTSMI.Text = "Настройки параметров кластеризации";
            // 
            // VisualizationTSMI
            // 
            this.VisualizationTSMI.Name = "VisualizationTSMI";
            this.VisualizationTSMI.Size = new System.Drawing.Size(121, 26);
            this.VisualizationTSMI.Text = "Визуализация";
            // 
            // ClusterizationTSDDB
            // 
            this.ClusterizationTSDDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClusterizationTSDDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartClusterizationTSMI,
            this.ContinueClusterizationTSMI,
            this.FinishClusterizationTSMI});
            this.ClusterizationTSDDB.Image = ((System.Drawing.Image)(resources.GetObject("ClusterizationTSDDB.Image")));
            this.ClusterizationTSDDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClusterizationTSDDB.Name = "ClusterizationTSDDB";
            this.ClusterizationTSDDB.Size = new System.Drawing.Size(128, 28);
            this.ClusterizationTSDDB.Text = "Кластеризация";
            // 
            // StartClusterizationTSMI
            // 
            this.StartClusterizationTSMI.Name = "StartClusterizationTSMI";
            this.StartClusterizationTSMI.Size = new System.Drawing.Size(180, 26);
            this.StartClusterizationTSMI.Text = "Начать";
            // 
            // ContinueClusterizationTSMI
            // 
            this.ContinueClusterizationTSMI.Name = "ContinueClusterizationTSMI";
            this.ContinueClusterizationTSMI.Size = new System.Drawing.Size(180, 26);
            this.ContinueClusterizationTSMI.Text = "Продолжить";
            // 
            // FinishClusterizationTSMI
            // 
            this.FinishClusterizationTSMI.Name = "FinishClusterizationTSMI";
            this.FinishClusterizationTSMI.Size = new System.Drawing.Size(180, 26);
            this.FinishClusterizationTSMI.Text = "Завершить";
            // 
            // ClusteringAlgorithmTSMI
            // 
            this.ClusteringAlgorithmTSMI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ClusteringAlgorithmTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SOMTSMI,
            this.GNGTSMI,
            this.KMeansTSMI,
            this.AglomerativeTSMI,
            this.DBSCANTSMI,
            this.AffinityPropagationTSMI,
            this.FORELTSMI,
            this.MinimumSpanningTreeTSMI,
            this.FullGraphTSMI});
            this.ClusteringAlgorithmTSMI.Image = ((System.Drawing.Image)(resources.GetObject("ClusteringAlgorithmTSMI.Image")));
            this.ClusteringAlgorithmTSMI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClusteringAlgorithmTSMI.Name = "ClusteringAlgorithmTSMI";
            this.ClusteringAlgorithmTSMI.Size = new System.Drawing.Size(199, 28);
            this.ClusteringAlgorithmTSMI.Text = "Алгоритм кластеризации";
            // 
            // SOMTSMI
            // 
            this.SOMTSMI.Name = "SOMTSMI";
            this.SOMTSMI.Size = new System.Drawing.Size(353, 26);
            this.SOMTSMI.Text = "Самоорганизующаяся сеть Кохонена";
            // 
            // GNGTSMI
            // 
            this.GNGTSMI.Name = "GNGTSMI";
            this.GNGTSMI.Size = new System.Drawing.Size(353, 26);
            this.GNGTSMI.Text = "Растущий нейронный газ";
            // 
            // KMeansTSMI
            // 
            this.KMeansTSMI.Name = "KMeansTSMI";
            this.KMeansTSMI.Size = new System.Drawing.Size(353, 26);
            this.KMeansTSMI.Text = "К-средних";
            // 
            // AglomerativeTSMI
            // 
            this.AglomerativeTSMI.Name = "AglomerativeTSMI";
            this.AglomerativeTSMI.Size = new System.Drawing.Size(353, 26);
            this.AglomerativeTSMI.Text = "Агломеративная кластеризация";
            // 
            // DBSCANTSMI
            // 
            this.DBSCANTSMI.Name = "DBSCANTSMI";
            this.DBSCANTSMI.Size = new System.Drawing.Size(353, 26);
            this.DBSCANTSMI.Text = "DBSCAN";
            // 
            // AffinityPropagationTSMI
            // 
            this.AffinityPropagationTSMI.Name = "AffinityPropagationTSMI";
            this.AffinityPropagationTSMI.Size = new System.Drawing.Size(353, 26);
            this.AffinityPropagationTSMI.Text = "Affinity Propagation";
            // 
            // FORELTSMI
            // 
            this.FORELTSMI.Name = "FORELTSMI";
            this.FORELTSMI.Size = new System.Drawing.Size(353, 26);
            this.FORELTSMI.Text = "Формальные элементы";
            // 
            // MinimumSpanningTreeTSMI
            // 
            this.MinimumSpanningTreeTSMI.Name = "MinimumSpanningTreeTSMI";
            this.MinimumSpanningTreeTSMI.Size = new System.Drawing.Size(353, 26);
            this.MinimumSpanningTreeTSMI.Text = "Минимальное остовное дерево";
            // 
            // FullGraphTSMI
            // 
            this.FullGraphTSMI.Name = "FullGraphTSMI";
            this.FullGraphTSMI.Size = new System.Drawing.Size(353, 26);
            this.FullGraphTSMI.Text = "Полный граф";
            // 
            // ClusteringAlgorithmTSL
            // 
            this.ClusteringAlgorithmTSL.Name = "ClusteringAlgorithmTSL";
            this.ClusteringAlgorithmTSL.Size = new System.Drawing.Size(185, 28);
            this.ClusteringAlgorithmTSL.Text = "Алгоритм кластеризации";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClusterizationTSDDB,
            this.ClusteringAlgorithmTSMI,
            this.ClusteringAlgorithmTSL});
            this.toolStrip1.Location = new System.Drawing.Point(0, 30);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1067, 31);
            this.toolStrip1.TabIndex = 65;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 485);
            this.Controls.Add(this.MeanSquareInterclusterDeviationTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DataGridViewL);
            this.Controls.Add(this.LinearInterclusterDeviatioin_TotalTB);
            this.Controls.Add(this.MeanSquareIntraclusterDeviation_TotalTB);
            this.Controls.Add(this.MeanLinearIntraclusterDeviation_TotalTB);
            this.Controls.Add(this.ClustersNumberTB);
            this.Controls.Add(this.FindClusterIndexB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.FindClusterNameB);
            this.Controls.Add(this.ObjectIDTB);
            this.Controls.Add(this.ClusterInfoB);
            this.Controls.Add(this.ClustersNumberNUD);
            this.Controls.Add(this.ShowClusterL);
            this.Controls.Add(this.ClustersNumberL);
            this.Controls.Add(this.ShowObjectListB);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1082, 522);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Кластеризация данных";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClustersNumberNUD)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MeanSquareInterclusterDeviationTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DataGridViewL;
        private System.Windows.Forms.TextBox LinearInterclusterDeviatioin_TotalTB;
        private System.Windows.Forms.TextBox MeanSquareIntraclusterDeviation_TotalTB;
        private System.Windows.Forms.TextBox MeanLinearIntraclusterDeviation_TotalTB;
        private System.Windows.Forms.TextBox ClustersNumberTB;
        private System.Windows.Forms.Button FindClusterIndexB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button FindClusterNameB;
        private System.Windows.Forms.TextBox ObjectIDTB;
        private System.Windows.Forms.Button ClusterInfoB;
        private System.Windows.Forms.NumericUpDown ClustersNumberNUD;
        private System.Windows.Forms.Label ShowClusterL;
        private System.Windows.Forms.Label ClustersNumberL;
        private System.Windows.Forms.Button ShowObjectListB;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox EpochNumTSTB;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripProgressBar ClusterizationTSPB;
        private System.Windows.Forms.ToolStripLabel ClusterizationStatusL;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ObjectListTSMI;
        private System.Windows.Forms.ToolStripMenuItem DownloadLObjectListTSMI;
        private System.Windows.Forms.ToolStripMenuItem DownloadObjectListCSVTSMI;
        private System.Windows.Forms.ToolStripMenuItem DownloadObjectListXMLTSMI;
        private System.Windows.Forms.ToolStripMenuItem DownloadObjectListXLSXTSMI;
        private System.Windows.Forms.ToolStripMenuItem DrawObjectListTSMI;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAllClustersTSMI;
        private System.Windows.Forms.ToolStripMenuItem SaveAllClustersCSVTSMI;
        private System.Windows.Forms.ToolStripMenuItem SaveAllClustersXMLTSMI;
        private System.Windows.Forms.ToolStripMenuItem SaveAllClustersXLSXTSMI;
        private System.Windows.Forms.ToolStripMenuItem SaveShownClusterTSMI;
        private System.Windows.Forms.ToolStripMenuItem SaveShownClusterCSVTSMI;
        private System.Windows.Forms.ToolStripMenuItem SaveShownClusterXMLTSMI;
        private System.Windows.Forms.ToolStripMenuItem SaveShownClusterXLSXTSMI;
        private System.Windows.Forms.ToolStripMenuItem OptionsTSMI;
        private System.Windows.Forms.ToolStripMenuItem AlgorithmOptionsTSMI;
        private System.Windows.Forms.ToolStripMenuItem ClusterizationParameterOptionsTSMI;
        private System.Windows.Forms.ToolStripMenuItem VisualizationTSMI;
        private System.Windows.Forms.ToolStripDropDownButton ClusterizationTSDDB;
        private System.Windows.Forms.ToolStripMenuItem StartClusterizationTSMI;
        private System.Windows.Forms.ToolStripMenuItem ContinueClusterizationTSMI;
        private System.Windows.Forms.ToolStripMenuItem FinishClusterizationTSMI;
        private System.Windows.Forms.ToolStripDropDownButton ClusteringAlgorithmTSMI;
        private System.Windows.Forms.ToolStripMenuItem SOMTSMI;
        private System.Windows.Forms.ToolStripMenuItem GNGTSMI;
        private System.Windows.Forms.ToolStripMenuItem KMeansTSMI;
        private System.Windows.Forms.ToolStripMenuItem AglomerativeTSMI;
        private System.Windows.Forms.ToolStripMenuItem DBSCANTSMI;
        private System.Windows.Forms.ToolStripMenuItem AffinityPropagationTSMI;
        private System.Windows.Forms.ToolStripMenuItem FORELTSMI;
        private System.Windows.Forms.ToolStripMenuItem MinimumSpanningTreeTSMI;
        private System.Windows.Forms.ToolStripMenuItem FullGraphTSMI;
        private System.Windows.Forms.ToolStripLabel ClusteringAlgorithmTSL;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

