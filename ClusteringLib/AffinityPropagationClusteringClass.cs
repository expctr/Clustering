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
    public class AffinityPropagationClusteringClass : ClusteringClass, IClustering
    {
        public double SelfSimilarity;
        public double ConvergencePrecision;
        public override event ProgressDel ProgressChanged;
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
            if (SelfSimilarity != opt.SelfSimilarity)
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
            for (int j = 0; j < MaxJ; ++j)
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
            for (int j = 0; j < MaxJ; ++j)
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
                if (Similarity == null)
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
            for (int EpochNum = 1; ; ++EpochNum)
            {
                if (EpochNum > 1 && StopFlag)
                {
                    ProgressChanged(EpochNum - 1);
                    break;
                }
                for (int i = 0; i < Items.Count; ++i)
                {
                    for (int k = 0; k < Items.Count; ++k)
                    {
                        List<double> list = new List<double>();
                        for (int j = 0; j < Items.Count; ++j)
                        {
                            if (j == k) continue;
                            list.Add(Availability[i, j] + Responsibility[i, j]);
                        }
                        Responsibility[i, k] = Similarity[i, k] - Algorithm.FindMax(list);
                    }
                }
                for (int i = 0; i < Items.Count; ++i)
                {
                    for (int k = 0; k < Items.Count; ++k)
                    {
                        if (i == k) continue;
                        Availability[i, k] =
                            Algorithm.Min(0, Responsibility[k, k] + Sum1(i, k, Responsibility));
                    }
                }
                for (int k = 0; k < Items.Count; ++k)
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
            for (int k = 0; k < Items.Count; ++k)
            {
                if (Availability[k, k] + Responsibility[k, k] > 0)
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
            for (int i = 0; i < Items.Count; ++i)
            {
                if (Exemplars.Contains(i))
                {
                    Leadership[i] = i;
                    continue;
                }
                Leadership[i] = Algorithm.FindMax(Exemplars, compareExemplars(i));
            }
            Dictionary<int, List<Item>> groups = new Dictionary<int, List<Item>>();
            foreach (var exmp in Exemplars)
            {
                groups[exmp] = new List<Item>();
            }
            for (int i = 0; i < Leadership.Length; ++i)
            {
                groups[Leadership[i]].Add(Items[i]);
            }
            List<List<Item>> result = new List<List<Item>>();
            foreach (var exmp in Exemplars)
            {
                result.Add(groups[exmp]);
            }
            return result;
        }
    }
}
