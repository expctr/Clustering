using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;

namespace RandomAlgoLib
{
    public static class RandomAlgo
    {
        static Random rand = new Random(DateTime.Now.Millisecond);
        public static double RandomNumber(double a, double b)
        {
            if(a > b)
            {
                double buf = b;
                b = a;
                a = buf;
            }
            return  a + rand.NextDouble() * (b - a);
        }
        public static void RandomShuffle(List<double[]> Input)
        {
            int j;
            for (int i = Input.Count - 1; i > -1; --i)
            {
                j = rand.Next(0, i + 1);
                var buf = Input[i];
                Input[i] = Input[j];
                Input[j] = buf;
            }
        }
        public static Color RandomColor()
        {
            return Color.FromArgb(255, rand.Next(0, 256), rand.Next(0, 256), rand.Next(0, 256));
        }
        public static void RandomColor(byte A, out Color col1, out Color col2)
        {
            int R = rand.Next(0, 256);
            int G = rand.Next(0, 256);
            int B = rand.Next(0, 256);
            col1 = Color.FromArgb(255, R, G, B);
            col2 = Color.FromArgb(A, R, G, B);
        }
        public static T RandomShuffleList<T>(T Input) where T : IList, new()
        {
            T result = new T();
            foreach(var i in Input)
            {
                result.Add(i);
            }
            int j;
            for (int i = result.Count - 1; i > -1; --i)
            {
                j = rand.Next(0, i + 1);
                var buf = result[i];
                result[i] = result[j];
                result[j] = buf;
            }
            return result;
        }
    }
}
