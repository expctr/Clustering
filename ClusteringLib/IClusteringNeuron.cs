using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusteringLib
{
    interface IClusteringNeuron
    {
        void SetCoordinates(double[] coordinates);

        double[] GetCoordinates();

        void SetSavedCoordinates(double[] savedCoordinates);

        double[] GetSavedCoordinates();

        void Learn(double[] point, double learningSpeed);

        bool Deflected(double ConvEps);

        void RewriteSavedCoordinates();
    }
}
