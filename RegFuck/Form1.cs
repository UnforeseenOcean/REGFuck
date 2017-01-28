using System;
using System.Windows.Forms;

namespace RegFuck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2(Math.Pow(2, (trackBar1.Value / (100.0 / 12)) - 12)).ShowDialog();
            Hide();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = String.Format("{0:F2}%", Math.Pow(2, (trackBar1.Value / (100.0/12))-12)*100);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = String.Format("{0:F2}%", Math.Pow(2, (trackBar1.Value / (100.0 / 12)) - 12) * 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form4().ShowDialog();
        }
    }
}
