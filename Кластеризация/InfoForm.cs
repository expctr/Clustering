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

        private void InfoForm_Load(object sender, EventArgs e)
        {

        }

        private void InfoForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = Width - 2 * 8 - 2 * 12;
            dataGridView1.Height = Height - 32 - 2 * 8 - 12;
        }
    }
}

