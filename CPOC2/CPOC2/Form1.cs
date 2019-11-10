using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace CPOC2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double angle = 0;
        float x = 0, y = 0;
        float x1 = 0, y1 = 0;
        int r = 30;

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        void do_something()
        {
            int[,] matrix = new int[1000, 1000];
            int[] array = new int[1000];
            int[] res1 = new int[1000];
            int[] res2 = new int[1000];
            Stopwatch StopwatchArray = new Stopwatch();
            Stopwatch StopwatchMatrix = new Stopwatch();
            for(int k=0;k<20;k++)
            {
                #region Filling
                for (int i = 0; i < 1000; i++)
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        Random rnd = new Random();
                        matrix[i, j] = rnd.Next(-1, 4);
                    }
                }
                for (int i = 0; i < 1000; i++)
                {
                    Random rnd = new Random();
                    array[i] = rnd.Next(-1, 4);
                }
                #endregion
                StopwatchArray.Start();
                for (int i = 0; i < 1000; i++)
                {
                    res1[i] = 0;
                    for (int j = 0; j < 1000; j++)
                    {
                        res1[i] += array[j] * matrix[j, i];
                    }
                }
                StopwatchArray.Stop();
                TimeSpan time1 = StopwatchArray.Elapsed;
                StopwatchArray.Reset();
                StopwatchMatrix.Start();
                for (int i = 0; i < 1000; i++)
                {
                    res2[i] = 0;
                    for (int j = 0; j < 1000; j++)
                    {
                        res2[i] += matrix[i, j] * array[j];
                    }
                }
                StopwatchMatrix.Stop();
                TimeSpan time2 = StopwatchMatrix.Elapsed;
                StopwatchMatrix.Reset();
                string elapsedTime1 = Convert.ToString(time1.Milliseconds);
                string elapsedTime2 = Convert.ToString(time2.Milliseconds);
                Action action =() => richTextBox1.Text += ("" + Convert.ToString(k + 1) + ": time for array on matrix: " + elapsedTime1 + " ; " + "time for matrix on array: " + elapsedTime2 + Environment.NewLine);
                if (InvokeRequired)
                    Invoke(action);
                else
                    action();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread mult = new Thread(do_something);
            mult.Start();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 50;
            timer1.Start();
          }

        private void timer1_Tick(object sender, EventArgs e)
        {
            angle += 0.1;
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            x = (float)(r * Math.Cos(angle) + pictureBox1.Width / 2);
            y = (float)(r * Math.Sin(angle) + pictureBox1.Height / 2);
            x1 = (float)((r+30) * Math.Cos(-angle) + pictureBox1.Width / 2);
            y1 = (float)((r+30) * Math.Sin(-angle) + pictureBox1.Height / 2);
            g.FillEllipse(Brushes.Black, x, y, 20, 20);
            g.FillEllipse(Brushes.Red, x1, y1, 20, 20);
        }
    }
}
