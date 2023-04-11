using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusteringLib
{
    interface IClusteringNeuron : IClusteringNode
    {
        void SetCoordinates(double[] coordinates);

        double[] GetCoordinates();

        void SetSavedCoordinates(double[] savedCoordinates);

        double[] GetSavedCoordinates();

        void Learn(double[] point, double learningSpeed);

        void Learn(double[] point);

        bool Deflected(double ConvEps);

        void RewriteSavedCoordinates();
    }
}
