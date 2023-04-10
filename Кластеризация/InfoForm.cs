using System;
using System.Windows.Forms;

namespace Кластеризация
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        public DataGridView GetDataGridView1()
        {
            return dataGridView1;
        }

        public int GetWidth()
        {
            return Width;
        }

        public int GetHeight()
        {
            return Height;
        }
    }
}

