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
        enum ClusteringAlgorithm
        {
            SOM, GNG, KMeans, Aglomerative, DBSCAN, AffinityPropagation,
            FOREL, MST, FullGraph, NULL
        };

        ClusteringParameterOptions CPOptions = new ClusteringParameterOptions();

        List<Item> OriginalItems = new List<Item>();

        List<Item> Items = new List<Item>();

        List<Cluster> Clusters = new List<Cluster>();

        TimeLimitMaster timeLimitMaster;

        ClusteringAlgorithm clusteringAlgorithm = ClusteringAlgorithm.NULL;

        IClustering clusteringClass;

        Stopwatch Watch = new Stopwatch();

        bool Continuable;

        string[] OriginalColsNames;

        string[] ColsNames;

        ToolStripDropDownButton ClusteringAlgorithmTSDDB;

        // MainFormViewMaster ViewMaster;

        public MainForm()
        {
            InitializeComponent();

            SizeChanged += MainForm_SizeChanged;
            //
            timeLimitMaster = new TimeLimitMaster(true, 0, 0, 10);
            //
            backgroundWorker1.DoWork += backgroundWorker_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            //
            DrawObjectListTSMI.Click += DrawObjectListTSMI_Click;
            //
            //
            //
            SOMTSMI.Click += SOMTSMI_Click;
            GNGTSMI.Click += GNGTSMI_Click;
            KMeansTSMI.Click += KMeansTSMI_Click;
            AglomerativeTSMI.Click += AglomerativeTSMI_Click;
            DBSCANTSMI.Click += DBSCANTSMI_Click;
            AffinityPropagationTSMI.Click += AffinityPropagationTSMI_Click;
            FORELTSMI.Click += FORELTSMI_Click;
            MinimumSpanningTreeTSMI.Click += MSTTSMI_Click;
            FullGraphTSMI.Click += FullGraphTSMI_Click;
            //
            StartClusterizationTSMI.Click += StartClusterizationTSMI_Click;
            ContinueClusterizationTSMI.Click += ContinueClusterizationTSMI_Click;
            FinishClusterizationTSMI.Click += FinishClusterizationTSMI_Click;
            //
            //
            //
            //
            ClustersNumberNUD.Enabled = false;
            ClustersNumberNUD.ValueChanged += ClustersNumberNUD_ValueChanged;
            //
            ClusterInfoB.Click += ClusterInfoB_Click;
            //
            //
            //
            ShowObjectListB.Click += ShowObjectListB_Click;
            //
            SaveAllClustersCSVTSMI.Click += SaveAllClustersCSVTSMI_Click;
            SaveShownClusterCSVTSMI.Click += SaveShownClusterCSVTSMI_Click;
            SaveAllClustersXMLTSMI.Click += SaveAllClustersXMLTSMI_Click;
            SaveShownClusterXMLTSMI.Click += SaveShownClusterXMLTSMI_Click;
            SaveAllClustersXLSXTSMI.Click += SaveAllClustersXLSXTSMI_Click;
            SaveShownClusterXLSXTSMI.Click += SaveShownClusterXLSXTSMI_Click;
            //
            //
            VisualizationTSMI.Click += VisualizationTSMI_Click;
            //
            DataGridViewL.Text = "";
            //
            AlgorithmOptionsTSMI.Click += AlgorithmOptionsTSMI_Click;
            ClusterizationParameterOptionsTSMI.Click +=
                ClusterizationParameterOptionsTSMI_Click;
            //
            FindClusterNameB.Click += FindClusterNameB_Click;
            FindClusterIndexB.Click += FindClusterIndexB_Click;
            //
            //
            ClusterizationStatusL.Text = "";
            //
            //
            DownloadObjectListCSVTSMI.Click += DownloadObjectListCSVTSMI_Click;
            DownloadObjectListXMLTSMI.Click += DownloadObjectListXMLTSMI_Click;
            DownloadObjectListXLSXTSMI.Click += DownloadObjectListXLSXTSMI_Click;
            //
            //
            SOMTSMI_Click(new object(), new EventArgs());
            //
            FirstState();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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

        //
        //Установить список объектов
        //

        public void SetItems(List<Item> items, string[] colsNames)
        {
            if (items == null || items.Count == 0 || colsNames == null ||
                colsNames.Length == 0)
            {
                return;
            }
            //
            OriginalColsNames = colsNames;
            ColsNames = (string[])OriginalColsNames.Clone();
            //
            OriginalItems = items;
            //
            CPOptions.ChosenClusterizationParameter = new bool[ColsNames.Length];
            for (int i = 0; i < CPOptions.ChosenClusterizationParameter.Length; ++i)
            {
                CPOptions.ChosenClusterizationParameter[i] = true;
            }
            //
            Item.AttachIndexes(ref OriginalItems);
            CPOptions.DimensionalWeights = new double[Dimension];
            for (int i = 0; i < Dimension; ++i)
            {
                CPOptions.DimensionalWeights[i] = 1;
            }
            CPOptions.Normalize = false;
            Items = new List<Item>(OriginalItems);
            clusteringClass.SetItems(Items);
            SecondState();
            ShowObjectListB_Click(new object(), new EventArgs());
        }

        //
        //Настройки
        //

        public void SetOptions(ClusteringOptions opt)
        {
            ContinueClusterizationTSMI.Enabled = false;
            clusteringClass.SetOptions(opt);
            timeLimitMaster.SetOptions(opt);
        }

        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = clusteringClass.GetOptions();
            timeLimitMaster.AddOptions(result);
            return result;
        }

        //
        //Выбор параметров кластеризации
        //

        public void SetClusterizationParameterOptions(ClusteringParameterOptions
            _CPOptions)
        {
            CPOptions = _CPOptions;
            Transform();
        }

        public ClusteringParameterOptions GetClusterizationParameterOptions()
        {
            ClusteringParameterOptions result = new ClusteringParameterOptions();
            result.ChosenClusterizationParameter = (bool[])CPOptions.ChosenClusterizationParameter.Clone();
            result.DimensionalWeights = (double[])CPOptions.DimensionalWeights.Clone();
            result.Normalize = CPOptions.Normalize;
            return result;
        }

        public string[] GetOriginalColsNames()
        {
            return (string[])OriginalColsNames.Clone();
        }

        Item TransformItem(Item item, bool[] chosenCoord,
            double[] dimensionalWeights)
        {
            List<double> NewCoordinatesList = new List<double>();
            for (int i = 0; i < chosenCoord.Length; ++i)
            {
                if (chosenCoord[i])
                {
                    NewCoordinatesList.Add(item[i] * dimensionalWeights[i]);
                }
            }
            double[] NewCoordinates = new double[NewCoordinatesList.Count];
            for (int i = 0; i < NewCoordinatesList.Count; ++i)
            {
                NewCoordinates[i] = NewCoordinatesList[i];
            }
            return new Item(NewCoordinates, item.Name, item.Index);
        }

        List<Item> TransformItems(List<Item> items, bool[] chosenCoord,
            double[] dimensionalWeights, bool normalize)
        {
            List<Item> result = new List<Item>();
            foreach (var item in items)
            {
                result.Add(TransformItem(item, chosenCoord, dimensionalWeights));
            }
            if (normalize)
            {
                List<double[]> coordList = new List<double[]>();
                foreach (var item in result)
                {
                    coordList.Add(item.GetCoordinates);
                }
                coordList = EuclideanGeometryLib.EuclideanGeometry.Normalize(coordList);
                for (int i = 0; i < result.Count; ++i)
                {
                    result[i] = new Item(coordList[i], result[i].Name, result[i].Index);
                }
            }
            return result;
        }

        string[] TransformColsNames(string[] colsNames, bool[] chosenCoord)
        {
            List<string> NewColsNamesList = new List<string>();
            for (int i = 0; i < chosenCoord.Length; ++i)
            {
                if (chosenCoord[i])
                {
                    NewColsNamesList.Add(colsNames[i]);
                }
            }
            string[] result = new string[NewColsNamesList.Count];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = NewColsNamesList[i];
            }
            return result;
        }

        void Transform()
        {
            Items = TransformItems(OriginalItems, CPOptions.ChosenClusterizationParameter,
                CPOptions.DimensionalWeights, CPOptions.Normalize);
            ColsNames = TransformColsNames(OriginalColsNames, CPOptions.ChosenClusterizationParameter);
            clusteringClass.SetItems(Items);
            Clusters = new List<Cluster>();
            ClustersUpdated();
            ShowObjectListB_Click(new object(), new EventArgs());
        }

        //
        int Dimension
        {
            get
            {
                if (ColsNames == null)
                {
                    return 0;
                }
                return ColsNames.Length;
            }
        }
        //

        void ClustersUpdated()
        {
            if (Clusters == null || Clusters.Count == 0)
            {
                Clusters = new List<Cluster>();
                SecondState();
                return;
            }
            ThirdState();
            ClustersNumberTB.Text = Clusters.Count.ToString();
            ClustersNumberNUD.Minimum = 0;
            ClustersNumberNUD.Maximum = Clusters.Count - 1;
            ShowItems(Clusters[0].GetElements(), "Кластер 0");
        }

        //
        //Отображение
        //

        void ShowItems(List<Item> items, string str)
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

        void ShowClusterizationInfo()
        {
            if (Clusters == null || Clusters.Count == 0)
            {
                SecondState();
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
        //Сохранение файла
        //
        List<string> PrintCluster_CSV(Cluster cluster, bool quotes)
        {
            List<string> result = new List<string>();
            result.Add(Algorithm.ToString_CSV(ColsNames, quotes));
            List<Item> items = cluster.GetElements();
            foreach (var item in items)
            {
                result.Add(item.Print_CSV());
            }
            return result;
        }

        List<string> PrintClusters_CSV()
        {
            List<string> result = new List<string>();
            result.AddRange(PrintCluster_CSV(Clusters[0], false));
            for (int i = 1; i < Clusters.Count; ++i)
            {
                result.AddRange(PrintCluster_CSV(Clusters[i], true));
            }
            return result;
        }

        XmlDocument PrintClusterXML(Cluster cluster)
        {
            List<Item> items = cluster.GetElements();
            XmlDocument result = new XmlDocument();
            XmlDeclaration xmlDecl = result.CreateXmlDeclaration("1.0", "utf-8", null);
            result.AppendChild(xmlDecl);
            XmlElement clusterElement =
                result.CreateElement("Cluster");
            for (int i = 0; i < cluster.Count; ++i)
            {
                clusterElement.AppendChild(
                    cluster[i].Print_XML(result,
                    ColsNames));
            }
            result.AppendChild(clusterElement);
            return result;
        }

        XmlElement PrintClusterXML(Cluster cluster, XmlDocument xmlDoc)
        {
            List<Item> items = cluster.GetElements();
            XmlElement result = xmlDoc.CreateElement("Cluster");
            for (int i = 0; i < cluster.Count; ++i)
            {
                result.AppendChild(
                    cluster[i].Print_XML(xmlDoc,
                    ColsNames));
            }
            return result;
        }

        XmlDocument PrintClustersXML()
        {
            XmlDocument result = new XmlDocument();
            XmlDeclaration xmlDecl = result.CreateXmlDeclaration("1.0", "utf-8", null);
            result.AppendChild(xmlDecl);
            XmlElement clustersElement = result.CreateElement("Clusters");
            foreach (var cluster in Clusters)
            {
                clustersElement.AppendChild(PrintClusterXML(cluster, result));
            }
            result.AppendChild(clustersElement);
            return result;
        }

        //
        //Скачивание файла
        //
        private void DownloadObjectListCSVTSMI_Click(object sender, EventArgs e)
        {
            StreamReader reader;
            try
            {
                openFileDialog1.Filter = "(*.csv)|*.csv|(*.txt)|*.txt";
                openFileDialog1.FileName = "";
                string Path;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Path = openFileDialog1.FileName;
                    try
                    {
                        reader = new StreamReader(Path, Encoding.GetEncoding(1251));
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается открыть файл \"{Path}\" для" +
                            $" чтения.");
                        return;
                    }
                    string str = reader.ReadLine();
                    if (str == null)
                    {
                        MessageBox.Show("Выбранный файл некорректен (первая строка пуста).");
                        reader.Dispose();
                        return;
                    }
                    string[] colsNames = Algorithm.GetColsNames_CSV(str);
                    List<Item> items = new List<Item>();
                    int rowNum = 2;
                    while (null != (str = reader.ReadLine()) && str.Length != 0)
                    {
                        try
                        {
                            Item item = Algorithm.GetItem_CSV(str, colsNames.Length);
                            items.Add(item);
                        }
                        catch
                        {
                            MessageBox.Show($"Ошибка. Строка {rowNum} файла " +
                                $"\"{Path}\" некорректна.");
                            MessageBox.Show(str);//del
                            reader.Dispose();
                            return;
                        }
                        ++rowNum;
                    }
                    if (items.Count == 0)
                    {
                        MessageBox.Show($"Ошибка. Файл \"{Path}\" содержал пустой список" +
                            " объектов.");
                        reader.Dispose();
                        return;
                    }
                    SetItems(items, colsNames);
                }
                MessageBox.Show("Скачивание завершено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                reader = null;
            }
        }

        private void DownloadObjectListXMLTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "(*.xml)|*.xml";
                openFileDialog1.FileName = "";
                string Path;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Path = openFileDialog1.FileName;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Path);
                    if (!xmlDoc.ChildNodes[1].HasChildNodes)
                    {
                        MessageBox.Show($"Ошибка. Файл \"{Path}\" не содержит список " +
                            $"объектов.");
                        return;
                    }
                    string[] colsNames = new string[0];
                    List<Item> items = new List<Item>();
                    try
                    {
                        items = Item.GetItems_XML(xmlDoc, out colsNames);
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка.  Файл \"{Path}\" некорректен.");
                        return;
                    }
                    SetItems(items, colsNames);
                }
                MessageBox.Show("Скачивание завершено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DownloadObjectListXLSXTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "(*.xlsx)|*.xlsx";
                openFileDialog1.FileName = "";
                string Path;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Path = openFileDialog1.FileName;
                    Excel excel = new Excel(Path, 1);
                    string cur = "";
                    int k = 1;
                    List<string> colsNamesList = new List<string>();
                    while ("" != (cur = excel.ReadCell(0, k++, 1)))
                    {
                        colsNamesList.Add(cur);
                    }
                    string[] colsNames = new string[colsNamesList.Count];
                    for (int i = 0; i < colsNames.Length; ++i)
                    {
                        colsNames[i] = colsNamesList[i];
                    }
                    List<Item> items = new List<Item>();
                    for (int rowNum = 1; ; ++rowNum)
                    {
                        try
                        {
                            items.Add(Item.GetItem_XLSX(excel, 1, rowNum, colsNames.Length));
                        }
                        catch
                        {
                            break;
                        }
                    }
                    excel.Close();
                    if (items.Count == 0)
                    {
                        MessageBox.Show($"Ошибка. Файл \"{Path}\" содержит пустой список объектов.");
                        return;
                    }
                    SetItems(items, colsNames);
                }
                MessageBox.Show("Скачивание завершено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //
        //Переход в состояние
        //

        void FirstState()//нет объектов
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

        void SecondState()//есть объекты, нет кластеров
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

        void ThirdState()//есть кластеры
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
        //Кластеризация
        //

        //
        void SOMTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = new SelfOrganisingKohonenNetwork(5, 0.05, 0.05, Items);
            timeLimitMaster.SetFinish(() => { clusteringClass.StopFlag = true; });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                timeLimitMaster.Check();
                if (Watch.Elapsed.Milliseconds > 50)
                {
                    backgroundWorker1.ReportProgress((int)x);
                    Watch.Restart();
                }
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.SOM;
            ClusteringAlgorithmTSL.Text = SOMTSMI.Text;
            Continuable = true;
        }

        void GNGTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = new GrowingNeuralGassNetwork(0.05, 0.0006, 10, 10, Items.Count / 5, 0.7, 0.9, 0.05, Items);
            timeLimitMaster.SetFinish(() => { clusteringClass.StopFlag = true; });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                timeLimitMaster.Check();
                if (Watch.Elapsed.Milliseconds > 100)
                {
                    backgroundWorker1.ReportProgress((int)x);
                    Watch.Restart();
                }
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.GNG;
            ClusteringAlgorithmTSL.Text = GNGTSMI.Text;
            Continuable = true;
        }

        void KMeansTSMI_Click(object sendr, EventArgs e)
        {
            clusteringClass = new KMeansClusteringClass(1, 0, Items);
            timeLimitMaster.SetFinish(() => { clusteringClass.StopFlag = true; });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                timeLimitMaster.Check();
                if (Watch.Elapsed.Milliseconds > 100)
                {
                    backgroundWorker1.ReportProgress((int)x);
                    Watch.Restart();
                }
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.KMeans;
            ClusteringAlgorithmTSL.Text = KMeansTSMI.Text;
            Continuable = true;
        }

        void AglomerativeTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = new AglomerativeClusteringClass(1,
                ClusteringOptions.AglomerativeClusteringDistance.SingleLink,
                Items);
            clusteringClass.debugEvent += (string message) => {
                MessageBox.Show(message);
            };
            timeLimitMaster.SetFinish(() => { });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                backgroundWorker1.ReportProgress((int)(x * ClusterizationTSPB.Maximum));
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.Aglomerative;
            ClusteringAlgorithmTSL.Text = AglomerativeTSMI.Text;
            Continuable = false;
        }

        void DBSCANTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = new DBSCANClusteringClass(0.5, 5, Items);
            clusteringClass.debugEvent += (string message) => {
                MessageBox.Show(message);
            };
            timeLimitMaster.SetFinish(() => { });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                backgroundWorker1.ReportProgress((int)(x * ClusterizationTSPB.Maximum));
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.DBSCAN;
            ClusteringAlgorithmTSL.Text = DBSCANTSMI.Text;
            Continuable = false;
        }

        void AffinityPropagationTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = new AffinityPropagationClusteringClass(-5, 0.01, Items);
            timeLimitMaster.SetFinish(() => { clusteringClass.StopFlag = true; });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                timeLimitMaster.Check();
                if (Watch.Elapsed.Milliseconds > 100)
                {
                    backgroundWorker1.ReportProgress((int)x);
                    Watch.Restart();
                }
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.AffinityPropagation;
            ClusteringAlgorithmTSL.Text = AffinityPropagationTSMI.Text;
            Continuable = true;
        }

        void FORELTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = clusteringClass = new FORELClusteringClass(5, Items);
            timeLimitMaster.SetFinish(() => { });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                backgroundWorker1.ReportProgress((int)(x * ClusterizationTSPB.Maximum));
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.FOREL;
            ClusteringAlgorithmTSL.Text = FORELTSMI.Text;
            Continuable = false;
        }

        void MSTTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = clusteringClass = new MinimumSpanningTreeClusteringClass(1, Items);
            clusteringClass.debugEvent += str => {
                MessageBox.Show(str);
            };//del
            timeLimitMaster.SetFinish(() => { });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                backgroundWorker1.ReportProgress((int)(x * ClusterizationTSPB.Maximum));
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.MST;
            ClusteringAlgorithmTSL.Text = MinimumSpanningTreeTSMI.Text;
            Continuable = false;
        }

        void FullGraphTSMI_Click(object sender, EventArgs e)
        {
            clusteringClass = clusteringClass = new FullGraphClusteringClass(5, Items);
            timeLimitMaster.SetFinish(() => { });
            //
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            backgroundWorker1.ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            backgroundWorker1.ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                backgroundWorker1.ReportProgress((int)(x * ClusterizationTSPB.Maximum));
            };
            //
            clusteringAlgorithm = ClusteringAlgorithm.FullGraph;
            ClusteringAlgorithmTSL.Text = FullGraphTSMI.Text;
            Continuable = false;
        }

        //
        bool ApplyEnabled = true;

        void ClusterizationOnRun()
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
            ClusteringAlgorithmTSDDB.Enabled = false;
            ClusterizationStatusL.Text = "Кластеризация выполняется";
        }

        void StartClusterizationTSMI_Click(object sender, EventArgs e)
        {
            curEpoch = 0;
            EpochNumTSTB.Text = "0";
            ClusterizationOnRun();
            backgroundWorker1.RunWorkerAsync();
        }

        void ContinueClusterizationTSMI_Click(object sender, EventArgs e)
        {
            ClusterizationOnRun();
            backgroundWorker1.RunWorkerAsync();
        }

        void FinishClusterizationTSMI_Click(object sender, EventArgs e)
        {
            FinishClusterizationTSMI.Enabled = false;
            Algorithm.DoWait(() => { clusteringClass.Stop(); }, 1);
        }

        //
        //Многопоточность
        //
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Watch.Restart();
            timeLimitMaster.Activate();
            List<Cluster> clusters;
            try
            {
                clusters = Cluster.CreateClusters(clusteringClass.GetClusters());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                clusters = new List<Cluster>();
            }
            Algorithm.DoWait(() => { Clusters = clusters; }, 1);
        }
        int curEpoch;

        private void backgroundWorker_ProgressChanged_TSTB(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                curEpoch = checked(curEpoch + e.ProgressPercentage);
            }
            catch
            {
                EpochNumTSTB.Text = "Переполнение";
                return;
            }
            EpochNumTSTB.Text = curEpoch.ToString();
        }

        private void backgroundWorker_ProgressChanged_TSPB(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > ClusterizationTSPB.Maximum)
            {
                ClusterizationTSPB.Value = ClusterizationTSPB.Maximum;
                return;
            }
            if (e.ProgressPercentage < ClusterizationTSPB.Minimum)
            {
                ClusterizationTSPB.Value = ClusterizationTSPB.Minimum;
                return;
            }
            ClusterizationTSPB.Value = e.ProgressPercentage;
        }

        void ClusterizationEnded()
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
            ClusteringAlgorithmTSDDB.Enabled = true;
            ClusterizationStatusL.Text = "Кластеризация завершена";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ClustersUpdated();
            ClusterizationTSPB.Value = 0;
            ClusterizationEnded();
            ShowClusterizationInfo();
            MessageBox.Show("Кластеризация завершена.");
        }

        //
        //Рисование объектов
        //

        void DrawObjectListTSMI_Click(object sender, EventArgs e)//добавить перенесение двумерных объектов
        {
            DrawingForm drawForm = new DrawingForm(this);
            DrawingModel drawModel = new DrawingModel(drawForm);
            DrawingController drawController = new DrawingController(drawModel, drawForm);
            drawController.AddEventHandlers();

            drawForm.ParentWinfForm = this;
            if (Dimension == 2)
            {
                drawModel.SetItems(Items, true);
            }
            else
            {
                drawModel.SetItems(new List<Item>(), false);
            }
            drawForm.ShowDialog();
        }

        //
        //Отображение информации
        //

        void ShowObjectListB_Click(object sender, EventArgs e)
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

        void ClustersNumberNUD_ValueChanged(object sender, EventArgs e)
        {
            if (Clusters == null || Clusters.Count == 0)
            {
                return;
            }
            int clusterNumber = (int)ClustersNumberNUD.Value;
            if (clusterNumber != ClustersNumberNUD.Value)
            {
                return;
            }
            if (ClustersNumberNUD.Minimum <= clusterNumber &&
                clusterNumber <= ClustersNumberNUD.Maximum)
            {
                ShowItems(Clusters[clusterNumber].GetElements(), $"Кластер {clusterNumber}");
            }
        }

        void ClusterInfoB_Click(object sender, EventArgs e)
        {
            int ind = (int)ClustersNumberNUD.Value;
            ClusterInfoForm infoForm = new ClusterInfoForm();
            ClusterInfoModel infoModel = new ClusterInfoModel(infoForm, Clusters[ind], ind,
                ColsNames);
            ClusterInfoController infoController = new ClusterInfoController(infoForm, infoModel);
            infoController.AddEventHandlers();
            infoForm.ShowDialog();
        }

        void FindClusterNameB_Click(object sender, EventArgs e)
        {
            int ind = Cluster.FindCluster_Name(Clusters, ObjectIDTB.Text);
            if (ind == -1)
            {
                MessageBox.Show($"Ошибка. Кластера, содержащего объект с названием " +
                    $"\"{ObjectIDTB.Text}\" не существует.");
                return;
            }
            int itemIndInCluster = Clusters[ind].FindIndex_Name(ObjectIDTB.Text);

            ObjectInfoForm infoForm = new ObjectInfoForm();
            ObjectInfoModel infoModel = new ObjectInfoModel(infoForm, Items[
                Clusters[ind][itemIndInCluster].Index],
                ColsNames, Clusters[ind], ind);
            ObjectInfoController infoController = new ObjectInfoController(infoForm, infoModel);
            infoController.AddEventHandlers();
            infoForm.ShowDialog();
        }

        void FindClusterIndexB_Click(object sender, EventArgs e)
        {
            int Index = 0;
            try
            {
                Index = int.Parse(ObjectIDTB.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка. Индекс указан некорректно.");
                return;
            }
            if (Index < 0)
            {
                MessageBox.Show("Ошибка. Индекс не может быть отрицательным.");
            }
            int ind = Cluster.FindCluster_Index(Clusters, Index);
            if (ind == -1)
            {
                MessageBox.Show($"Ошибка. Кластера, содержащего объект с индексом " +
                    $"\"{ObjectIDTB.Text}\" не существует.");
                return;
            }
            int itemIndInCluster = Clusters[ind].FindIndex_Index(Index);
            ObjectInfoForm infoForm = new ObjectInfoForm();
            ObjectInfoModel infoModel = new ObjectInfoModel(infoForm, Items[
                Clusters[ind][itemIndInCluster].Index],
                ColsNames, Clusters[ind], ind);
            ObjectInfoController infoController = new ObjectInfoController(infoForm, infoModel);
            infoController.AddEventHandlers();
            infoForm.ShowDialog();
        }

        //
        //Визуализация
        //
        void VisualizationTSMI_Click(object sender, EventArgs e)
        {
            VisualizationForm visualizationForm = new VisualizationForm();
            VisualizationModel visualizationModel = new VisualizationModel(visualizationForm);
            VisualizationController visualizationController = new VisualizationController(visualizationForm, visualizationModel);
            visualizationModel.SetInfo(Items, Clusters, false);
            visualizationController.AddEventHandlers();
            visualizationForm.ShowDialog();
        }

        //
        //Настройки
        //

        void AlgorithmOptionsTSMI_Click(object sender, EventArgs e)
        {
            switch (clusteringAlgorithm)
            {
                case ClusteringAlgorithm.SOM:
                    SOMOptionsForm _SOMOptionsForm = new SOMOptionsForm(this, ApplyEnabled);
                    _SOMOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.GNG:
                    GNGOptionsForm _GNGOptionsForm = new GNGOptionsForm(this, ApplyEnabled);
                    _GNGOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.KMeans:
                    KMeansOptionsForm _KMeansOptionsForm = new KMeansOptionsForm(this, ApplyEnabled);
                    KMeansOptionsModel kMeansOptionsModel = new KMeansOptionsModel(_KMeansOptionsForm);
                    KMeansOptionsController kMeansOptionsController = new KMeansOptionsController(_KMeansOptionsForm, kMeansOptionsModel);
                    kMeansOptionsController.AddEventHandlers();
                    _KMeansOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.Aglomerative:
                    AglomerativeOptionsForm _AglomerativeOptionsForm =
                        new AglomerativeOptionsForm(this, ApplyEnabled);
                    _AglomerativeOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.DBSCAN:
                    DBSCANOptionsForm _DBSCANOptionsForm =
                        new DBSCANOptionsForm(this, ApplyEnabled);
                    DBSCANOptionsModel dbscanOptionsModel = new DBSCANOptionsModel(_DBSCANOptionsForm);
                    DBSCANOptionsController dbscanOptionsController = new DBSCANOptionsController(_DBSCANOptionsForm, dbscanOptionsModel);
                    dbscanOptionsController.AddEventHadlers();
                    _DBSCANOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.AffinityPropagation:
                    AffinityPropagationOptionsForm _APOptionsForm =
                        new AffinityPropagationOptionsForm(this, ApplyEnabled);
                    AffinityPropagationOptionsModel affinityPropagationOptionsModel
                        = new AffinityPropagationOptionsModel(_APOptionsForm);
                    AffinityPropagationOptionsController affinityPropagationOptionsController
                        = new AffinityPropagationOptionsController(_APOptionsForm, affinityPropagationOptionsModel);
                    affinityPropagationOptionsController.AddEventHandlers();
                    _APOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.FOREL:
                    FORELOptionsForm _FORELOptionsForm =
                        new FORELOptionsForm(this, ApplyEnabled);
                    _FORELOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.MST:
                    MSTOptionsForm _MSTOptionsForm =
                        new MSTOptionsForm(this, ApplyEnabled);
                    MSTOptionsModel mstOptionsModel = new MSTOptionsModel(_MSTOptionsForm);
                    MSTOptionsController mstOptionsController = new MSTOptionsController(_MSTOptionsForm, mstOptionsModel);
                    mstOptionsController.AddEventHandlers();
                    _MSTOptionsForm.ShowDialog();
                    break;
                case ClusteringAlgorithm.FullGraph:
                    FullGraphOptionsForm _FullGraphOptionsForm =
                        new FullGraphOptionsForm(this, ApplyEnabled);
                    _FullGraphOptionsForm.ShowDialog();
                    break;
            }
        }

        void ClusterizationParameterOptionsTSMI_Click(object sender, EventArgs e)
        {
            ClusterizationParameterOptionsForm parameterForm =
                new ClusterizationParameterOptionsForm(this, ApplyEnabled);
            ClusterizationParameterOptionsModel parameterModel
                = new ClusterizationParameterOptionsModel(parameterForm);
            ClusterizationParameterOptionsController parameterController
                = new ClusterizationParameterOptionsController(parameterForm, parameterModel);
            parameterController.AddEventHandlers();
            parameterForm.ShowDialog();
        }

        //
        //Сохранение
        //

        void SaveShownClusterCSVTSMI_Click(object sender, EventArgs e)
        {
            StreamWriter writer;
            try
            {
                saveFileDialog1.Filter = "(*.csv)|*.csv|(*.txt)|*.txt";
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string Path = saveFileDialog1.FileName;
                    try
                    {
                        writer = new StreamWriter(Path, true, Encoding.GetEncoding(1251));
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается открыть файл \"{Path}\"" +
                            $"для записи.");
                        return;
                    }
                    List<string> Text = PrintCluster_CSV(Clusters[(int)ClustersNumberNUD.Value], false);
                    foreach (var i in Text)
                    {
                        writer.WriteLine(i);
                    }
                    writer.Dispose();
                    MessageBox.Show("Сохранение успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer = null;
            }
        }

        void SaveAllClustersCSVTSMI_Click(object sender, EventArgs e)
        {
            StreamWriter writer;
            try
            {
                saveFileDialog1.Filter = "(*.csv)|*.csv";
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string Path = saveFileDialog1.FileName;
                    try
                    {
                        writer = new StreamWriter(Path, true, Encoding.GetEncoding(1251));
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается открыть файл \"{Path}\"" +
                            $"для записи.");
                        return;
                    }
                    List<string> Text = PrintClusters_CSV();
                    foreach (var i in Text)
                    {
                        writer.WriteLine(i);
                    }
                    writer.Dispose();
                    MessageBox.Show("Сохранение успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer = null;
            }
        }

        void SaveShownClusterXMLTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "(*.xml)|*.xml";
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string Path = saveFileDialog1.FileName;
                    XmlDocument xmlDoc = PrintClusterXML(
                        Clusters[(int)ClustersNumberNUD.Value]);
                    try
                    {
                        xmlDoc.Save(Path);
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается сохранить .xml документ " +
                            $"в файл \"{Path}\"");
                        return;
                    }
                    MessageBox.Show("Сохранение успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SaveAllClustersXMLTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "(*.xml)|*.xml";
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string Path = saveFileDialog1.FileName;
                    XmlDocument xmlDoc = PrintClustersXML();
                    try
                    {
                        xmlDoc.Save(Path);
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается сохранить .xml документ" +
                            $"в файл \"{Path}\".");
                        return;
                    }
                    MessageBox.Show("Сохранение успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SaveShownClusterXLSXTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "(*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string Path = saveFileDialog1.FileName;
                    Excel excel;
                    try
                    {
                        excel = new Excel(Path, 1);
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается сохранить .xlsx документ " +
                            $"в файл \"{Path}\"");
                        return;
                    }
                    Algorithm.WriteSeparator_XLSX(ColsNames, excel, 1, 0);
                    List<Item> items = Clusters[(int)ClustersNumberNUD.Value].GetElements();
                    for (int rowNum = 1; rowNum <= items.Count; ++rowNum)
                    {
                        Item.WriteItem_XLSX(items[rowNum - 1], excel, 1, rowNum);
                    }
                    excel.Close(Path);
                    MessageBox.Show("Сохранение успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SaveAllClustersXLSXTSMI_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "(*.xlsx)|*.xlsx";
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string Path = saveFileDialog1.FileName;
                    Excel excel;
                    try
                    {
                        excel = new Excel(Path, Clusters.Count);
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается сохранить .xlsx документ " +
                            $"в файл \"{Path}\"");
                        return;
                    }

                    int sheet = 1;

                    foreach (Cluster cluster in Clusters)
                    {
                        List<Item> items = cluster.GetElements();
                        int rowNum = 0;
                        Algorithm.WriteSeparator_XLSX(ColsNames, excel, sheet, rowNum++);
                        foreach (var item in items)
                        {
                            Item.WriteItem_XLSX(item, excel, sheet, rowNum++);
                        }
                        ++sheet;
                    }

                    excel.Close(Path);
                    MessageBox.Show("Сохранение успешно.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //
        //Изменение размеров формы
        //

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Height = Height - 8 - 12 - dataGridView1.Location.Y - 32;
            dataGridView1.Width = Width - 8 - dataGridView1.Location.X - 8 - 12;
        }
    }
}
