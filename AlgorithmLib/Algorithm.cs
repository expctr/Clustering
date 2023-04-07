using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemLib;
using System.Collections;
using System.Threading;
using ExcelLib;

namespace AlgorithmLib
{
    public static class Algorithm
    {
        public static int FindIndOfMax<T>(List<T> input) where T : IComparable
        {
            int result = 0;
            for(int i = 1; i < input.Count; ++i)
            {
                if(input[result].CompareTo(input[i]) == -1)
                {
                    result = i;
                }
            }
            return result;
        }
        public static T FindMax<T>(List<T> input) where T : IComparable
        {
            return input[FindIndOfMax(input)];
        }
        public delegate int Compare<T>(T a, T b);
        public static int FindIndOfMax<T>(List<T> input, Compare<T> compare)
        {
            int result = 0;
            for (int i = 1; i < input.Count; ++i)
            {
                if (compare(input[result], (input[i])) == -1)
                {
                    result = i;
                }
            }
            return result;
        }
        public static T FindMax<T>(List<T> input, Compare<T> compare)
        {
            return input[FindIndOfMax(input, compare)];
        }

        public static int FindIndOfMin<T>(List<T> input) where T : IComparable
        {
            int result = 0;
            for (int i = 1; i < input.Count; ++i)
            {
                if (input[result].CompareTo(input[i]) == 1)
                {
                    result = i;
                }
            }
            return result;
        }
        public static T FindMin<T>(List<T> input) where T : IComparable
        {
            return input[FindIndOfMin(input)];
        }
        public static int FindIndOfMin<T>(List<T> input, 
            Compare<T> compare)
        {
            int result = 0;
            for (int i = 1; i < input.Count; ++i)
            {
                if (compare(input[result], input[i]) == 1)
                {
                    result = i;
                }
            }
            return result;
        }
        public static T FindMin<T>(List<T> input, Compare<T> compare)
        {
            return input[FindIndOfMin(input, compare)];
        }



        public static string Print<T>(T input) where T : ICollection, IEnumerable
        {
            string result = "";
            foreach(var i in input)
            {
                result += i.ToString() + ", ";
            }
            return result;
        }
        public static T Max<T> (T a, T b) where T : IComparable
        {
            if (a.CompareTo(b) == 1) return a;
            return b;
        }
        public static T Min<T> (T a, T b) where T : IComparable
        {
            if (a.CompareTo(b) == -1) return a;
            return b;
        }
        public static void DoWait(Action act, int time)
        {
            while (true)
            {
                try
                {
                    act();
                    return;
                }
                catch
                {
                    Thread.Sleep(time);
                }
            }
        }
        public static bool SimilarMatrices(double[,] first, double[,] second, double eps)
        {
            if (first == null || second == null) return false;
            for(int i = 0; i < first.GetLength(0); ++i)
            {
                for(int j = 0; j < first.GetLength(1); ++j)
                {
                    if (Math.Abs(first[i, j] - second[i, j]) > eps) return false;
                }
            }
            return true;
        }
        public static string ToString_CSV(string[] input, bool quotes)
        {
            string result;
            if (quotes)
            {
                result = "\"Name\";";
            }
            else
            {
                result = "Name;";
            }
            for(int i = 0; i < input.Length; ++i)
            {
                if (quotes)
                {
                    result += $"\"{input[i]}\"";
                }
                else
                {
                    result += input[i];
                }
                if(i < input.Length - 1)
                {
                    result += ";";
                }
            }
            return result;
        }
        public static string[] GetColsNames_CSV(string str)
        {
            List<string> resultList = new List<string>();
            string cur = "";
            str += ";";
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == ';')
                {
                    resultList.Add(cur);
                    cur = "";
                    continue;
                }
                cur += str[i];
            }
            string[] result = new string[resultList.Count - 1];
            for (int i = 1; i < resultList.Count; ++i)
            {
                result[i - 1] = resultList[i];
            }
            return result;
        }
        public static List<string> SplitString_CSV(string str)
        {
            List<string> result = new List<string>();
            string cur = "";
            bool add = false;
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == '"')
                {
                    if (!add)
                    {
                        add = true;
                        continue;
                    }
                    else
                    {
                        add = false;
                        result.Add(cur);
                        cur = "";
                        continue;
                    }
                }
                if (add)
                {
                    cur += str[i];
                }
            }
            return result;
        }
        public static Item GetItem_CSV(string str, int dim)
        {
            List<string> strList = SplitString_CSV(str);
            string name = strList[0];
            double[] coordinates = new double[dim];
            if(strList.Count != 1 + dim)
            {
                throw new FormatException();
            }
            for(int i = 1; i < strList.Count; ++i)
            {
                coordinates[i - 1] = double.Parse(strList[i]);
            }
            Item result = new Item(coordinates, name, 0);
            return result;
        }
        public static void WriteSeparator_XLSX(string[] colsNames, Excel excel, int sheet,
            int rowNum)
        {
            excel.WriteCell(rowNum, 0, "Name", sheet);
            for (int i = 1; i <= colsNames.Length; ++i)
            {
                excel.WriteCell(rowNum, i, colsNames[i - 1], sheet);
            }
        }
    }
}
