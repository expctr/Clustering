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
    public interface IGNGNeuron : IClusteringNode
    {
        void GetComponent(List<GNGNeuron> Component, Dictionary<GNGNeuron, bool> UsedNeurons);

        void IncreaseError(Item item);

        void Learn(double[] point, double learningSpeed);

        void LearnNeighbours(Item item, double learningSpeed);

        void IncreaseAges();

        void Connect(GNGNeuron neuron);

        void DeleteOldNeighbours(int maxAge);

        bool NoNeighbours { get; }

        GNGNeuron FindMostIncorrectNeighbour();

        void MultiplyError(double x);

        void PartialConnect(GNGNeuron neuron);

        void PartialDisconnect(GNGNeuron neuron);

        double GetError { get; }

        void Disconnect(GNGNeuron neuron);
    }
}
