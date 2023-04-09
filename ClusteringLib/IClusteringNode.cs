using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusteringLib
{
    public interface IClusteringNode
    {
        double[] GetCoordinates();

        bool Deflected(double ConvEps);
    }
}
