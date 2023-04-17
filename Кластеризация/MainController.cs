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
    class MainController
    {
        MainForm form;

        MainModel model;

        public MainController(MainForm form, MainModel model)
        {
            this.form = form;
            this.model = model;
        }

        public void AddEventHandlers()
        {
            form.SizeChanged += MainForm_SizeChanged;
            form.GetBackgroundWorker1().DoWork += backgroundWorker_DoWork;
            form.GetBackgroundWorker1().RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            form.GetDrawObjectListTSMI().Click += DrawObjectListTSMI_Click;
            form.GetSOMTSMI().Click += SOMTSMI_Click;
            form.GetGNGTSMI().Click += GNGTSMI_Click;
            form.GetKMeansTSMI().Click += KMeansTSMI_Click;
            form.GetAglomerativeTSMI().Click += AglomerativeTSMI_Click;
            form.GetDBSCANTSMI().Click += DBSCANTSMI_Click;
            form.GetAffinityPropagationTSMI().Click += AffinityPropagationTSMI_Click;
            form.GetFORELTSMI().Click += FORELTSMI_Click;
            form.GetMinimumSpanningTreeTSMI().Click += MSTTSMI_Click;
            form.GetFullGraphTSMI().Click += FullGraphTSMI_Click;
            form.GetStartClusterizationTSMI().Click += StartClusterizationTSMI_Click;
            form.GetContinueClusterizationTSMI().Click += ContinueClusterizationTSMI_Click;
            form.GetFinishClusterizationTSMI().Click += FinishClusterizationTSMI_Click;
            form.GetClustersNumberNUD().ValueChanged += ClustersNumberNUD_ValueChanged;
            form.GetClusterInfoB().Click += ClusterInfoB_Click;
            form.GetShowObjectListB().Click += ShowObjectListB_Click;
            form.GetSaveAllClustersCSVTSMI().Click += SaveAllClustersCSVTSMI_Click;
            form.GetSaveShownClusterCSVTSMI().Click += SaveShownClusterCSVTSMI_Click;
            form.GetSaveAllClustersXMLTSMI().Click += SaveAllClustersXMLTSMI_Click;
            form.GetSaveShownClusterXMLTSMI().Click += SaveShownClusterXMLTSMI_Click;
            form.GetSaveAllClustersXLSXTSMI().Click += SaveAllClustersXLSXTSMI_Click;
            form.GetSaveShownClusterXLSXTSMI().Click += SaveShownClusterXLSXTSMI_Click;
            form.GetVisualizationTSMI().Click += VisualizationTSMI_Click;
            form.GetAlgorithmOptionsTSMI().Click += AlgorithmOptionsTSMI_Click;
            form.GetClusterizationParameterOptionsTSMI().Click += ClusterizationParameterOptionsTSMI_Click;
            form.GetFindClusterNameB().Click += FindClusterNameB_Click;
            form.GetFindClusterIndexB().Click += FindClusterIndexB_Click;
            form.GetDownloadObjectListCSVTSMI().Click += DownloadObjectListCSVTSMI_Click;
            form.GetDownloadObjectListXMLTSMI().Click += DownloadObjectListXMLTSMI_Click;
            form.GetDownloadObjectListXLSXTSMI().Click += DownloadObjectListXLSXTSMI_Click;
        }

        public void PrepareForm()
        {
            form.GetClustersNumberNUD().Enabled = false;
            form.GetDataGridViewL().Text = "";
            form.GetClusterizationStatusL().Text = "";
            SOMTSMI_Click(new object(), new EventArgs());
            form.FirstState();
        }

        //
        //Скачивание файла
        //
        private void DownloadObjectListCSVTSMI_Click(object sender, EventArgs e)
        {
            StreamReader reader;
            try
            {
                form.GetOpenFileDialog1().Filter = "(*.csv)|*.csv|(*.txt)|*.txt";
                form.GetOpenFileDialog1().FileName = "";
                string Path;
                if (form.GetOpenFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    Path = form.GetOpenFileDialog1().FileName;
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
                    model.SetItems(items, colsNames);
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
                form.GetOpenFileDialog1().Filter = "(*.xml)|*.xml";
                form.GetOpenFileDialog1().FileName = "";
                string Path;
                if (form.GetOpenFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    Path = form.GetOpenFileDialog1().FileName;
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
                    model.SetItems(items, colsNames);
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
                form.GetOpenFileDialog1().Filter = "(*.xlsx)|*.xlsx";
                form.GetOpenFileDialog1().FileName = "";
                string Path;
                if (form.GetOpenFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    Path = form.GetOpenFileDialog1().FileName;
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
                    model.SetItems(items, colsNames);
                }
                MessageBox.Show("Скачивание завершено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //
        //Кластеризация
        //

        //
        void SOMTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass = new SelfOrganisingKohonenNetwork(5, 0.05, 0.05, model.Items);
            model.timeLimitMaster.SetFinish(() => { model.clusteringClass.StopFlag = true; });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                model.timeLimitMaster.Check();
                if (model.Watch.Elapsed.Milliseconds > 50)
                {
                    form.GetBackgroundWorker1().ReportProgress((int)x);
                    model.Watch.Restart();
                }
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.SOM;
            form.GetClusteringAlgorithmTSL().Text = form.GetSOMTSMI().Text;
            model.Continuable = true;
        }

        void GNGTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass
                = new GrowingNeuralGassNetwork(0.05, 0.0006, 10, 10, model.Items.Count / 5, 0.7, 0.9, 0.05, model.Items);
            model.timeLimitMaster.SetFinish(() => { model.clusteringClass.StopFlag = true; });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                model.timeLimitMaster.Check();
                if (model.Watch.Elapsed.Milliseconds > 100)
                {
                    form.GetBackgroundWorker1().ReportProgress((int)x);
                    model.Watch.Restart();
                }
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.GNG;
            form.GetClusteringAlgorithmTSL().Text = form.GetGNGTSMI().Text;
            model.Continuable = true;
        }

        void KMeansTSMI_Click(object sendr, EventArgs e)
        {
            model.clusteringClass = new KMeansClusteringClass(1, 0, model.Items);
            model.timeLimitMaster.SetFinish(() => { model.clusteringClass.StopFlag = true; });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                model.timeLimitMaster.Check();
                if (model.Watch.Elapsed.Milliseconds > 100)
                {
                    form.GetBackgroundWorker1().ReportProgress((int)x);
                    model.Watch.Restart();
                }
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.KMeans;
            form.GetClusteringAlgorithmTSL().Text = form.GetKMeansTSMI().Text;
            model.Continuable = true;
        }

        void AglomerativeTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass = new AglomerativeClusteringClass(1,
                ClusteringOptions.AglomerativeClusteringDistance.SingleLink,
                model.Items);
            model.clusteringClass.debugEvent += (string message) => {
                MessageBox.Show(message);
            };
            model.timeLimitMaster.SetFinish(() => { });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                form.GetBackgroundWorker1().ReportProgress((int)(x * form.GetClusterizationTSPB().Maximum));
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.Aglomerative;
            form.GetClusteringAlgorithmTSL().Text = form.GetAglomerativeTSMI().Text;
            model.Continuable = false;
        }

        void DBSCANTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass = new DBSCANClusteringClass(0.5, 5, model.Items);
            model.clusteringClass.debugEvent += (string message) => {
                MessageBox.Show(message);
            };
            model.timeLimitMaster.SetFinish(() => { });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                form.GetBackgroundWorker1().ReportProgress((int)(x * form.GetClusterizationTSPB().Maximum));
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.DBSCAN;
            form.GetClusteringAlgorithmTSL().Text = form.GetDBSCANTSMI().Text;
            model.Continuable = false;
        }

        void AffinityPropagationTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass = new AffinityPropagationClusteringClass(-5, 0.01, model.Items);
            model.timeLimitMaster.SetFinish(() => { model.clusteringClass.StopFlag = true; });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSTB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                model.timeLimitMaster.Check();
                if (model.Watch.Elapsed.Milliseconds > 100)
                {
                    form.GetBackgroundWorker1().ReportProgress((int)x);
                    model.Watch.Restart();
                }
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.AffinityPropagation;
            form.GetClusteringAlgorithmTSL().Text = form.GetAffinityPropagationTSMI().Text;
            model.Continuable = true;
        }

        void FORELTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass = new FORELClusteringClass(5, model.Items);
            model.timeLimitMaster.SetFinish(() => { });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                form.GetBackgroundWorker1().ReportProgress((int)(x * form.GetClusterizationTSPB().Maximum));
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.FOREL;
            form.GetClusteringAlgorithmTSL().Text = form.GetFORELTSMI().Text;
            model.Continuable = false;
        }

        void MSTTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass = new MinimumSpanningTreeClusteringClass(1, model.Items);
            model.timeLimitMaster.SetFinish(() => { });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                form.GetBackgroundWorker1().ReportProgress((int)(x * form.GetClusterizationTSPB().Maximum));
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.MST;
            form.GetClusteringAlgorithmTSL().Text = form.GetMinimumSpanningTreeTSMI().Text;
            model.Continuable = false;
        }

        void FullGraphTSMI_Click(object sender, EventArgs e)
        {
            model.clusteringClass = new FullGraphClusteringClass(5, model.Items);
            model.timeLimitMaster.SetFinish(() => { });
            //
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSTB;
            form.GetBackgroundWorker1().ProgressChanged -= backgroundWorker_ProgressChanged_TSPB;
            form.GetBackgroundWorker1().ProgressChanged += backgroundWorker_ProgressChanged_TSPB;
            //
            model.clusteringClass.ProgressChanged += x =>
            {
                //timeLimitMaster.Check();
                form.GetBackgroundWorker1().ReportProgress((int)(x * form.GetClusterizationTSPB().Maximum));
            };
            //
            model.clusteringAlgorithm = MainModel.ClusteringAlgorithm.FullGraph;
            form.GetClusteringAlgorithmTSL().Text = form.GetFullGraphTSMI().Text;
            model.Continuable = false;
        }

        void StartClusterizationTSMI_Click(object sender, EventArgs e)
        {
            curEpoch = 0;
            form.GetEpochNumTSTB().Text = "0";
            form.ClusterizationOnRun();
            form.GetBackgroundWorker1().RunWorkerAsync();
        }

        void ContinueClusterizationTSMI_Click(object sender, EventArgs e)
        {
            form.ClusterizationOnRun();
            form.GetBackgroundWorker1().RunWorkerAsync();
        }

        void FinishClusterizationTSMI_Click(object sender, EventArgs e)
        {
            form.GetFinishClusterizationTSMI().Enabled = false;
            Algorithm.DoWait(() => { model.clusteringClass.Stop(); }, 1);
        }

        //
        //Многопоточность
        //
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            model.Watch.Restart();
            model.timeLimitMaster.Activate();
            List<Cluster> clusters;
            try
            {
                clusters = Cluster.CreateClusters(model.clusteringClass.GetClusters());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                clusters = new List<Cluster>();
            }
            Algorithm.DoWait(() => { model.Clusters = clusters; }, 1);
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
                form.GetEpochNumTSTB().Text = "Переполнение";
                return;
            }
            form.GetEpochNumTSTB().Text = curEpoch.ToString();
        }

        private void backgroundWorker_ProgressChanged_TSPB(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > form.GetClusterizationTSPB().Maximum)
            {
                form.GetClusterizationTSPB().Value = form.GetClusterizationTSPB().Maximum;
                return;
            }
            if (e.ProgressPercentage < form.GetClusterizationTSPB().Minimum)
            {
                form.GetClusterizationTSPB().Value = form.GetClusterizationTSPB().Minimum;
                return;
            }
            form.GetClusterizationTSPB().Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            form.ClustersUpdated(ref model.Clusters, model.Dimension, model.ColsNames);
            form.GetClusterizationTSPB().Value = 0;
            form.ClusterizationEnded(model.Continuable);
            form.ShowClusterizationInfo(ref model.Clusters, model.Dimension);
            MessageBox.Show("Кластеризация завершена.");
        }

        //
        //Рисование объектов
        //

        void DrawObjectListTSMI_Click(object sender, EventArgs e)//добавить перенесение двумерных объектов
        {
            DrawingForm drawForm = new DrawingForm(form, model);
            DrawingModel drawModel = new DrawingModel(drawForm);
            DrawingController drawController = new DrawingController(drawModel, drawForm);
            drawController.AddEventHandlers();

            drawForm.ParentWinfForm = form;
            if (model.Dimension == 2)
            {
                drawModel.SetItems(model.Items, true);
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
            form.ShowObjectListBClickEventHandler(model.Dimension, model.ColsNames, model.Items);
        }

        void ClustersNumberNUD_ValueChanged(object sender, EventArgs e)
        {
            if (model.Clusters == null || model.Clusters.Count == 0)
            {
                return;
            }
            int clusterNumber = (int)form.GetClustersNumberNUD().Value;
            if (clusterNumber != form.GetClustersNumberNUD().Value)
            {
                return;
            }
            if (form.GetClustersNumberNUD().Minimum <= clusterNumber &&
                clusterNumber <= form.GetClustersNumberNUD().Maximum)
            {
                form.ShowItems(model.Clusters[clusterNumber].GetElements(),
                    $"Кластер {clusterNumber}", model.Dimension, model.ColsNames);
            }
        }

        void ClusterInfoB_Click(object sender, EventArgs e)
        {
            int ind = (int)form.GetClustersNumberNUD().Value;
            ClusterInfoForm infoForm = new ClusterInfoForm();
            ClusterInfoModel infoModel = new ClusterInfoModel(infoForm, model.Clusters[ind], ind,
                model.ColsNames);
            ClusterInfoController infoController = new ClusterInfoController(infoForm, infoModel);
            infoController.AddEventHandlers();
            infoForm.ShowDialog();
        }

        void FindClusterNameB_Click(object sender, EventArgs e)
        {
            int ind = Cluster.FindCluster_Name(model.Clusters, form.GetObjectIDTB().Text);
            if (ind == -1)
            {
                MessageBox.Show($"Ошибка. Кластера, содержащего объект с названием " +
                    $"\"{form.GetObjectIDTB().Text}\" не существует.");
                return;
            }
            int itemIndInCluster = model.Clusters[ind].FindIndex_Name(form.GetObjectIDTB().Text);

            ObjectInfoForm infoForm = new ObjectInfoForm();
            ObjectInfoModel infoModel = new ObjectInfoModel(infoForm, model.Items[
                model.Clusters[ind][itemIndInCluster].Index],
                model.ColsNames, model.Clusters[ind], ind);
            ObjectInfoController infoController = new ObjectInfoController(infoForm, infoModel);
            infoController.AddEventHandlers();
            infoForm.ShowDialog();
        }

        void FindClusterIndexB_Click(object sender, EventArgs e)
        {
            int Index = 0;
            try
            {
                Index = int.Parse(form.GetObjectIDTB().Text);
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
            int ind = Cluster.FindCluster_Index(model.Clusters, Index);
            if (ind == -1)
            {
                MessageBox.Show($"Ошибка. Кластера, содержащего объект с индексом " +
                    $"\"{form.GetObjectIDTB().Text}\" не существует.");
                return;
            }
            int itemIndInCluster = model.Clusters[ind].FindIndex_Index(Index);
            ObjectInfoForm infoForm = new ObjectInfoForm();
            ObjectInfoModel infoModel = new ObjectInfoModel(infoForm, model.Items[
                model.Clusters[ind][itemIndInCluster].Index],
                model.ColsNames, model.Clusters[ind], ind);
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
            visualizationModel.SetInfo(model.Items, model.Clusters, false);
            visualizationController.AddEventHandlers();
            visualizationForm.ShowDialog();
        }

        //
        //Настройки
        //

        void AlgorithmOptionsTSMI_Click(object sender, EventArgs e)
        {
            switch (model.clusteringAlgorithm)
            {
                case MainModel.ClusteringAlgorithm.SOM:
                    SOMOptionsForm _SOMOptionsForm = new SOMOptionsForm(form, model, form.ApplyEnabled);
                    _SOMOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.GNG:
                    GNGOptionsForm _GNGOptionsForm = new GNGOptionsForm(form, model, form.ApplyEnabled);
                    _GNGOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.KMeans:
                    KMeansOptionsForm _KMeansOptionsForm = new KMeansOptionsForm(form, model, form.ApplyEnabled);
                    KMeansOptionsModel kMeansOptionsModel = new KMeansOptionsModel(_KMeansOptionsForm, model);
                    KMeansOptionsController kMeansOptionsController = new KMeansOptionsController(_KMeansOptionsForm, kMeansOptionsModel);
                    kMeansOptionsController.AddEventHandlers();
                    _KMeansOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.Aglomerative:
                    AglomerativeOptionsForm _AglomerativeOptionsForm =
                        new AglomerativeOptionsForm(form, model, form.ApplyEnabled);
                    _AglomerativeOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.DBSCAN:
                    DBSCANOptionsForm _DBSCANOptionsForm =
                        new DBSCANOptionsForm(form, model, form.ApplyEnabled);
                    DBSCANOptionsModel dbscanOptionsModel = new DBSCANOptionsModel(_DBSCANOptionsForm, model.GetOptions());
                    DBSCANOptionsController dbscanOptionsController = new DBSCANOptionsController(_DBSCANOptionsForm, dbscanOptionsModel);
                    dbscanOptionsController.AddEventHadlers();
                    _DBSCANOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.AffinityPropagation:
                    AffinityPropagationOptionsForm _APOptionsForm =
                        new AffinityPropagationOptionsForm(form, model, form.ApplyEnabled);
                    AffinityPropagationOptionsModel affinityPropagationOptionsModel
                        = new AffinityPropagationOptionsModel(_APOptionsForm, model);
                    AffinityPropagationOptionsController affinityPropagationOptionsController
                        = new AffinityPropagationOptionsController(_APOptionsForm, affinityPropagationOptionsModel);
                    affinityPropagationOptionsController.AddEventHandlers();
                    _APOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.FOREL:
                    FORELOptionsForm _FORELOptionsForm =
                        new FORELOptionsForm(form, model, form.ApplyEnabled);
                    FORELOptionsModel _FORELOptionsModel = new FORELOptionsModel(_FORELOptionsForm, model);
                    FORELOptionsController _FORELOptionController = new FORELOptionsController(_FORELOptionsForm, _FORELOptionsModel);
                    _FORELOptionController.AddEventHandlers();
                    _FORELOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.MST:
                    MSTOptionsForm _MSTOptionsForm =
                        new MSTOptionsForm(form, model, form.ApplyEnabled);
                    MSTOptionsModel mstOptionsModel = new MSTOptionsModel(_MSTOptionsForm, model);
                    MSTOptionsController mstOptionsController = new MSTOptionsController(_MSTOptionsForm, mstOptionsModel);
                    mstOptionsController.AddEventHandlers();
                    _MSTOptionsForm.ShowDialog();
                    break;
                case MainModel.ClusteringAlgorithm.FullGraph:
                    FullGraphOptionsForm _FullGraphOptionsForm =
                        new FullGraphOptionsForm(form, model, form.ApplyEnabled);
                    _FullGraphOptionsForm.ShowDialog();
                    break;
            }
        }

        void ClusterizationParameterOptionsTSMI_Click(object sender, EventArgs e)
        {
            ClusterizationParameterOptionsForm parameterForm =
                new ClusterizationParameterOptionsForm(form, model, form.ApplyEnabled);
            ClusterizationParameterOptionsModel parameterModel
                = new ClusterizationParameterOptionsModel(parameterForm, model);
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
                form.GetSaveFileDialog1().Filter = "(*.csv)|*.csv|(*.txt)|*.txt";
                form.GetSaveFileDialog1().FileName = "";
                if (form.GetSaveFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    string Path = form.GetSaveFileDialog1().FileName;
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
                    List<string> Text = model.PrintCluster_CSV(model.Clusters[(int)form.GetClustersNumberNUD().Value], false);
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
                form.GetSaveFileDialog1().Filter = "(*.csv)|*.csv";
                form.GetSaveFileDialog1().FileName = "";
                if (form.GetSaveFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    string Path = form.GetSaveFileDialog1().FileName;
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
                    List<string> Text = model.PrintClusters_CSV();
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
                form.GetSaveFileDialog1().Filter = "(*.xml)|*.xml";
                form.GetSaveFileDialog1().FileName = "";
                if (form.GetSaveFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    string Path = form.GetSaveFileDialog1().FileName;
                    XmlDocument xmlDoc = model.PrintClusterXML(
                        model.Clusters[(int)form.GetClustersNumberNUD().Value]);
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
                form.GetSaveFileDialog1().Filter = "(*.xml)|*.xml";
                form.GetSaveFileDialog1().FileName = "";
                if (form.GetSaveFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    string Path = form.GetSaveFileDialog1().FileName;
                    XmlDocument xmlDoc = model.PrintClustersXML();
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
                form.GetSaveFileDialog1().Filter = "(*.xlsx)|*.xlsx";
                form.GetSaveFileDialog1().FileName = "";
                if (form.GetSaveFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    string Path = form.GetSaveFileDialog1().FileName;
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
                    Algorithm.WriteSeparator_XLSX(model.ColsNames, excel, 1, 0);
                    List<Item> items = model.Clusters[(int)form.GetClustersNumberNUD().Value].GetElements();
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
                form.GetSaveFileDialog1().Filter = "(*.xlsx)|*.xlsx";
                form.GetSaveFileDialog1().FileName = "";
                if (form.GetSaveFileDialog1().ShowDialog() == DialogResult.OK)
                {
                    string Path = form.GetSaveFileDialog1().FileName;
                    Excel excel;
                    try
                    {
                        excel = new Excel(Path, model.Clusters.Count);
                    }
                    catch
                    {
                        MessageBox.Show($"Ошибка. Не удается сохранить .xlsx документ " +
                            $"в файл \"{Path}\"");
                        return;
                    }

                    int sheet = 1;

                    foreach (Cluster cluster in model.Clusters)
                    {
                        List<Item> items = cluster.GetElements();
                        int rowNum = 0;
                        Algorithm.WriteSeparator_XLSX(model.ColsNames, excel, sheet, rowNum++);
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
            form.GetDataGridView1().Height = form.Height - 8 - 12 - form.GetDataGridView1().Location.Y - 32;
            form.GetDataGridView1().Width = form.Width - 8 - form.GetDataGridView1().Location.X - 8 - 12;
        }
    }
}
