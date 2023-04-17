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
    public class GrowingNeuralGassNetwork : IClustering
    {
        private IClusteringNodeClass clusteringNodeClass;

        private List<IGNGNeuron> Nodes = new List<IGNGNeuron>();

        public double WinnerLearningSpeed, NeighbourLearningSpeed;

        public int MaxAge;

        public int ReplicationPeriod;

        public int MaxNumberOfNeurons;

        public double ERRMN, CERR;

        public double ConvergencePrecision;

        //public bool StopFlag { set; get; }

        public event ProgressDel ProgressChanged;
        public event DebugDel debugEvent;

        //public LearningMode learningMode { set; get; }

        List<List<IGNGNeuron>> Components = new List<List<IGNGNeuron>>();
        public GrowingNeuralGassNetwork(double winnerLearningSpeed, double neighbourLearningSpeed,
            int maxAge, int replicationPeriod, int maxNumOfNeurons, double _ERRMN, double _CERR, double convergencePrecision,
            List<Item> items)
        {
            clusteringNodeClass = new ClusteringNodeClass();
            if (items == null)
            {
                //Items = new List<Item>();
                clusteringNodeClass.SetItems(new List<Item>());
            }
            else
            {
                //Items = new List<Item>(items);
                clusteringNodeClass.SetItems(new List<Item>(items));
            }
            WinnerLearningSpeed = winnerLearningSpeed;
            NeighbourLearningSpeed = neighbourLearningSpeed;
            MaxAge = maxAge;
            ReplicationPeriod = replicationPeriod;
            MaxNumberOfNeurons = maxNumOfNeurons;
            ERRMN = _ERRMN;
            CERR = _CERR;
            ConvergencePrecision = convergencePrecision;
            clusteringNodeClass.learningMode = (int)LearningMode.Start;
        }

        public void SetOptions(ClusteringOptions opt)
        {
            WinnerLearningSpeed = opt.LearningSpeed1;
            NeighbourLearningSpeed = opt.LearningSpeed2;
            MaxAge = opt.MaxAge;
            ReplicationPeriod = opt.ReplicationPeriod;
            MaxNumberOfNeurons = opt.MaxNumberOfNeurons;
            ERRMN = opt.ERRMN;
            CERR = opt.CERR;
            ConvergencePrecision = opt.ConvergencePrecision;
        }

        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.LearningSpeed1 = WinnerLearningSpeed;
            result.LearningSpeed2 = NeighbourLearningSpeed;
            result.MaxAge = MaxAge;
            result.ReplicationPeriod = ReplicationPeriod;
            result.MaxNumberOfNeurons = MaxNumberOfNeurons;
            result.ERRMN = ERRMN;
            result.CERR = CERR;
            result.ConvergencePrecision = ConvergencePrecision;
            return result;
        }

        void TwoWinneers(Item item, out int indOfFirst, out int indOfSecond)
        {
            indOfFirst = 0;
            indOfSecond = 1;
            double firstMinDist = EuclideanGeometry.Distance(item.GetCoordinates, Nodes[indOfFirst].GetCoordinates());
            double secondMinDist = EuclideanGeometry.Distance(item.GetCoordinates, Nodes[indOfSecond].GetCoordinates());
            if (firstMinDist > secondMinDist)
            {
                indOfFirst = 1;
                indOfSecond = 0;
                double buf = firstMinDist;
                firstMinDist = secondMinDist;
                secondMinDist = buf;
            }
            for (int i = 2; i < Nodes.Count; ++i)
            {
                double curDist = EuclideanGeometry.Distance(item.GetCoordinates, Nodes[i].GetCoordinates());
                if (curDist > secondMinDist)
                    continue;
                if (curDist < firstMinDist)
                {
                    indOfSecond = indOfFirst;
                    secondMinDist = firstMinDist;
                    indOfFirst = i;
                    firstMinDist = curDist;
                    continue;
                }
                indOfSecond = i;
                secondMinDist = curDist;
            }
        }
        void CreateComponents()
        {
            Components = new List<List<IGNGNeuron>>();
            Dictionary<IGNGNeuron, bool> UsedNeurons = new Dictionary<IGNGNeuron, bool>();
            Nodes.ForEach(x => UsedNeurons.Add(x, false));
            foreach (var neuron in Nodes)
            {
                if (!UsedNeurons[neuron])
                {
                    List<IGNGNeuron> curComponent = new List<IGNGNeuron>();
                    neuron.GetComponent(curComponent, UsedNeurons);
                    Components.Add(curComponent);
                }
            }
            Components.RemoveAll(x => x.Count == 0);
        }
        public List<List<IGNGNeuron>> GetComponents()
        {
            List<List<IGNGNeuron>> result = new List<List<IGNGNeuron>>();
            Components.ForEach(x => result.Add(new List<IGNGNeuron>(x)));
            return result;
        }
        public List<List<Item>> GetClusters()
        {
            clusteringNodeClass.StopFlag = false;
            if (clusteringNodeClass.GetItems() == null || clusteringNodeClass.GetItems().Count == 0) //if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (clusteringNodeClass.GetItems().Count == 1) //if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(clusteringNodeClass.GetItems()[0]); //cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            Learn();
            CreateComponents();
            Dictionary<IGNGNeuron, List<Item>> Domain = new Dictionary<IGNGNeuron, List<Item>>();
            Domain = clusteringNodeClass.CreateDomains(Nodes);
            List<List<Item>> result = new List<List<Item>>();
            foreach (var component in Components)
            {
                List<Item> curCluster = new List<Item>();
                component.ForEach(x => curCluster.AddRange(Domain[x]));
                result.Add(curCluster);
            }
            result.RemoveAll(x => x.Count == 0);
            return result;
        }

        protected void Learn()
        {
            if (clusteringNodeClass.GetItems().Count < 1) throw new Exception("Ошибка. Попытка кластеризовать пустое множество."); //if (Items.Count < 1) throw new Exception("Ошибка. Попытка кластеризовать пустое множество.");
            if (clusteringNodeClass.learningMode == (int)LearningMode.Start)
            {
                Nodes = new List<IGNGNeuron>();
                Nodes.Add(new GNGNeuron(clusteringNodeClass.GetItems()[0].GetCoordinates)); //Nodes.Add(new GNGNeuron(Items[0].GetCoordinates));
                Nodes.Add(new GNGNeuron(clusteringNodeClass.GetItems()[0].GetCoordinates)); //Nodes.Add(new GNGNeuron(Items[0].GetCoordinates));
            }
            int it = 0;
            for (int EpochNum = 1; ; ++EpochNum)
            {
                if (EpochNum > 1 && (clusteringNodeClass.StopFlag || clusteringNodeClass.Converged(ConvergencePrecision, Nodes))) //if (EpochNum > 1 && (StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                List<Item> _items = RandomAlgo.RandomShuffleList(clusteringNodeClass.GetItems()); //List<Item> _items = RandomAlgo.RandomShuffleList(Items);
                foreach (var item in _items)
                {
                    ++it;
                    int indOfFirst, indOfSecond;
                    TwoWinneers(item, out indOfFirst, out indOfSecond);
                    IGNGNeuron firstWinner = Nodes[indOfFirst];
                    firstWinner.IncreaseError(item);
                    firstWinner.Learn(item.GetCoordinates, WinnerLearningSpeed);
                    firstWinner.LearnNeighbours(item, NeighbourLearningSpeed);
                    firstWinner.IncreaseAges();
                    firstWinner.Connect(Nodes[indOfSecond]);
                    firstWinner.DeleteOldNeighbours(MaxAge);
                    Nodes.RemoveAll(x => x.NoNeighbours);
                    if (it % ReplicationPeriod == 0 && Nodes.Count < MaxNumberOfNeurons)
                    {
                        IGNGNeuron neuronU = Nodes[FindMostIncorrectNeuron()];
                        IGNGNeuron neuronV = neuronU.FindMostIncorrectNeighbour();
                        neuronU.MultiplyError(ERRMN);
                        neuronV.MultiplyError(ERRMN);
                        GNGNeuron neuronR = new GNGNeuron(EuclideanGeometry.Midpoint(neuronU.GetCoordinates(),
                            neuronV.GetCoordinates()), (neuronU.GetError + neuronV.GetError) / 2);
                        neuronU.Connect(neuronR);
                        neuronV.Connect(neuronR);
                        neuronU.Disconnect(neuronV);
                        Nodes.Add(neuronR);
                    }
                    Nodes.ForEach(x => x.MultiplyError(CERR));
                }
                int FindMostIncorrectNeuron()
                {
                    int result = 0;
                    double maxError = Nodes[0].GetError;
                    for (int i = 1; i < Nodes.Count; ++i)
                    {
                        if (Nodes[i].GetError > maxError)
                        {
                            result = i;
                            maxError = Nodes[result].GetError;
                        }
                    }
                    return result;
                }
                ProgressChanged(EpochNum);
            }
        }//void Learn()

        public void SetItems(List<Item> items)
        {
            clusteringNodeClass.SetItems(items);
        }

        public void Stop()
        {
            clusteringNodeClass.Stop();
        }

        public int GetNumberOfNeurons
        {
            get
            {
                return Nodes.Count();
            }
        }

        public bool StopFlag { set { clusteringNodeClass.StopFlag = value; } get { return clusteringNodeClass.StopFlag; } }
        public LearningMode learningMode { set { clusteringNodeClass.learningMode = value; } get { return clusteringNodeClass.learningMode; } }
    }//class GrowingNeuralGassNetwork
}