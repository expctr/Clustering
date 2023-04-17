using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusteringLib
{
    public interface IClusteringNode
    {
        void SetCoordinates(double[] coordinates);

        double[] GetCoordinates();

        double[] _Coordinates { get; }

        void SetSavedCoordinates(double[] savedCoordinates);

        double[] GetSavedCoordinates();

        bool Deflected(double ConvEps);

        void RewriteSavedCoordinates();
    }
}
