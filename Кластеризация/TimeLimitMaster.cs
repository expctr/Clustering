using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ItemLib;
using EuclideanGeometryLib;
using AlgorithmLib;
using System.Diagnostics;
using ClusteringLib;

namespace Кластеризация
{
    public class TimeLimitMaster
    {
        public bool TimeLimitActivated;
        public double Hours, Minutes, Seconds;
        double timeLimit_Seconds;
        Stopwatch watch;
        Action Finish;
        public TimeLimitMaster(bool timeLimitActivated, double hours, double minutes, double seconds)
        {
            TimeLimitActivated = timeLimitActivated;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            watch = new Stopwatch();
        }

        public void SetOptions(ClusteringOptions opt)
        {
            TimeLimitActivated = opt.TimeLimitActivated;
            Hours = opt.Hours;
            Minutes = opt.Minutes;
            Seconds = opt.Seconds;
        }
        public void AddOptions(ClusteringOptions opt)
        {
            opt.TimeLimitActivated = TimeLimitActivated;
            opt.Hours = Hours;
            opt.Minutes = Minutes;
            opt.Seconds = Seconds;
        }

        public double TimeLimit_Seconds
        {
            get
            {
                return Hours * 3600 + Minutes * 60 + Seconds;
            }
        }
        public void SetTimeLimit(double hours, double minutes, double seconds)
        {
            Hours = hours; Minutes = minutes; Seconds = seconds;
        }
        public void SetFinish(Action finish)
        {
            Finish = null;
            Finish += finish;
        }
        public void Activate()
        {
            watch.Restart();
            timeLimit_Seconds = TimeLimit_Seconds;
        }

        public void Check()
        {
            if (!TimeLimitActivated) return;
            if (watch.Elapsed.Seconds > TimeLimit_Seconds)
            {
                watch.Reset();
                Finish();
            }
        }
    }//class SOMTimeLimitMaster
}
