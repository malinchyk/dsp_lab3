using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FormApp
{
    public partial class Form1 : Form
    {
        public int N;
        public Bitmap original, work;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "bmp |*.bmp";
            openFileDialog1.ShowDialog();
            BinaryReader bReader = new BinaryReader(File.Open(openFileDialog1.FileName, FileMode.Open));
            bReader.Close();

            Bitmap original_image = new Bitmap(openFileDialog1.FileName);
            original = new Bitmap(openFileDialog1.FileName);
            work = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = original_image;
            pictureBox1.Show();
            pictureBox2.Image = work;
            pictureBox2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int lvlshum = 0;
            Random rand = new Random();
            if (radioButton1.Checked == true)
                lvlshum = 2000;
            else if (radioButton2.Checked == true)
                lvlshum = 7000;
            else if (radioButton3.Checked == true)
                lvlshum = 20000;

            for (int i = 0; i < lvlshum; i++)
            {
                work.SetPixel(rand.Next(work.Width), rand.Next(work.Height), Color.White);
            }
            pictureBox2.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            N = Convert.ToInt32(textBox1.Text);
            if (radioButton4.Checked == true)
            {
                for (int i = 0; i < work.Width - N; i++)
                {
                    for (int j = 0; j < work.Height; j++)
                    {
                        median_filter(work, i, j);
                    }
                }
                pictureBox2.Refresh();
            }
            if (radioButton5.Checked == true)
            {
                for (int i = 0; i < work.Width; i++)
                {
                    for (int j = 0; j < work.Height - N; j++)
                    {
                        median_filter2(work, i, j);
                    }
                }
                pictureBox2.Refresh();
            }
            if (radioButton6.Checked == true)
                for (int i = 0; i < work.Width - N; i++)
                {
                    for (int j = 0; j < work.Height - N; j++)
                    {
                        median_filter(work, i, j);
                        median_filter2(work, i, j);
                    }
                }
            pictureBox2.Refresh();

        }
        private void median_filter(Bitmap my_bitmap, int x, int y)
        {
            int cR_, cB_, cG_;
            int k = 0;
            int n = N;

            int[] cR = new int[n + 1];
            int[] cB = new int[n + 1];
            int[] cG = new int[n + 1];

            for (int i = 0; i < n + 1; i++)
            {
                cR[i] = 0;
                cG[i] = 0;
                cB[i] = 0;
            }

            for (int i = x; i < x + N; i++)
            {
                System.Drawing.Color c = my_bitmap.GetPixel(i, y);
                cR[k] = System.Convert.ToInt32(c.R);
                cG[k] = System.Convert.ToInt32(c.G);
                cB[k] = System.Convert.ToInt32(c.B);
                k++;
            }

            Array.Sort(cR);
            Array.Sort(cG);
            Array.Sort(cB);
            int n_ = (int)(n / 2) + 1;

            cR_ = cR[n_];
            cG_ = cG[n_];
            cB_ = cB[n_];

            my_bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(cR_, cG_, cB_));

        }
        private void median_filter2(Bitmap my_bitmap, int x, int y)
        {
            int n;
            int cR_, cB_, cG_;
            int k = 0;

            n = N;

            int[] cR = new int[n + 1];
            int[] cB = new int[n + 1];
            int[] cG = new int[n + 1];
            for (int i = 0; i < n + 1; i++)
            {
                cR[i] = 0;
                cG[i] = 0;
                cB[i] = 0;
            }
            for (int j = y; j < y + N; j++)
            {
                System.Drawing.Color c = my_bitmap.GetPixel(x, j);
                cR[k] = System.Convert.ToInt32(c.R);
                cG[k] = System.Convert.ToInt32(c.G);
                cB[k] = System.Convert.ToInt32(c.B);
                k++;
            }
            Array.Sort(cR);
            Array.Sort(cG);
            Array.Sort(cB);
            int n_ = (int)(n / 2) + 1;

            cR_ = cR[n_];
            cG_ = cG[n_];
            cB_ = cB[n_];

            my_bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(cR_, cG_, cB_));
        }
    }
}

