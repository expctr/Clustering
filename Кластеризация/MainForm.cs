using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemLib;
using AlgorithmLib;
using ClusteringLib;
using System.Diagnostics;
using System.IO;
using System.Xml;
using ExcelLib;

namespace Кластеризация
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        public BackgroundWorker GetBackgroundWorker1()
        {
            return backgroundWorker1;
        }

        public ToolStripMenuItem GetDrawObjectListTSMI()
        {
            return DrawObjectListTSMI;
        }

        public ToolStripMenuItem GetSOMTSMI()
        {
            return SOMTSMI;
        }

        public ToolStripMenuItem GetGNGTSMI()
        {
            return GNGTSMI;
        }

        public ToolStripMenuItem GetKMeansTSMI()
        {
            return KMeansTSMI;
        }

        public ToolStripMenuItem GetAglomerativeTSMI()
        {
            return AglomerativeTSMI;
        }

        public ToolStripMenuItem GetDBSCANTSMI()
        {
            return DBSCANTSMI;
        }

        public ToolStripMenuItem GetAffinityPropagationTSMI()
        {
            return AffinityPropagationTSMI;
        }

        public ToolStripMenuItem GetFORELTSMI()
        {
            return FORELTSMI;
        }

        public ToolStripMenuItem GetMinimumSpanningTreeTSMI()
        {
            return MinimumSpanningTreeTSMI;
        }

        public ToolStripMenuItem GetFullGraphTSMI()
        {
            return FullGraphTSMI;
        }

        public ToolStripMenuItem GetStartClusterizationTSMI()
        {
            return StartClusterizationTSMI;
        }

        public ToolStripMenuItem GetContinueClusterizationTSMI()
        {
            return ContinueClusterizationTSMI;
        }

        public ToolStripMenuItem GetFinishClusterizationTSMI()
        {
            return FinishClusterizationTSMI;
        }

        public NumericUpDown GetClustersNumberNUD()
        {
            return ClustersNumberNUD;
        }

        public Button GetClusterInfoB()
        {
            return ClusterInfoB;
        }

        public Button GetShowObjectListB()
        {
            return ShowObjectListB;
        }

        public ToolStripMenuItem GetSaveAllClustersCSVTSMI()
        {
            return SaveAllClustersCSVTSMI;
        }

        public ToolStripMenuItem GetSaveShownClusterCSVTSMI()
        {
            return SaveShownClusterCSVTSMI;
        }

        public ToolStripMenuItem GetSaveAllClustersXMLTSMI()
        {
            return SaveAllClustersXMLTSMI;
        }

        public ToolStripMenuItem GetSaveShownClusterXMLTSMI()
        {
            return SaveShownClusterXMLTSMI;
        }

        public ToolStripMenuItem GetSaveAllClustersXLSXTSMI()
        {
            return SaveAllClustersXLSXTSMI;
        }

        public ToolStripMenuItem GetSaveShownClusterXLSXTSMI()
        {
            return SaveShownClusterXLSXTSMI;
        }

        public ToolStripMenuItem GetVisualizationTSMI()
        {
            return VisualizationTSMI;
        }

        public ToolStripMenuItem GetAlgorithmOptionsTSMI()
        {
            return AlgorithmOptionsTSMI;
        }

        public ToolStripMenuItem GetClusterizationParameterOptionsTSMI()
        {
            return ClusterizationParameterOptionsTSMI;
        }

        public Button GetFindClusterNameB()
        {
            return FindClusterNameB;
        }

        public Button GetFindClusterIndexB()
        {
            return FindClusterIndexB;
        }

        public ToolStripMenuItem GetDownloadObjectListCSVTSMI()
        {
            return DownloadObjectListCSVTSMI;
        }

        public ToolStripMenuItem GetDownloadObjectListXMLTSMI()
        {
            return DownloadObjectListXMLTSMI;
        }

        public ToolStripMenuItem GetDownloadObjectListXLSXTSMI()
        {
            return DownloadObjectListXLSXTSMI;
        }

        public Label GetDataGridViewL()
        {
            return DataGridViewL;
        }

        public ToolStripLabel GetClusterizationStatusL()
        {
            return ClusterizationStatusL;
        }

        public OpenFileDialog GetOpenFileDialog1()
        {
            return openFileDialog1;
        }

        public ToolStripLabel GetClusteringAlgorithmTSL()
        {
            return ClusteringAlgorithmTSL;
        }

        public ToolStripProgressBar GetClusterizationTSPB()
        {
            return ClusterizationTSPB;
        }

        public ToolStripTextBox GetEpochNumTSTB()
        {
            return EpochNumTSTB;
        }

        public TextBox GetObjectIDTB()
        {
            return ObjectIDTB;
        }

        public SaveFileDialog GetSaveFileDialog1()
        {
            return saveFileDialog1;
        }

        public DataGridView GetDataGridView1()
        {
            return dataGridView1;
        }

        //
        //Методы
        //

        //
        //Управление доступностью элементов управления
        //

        void Save_Availability(bool enabled)
        {
            SaveAllClustersCSVTSMI.Enabled = enabled;
            SaveAllClustersXMLTSMI.Enabled = enabled;
            SaveAllClustersXLSXTSMI.Enabled = enabled;
            SaveShownClusterCSVTSMI.Enabled = enabled;
            SaveShownClusterXMLTSMI.Enabled = enabled;
            SaveShownClusterXLSXTSMI.Enabled = enabled;
        }

        public void ClustersUpdated(ref List<Cluster> Clusters, int Dimension, string[] ColsNames)
        {
            if (Clusters == null || Clusters.Count == 0)
            {
                Clusters = new List<Cluster>();
                SecondState(ref Clusters, Dimension);
                return;
            }
            ThirdState(Dimension);
            ClustersNumberTB.Text = Clusters.Count.ToString();
            ClustersNumberNUD.Minimum = 0;
            ClustersNumberNUD.Maximum = Clusters.Count - 1;
            ShowItems(Clusters[0].GetElements(), "Кластер 0", Dimension, ColsNames);
        }

        //
        //Отображение
        //

        public void ShowItems(List<Item> items, string str, int Dimension, string[] ColsNames)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = Dimension + 2;
            for (int i = 0; i < dataGridView1.ColumnCount; ++i)
            {
                dataGridView1.Columns[i].Name = (i + 1).ToString();
            }
            string[] _ColsNames = new string[ColsNames.Length + 2];
            _ColsNames[0] = "Индекс";
            _ColsNames[1] = "Название";
            for (int i = 0; i < ColsNames.Length; ++i)
            {
                _ColsNames[i + 2] = ColsNames[i];
            }
            dataGridView1.Rows.Add(_ColsNames);
            DataGridViewL.Text = str;
            if (items == null) return;
            for (int i = 0; i < items.Count; ++i)
            {
                dataGridView1.Rows.Add(items[i].ToRow());
            }
        }

        public void ShowClusterizationInfo(ref List<Cluster> Clusters, int Dimension)
        {
            if (Clusters == null || Clusters.Count == 0)
            {
                SecondState(ref Clusters, Dimension);
                return;
            }
            MeanLinearIntraclusterDeviation_TotalTB.Text =
                Cluster.MeanLinearIntraclusterDeviation_Total(Clusters).ToString();
            MeanSquareIntraclusterDeviation_TotalTB.Text =
                Cluster.MeanSquareIntraclusterDeviation_Total(Clusters).ToString();
            LinearInterclusterDeviatioin_TotalTB.Text =
                Cluster.LinearInterclusterDeviation_Total(Clusters).ToString();
            MeanSquareInterclusterDeviationTB.Text =
                Cluster.MeanSquareInterclusterDeviation(Clusters).ToString();
        }

        //
        //Переход в состояние
        //

        public void FirstState()//нет объектов
        {
            SaveAllClustersCSVTSMI.Enabled = false;
            SaveShownClusterCSVTSMI.Enabled = false;
            SaveAllClustersXMLTSMI.Enabled = false;
            SaveShownClusterXMLTSMI.Enabled = false;
            SaveAllClustersXLSXTSMI.Enabled = false;
            SaveShownClusterXLSXTSMI.Enabled = false;
            ClusterizationParameterOptionsTSMI.Enabled = false;
            VisualizationTSMI.Enabled = false;
            StartClusterizationTSMI.Enabled = false;
            ContinueClusterizationTSMI.Enabled = false;
            FinishClusterizationTSMI.Enabled = false;
            ShowObjectListB.Enabled = false;
            ClustersNumberNUD.Enabled = false;
            ClusterInfoB.Enabled = false;
            ObjectIDTB.Enabled = false;
            FindClusterNameB.Enabled = false;
            FindClusterIndexB.Enabled = false;
            ClustersNumberNUD.Minimum = 0;
            ClustersNumberNUD.Maximum = 0;
            ClustersNumberNUD.Value = 0;
            ClustersNumberTB.Text = "";
            MeanLinearIntraclusterDeviation_TotalTB.Text = "";
            MeanSquareIntraclusterDeviation_TotalTB.Text = "";
            LinearInterclusterDeviatioin_TotalTB.Text = "";
            MeanSquareInterclusterDeviationTB.Text = "";
            ObjectIDTB.Text = "";
        }

        public void SecondState(ref List<Cluster> Clusters, int Dimension)//есть объекты, нет кластеров
        {
            Clusters = new List<Cluster>();
            ClusterizationParameterOptionsTSMI.Enabled = true;
            VisualizationTSMI.Enabled = Dimension == 2;
            StartClusterizationTSMI.Enabled = true;
            ShowObjectListB.Enabled = true;
            ClustersNumberNUD.Minimum = 0;
            ClustersNumberNUD.Maximum = 0;
            ClustersNumberNUD.Value = 0;
            ClustersNumberTB.Text = "";
            SaveAllClustersCSVTSMI.Enabled = false;
            SaveShownClusterCSVTSMI.Enabled = false;
            SaveAllClustersXMLTSMI.Enabled = false;
            SaveShownClusterXMLTSMI.Enabled = false;
            SaveAllClustersXLSXTSMI.Enabled = false;
            SaveShownClusterXLSXTSMI.Enabled = false;
            ClustersNumberNUD.Enabled = false;
            ClusterInfoB.Enabled = false;
            ObjectIDTB.Enabled = false;
            FindClusterNameB.Enabled = false;
            FindClusterIndexB.Enabled = false;
            MeanLinearIntraclusterDeviation_TotalTB.Text = "";
            MeanSquareIntraclusterDeviation_TotalTB.Text = "";
            LinearInterclusterDeviatioin_TotalTB.Text = "";
            MeanSquareInterclusterDeviationTB.Text = "";
            ObjectIDTB.Text = "";
            ContinueClusterizationTSMI.Enabled = false;
            EpochNumTSTB.Text = "";
        }

        public void ThirdState(int Dimension)//есть кластеры
        {
            SaveAllClustersCSVTSMI.Enabled = true;
            SaveShownClusterCSVTSMI.Enabled = true;
            SaveAllClustersXMLTSMI.Enabled = true;
            SaveShownClusterXMLTSMI.Enabled = true;
            SaveAllClustersXLSXTSMI.Enabled = true;
            SaveShownClusterXLSXTSMI.Enabled = true;
            ClusterizationParameterOptionsTSMI.Enabled = true;
            VisualizationTSMI.Enabled = Dimension == 2;
            StartClusterizationTSMI.Enabled = true;
            ShowObjectListB.Enabled = true;
            ClustersNumberNUD.Enabled = true;
            ClusterInfoB.Enabled = true;
            ObjectIDTB.Enabled = true;
            FindClusterNameB.Enabled = true;
            FindClusterIndexB.Enabled = true;
        }

        //
        //Обработчики событий
        //

        //
        public bool ApplyEnabled = true;

        public void ClusterizationOnRun()
        {
            StartClusterizationTSMI.Enabled = false;
            ContinueClusterizationTSMI.Enabled = false;
            FinishClusterizationTSMI.Enabled = true;
            DownloadObjectListCSVTSMI.Enabled = false;
            DownloadObjectListXMLTSMI.Enabled = false;
            DownloadObjectListXLSXTSMI.Enabled = false;
            DrawObjectListTSMI.Enabled = false;
            Save_Availability(false);
            ApplyEnabled = false;
            ClusterizationTSDDB.Enabled = false;
            ClusterizationStatusL.Text = "Кластеризация выполняется";
        }

        public void ClusterizationEnded(bool Continuable)
        {
            StartClusterizationTSMI.Enabled = true;
            if (Continuable)
                ContinueClusterizationTSMI.Enabled = true;
            FinishClusterizationTSMI.Enabled = false;
            DownloadObjectListCSVTSMI.Enabled = true;
            DownloadObjectListXMLTSMI.Enabled = true;
            DownloadObjectListXLSXTSMI.Enabled = true;
            DrawObjectListTSMI.Enabled = true;
            Save_Availability(true);
            ApplyEnabled = true;
            ClusterizationTSDDB.Enabled = true;
            ClusterizationStatusL.Text = "Кластеризация завершена";
        }

        public void ShowObjectListBClickEventHandler(int Dimension, string[] ColsNames, List<Item> Items)
        {
            DataGridViewL.Text = "Cписок объектов";
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = Dimension + 2;
            for (int i = 0; i < Dimension + 2; ++i)
            {
                dataGridView1.Columns[i].Name = (i + 1).ToString();
            }
            string[] _ColsNames = new string[Dimension + 2];
            _ColsNames[0] = "Индекс";
            _ColsNames[1] = "Название";
            for (int i = 2; i < _ColsNames.Length; ++i)
            {
                _ColsNames[i] = ColsNames[i - 2];
            }
            dataGridView1.Rows.Add(_ColsNames);
            for (int i = 0; i < Items.Count; ++i)
            {
                dataGridView1.Rows.Add(Items[i].ToRow());
            }
        }
    }
}
