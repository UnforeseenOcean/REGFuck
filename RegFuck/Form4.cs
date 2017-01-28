using System;
using System.Windows.Forms;

namespace RegFuck
{
    public partial class Form4 : Form
    {
        public static double hkcr = 0.05;
        public static double hku = 1.0;
        public static double hklm = 0.0;
        public static double del = 0.05;

        public static int seed = new Random().Next();

        public Form4()
        {
            InitializeComponent();

            textBox1.Text = seed.ToString();

            trackBar1.Value = (int)(hkcr * 100);
            label2.Text = trackBar1.Value + "%";

            trackBar2.Value = (int)(hku * 100);
            label4.Text = trackBar2.Value + "%";

            trackBar3.Value = (int)(hklm * 100);
            label6.Text = trackBar3.Value + "%";

            trackBar4.Value = (int)(del * 100);
            label8.Text = trackBar4.Value + "%";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            hkcr = trackBar1.Value / 100.0;
            label2.Text = trackBar1.Value + "%";
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            hku = trackBar2.Value / 100.0;
            label4.Text = trackBar2.Value + "%";
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            hklm = trackBar3.Value / 100.0;
            label6.Text = trackBar3.Value + "%";
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            del = trackBar4.Value / 100.0;
            label8.Text = trackBar4.Value + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = new Random().Next().ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out seed))
                seed = textBox1.Text.GetHashCode();
        }
    }
}
