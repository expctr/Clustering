using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuclideanGeometryLib;
using ItemLib;
using AlgorithmLib;

namespace EuclideanGeometryLib
{
    public static class EuclideanGeometry
    {
        public static double[] VectorSubtraction(double[] first, double[] second)
        {
            double[] result = new double[first.Length];
            for (int i = 0; i < first.Length; ++i)
            {
                result[i] = first[i] - second[i];
            }
            return result;
        }
        public static double Norme(double[] input)
        {
            double result = 0;
            foreach (var i in input)
            {
                result += i * i;
            }
            result = Math.Sqrt(result);
            return result;
        }
        public static double Distance(double[] FirstPoint, double[] SecondPoint)
        {
            return Norme(VectorSubtraction(FirstPoint, SecondPoint));
        }
        public static double Distance(Item first, Item second)
        {
            return Distance(first.GetCoordinates, second.GetCoordinates);
        }
        public static double[] Barycentre(List<double[]> Points)
        {
            double[] Result = new double[Points[0].Length];
            for (int i = 0; i < Result.Length; ++i)
            {
                double CoordSum = 0;
                for (int j = 0; j < Points.Count; ++j)
                {
                    CoordSum += Points[j][i];
                }
                Result[i] = CoordSum / Points.Count;
            }
            return Result;
        }
        public static double[] Barycentre(List<double[]> points, List<double> masses)
        {
            double[] result = new double[points[0].Length];
            double massSum = 0;
            masses.ForEach(x => { massSum += x; });
            for(int i = 0; i < result.Length; ++i)
            {
                for(int j = 0; j < points.Count; ++j)
                {
                    result[i] += masses[j] * points[j][i] / massSum;
                }
            }
            return result;
        }
        public static double[] Midpoint(double[] first, double[] second)
        {
            double[] result = new double[first.Length];
            for (int i = 0; i < first.Length; ++i)
            {
                result[i] = (first[i] + second[i]) / 2;
            }
            return result;
        }
        public static Tuple<double[], double[]>  MinMaxDim(List<Item> items)
        {
            double[] minDim = new double[items[0].GetCoordinates.Count()];
            double[] maxDim = new double[items[0].GetCoordinates.Count()];
            for (int i = 0; i < minDim.Length; ++i)
            {
                double curMinDim = items[0][i];
                foreach (var item in items)
                {
                    if (curMinDim > item[i])
                        curMinDim = item[i];
                }
                minDim[i] = curMinDim;
            }
            for (int i = 0; i < maxDim.Length; ++i)
            {
                double curMaxDim = items[0][i];
                foreach(var item in items)
                {
                    if (curMaxDim < item[i])
                        curMaxDim = item[i];
                }
                maxDim[i] = curMaxDim;
            }
            return new Tuple<double[], double[]>(minDim, maxDim);
        }
        public static double Distance_SingleLink(List<double[]> first, List<double[]> second)
        {
            List<double> minDists = new List<double>();
            for(int i = 0; i < first.Count; ++i)
            {
                List<double> curDist = new List<double>();
                for(int j = 0; j < second.Count; ++j)
                {
                    curDist.Add(Distance(first[i], second[j]));
                }
                minDists.Add(Algorithm.FindMin(curDist));
            }
            return Algorithm.FindMin(minDists);
        }
        public static double[] RatioPoint(double[] first, double[] second, double lambda)
        {
            double[] result = new double[first.Length];
            for(int i = 0; i < result.Length; ++i)
            {
                result[i] = (first[i] + lambda * second[i]) / (lambda + 1);
            }
            return result;
        }
        public static double[] Barycentre(double[] first, double weight1, double[] second, double weight2)
        {
            return RatioPoint(first, second, weight2 / weight1);
        }
        public static double[] TransformVector(double[] point, double[] normCoef)
        {
            double[] result = new double[point.Length];
            for(int i = 0; i < result.Length; ++i)
            {
                if(normCoef[i] != 0)
                {
                    result[i] = point[i] / normCoef[i];
                }
            }
            return result;
        }
        public static List<double[]> Normalize(List<double[]> points)
        {
            int dimension = points[0].Length;
            List<double[]> result = new List<double[]>();
            List<double> coordList;
            double[] normCoef = new double[dimension];
            for(int i = 0; i < dimension; ++i)
            {
                coordList = new List<double>();
                foreach(var point in points)
                {
                    coordList.Add(point[i]);
                }
                normCoef[i] = Math.Abs(Algorithm.FindMax(coordList, 
                    (a, b) => {
                        return Math.Abs(a).CompareTo(Math.Abs(b));
                    }));
            }
            foreach(var point in points)
            {
                result.Add(TransformVector(point, normCoef));
            }
            return result;
        }
    }
}
