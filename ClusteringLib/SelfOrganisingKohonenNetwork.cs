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
    public class SelfOrganisingKohonenNetwork : ClusteringNodeClass<ClusteringNeuron>, IClustering
    {
        public double MaxDistance;
        public double LearningSpeed;
        public double ConvergencePrecision;
        public override event ProgressDel ProgressChanged;

        public void SetLearningMode(LearningMode _learningMode)
        {
            learningMode = _learningMode;
        }

        public SelfOrganisingKohonenNetwork(double maxDistance, double learningSpeed,
            double convergencePrecision, List<Item> items) : base()
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            MaxDistance = maxDistance;
            LearningSpeed = learningSpeed;
            ConvergencePrecision = convergencePrecision;
            learningMode = LearningMode.Start;
        }

        public override void SetOptions(ClusteringOptions opt)
        {
            MaxDistance = opt.MaxDistance;
            LearningSpeed = opt.LearningSpeed1;
            ConvergencePrecision = opt.ConvergencePrecision;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.MaxDistance = MaxDistance;
            result.LearningSpeed1 = LearningSpeed;
            result.ConvergencePrecision = ConvergencePrecision;
            return result;
        }
        protected override void Learn() //Обучение (не латеральное)
        {
            if (Items.Count == 0)
                throw new InvalidOperationException("Попытка кластеризовать пустое множество.");
            if (learningMode == (int)LearningMode.Start)
            {
                Nodes = new List<ClusteringNeuron>();
                Nodes.Add(new ClusteringNeuron(Items[0].GetCoordinates, LearningSpeed)); //Инициализация первого нейрона
            }
            List<int> IndexesOfActiveNeurons;
            for (int EpochNum = 1; ; ++EpochNum)
            {
                if (EpochNum > 1 && (StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                IndexesOfActiveNeurons = new List<int>();
                Nodes.ForEach(x => x.RewriteSavedCoordinates());
                foreach (var item in RandomAlgo.RandomShuffleList(Items)) //Цикл реализует одну эпоху обучения
                {
                    double distance;
                    int IndOfCurWinner = Winner(item, out distance);
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
        }//void Learn()
    }
}
