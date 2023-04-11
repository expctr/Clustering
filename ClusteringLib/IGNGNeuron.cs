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
        void GetComponent(List<IGNGNeuron> Component, Dictionary<IGNGNeuron, bool> UsedNeurons);

        void IncreaseError(Item item);

        void Learn(double[] point, double learningSpeed);

        void LearnNeighbours(Item item, double learningSpeed);

        void IncreaseAges();

        void Connect(IGNGNeuron neuron);

        void DeleteOldNeighbours(int maxAge);

        bool NoNeighbours { get; }

        IGNGNeuron FindMostIncorrectNeighbour();

        void MultiplyError(double x);

        void PartialConnect(IGNGNeuron neuron);

        void PartialDisconnect(IGNGNeuron neuron);

        double GetError { get; }

        void Disconnect(IGNGNeuron neuron);
    }
}
