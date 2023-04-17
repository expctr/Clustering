using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLib;
using EuclideanGeometryLib;
using RandomAlgoLib;
using AlgorithmLib;
using System.Drawing;
using System.Threading;

namespace ClusteringLib
{
    public class SelfOrganisingKohonenNetwork : IClustering
    {
        public double MaxDistance;

        public double LearningSpeed;

        public double ConvergencePrecision;

        public event ProgressDel ProgressChanged;
        public event DebugDel debugEvent;

        private IClusteringNodeClass clusteringNodeClass;

        private List<ClusteringNeuron> Nodes = new List<ClusteringNeuron>();

        public bool StopFlag { set { clusteringNodeClass.StopFlag = value; } get { return clusteringNodeClass.StopFlag; } }
        public LearningMode learningMode { set { clusteringNodeClass.learningMode = value; } get { return clusteringNodeClass.learningMode; } }

        public void SetLearningMode(LearningMode learningMode)
        {
            clusteringNodeClass.learningMode = learningMode;
        }

        public SelfOrganisingKohonenNetwork(double maxDistance, double learningSpeed,
            double convergencePrecision, List<Item> items) : base()
        {
            clusteringNodeClass = new ClusteringNodeClass();

            if (items == null)
            {
                // Items = new List<Item>();
                clusteringNodeClass.SetItems(new List<Item>());
            }
            else
            {
                // Items = new List<Item>(items);
                clusteringNodeClass.SetItems(new List<Item>(items));
            }
            MaxDistance = maxDistance;
            LearningSpeed = learningSpeed;
            ConvergencePrecision = convergencePrecision;
            // learningMode = LearningMode.Start;
            clusteringNodeClass.learningMode = LearningMode.Start;
        }

        public void SetOptions(ClusteringOptions opt)
        {
            MaxDistance = opt.MaxDistance;
            LearningSpeed = opt.LearningSpeed1;
            ConvergencePrecision = opt.ConvergencePrecision;
        }
        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.MaxDistance = MaxDistance;
            result.LearningSpeed1 = LearningSpeed;
            result.ConvergencePrecision = ConvergencePrecision;
            return result;
        }
        protected void Learn() //Обучение (не латеральное)
        {
            if (clusteringNodeClass.GetItems().Count == 0) // Items.Count == 0
                throw new InvalidOperationException("Попытка кластеризовать пустое множество.");
            if (clusteringNodeClass.learningMode == (int)LearningMode.Start) // learningMode == (int)LearningMode.Start
            {
                Nodes = new List<ClusteringNeuron>();
                Nodes.Add(new ClusteringNeuron(clusteringNodeClass.GetItems()[0].GetCoordinates, LearningSpeed)); //Инициализация первого нейрона // Nodes.Add(new ClusteringNeuron(Items[0].GetCoordinates, LearningSpeed)); 
            }
            List<int> IndexesOfActiveNeurons;
            for (int EpochNum = 1; ; ++EpochNum)
            {
                if (EpochNum > 1 && (clusteringNodeClass.StopFlag || clusteringNodeClass.Converged(ConvergencePrecision, Nodes))) // if (EpochNum > 1 && (clusteringNodeClass.StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                IndexesOfActiveNeurons = new List<int>();
                Nodes.ForEach(x => x.RewriteSavedCoordinates());
                foreach (var item in RandomAlgo.RandomShuffleList(clusteringNodeClass.GetItems())) //Цикл реализует одну эпоху обучения
                {
                    double distance;
                    int IndOfCurWinner = clusteringNodeClass.Winner(item, out distance, Nodes); // int IndOfCurWinner = Winner(item, out distance);
                    if (distance > MaxDistance)
                    {
                        Nodes.Add(new ClusteringNeuron(item.GetCoordinates, LearningSpeed));
                        IndexesOfActiveNeurons.Add(Nodes.Count - 1);
                    }
                    else
                    {
                        Nodes[IndOfCurWinner].Learn(item.GetCoordinates);
                        IndexesOfActiveNeurons.Add(IndOfCurWinner);
                    }
                }
                List<ClusteringNeuron> NewNeurons = new List<ClusteringNeuron>();
                for (int i = 0; i <= Nodes.Count; ++i)
                {
                    if (IndexesOfActiveNeurons.FindIndex(x => x == i) != -1)
                    {
                        NewNeurons.Add(Nodes[i]);
                    }
                }
                Nodes = NewNeurons;
                ProgressChanged(EpochNum);
            }
        }

        public void SetItems(List<Item> items)
        {
            clusteringNodeClass.SetItems(items);
        }

        public List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (clusteringNodeClass.GetItems() == null || clusteringNodeClass.GetItems().Count == 0) // if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (clusteringNodeClass.GetItems().Count == 1) // if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(clusteringNodeClass.GetItems()[0]); // cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            Learn();
            List<List<Item>> Clusters = new List<List<Item>>();
            for (int i = 0; i < Nodes.Count; ++i)
            {
                Clusters.Add(new List<Item>());
            }
            foreach (var item in clusteringNodeClass.GetItems()) // foreach (var item in Items)
            {
                Clusters[clusteringNodeClass.Winner(item, Nodes)].Add(item);
            }
            Clusters.RemoveAll(cluster => cluster.Count == 0);
            return Clusters;
        }

        public void Stop()
        {
            clusteringNodeClass.Stop();
        }
    }
}