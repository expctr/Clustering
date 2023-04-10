using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItemLib;
using ClusteringLib;
using AlgorithmLib;
using EuclideanGeometryLib;

namespace Кластеризация
{
    public partial class ObjectInfoForm : Form
    {
        public ObjectInfoForm()
        {
            InitializeComponent();
        }

        public Label GetObjectNameL()
        {
            return ObjectNameL;
        }

        public Label GetObjectIndexL()
        {
            return ObjectIndexL;
        }

        public Label GetClusterIndexL()
        {
            return ClusterIndexL;
        }

        public Label GetDistanceL()
        {
            return DistanceL;
        }

        public DataGridView GetObjectInfoDGV()
        {
            return ObjectInfoDGV;
        }

        public Button GetFindB()
        {
            return FindB;
        }

        public TextBox GetRadiusTB()
        {
            return RadiusTB;
        }

        public Label GetNumberOfElementsInRadius()
        {
            return NumberOfElementsInRadius;
        }

        public int GetWidth()
        {
            return Width;
        }
    }
}