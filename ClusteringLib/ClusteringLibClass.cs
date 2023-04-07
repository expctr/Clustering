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
    public delegate void DebugDel(string Message);//del
    public class ClusteringOptions
    {
        public double MaxDistance;
        public double LearningSpeed1;
        public double LearningSpeed2;
        public double ConvergencePrecision;
        public bool TimeLimitActivated;
        public double Hours, Minutes, Seconds;
        public int MaxAge;
        public int ReplicationPeriod;
        public int MaxNumberOfNeurons;
        public double ERRMN;
        public double CERR;
        public double ConvergncePrecision;
        public int ClustersNumber;
        public double DetalizationCoef;
        public double ReachabilityRadius;
        public int Threshold;
        public double SelfSimilarity;
        public enum AglomerativeClusteringDistance { SingleLink,
        CentreDistance, WardDistance}
        public AglomerativeClusteringDistance ACDistance;
    }
    public class ClusteringParameterOptions
    {
        public bool[] ChosenClusterizationParameter;
        public double[] DimensionalWeights;
        public bool Normalize;
    }
    public interface IClustering
    {
        void SetItems(List<Item> items);
        List<List<Item>> GetClusters();
        bool StopFlag
        {
            set;
            get;
        }
        void Stop();
        event progressDel ProgressChanged;
        LearningMode learningMode { set; get; }
        void SetOptions(ClusteringOptions opt);
        ClusteringOptions GetOptions();
        event DebugDel debugEvent;//del
    }
    public delegate void progressDel(double x);
    public enum LearningMode { Start, Continue };

    public class Cluster
    {
        List<Item> Elements;
        Color ElementColor;
        Color CentreColor;
        public Cluster(List<Item> elements)
        {
            Elements = new List<Item>();
            foreach (var element in elements)
            {
                Elements.Add(new Item(element));
            }
            RandomAlgo.RandomColor(170, out ElementColor, out CentreColor);
        }
        public static List<Cluster> CreateClusters(List<List<Item>> input)
        {
            if (input == null) return null;
            List<Cluster> result = new List<Cluster>();
            foreach (var i in input)
            {
                result.Add(new Cluster(i));
            }
            return result;
        }
        public List<Item> GetElements()
        {
            List<Item> result = new List<Item>();
            foreach (var el in Elements)
            {
                result.Add(new Item(el));
            }
            return result;
        }
        public double[] GetCentre
        {
            get
            {
                return EuclideanGeometry.Barycentre(Item.ToDoubleArray(Elements));
            }
        }
        public Color GetElementColor
        {
            get
            {
                return ElementColor;
            }
        }
        public Color GetCentreColor
        {
            get
            {
                return CentreColor;
            }
        }
        public int Count
        {
            get
            {
                return Elements.Count;
            }
        }

        public Item this[int ind]
        {
            get
            {
                return Elements[ind];
            }
        }
        //
        //Описательные характеристики
        //
        public double MeanLinearIntraclusterDeviation
        {
            get
            {
                int count = Count;
                double[] centre = GetCentre;
                double result = 0;
                foreach(var el in Elements)
                {
                    result += EuclideanGeometry.Distance(el.GetCoordinates, centre) / count;
                }
                return result;
            }
        }
        public double Dispersion
        {
            get
            {
                double[] centre = GetCentre;
                double countSqrt = Math.Sqrt((double)Count);
                double result = 0;
                foreach (var el in Elements)
                {
                    result += Math.Pow(EuclideanGeometry.Distance(el.GetCoordinates, centre) /
                        countSqrt, 2);
                }
                return result;
            }
        }
        public double MeanSquareIntraclusterDeviation
        {
            get
            {
                return Math.Sqrt(Dispersion);
            }
        }
        public double QuartileRange
        {
            get
            {
                double[] centre = GetCentre;
                List<Item> elements = GetElements();
                elements.Sort((el1, el2) =>
                {
                    return EuclideanGeometry.Distance(el1.GetCoordinates,
                        centre).CompareTo(EuclideanGeometry.Distance(el2.GetCoordinates,
                        centre));
                });
                int ind = elements.Count / 2 + (elements.Count % 2 == 0 ? 0 : 1);
                return EuclideanGeometry.Distance(elements[ind - 1].GetCoordinates, centre);
            }
        }
        public event Action<double> progressChanged;
        public void ResetProgressChanged()
        {
            progressChanged = null;
        }
        public double AverageDistanceBetweenElements
        {
            get
            {
                double result = 0;
                double cur = 0;
                double total = (double)Count / 2 * (Count - 1);
                for(int i = 0; i < Count; ++i)
                {
                    for(int j = 0; j < Count; ++j)
                    {
                        if (i <= j) continue;
                        result += EuclideanGeometry.Distance(Elements[i].GetCoordinates,
                            Elements[j].GetCoordinates) / total;
                        progressChanged(++cur / total);
                    }
                }
                return result;
            }
        }
        public int FindIndex_Name(string name)
        {
            return Elements.FindIndex(el => el.Name == name);
        }
        public int FindIndex_Index(int index)
        {
            return Elements.FindIndex(el => el.Index == index);
        }
        public static int FindCluster_Name(List<Cluster> clusters, string name)
        {
            for(int i = 0; i < clusters.Count; ++i)
            {
                if(clusters[i].FindIndex_Name(name) != -1)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int FindCluster_Index(List<Cluster> clusters, int index)
        {
            for (int i = 0; i < clusters.Count; ++i)
            {
                if (clusters[i].FindIndex_Index(index) != -1)
                {
                    return i;
                }
            }
            return -1;
        }
        public int CountInRadius(double[] point, double radius)
        {
            int result = 0;
            Elements.ForEach(el =>
            {
                if (EuclideanGeometry.Distance(el.GetCoordinates, point) <= radius)
                    result += 1;
            });
            return result;
        }
        delegate double clusterDel(Cluster cluster);
        static double IntraclusterDeviation_Total(List<Cluster> clusters, clusterDel mean)
        {
            double result = 0;
            clusters.ForEach(cluster => { result += mean(cluster); });
            return result;
        }
        public static double MeanLinearIntraclusterDeviation_Total(List<Cluster> clusters)
        {
            return IntraclusterDeviation_Total(clusters, cluster =>
            cluster.MeanLinearIntraclusterDeviation);
        }
        public static double MeanSquareIntraclusterDeviation_Total(List<Cluster> clusters)
        {
            return IntraclusterDeviation_Total(clusters, cluster =>
            cluster.MeanSquareIntraclusterDeviation);
        }
        public static double[] ClustersBarycentre(List<Cluster> clusters)
        {
            List<double[]> centres = new List<double[]>();
            List<double> masses = new List<double>();
            clusters.ForEach(cluster => { masses.Add(cluster.Count);
                centres.Add(cluster.GetCentre);
            });
            return EuclideanGeometry.Barycentre(centres, masses);
        }
        public static double LinearInterclusterDeviation_Total(List<Cluster> clusters)
        {
            double result = 0;
            double[] barycentre = ClustersBarycentre(clusters);
            clusters.ForEach(cluster =>
            {
                result +=
                    EuclideanGeometry.Distance(cluster.GetCentre, barycentre);
            });
            return result;
        }
        public static double MeanSquareInterclusterDeviation(List<Cluster> clusters)
        {
            double result = 0;
            double[] barycentre = ClustersBarycentre(clusters);
            double sqrtCount = Math.Sqrt(clusters.Count);
            clusters.ForEach(cluster =>
            {
                result += Math.Pow(EuclideanGeometry.Distance(cluster.GetCentre,
                    barycentre) / sqrtCount, 2);
            });
            return Math.Sqrt(result);
        }

    }//class Cluster

    public class ClusteringNode
    {
        protected double[] Coordinates;
        protected double[] SavedCoordinates;
        public ClusteringNode(double[] coordinates)
        {
            Coordinates = (double[])coordinates.Clone();
            SavedCoordinates = null;
        }
        public void RewriteSavedCoordinates()
        {
            SavedCoordinates = (double[])Coordinates.Clone();
        }
        public bool Deflected(double ConvEps)
        {
            if (SavedCoordinates == null) return true;
            return EuclideanGeometry.Distance(Coordinates, SavedCoordinates) > ConvEps;
        }
        public double[] GetCoordinates()
        {
            double[] result = new double[Coordinates.Length];
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                result[i] = Coordinates[i];
            }
            return result;
        }
    }//class ClusteringNode

    public class ClusteringNeuron : ClusteringNode
    {
        protected double LearningSpeed;
        public ClusteringNeuron(double[] coordinates, double learningSpeed) : base(coordinates)
        {
            LearningSpeed = learningSpeed;
            SavedCoordinates = null;
        }
        public ClusteringNeuron(double[] coordinates) : base(coordinates)
        {
            SavedCoordinates = null;
        }
        public void Learn(double[] point)
        {
            double[] offset = EuclideanGeometry.VectorSubtraction(point, Coordinates);
            for(int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] = Coordinates[i] + LearningSpeed * offset[i];
            }
        }
        public void Learn(double[] point, double learningSpeed)
        {
            double[] offset = EuclideanGeometry.VectorSubtraction(point, Coordinates);
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] = Coordinates[i] + learningSpeed * offset[i];
            }
        }
    }//class ClusteringNeuron

    public abstract class ClusteringClass : IClustering
    {
        public abstract void SetOptions(ClusteringOptions opt);
        public abstract ClusteringOptions GetOptions();
        public virtual event DebugDel debugEvent;//del
        protected List<Item> Items = new List<Item>();
        public virtual void SetItems(List<Item> items)
        {
            if(items == null)
            {
                Items = new List<Item>();
            }
            Items = new List<Item>(items);
        }
        public abstract List<List<Item>> GetClusters();
        public bool StopFlag { set; get; }
        public virtual void Stop()
        {
            StopFlag = true;
        }
        public virtual event progressDel ProgressChanged;
        public LearningMode learningMode { set; get; }
    }//class ClusteringClass

    class MinimumSpanningTreeMaster
    {
        public bool StopFlag;
        public event progressDel ProgressChanged;
        public delegate double distanceDel<T>(int a, int b, List<T> set);
        public void Clear()
        {
            ProgressChanged = null;
        }
        public List<List<int>> CreateMinimumSpanningTree<T>(List<T> set,
            distanceDel<T> distance)//построение минимального остовного дерева
        {
            bool[] visited = new bool[set.Count];
            Tuple<double, int>[] table = new Tuple<double, int>[set.Count()];
            table[0] = new Tuple<double, int>(0, -1);
            for (int i = 1; i < table.Length; ++i)//инициализация таблицы
            {
                if (StopFlag) return new List<List<int>>();
                table[i] = new Tuple<double, int>(double.PositiveInfinity, -1);
            }
            int FindInd()
            {
                int resultInd = -1;
                for (int i = 0; i < visited.Length; ++i)
                {
                    if (!visited[i])
                    {
                        resultInd = i;
                        break;
                    }
                }
                if (resultInd == -1)
                {
                    return -1;
                }
                double resultLambda = table[resultInd].Item1;
                for (int i = 0; i < table.Length; ++i)
                {
                    if (visited[i]) continue;
                    if (table[i].Item1 < resultLambda)
                    {
                        resultInd = i;
                        resultLambda = table[i].Item1;
                    }
                }
                return resultInd;
            }
            int curInd;
            double buildingTableCurProgress = 0;
            double buildingTableTotalProgress = set.Count;
            while (-1 != (curInd = FindInd()))//заполнение таблицы
            {
                for (int i = 0; i < table.Length; ++i)
                {
                    if (StopFlag) return new List<List<int>>();
                    if (visited[i]) continue;
                    double curDist = distance(curInd, i, set);
                    if (table[i].Item1 > curDist)
                    {
                        table[i] = new Tuple<double, int>(curDist, curInd);
                    }
                }
                visited[curInd] = true;
                ProgressChanged(++buildingTableCurProgress / buildingTableTotalProgress);
            }
            List<List<int>> result = new List<List<int>>();
            for (int i = 0; i < table.Length; ++i)
            {
                if (StopFlag) return new List<List<int>>();
                result.Add(new List<int>());
            }
            for (int i = 1; i < table.Length; ++i)//извлечение ответа из таблицы
            {
                if (StopFlag) return new List<List<int>>();
                result[i].Add(table[i].Item2);
                result[table[i].Item2].Add(i);
            }
            return result;
        }
    }

    public abstract class ClusteringNodeClass<Node> : ClusteringClass where Node : ClusteringNode
    {
        protected List<Node> Nodes = new List<Node>();
        protected int Winner(Item item)
        {
            double[] itemCoordinates = item.GetCoordinates;
            double MinDistance = EuclideanGeometry.Distance(Nodes[0].GetCoordinates(), itemCoordinates);
            int result = 0;
            for (int i = 1; i < Nodes.Count; ++i)
            {
                double CurDistance = EuclideanGeometry.Distance(Nodes[i].GetCoordinates(), itemCoordinates);
                if (CurDistance < MinDistance)
                {
                    MinDistance = CurDistance;
                    result = i;
                }
            }
            return result;
        }
        protected int Winner(Item item, out double distance)
        {
            double[] itemCoordinates = item.GetCoordinates;
            double MinDistance = EuclideanGeometry.Distance(Nodes[0].GetCoordinates(), itemCoordinates);
            int result = 0;
            for (int i = 1; i < Nodes.Count; ++i)
            {
                double CurDistance = EuclideanGeometry.Distance(Nodes[i].GetCoordinates(), itemCoordinates);
                if (CurDistance < MinDistance)
                {
                    MinDistance = CurDistance;
                    result = i;
                }
            }
            distance = MinDistance;
            return result;
        }
        protected abstract void Learn();
        public override List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
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
            foreach (var item in Items)
            {
                Clusters[Winner(item)].Add(item);
            }
            Clusters.RemoveAll(cluster => cluster.Count == 0);
            return Clusters;
        }
        public Dictionary<Node, List<Item>> CreateDomains()
        {
            Dictionary<Node, List<Item>> result = new Dictionary<Node, List<Item>>();
            foreach(var node in Nodes)
            {
                result[node] = new List<Item>();
            }
            foreach(var item in Items)
            {
                result[Nodes[Winner(item)]].Add(item);
            }
            return result;
        }
        public List<double[]> GetNodesCoordinates()
        {
            List<double[]> result = new List<double[]>();
            foreach (var neuron in Nodes)
            {
                result.Add(neuron.GetCoordinates());
            }
            return result;
        }
        protected bool Converged(double ConvEps)
        {
            for(int i = 0; i < Nodes.Count; ++i)
            {
                if (Nodes[i].Deflected(ConvEps))
                {
                    return false;
                }
            }
            return true;
        }
    }//class ClusteringNodeClass<Node>

    public class SelfOrganisingKohonenNetwork : ClusteringNodeClass<ClusteringNeuron>, IClustering
    {
        public double MaxDistance;
        public double LearningSpeed;
        public double ConvergencePrecision;
        public override event progressDel ProgressChanged;

        public void SetLearningMode(LearningMode _learningMode)
        {
            learningMode = _learningMode;
        }

        public SelfOrganisingKohonenNetwork(double maxDistance, double learningSpeed,
            double convergencePrecision, List<Item> items) : base()
        {
            if(items == null)
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
            if(learningMode == (int)LearningMode.Start)
            {
                Nodes = new List<ClusteringNeuron>();
                Nodes.Add(new ClusteringNeuron(Items[0].GetCoordinates, LearningSpeed)); //Инициализация первого нейрона
            }
            List<int> IndexesOfActiveNeurons;       
            for(int EpochNum = 1;; ++EpochNum)
            {
                if (EpochNum > 1 && (StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                IndexesOfActiveNeurons = new List<int>();
                Nodes.ForEach(x => x.RewriteSavedCoordinates());
                foreach(var item in RandomAlgo.RandomShuffleList(Items)) //Цикл реализует одну эпоху обучения
                {
                    double distance;
                    int IndOfCurWinner = Winner(item, out distance);
                    if(distance > MaxDistance)
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
                for(int i = 0; i <= Nodes.Count; ++i)
                {
                    if(IndexesOfActiveNeurons.FindIndex(x => x == i) != -1)
                    {
                        NewNeurons.Add(Nodes[i]);
                    }
                }
                Nodes = NewNeurons;
                ProgressChanged(EpochNum);
            }
        }//void Learn()
    }//class SelfOrganisingKohonenNetwork

    public class GNGNeuron : ClusteringNeuron
    {
        double Error;
        List<GNGNeuron> Neighbours = new List<GNGNeuron>();
        Dictionary<GNGNeuron, int> Ages = new Dictionary<GNGNeuron, int>();
        public GNGNeuron(double[] coordinates) : base(coordinates)
        {
            Error = 0;
        }
        public GNGNeuron(double[] coordinates, double _Error) : base(coordinates)
        {
            Error = _Error;
        }
        public double GetError
        {
            get
            {
                return Error;
            }
        }
        public void IncreaseError(Item item)
        {
            Error += EuclideanGeometry.Distance(item.GetCoordinates, Coordinates);
        }
        public void MultiplyError(double x)
        {
            Error *= x;
        }
        public void Connect(GNGNeuron neuron)
        {
            PartialConnect(neuron);
            neuron.PartialConnect(this);
        }
        public void PartialConnect(GNGNeuron neuron)
        {
            if (Neighbours.FindIndex(x => x == neuron) == -1) Neighbours.Add(neuron);
            Ages[neuron] = 0;
        }
        public void Disconnect(GNGNeuron neuron)
        {
            PartialDisconnect(neuron);
            neuron.PartialDisconnect(this);
        }
        public void PartialDisconnect(GNGNeuron neuron)
        {
            Neighbours.RemoveAt(Neighbours.FindIndex(x => x == neuron));
            Ages.Remove(neuron);
        }
        public void LearnNeighbours(Item item, double learningSpeed)
        {
            foreach(var neighbour in Neighbours)
            {
                neighbour.Learn(item.GetCoordinates, learningSpeed);
            }
        }
        public void IncreaseAges()
        {
            foreach(var neighbour in Neighbours)
            {
                ++Ages[neighbour];
            }
        }
        public void DeleteOldNeighbours(int maxAge)
        {
            for(int i = 0; i < Neighbours.Count; ++i)
            {
                if (Ages[Neighbours[i]] > maxAge)
                {
                    Disconnect(Neighbours[i]);
                    --i;
                }
            }
        }
        public bool NoNeighbours
        {
            get
            {
                return Neighbours.Count == 0;
            }
        }
        public GNGNeuron FindMostIncorrectNeighbour()
        {
            int result = 0;
            double maxError = Neighbours[0].GetError;
            for (int i = 1; i < Neighbours.Count; ++i)
            {
                if (Neighbours[i].GetError > maxError)
                {
                    result = i;
                    maxError = Neighbours[result].GetError;
                }
            }
            return Neighbours[result];
        }
        public delegate void del1();
        public void GetComponent(List<GNGNeuron> Component, Dictionary<GNGNeuron, bool> UsedNeurons)
        {
            if (UsedNeurons[this]) return;
            Component.Add(this);
            UsedNeurons[this] = true;
            foreach(var neighbour in Neighbours)
            {
                neighbour.GetComponent(Component, UsedNeurons);
            }
        }
    }//class GNGNeuron

    public class GrowingNeuralGassNetwork : ClusteringNodeClass<GNGNeuron>, IClustering
    {
        public double WinnerLearningSpeed, NeighbourLearningSpeed;
        public int MaxAge;
        public int ReplicationPeriod;
        public int MaxNumberOfNeurons;
        public double ERRMN, CERR;
        public double ConvergencePrecision;
        //public bool StopFlag { set; get; }
        public override event progressDel ProgressChanged;
        //public LearningMode learningMode { set; get; }
        List<List<GNGNeuron>> Components = new List<List<GNGNeuron>>();
        public GrowingNeuralGassNetwork(double winnerLearningSpeed, double neighbourLearningSpeed,
            int maxAge, int replicationPeriod, int maxNumOfNeurons, double _ERRMN, double _CERR, double convergencePrecision,
            List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            WinnerLearningSpeed = winnerLearningSpeed;
            NeighbourLearningSpeed = neighbourLearningSpeed;
            MaxAge = maxAge;
            ReplicationPeriod = replicationPeriod;
            MaxNumberOfNeurons = maxNumOfNeurons;
            ERRMN = _ERRMN;
            CERR = _CERR;
            ConvergencePrecision = convergencePrecision;
            learningMode = (int)LearningMode.Start;
        }

        public override void SetOptions(ClusteringOptions opt)
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

        public override ClusteringOptions GetOptions()
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
            if(firstMinDist > secondMinDist)
            {
                indOfFirst = 1;
                indOfSecond = 0;
                double buf = firstMinDist;
                firstMinDist = secondMinDist;
                secondMinDist = buf;
            }
            for(int i = 2; i < Nodes.Count; ++i)
            {
                double curDist = EuclideanGeometry.Distance(item.GetCoordinates, Nodes[i].GetCoordinates());
                if (curDist > secondMinDist)
                    continue;
                if(curDist < firstMinDist)
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
            Components = new List<List<GNGNeuron>>();
            Dictionary<GNGNeuron, bool> UsedNeurons = new Dictionary<GNGNeuron, bool>();
            Nodes.ForEach(x => UsedNeurons.Add(x, false));
            foreach(var neuron in Nodes)
            {
                if (!UsedNeurons[neuron])
                {
                    List<GNGNeuron> curComponent = new List<GNGNeuron>();
                    neuron.GetComponent(curComponent, UsedNeurons);
                    Components.Add(curComponent);
                }
            }
            Components.RemoveAll(x => x.Count == 0);
        }
        public List<List<GNGNeuron>> GetComponents()
        {
            List<List<GNGNeuron>> result = new List<List<GNGNeuron>>();
            Components.ForEach(x => result.Add(new List<GNGNeuron>(x)));
            return result;
        }
        public override List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if(Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if(Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            Learn();
            CreateComponents();
            Dictionary<GNGNeuron, List<Item>> Domain = new Dictionary<GNGNeuron, List<Item>>();
            Domain = CreateDomains();
            List<List<Item>> result = new List<List<Item>>();
            foreach(var component in Components)
            {
                List<Item> curCluster = new List<Item>();
                component.ForEach(x => curCluster.AddRange(Domain[x]));
                result.Add(curCluster);
            }
            result.RemoveAll(x => x.Count == 0);
            return result;
        }
        protected override void Learn()
        {
            if (Items.Count < 1) throw new Exception("Ошибка. Попытка кластеризовать пустое множество.");
            if (learningMode == (int)LearningMode.Start)
            {
                Nodes = new List<GNGNeuron>();
                Nodes.Add(new GNGNeuron(Items[0].GetCoordinates));
                Nodes.Add(new GNGNeuron(Items[0].GetCoordinates));
            }
            int it = 0;
            for (int EpochNum = 1;; ++EpochNum)
            {
                if (EpochNum > 1 && (StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                List<Item> _items = RandomAlgo.RandomShuffleList(Items);
                foreach (var item in _items)
                {
                    ++it;
                    int indOfFirst, indOfSecond;
                    TwoWinneers(item, out indOfFirst, out indOfSecond);
                    GNGNeuron firstWinner = Nodes[indOfFirst];
                    firstWinner.IncreaseError(item);
                    firstWinner.Learn(item.GetCoordinates, WinnerLearningSpeed);
                    firstWinner.LearnNeighbours(item, NeighbourLearningSpeed);
                    firstWinner.IncreaseAges();
                    firstWinner.Connect(Nodes[indOfSecond]);
                    firstWinner.DeleteOldNeighbours(MaxAge);
                    Nodes.RemoveAll(x => x.NoNeighbours);
                    if(it % ReplicationPeriod == 0 && Nodes.Count < MaxNumberOfNeurons)
                    {
                        GNGNeuron neuronU = Nodes[FindMostIncorrectNeuron()];
                        GNGNeuron neuronV = neuronU.FindMostIncorrectNeighbour();
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
                    for(int i = 1; i < Nodes.Count; ++i)
                    {
                        if(Nodes[i].GetError > maxError)
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
        public int GetNumberOfNeurons
        {
            get
            {
                return Nodes.Count();
            }
        }
    }//class GrowingNeuralGassNetwork

    public class KMeansNode : ClusteringNode
    {
        public KMeansNode(double[] coordinates) : base(coordinates)
        {

        }
        public void Learn(List<Item> items)
        {
            if (items.Count == 0) return;
            Coordinates = EuclideanGeometry.Barycentre(Item.ToDoubleArray(items));
        }
    }//class KMeansNode

    public class KMeansClusteringClass : ClusteringNodeClass<KMeansNode>, IClustering
    {
        public int NodesNumber;
        public double ConvergencePrecision;
        public override event progressDel ProgressChanged;
        public KMeansClusteringClass(int nodesNumber, double convergencePrecision, List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            NodesNumber = nodesNumber;
            ConvergencePrecision = convergencePrecision;
        }

        public override void SetOptions(ClusteringOptions opt)
        {
            NodesNumber = opt.ClustersNumber;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ClustersNumber = NodesNumber;
            return result;
        }

        protected override void Learn()
        {
            if(learningMode == LearningMode.Start)
            {
                Tuple<double[], double[]> lims = EuclideanGeometry.MinMaxDim(Items);
                InitializeNodes(lims.Item1, lims.Item2);
            }
            for(int EpochNum = 1; ; ++EpochNum)
            {
                if (EpochNum > 1 && (StopFlag || Converged(ConvergencePrecision)))
                {
                    ProgressChanged(EpochNum - 1);
                    return;
                }
                for (int i = 1; i <= NodesNumber; ++i)
                {
                    Dictionary<KMeansNode, List<Item>> domains = CreateDomains();
                    for (int j = 0; j < Nodes.Count; ++j)
                    {
                        Nodes[j].Learn(domains[Nodes[j]]);
                    }
                }
                ProgressChanged(EpochNum);
            }
        }
        KMeansNode InitializeNode(double[] minDin, double[] maxDim)
        {
            double[] coord = new double[minDin.Length];
            for(int i = 0; i < coord.Length; ++i)
            {
                coord[i] = RandomAlgo.RandomNumber(minDin[i], maxDim[i]);
            }
            return new KMeansNode(coord);
        }
        void InitializeNodes(double[] minDin, double[] maxDim)
        {
            Nodes = new List<KMeansNode>();
            for(int i = 0; i < NodesNumber; ++i)
            {
                Nodes.Add(InitializeNode(minDin, maxDim));
            }
        }
    }//class KMeansClustering

    public class AglomerativeNode
    {
        public bool Locked = false;
        bool Completed;
        int Level;
        List<int> Indexes;
        double[] Centre;
        public AglomerativeNode SubNode1, SubNode2;
        double Distance;
        List<Item> Items;
        public string ID;
        public AglomerativeNode(int ind, List<Item> items, string _ID)
        {
            Completed = false;
            Level = 0;
            Indexes = new List<int>(new int[]{ ind });
            Centre = items[ind].GetCoordinates;
            SubNode1 = null;
            SubNode2 = null;
            Distance = 0;
            Items = items;
            ID = _ID;
        }
        public AglomerativeNode(AglomerativeNode first, AglomerativeNode second, double distance, List<Item> items)
        {
            Completed = false;
            Level = first.GetLevel > second.GetLevel ? first.GetLevel + 1 : second.GetLevel + 1;
            Indexes = new List<int>();
            Indexes.AddRange(first.GetIndexes);
            Indexes.AddRange(second.GetIndexes);
            Centre = EuclideanGeometry.Barycentre(first.GetCentre, first.Weight, second.GetCentre,
                second.Weight);
            SubNode1 = first;
            SubNode2 = second;
            Distance = distance;
            Items = items;
            ID = $"{first.ID} {second.ID}";
        }
        public static double Distance_SingleLink(AglomerativeNode node1, AglomerativeNode node2)
        {
            return EuclideanGeometry.Distance_SingleLink(node1.GetCoordinates(), node2.GetCoordinates());
        }
        public List<double[]> GetCoordinates()
        {
            List<double[]> result = new List<double[]>();
            foreach(int ind in Indexes)
            {
                result.Add(Items[ind].GetCoordinates);
            }
            return result;
        }
        public int GetLevel
        {
            get
            {
                return Level;
            }
        }
        public double[] GetCentre
        {
            get
            {
                return (double[])Centre.Clone();
            }
        }
        public int Weight
        {
            get
            {
                return Indexes.Count;
            }
        }
        public List<int> GetIndexes
        {
            get
            {
                List<int> result = new List<int>();
                result.AddRange(Indexes);
                return result;
            }
        }
        public bool IsCompleted
        {
            get
            {
                return Completed;
            }
        }
        public void Complete()
        {
            Completed = true;
        }
        public double GetDistance
        {
            get
            {
                return Distance;
            }
        }
        public List<Item> GetItems
        {
            get
            {
                List<Item> result = new List<Item>();
                foreach(var ind in Indexes)
                {
                    result.Add(Items[ind]);
                }
                return result;
            }
        }
        public Item this[int ind]
        {
            get
            {
                return Items[ind];
            }
        }
        public int Count
        {
            get
            {
                return Indexes.Count;
            }
        }
    }//class AglomerativeNode

    class AglomerativeEdge
    {
        public AglomerativeNode Node1;
        public AglomerativeNode Node2;
        public readonly double Distance;
        public AglomerativeEdge(AglomerativeNode node1, AglomerativeNode node2,
            double distance)
        {
            Node1 = node1;
            Node2 = node2;
            Distance = distance;
        }
        public void Redirect(AglomerativeNode oldNode1, AglomerativeNode oldNode2, AglomerativeNode newNode)
        {
            if(Node1 == oldNode1 || Node1 == oldNode2)
            {
                Node1 = newNode;
            }
            if(Node2 == oldNode1 || Node2 == oldNode2)
            {
                Node2 = newNode;
            }
        }
    }

    public class AglomerativeClusteringClass : IClustering 
    {
        public event DebugDel debugEvent;
        List<Item> Items;
        public double DetalizationCoef;
        public ClusteringOptions.AglomerativeClusteringDistance ACDistance;
        MinimumSpanningTreeMaster MSTMaster;
        AglomerativeNode DendraRoot = null;
        List<List<AglomerativeNode>> Levels = null;
        public bool StopFlag { set; get; }
        public void Stop()
        {
            StopFlag = true;
        }
        public event progressDel ProgressChanged;
        //
        double CDpart = 25.0 / 30, CMSTpart = 1.0 / 4,
            CELpart = 1.0 / 4, SELpart = 1.0 / 4,
            CD1part = 1.0 / 4;
        double SDpart = 4.0 / 30;
        double ECpart = 1.0 / 30, 
            MInANpart = 1.0 / 2, EANpart = 1.0 / 2;
        double CurProgress = 0;
        void Report(double x)
        {
            ProgressChanged(CurProgress + x);
        }
        //
        public LearningMode learningMode { set; get; }//костыль
        public AglomerativeClusteringClass(double detalizationCoef,
            ClusteringOptions.AglomerativeClusteringDistance _ACDistance,
            List<Item> items)
        {
            DetalizationCoef = detalizationCoef;
            ACDistance = _ACDistance;
            Items = new List<Item>();
            Items.AddRange(items);
        }

        public void SetOptions(ClusteringOptions opt)
        {
            if(ACDistance != opt.ACDistance)
            {
                DendraRoot = null;
                Levels = null;
            }
            DetalizationCoef = opt.DetalizationCoef;
            ACDistance = opt.ACDistance;
        }
        public ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.DetalizationCoef = DetalizationCoef;
            result.ACDistance = ACDistance;
            return result;
        }

        public void SetItems(List<Item> items)
        {
            if(items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            DendraRoot = null;
            Levels = null;
        }
        Tuple<AglomerativeNode, AglomerativeNode> NodeKey(int i, int j, List<AglomerativeNode> nodes)
        {
            return new Tuple<AglomerativeNode, AglomerativeNode>(nodes[i], nodes[j]);
        }
        Tuple<AglomerativeNode, AglomerativeNode> NodeKey(AglomerativeNode first, AglomerativeNode second)
        {
            return new Tuple<AglomerativeNode, AglomerativeNode>(first, second);
        }

        List<AglomerativeNode> PrimaryNodes()
        {
            List<AglomerativeNode> result = new List<AglomerativeNode>();
            for(int i = 0; i < Items.Count; ++i)
            {
                result.Add(new AglomerativeNode(i, Items, i.ToString()));
            }
            return result;
        }
        
        Dictionary<AglomerativeNode, List<AglomerativeNode>> 
            CreateMinimumSpanningTree( out List<AglomerativeNode> _primaryNodes)
        {
            MSTMaster = new MinimumSpanningTreeMaster();
            MSTMaster.ProgressChanged += (double x) =>
            {
                Report(x * CMSTpart * CDpart);
            };
            List<List<int>> mst_ind = MSTMaster.CreateMinimumSpanningTree(Items,
                (a, b, items) => {
                    if (a == b) return double.PositiveInfinity;
                    return EuclideanGeometry.Distance(items[a].GetCoordinates,
                        items[b].GetCoordinates);
                });
            _primaryNodes = PrimaryNodes();
            Dictionary<AglomerativeNode, List<AglomerativeNode>> result =
                new Dictionary<AglomerativeNode, List<AglomerativeNode>>();
            for(int i = 0; i < mst_ind.Count; ++i)//преобразование списка смежности из списка индексов в список ссылок
            {
                result[_primaryNodes[i]] = new List<AglomerativeNode>();
                for(int j = 0; j < mst_ind[i].Count; ++j)
                {
                    result[_primaryNodes[i]].Add(_primaryNodes[mst_ind[i][j]]);
                }
            }
            return result;
        }

        AglomerativeNode CreateDendrogramm_SingleLinkDistance()
        {
            List<AglomerativeNode> primaryNodes;
            Dictionary<AglomerativeNode, List<AglomerativeNode>> mst =
                CreateMinimumSpanningTree(out primaryNodes);//минимальное остовное дерево изначальных агломеративных нод
            CurProgress += CMSTpart * CDpart;
            List<AglomerativeEdge> edges =
                new List<AglomerativeEdge>();//список ребер минимального остовного дерева (ребра не должны повторяться)
            //сформируем edjes обходом в ширину
            Dictionary<AglomerativeNode, bool> visited =
                new Dictionary<AglomerativeNode, bool>();//словарь посещенности
            foreach (var node in primaryNodes)
            {
                visited[node] = false;
            }
            List<AglomerativeNode> curLayer = new List<AglomerativeNode>();
            curLayer.Add(primaryNodes[0]);
            List<AglomerativeNode> nextLayer;
            double buildingEdgesListCurProgress = 0;
            double buildingEdgesListTotalProgress = Items.Count - 1;
            while (curLayer.Count != 0)//обход в ширину
            {
                nextLayer = new List<AglomerativeNode>();
                foreach (var nodeI in curLayer)//получаем ребра с помощью текущего слоя
                {
                    foreach (var nodeJ in mst[nodeI])//обходим смежные ноды с текущей нодой текущего слоя
                    {
                        if (StopFlag)
                        {
                            return null;
                        }
                        if (visited[nodeJ]) continue;
                        edges.Add(new AglomerativeEdge(
                            nodeI, nodeJ, EuclideanGeometry.Distance(nodeI.GetItems[0].GetCoordinates,
                            nodeJ.GetItems[0].GetCoordinates)));
                        nextLayer.Add(nodeJ);
                    }
                    visited[nodeI] = true;
                }
                Report(CELpart * CDpart * ((buildingEdgesListCurProgress += curLayer.Count) / buildingEdgesListTotalProgress));
                curLayer = nextLayer;
            }
            CurProgress += CELpart * CDpart;
            edges.Sort((edge1, edge2) => -edge1.Distance.CompareTo(edge2.Distance));
            Report(SELpart * CDpart);
            CurProgress += SELpart * CDpart;
            double CD1_CurProgress = 0;
            double CD1_TotalProgress = edges.Count;
            while (edges.Count > 0)//процесс построения дендрограммы
            {
                if (StopFlag)
                {
                    return null;
                }
                AglomerativeEdge curEdge =
                    edges[edges.Count - 1];//текущее кратчайшее ребро
                AglomerativeNode node1 = curEdge.Node1;
                AglomerativeNode node2 = curEdge.Node2;
                AglomerativeNode newNode = new AglomerativeNode(node1, node2,
                    curEdge.Distance, Items);
                edges.RemoveAt(edges.Count - 1);
                for (int i = 0; i < edges.Count; ++i)//перенаправление ребер в списке
                {
                    if (StopFlag)
                    {
                        return null;
                    }
                    edges[i].Redirect(node1, node2, newNode);
                }
                //Стянем две вершины в одну
                List<AglomerativeNode> newNeighbours =
                    new List<AglomerativeNode>();
                newNeighbours.AddRange(mst[node1]);
                newNeighbours.AddRange(mst[node2]);
                newNeighbours.Remove(node1);
                newNeighbours.Remove(node2);
                mst.Remove(node1);
                mst.Remove(node2);
                mst[newNode] = newNeighbours;
                Report(CD1part * CDpart * (++CD1_CurProgress / CD1_TotalProgress));
            }
            var keys = mst.Keys;
            return keys.ElementAt(0);
        }//AglomerativeNode CreateDendrogramm_SingleLinkDistance()

        double CentreDistance(AglomerativeNode node1, AglomerativeNode node2)
        {
            return EuclideanGeometry.Distance(node1.GetCentre, node2.GetCentre);
        }

        double WardDistance(AglomerativeNode node1, AglomerativeNode node2)
        {
            return (node1.Count * node2.Count / (node1.Count + node1.Count)) *
                CentreDistance(node1, node2);
        }

        delegate double AglomerativeDistance(AglomerativeNode node1,
            AglomerativeNode node2);
        AglomerativeNode CreateDendrogramm(AglomerativeDistance distance)
        {
            List<AglomerativeNode> _ANNodes = PrimaryNodes();//инициализация списка агломеративных нод
            MSTMaster = new MinimumSpanningTreeMaster();
            double CD_CurProgress = 0;
            double CD_TotalProgress = 2 * Items.Count - 1;
            while (_ANNodes.Count > 1)//процесс слияния агломеративных нод
            {
                if (StopFlag)
                {
                    return null;
                }
                MSTMaster.Clear();
                MSTMaster.ProgressChanged += (double x) => {};
                //Найдем ближайшие агломеративные ноды для слияния
                List<List<int>> mst = MSTMaster.CreateMinimumSpanningTree(_ANNodes,
                    (a, b, nodes) => {
                        if (a == b) return double.PositiveInfinity;
                        return distance(nodes[a], nodes[b]);
                    });//минимальное ребро гарантированно входит в минимальное остовное дерево
                int targetI = -1, targetJ = -1;
                double minDist = double.PositiveInfinity;
                for(int i = 0; i < mst.Count; ++i)
                {
                    foreach(var ind in mst[i])
                    {
                        if (StopFlag)
                        {
                            return null;
                        }
                        double curDist = distance(_ANNodes[i], _ANNodes[ind]);
                        if (curDist < minDist)
                        {
                            minDist = curDist;
                            targetI = i;
                            targetJ = ind;
                        }
                    }
                }
                AglomerativeNode newNode = new AglomerativeNode(
                    _ANNodes[targetI], _ANNodes[targetJ], CentreDistance(
                        _ANNodes[targetI], _ANNodes[targetJ]), Items);
                AglomerativeNode nodeI = _ANNodes[targetI];
                AglomerativeNode nodeJ = _ANNodes[targetJ];
                _ANNodes.Remove(nodeI);
                _ANNodes.Remove(nodeJ);
                _ANNodes.Add(newNode);
                Report(CDpart * (++CD_CurProgress / CD_TotalProgress));
            }
            return _ANNodes[0];
        }

        double SD_CurProgress;
        double SD_TotalProgress;
        public void SplitDendrogramm(AglomerativeNode node, List<List<AglomerativeNode>> levels)//распределяем ноды дендрограммы на уровни
        {
            if (StopFlag)
            {
                return;
            }
            if (node == null)
            {
                return;
            }
            levels[node.GetLevel].Add(node);
            Report(SDpart * (++SD_CurProgress / SD_TotalProgress));
            SplitDendrogramm(node.SubNode1, levels);
            SplitDendrogramm(node.SubNode2, levels);
        }
        public List<List<Item>> GetClusters()
        {
            StopFlag = false;
            CurProgress = 0;
            if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            if(DendraRoot == null)
            {
                switch (ACDistance)
                {
                    case ClusteringOptions.AglomerativeClusteringDistance.SingleLink:
                        DendraRoot = CreateDendrogramm_SingleLinkDistance();
                        break;
                    case ClusteringOptions.AglomerativeClusteringDistance.CentreDistance:
                        DendraRoot = CreateDendrogramm(CentreDistance);
                        break;
                    case ClusteringOptions.AglomerativeClusteringDistance.WardDistance:
                        DendraRoot = CreateDendrogramm(WardDistance);
                        break;
                }
            }
            CurProgress = CDpart;
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            if(Levels == null)
            {
                Levels = new List<List<AglomerativeNode>>();
                for (int i = 0; i <= DendraRoot.GetLevel; ++i)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    Levels.Add(new List<AglomerativeNode>());
                }
                SD_CurProgress = 0;
                SD_TotalProgress = 2 * Items.Count - 1;
                SplitDendrogramm(DendraRoot, Levels);
            }
            else
            {
                for (int i = 1; i < Levels.Count; ++i)//разблокируем все ноды
                {
                    for (int j = 0; j < Levels[i].Count; ++j)
                    {
                        if (StopFlag)
                        {
                            return new List<List<Item>>();
                        }
                        Levels[i][j].Locked = false;
                    }
                }
            }
            CurProgress += SDpart;
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            double EC_CurProgress = 0;
            double EC_TotalProgress = 2 * (Levels.Count - 1);
            for(int i = 1; i < Levels.Count; ++i)//помечаем ноды со слишком большим собственным расстоянием
            {
                for(int j = 0; j < Levels[i].Count; ++j)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    AglomerativeNode curNode = Levels[i][j];
                    if(curNode.GetDistance > DetalizationCoef)
                    {
                        curNode.Locked = true;
                    }
                }
                Report(MInANpart * ECpart * (++EC_CurProgress / EC_TotalProgress));
            }
            for (int i = 1; i < Levels.Count; ++i)//помечаем ноды, у которых помечен хотя бы 1 потомок
            {
                for (int j = 0; j < Levels[i].Count; ++j)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    AglomerativeNode curNode = Levels[i][j];
                    if(curNode.SubNode1.Locked || curNode.SubNode2.Locked)
                    {
                        curNode.Locked = true;
                    }
                }
                Report(MInANpart * ECpart * (++EC_CurProgress / EC_TotalProgress));
            }
            CurProgress += MInANpart * ECpart;
            List<AglomerativeNode> CompletedNodes = new List<AglomerativeNode>();
            if(!Levels[Levels.Count - 1][0].Locked)
            {
                CompletedNodes.Add(Levels[Levels.Count - 1][0]);
            }
            double EAN_CurProgress = 0;
            double EAN_TotalProgress = Levels.Count - 1;
            for (int i = 1; i < Levels.Count; ++i)//извлекаем непомеченные ноды, они задают искомую кластеризацию
            {
                if (StopFlag)
                {
                    return new List<List<Item>>();
                }
                for (int j = 0; j < Levels[i].Count; ++j)
                {
                    if (StopFlag)
                    {
                        return new List<List<Item>>();
                    }
                    AglomerativeNode curNode = Levels[i][j];
                    if (!curNode.Locked) continue;
                    if (!curNode.SubNode1.Locked) CompletedNodes.Add(curNode.SubNode1);
                    if (!curNode.SubNode2.Locked) CompletedNodes.Add(curNode.SubNode2);
                }
                Report(EANpart * ECpart * (++EAN_CurProgress / EAN_TotalProgress));
            }
            List<List<Item>> result = new List<List<Item>>();
            foreach(var node in CompletedNodes)
            {
                if (StopFlag)
                {
                    return new List<List<Item>>();
                }
                result.Add(node.GetItems);
            }
            return result; 
        }
    }//class AglomerativeClusteringClass

    public class DBSCANClusteringClass :  ClusteringClass, IClustering
    {
        public double ReachabilityRadius;
        public int Threshold;
        public override event DebugDel debugEvent;
        //
        double CGpart = 35.0 / 40;
        double SRpart = 4.0 / 40;
        double CCpart = 1.0 / 40;
        double CurProgress = 0;
        void Report(double x)
        {
            ProgressChanged(x + CurProgress);
        }
        //
        public DBSCANClusteringClass(double reachabilityRadius, int threshold, List<Item> items)
        {
            ReachabilityRadius = reachabilityRadius;
            Threshold = threshold;
            Items = items;
        }

        public override void SetOptions(ClusteringOptions opt)
        {
            ReachabilityRadius = opt.ReachabilityRadius;
            Threshold = opt.Threshold;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ReachabilityRadius = ReachabilityRadius;
            result.Threshold = Threshold;
            return result;
        }

        public override event progressDel ProgressChanged;
        //public LearningMode learningMode { set; get; }
        public override List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            List<List<Item>> result = new List<List<Item>>();
            bool[] visited = new bool[Items.Count];//информация о посещенных вершинах
            List<int>[] graph = new List<int>[Items.Count];//список смежности
            for(int i = 0; i < graph.Length; ++i)
            {
                graph[i] = new List<int>();
            }
            //int total = Items.Count * (Items.Count + 1) / 2;
            //double cur = 0;
            double CG_CurProgress = 0;
            double CG_TotalProgress = Items.Count * Items.Count;
            for(int i = 0; i < Items.Count; ++i)//построение графа
            {
                for(int j = 0; j < Items.Count; ++j)
                {
                    Report(CGpart * (++CG_CurProgress / CG_TotalProgress));
                    if (j > i) continue;
                    if(EuclideanGeometry.Distance(Items[i].GetCoordinates, Items[j].GetCoordinates)
                        <= ReachabilityRadius)
                    {
                        if (StopFlag) return null;
                        graph[i].Add(j);
                        graph[j].Add(i);
                    }
                }
            }
            CurProgress = CGpart;
            double SR_CurProgress = 0;
            double SR_TotalProgress = Items.Count;
            bool[] IsRoot = new bool[Items.Count];
            for(int i = 0; i < Items.Count; ++i)//выявление корневых вершин
            {
                if(graph[i].Count >= Threshold)
                {
                    if (StopFlag) return null;
                    IsRoot[i] = true;
                }
                Report(SRpart * (++SR_CurProgress / SR_TotalProgress));
            }
            CurProgress += SRpart;
            double CC_CurProgress = 0;
            double CC_TotalProgress = Items.Count;
            for(int i = 0; i < Items.Count; ++i)//
            {
                if (visited[i]) continue;
                if (!IsRoot[i]) continue;
                List<int> curCluster = new List<int>();
                List<int> curLayer = new List<int>();
                curLayer.Add(i);
                while(curLayer.Count > 0)//формирование кластера как компоненту связности через обход в глубину
                {
                    foreach(var ind in curLayer)//извлечение вершин из текущего слоя обхода для текущего кластера
                    {
                        if (StopFlag) return new List<List<Item>>();
                        if (visited[ind]) continue;
                        visited[ind] = true;
                        curCluster.Add(ind);
                        Report(CCpart * (++CC_CurProgress / CC_TotalProgress));
                    }
                    List<int> nextLayer = new List<int>();
                    bool[] added = new bool[Items.Count];
                    foreach(var ind in curLayer)//формирование следующего слоя обхода
                    {
                        if (IsRoot[ind])
                        {
                            foreach(var _ind in graph[ind])
                            {
                                if (StopFlag) return null;
                                if (!visited[_ind] && !added[_ind])
                                {
                                    nextLayer.Add(_ind);
                                    added[_ind] = true;
                                }
                            }
                        }
                    }
                    curLayer = nextLayer;
                }
                List<Item> _curCluster = new List<Item>();
                foreach(var ind in curCluster)
                {
                    if (StopFlag) return new List<List<Item>>();
                    _curCluster.Add(Items[ind]);
                }
                result.Add(_curCluster);
            }
            for(int i = 0; i < Items.Count; ++i)//формирование одноэлементных кластеров шума
            {
                if (!visited[i])
                {
                    if (StopFlag) return new List<List<Item>>();
                    result.Add(new List<Item>(new Item[] { Items[i] }));
                    Report(CCpart * (++CC_CurProgress / CC_TotalProgress));
                }
            }
            return result;
        }
    }//class DBSCANClusteringClass

    public class AffinityPropagationClusteringClass : ClusteringClass, IClustering
    {
        public double SelfSimilarity;
        public double ConvergencePrecision;
        public override event progressDel ProgressChanged;
        double[,] Similarity = null;
        double[,] Responsibility = null;
        double[,] prevResponsibility = null;
        double[,] Availability = null;
        double[,] prevAvailability = null;
        public AffinityPropagationClusteringClass(int selfSimilarity, double convergencePrecision, List<Item> items)
        {
            SelfSimilarity = selfSimilarity;
            ConvergencePrecision = convergencePrecision;
            Items = items;
        }

        public override void SetItems(List<Item> items)
        {
            base.SetItems(items);
            Similarity = null;
        }

        public override void SetOptions(ClusteringOptions opt)
        {
            if(SelfSimilarity != opt.SelfSimilarity)
            {
                Similarity = null;
            }
            SelfSimilarity = opt.SelfSimilarity;
            ConvergencePrecision = opt.ConvergencePrecision;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.SelfSimilarity = SelfSimilarity;
            result.ConvergencePrecision = ConvergencePrecision;
            return result;
        }

        double Sum1(int i, int k, double[,] responsibility)
        {
            double result = 0;
            int MaxJ = responsibility.GetLength(0);
            for(int j = 0; j < MaxJ; ++j)
            {
                if (j == i || j == k) continue;
                result += Algorithm.Max(0, responsibility[j, k]);
            }
            return result;
        }
        double Sum2(int k, double[,] responsibility)
        {
            double result = 0;
            int MaxJ = responsibility.GetLength(0);
            for(int j = 0; j < MaxJ; ++j)
            {
                if (j == k) continue;
                result += Algorithm.Max(0, responsibility[j, k]);
            }
            return result; 
        }
        public void SetLearningMode(LearningMode _learningMode)
        {
            learningMode = _learningMode;
        }
        public override List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            if (learningMode == LearningMode.Start)
            {
                if(Similarity == null)
                {
                    Similarity = new double[Items.Count, Items.Count];
                    for (int i = 0; i < Items.Count; ++i)
                    {
                        for (int j = 0; j < Items.Count; ++j)
                        {
                            if (j > i) continue;
                            if (i == j)
                            {
                                Similarity[i, j] = SelfSimilarity;
                                continue;
                            }
                            double curSim = -EuclideanGeometry.Distance(Items[i].GetCoordinates,
                                Items[j].GetCoordinates);
                            Similarity[i, j] = Similarity[j, i] = curSim;
                        }
                    }
                }
                prevResponsibility = new double[Items.Count, Items.Count];
                Responsibility = new double[Items.Count, Items.Count];
                prevAvailability = new double[Items.Count, Items.Count];
                Availability = new double[Items.Count, Items.Count];
            }
            for(int EpochNum = 1; ; ++EpochNum)
            {
                if(EpochNum > 1 && StopFlag)
                {
                    ProgressChanged(EpochNum - 1);
                    break;
                }
                for (int i = 0; i < Items.Count; ++i)
                {
                    for(int k = 0; k < Items.Count; ++k)
                    {
                        List<double> list = new List<double>();
                        for(int j = 0; j < Items.Count; ++j)
                        {
                            if (j == k) continue;
                            list.Add(Availability[i, j] + Responsibility[i, j]);
                        }
                        Responsibility[i, k] = Similarity[i, k] - Algorithm.FindMax(list);
                    }
                }
                for(int i = 0; i < Items.Count; ++i)
                {
                    for(int k = 0; k < Items.Count; ++k)
                    {
                        if (i == k) continue;
                        Availability[i, k] =
                            Algorithm.Min(0, Responsibility[k, k] + Sum1(i, k, Responsibility));
                    }
                }
                for(int k = 0; k < Items.Count; ++k)
                {
                    Availability[k, k] = Sum2(k, Responsibility);
                }
                ProgressChanged(EpochNum);
                if (EpochNum > 1 && Algorithm.SimilarMatrices(Responsibility, prevResponsibility, 
                    ConvergencePrecision) && Algorithm.SimilarMatrices(Availability, prevAvailability, 
                    ConvergencePrecision))
                {
                    break;
                }
                prevResponsibility = (double[,])Responsibility.Clone();
                prevAvailability = (double[,])Availability.Clone();
            }
            List<int> Exemplars = new List<int>();
            for(int k = 0; k < Items.Count; ++k)
            {
                if(Availability[k, k] + Responsibility[k, k] > 0)
                {
                    Exemplars.Add(k);
                }
            }
            if (Exemplars.Count == 0)
            {
                for (int k = 0; k < Items.Count; ++k)
                {
                    Exemplars.Add(k);
                }
            }
            Algorithm.Compare<int> compareExemplars(int i)
            {
                Algorithm.Compare<int> res = delegate (int exmp1, int exmp2)
                {
                    return (Availability[i, exmp1] + Responsibility[i, exmp1]).CompareTo(
                    Availability[i, exmp2] + Responsibility[i, exmp2]);
                };
                return res;
            }
            int[] Leadership = new int[Items.Count];
            for(int i = 0; i < Items.Count; ++i)
            {
                if (Exemplars.Contains(i))
                {
                    Leadership[i] = i;
                    continue;
                }
                Leadership[i] = Algorithm.FindMax(Exemplars, compareExemplars(i));
            }
            Dictionary<int, List<Item>> groups = new Dictionary<int, List<Item>>();
            foreach(var exmp in Exemplars)
            {
                groups[exmp] = new List<Item>();
            }
            for(int i = 0; i < Leadership.Length; ++i)
            {
                groups[Leadership[i]].Add(Items[i]);
            }
            List<List<Item>> result = new List<List<Item>>();
            foreach(var exmp in Exemplars)
            {
                result.Add(groups[exmp]);
            }
            return result;
        }
    }//class AffinityPropagationClusteringClass

    public class FORELNode : ClusteringNode
    {
        double Range;
        List<Item> Domain = new List<Item>();
        public FORELNode(double[] coordinates, double range) : base(coordinates)
        {
            Range = range;
        }
        public void Grab(List<Item> items, bool[] used)
        {
            Domain = new List<Item>();
            for(int i = 0; i < items.Count; ++i)
            {
                if (used[i]) continue;
                if(EuclideanGeometry.Distance(items[i].GetCoordinates, Coordinates) <= Range)
                {
                    Domain.Add(items[i]);
                }
            }
        }
        public List<Item> Grab_Own(List<Item> items, bool[] used)
        {
            List<int> DomainInd = new List<int>();
            for (int i = 0; i < items.Count; ++i)
            {
                if (used[i]) continue;
                if (EuclideanGeometry.Distance(items[i].GetCoordinates, Coordinates) <= Range)
                {
                    DomainInd.Add(i);
                }
            }
            foreach(var ind in DomainInd)
            {
                used[ind] = true;
            }
            List<Item> result = new List<Item>();
            foreach(var ind in DomainInd)
            {
                result.Add(items[ind]);
            }
            return result;
        }
        public void Learn()
        {
            Coordinates = EuclideanGeometry.Barycentre(Item.ToDoubleArray(Domain));
        }
    }//class FORELNode

    public class FORELClusteringClass : ClusteringClass
    {
        public double ReachabilityRadius;
        public override event progressDel ProgressChanged;
        public FORELClusteringClass(double reachabilityRadius, List<Item> items)
        {
            Items = new List<Item>();
            Items.AddRange(items);
            ReachabilityRadius = reachabilityRadius; 
        }

        public override void SetOptions(ClusteringOptions opt)
        {
            ReachabilityRadius = opt.ReachabilityRadius;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ReachabilityRadius = ReachabilityRadius;
            return result;
        }

        public override List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            List<List<Item>> result = new List<List<Item>>();
            bool[] used = new bool[Items.Count];
            FORELNode node;
            double grabbedElements = 0;
            while (used.Contains(false))
            {
                for (int i = 0; i < Items.Count; ++i)
                {
                    if (used[i]) continue;
                    node = new FORELNode(Items[i].GetCoordinates, ReachabilityRadius);
                    node.Grab(Items, used);
                    double[] oldCoord = node.GetCoordinates();
                    while (true)
                    {
                        if (StopFlag) return null;
                        node.Learn();
                        if (EuclideanGeometry.Distance(oldCoord, node.GetCoordinates()) == 0)
                        {
                            break;
                        }
                        node.Grab(Items, used);
                        oldCoord = node.GetCoordinates();
                    }
                    List<Item> curCluster = node.Grab_Own(Items, used);
                    result.Add(curCluster);
                    grabbedElements += curCluster.Count;
                    ProgressChanged(grabbedElements / Items.Count);
                }
            }
            return result;
        }
    }//class FORELClusteringClass

    public abstract class GraphClusteringClass : ClusteringClass
    {
        public object Cluster;
        List<List<int>> minimumSpanningTree = null;
        MinimumSpanningTreeMaster MSTMaster = new MinimumSpanningTreeMaster();
        public override event progressDel ProgressChanged;
        public override event DebugDel debugEvent;

        public abstract void Report(double x);

        public abstract List<List<int>> SplitGraph(List<List<int>> graph);

        public override void Stop()
        {
            StopFlag = true;
            MSTMaster.StopFlag = true;
            minimumSpanningTree = null;
        }

        public override void SetItems(List<Item> items)
        {
            if (items == null)
            {
                Items = new List<Item>();
            }
            else
            {
                Items = new List<Item>(items);
            }
            minimumSpanningTree = null;
        }

        protected double Distances(int a, int b)
        {
            if (a == b)
            {
                return double.PositiveInfinity;
            }
            return EuclideanGeometry.Distance(Items[a].GetCoordinates,
                Items[b].GetCoordinates);
        }

        public List<List<int>> GetCompenents(List<List<int>> graph)
        {
            if (StopFlag)
            {
                return new List<List<int>>();
            }
            List<List<int>> result = new List<List<int>>();//искомый список компонент
            bool[] visited = new bool[graph.Count];//массив, содержащий информацию о посещенных вершинах
            for (int i = 0; i < Items.Count; ++i)
            {
                if (StopFlag)
                {
                    return new List<List<int>>();
                };
                if (visited[i]) continue; //если вершина посещена, то не следует снова искать ее компоненту связности//here
                List<int> curComponent = new List<int>();//текущая компонента
                List<int> curLayer = new List<int>();//текущий слой вершин обхода
                curLayer.Add(i);//первый слой состоит из вершины, с которой начинается обход
                double cur6 = 0;
                double total6 = Items.Count;
                while (curLayer.Count > 0)//пока текущий слой обхода непуст
                {
                    if (StopFlag)
                    {
                        return new List<List<int>>();
                    };
                    foreach (var ind in curLayer)//относим к текущей компоненте вершины текущего слоя
                    {
                        if (StopFlag)
                        {
                            return new List<List<int>>();
                        };
                        if (visited[ind]) continue;
                        visited[ind] = true;
                        curComponent.Add(ind);
                    }
                    List<int> nextLayer = new List<int>();//список вершин следующего слоя
                    bool[] added = new bool[Items.Count];//этот массив нужен, чтобы никакая вершина не вошла в новый слой дважды
                    foreach (var ind in curLayer)
                    {
                        foreach (var _ind in graph[ind])//для каждой вершины текущего слоя обходим список смежных с ней вершин
                        {
                            if (StopFlag)
                            {
                                return new List<List<int>>();
                            };
                            if (!visited[_ind] && !added[_ind])
                            {
                                nextLayer.Add(_ind);
                                added[_ind] = true;
                            }
                        }
                    }
                    curLayer = nextLayer;
                }
                result.Add(curComponent);
                cur6 += curComponent.Count;
                Report((5.0 / 6) + (1.0 / 6) * cur6 / total6);
            }
            return result;
        }//List<List<int>> GetCompenents(List<List<int>> graph)
        public override List<List<Item>> GetClusters()
        {
            StopFlag = false;
            if (Items == null || Items.Count == 0)
            {
                return new List<List<Item>>();
            }
            if (Items.Count == 1)
            {
                List<Item> cluster = new List<Item>();
                cluster.Add(Items[0]);
                List<List<Item>> clusters = new List<List<Item>>();
                clusters.Add(cluster);
                return clusters;
            }
            if(minimumSpanningTree == null || minimumSpanningTree.Count == 0)
            {
                MSTMaster = new MinimumSpanningTreeMaster();
                MSTMaster.ProgressChanged += (double x) => {
                    Report(x);
                };
                minimumSpanningTree = MSTMaster.CreateMinimumSpanningTree(Items,
                    (a, b, items) => {
                        if (a == b) return double.PositiveInfinity;
                        return EuclideanGeometry.Distance(items[a].GetCoordinates,
                            items[b].GetCoordinates);
                    });
            }
            if (StopFlag)
            {
                return new List<List<Item>>();
            }

            List<List<int>> SplittedGraph = SplitGraph(minimumSpanningTree);
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            List<List<int>> Componets = GetCompenents(SplittedGraph);
            if (StopFlag)
            {
                return new List<List<Item>>();
            }
            List<List<Item>> result = new List<List<Item>>();
            foreach (var component in Componets)
            {
                if (StopFlag)
                {
                    return new List<List<Item>>();
                }
                result.Add(new List<Item>());
                foreach (var ind in component)
                {
                    if (StopFlag) return null;
                    result[result.Count - 1].Add(Items[ind]);
                }
            }
            return result;
        }//List<List<Item>> GetClusters()
    }//class GraphClusteringClass

    public class MinimumSpanningTreeClusteringClass : GraphClusteringClass
    {
        public override event progressDel ProgressChanged;
        public int ClustersNumber;
        public MinimumSpanningTreeClusteringClass(int clustersNumber, List<Item> items)
        {
            ClustersNumber = clustersNumber;
            Items = items;
        }
        public override void SetOptions(ClusteringOptions opt)
        {
            ClustersNumber = opt.ClustersNumber;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.ClustersNumber = ClustersNumber;
            return result;
        }
        public override void Report(double x)
        {
            ProgressChanged(x);
        }
        public override List<List<int>> SplitGraph(List<List<int>> graph)
        {
            List<Tuple<int, int, double>> edges = new List<Tuple<int, int, double>>();
            double cur4 = 0;
            double total4 = 0;
            graph.ForEach(x => total4 += x.Count);
            for (int i = 0; i < graph.Count; ++i)
            {
                for (int j = 0; j < graph[i].Count; ++j)
                {
                    if (StopFlag) return null;
                    if (i >= graph[i][j]) continue;
                    edges.Add(new Tuple<int, int, double>(i, graph[i][j], Distances(i, graph[i][j])));
                }
                ++cur4;
                ProgressChanged((3.0 / 6) + (1.0 / 6) * cur4 / total4);
            }
            edges.Sort((edge1, edge2) => -edge1.Item3.CompareTo(edge2.Item3));
            List<List<int>> result = new List<List<int>>();
            foreach (var i in graph)
            {
                if (StopFlag) return null;
                result.Add(new List<int>(i));
            }
            for (int i = 0; i < ClustersNumber - 1 && i < edges.Count; ++i)
            {
                if (StopFlag)
                {

                    return new List<List<int>>();
                }
                result[edges[i].Item1].Remove(edges[i].Item2);
                result[edges[i].Item2].Remove(edges[i].Item1);
            }
            return result;
        }
    }//class MinimumSpanningTreeClusteringClass

    public class FullGraphClusteringClass : GraphClusteringClass
    {
        public override event progressDel ProgressChanged;
        public double MaxDistance;
        public FullGraphClusteringClass(double maxDistance, List<Item> items)
        {
            MaxDistance = maxDistance;
            Items = items;
        }
        public override void SetOptions(ClusteringOptions opt)
        {
            MaxDistance = opt.MaxDistance;
        }
        public override ClusteringOptions GetOptions()
        {
            ClusteringOptions result = new ClusteringOptions();
            result.MaxDistance = MaxDistance;
            return result;
        }
        public override void Report(double x)
        {
            ProgressChanged(x);
        }
        public override List<List<int>> SplitGraph(List<List<int>> graph)
        {
            List<Tuple<int, int, double>> edges = new List<Tuple<int, int, double>>();
            double cur4 = 0;
            double total4 = 0;
            graph.ForEach(x => total4 += x.Count);
            for (int i = 0; i < graph.Count; ++i)
            {
                for (int j = 0; j < graph[i].Count; ++j)
                {
                    if (StopFlag) return null;
                    if (i >= graph[i][j]) continue;
                    edges.Add(new Tuple<int, int, double>(i, graph[i][j], Distances(i, graph[i][j])));
                }
                ++cur4;
                ProgressChanged((3.0 / 6) + (1.0 / 6) * cur4 / total4);
            }
            edges.Sort((edge1, edge2) => -edge1.Item3.CompareTo(edge2.Item3));
            edges.RemoveAll(x => x.Item3 >= MaxDistance);
            List<List<int>> result = new List<List<int>>();
            foreach (var i in graph)
            {
                if (StopFlag) return null;
                result.Add(new List<int>());
            }
            double cur = 0;
            double total = edges.Count;
            foreach (var edge in edges)
            {
                //if (edge.Item3 <= MaxDistance) continue;
                result[edge.Item1].Add(edge.Item2);
                result[edge.Item2].Add(edge.Item1);
                ++cur;
                ProgressChanged((4.0 / 6) + (1.0 / 6) * cur / total);
            }
            return result;
        }
    }//class FullGraphClusteringClass

}
