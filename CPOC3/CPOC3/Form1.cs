using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace CPOC3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int size = 1000;
        Random rnd = new Random();
        Random rnd1 = new Random();

        void get_column(int[] ar, int ind)
        {
            StreamReader str1 = new StreamReader("matrix.txt");
            BinaryReader binr1 = new BinaryReader(str1.BaseStream);
            binr1.BaseStream.Position = ind*sizeof(Int32);
            for (int i=0;i<size;i++)
            {
                ar[i] = binr1.ReadInt32();
                binr1.BaseStream.Seek((size-1)*sizeof(Int32), SeekOrigin.Current);
            }
            binr1.Close();
            binr1.Dispose();
            str1.DiscardBufferedData();
            str1.Close(); 
        }

        void get_row(int[] ar, int ind)
        {
            StreamReader str1 = new StreamReader("matrix.txt");
            BinaryReader binr1 = new BinaryReader(str1.BaseStream);
            binr1.BaseStream.Position = ind * sizeof(int) * size; ;
            for (int i = 0; i < size; i++)
            {
                ar[i] = binr1.ReadInt32();
            }
            binr1.Close();
            binr1.Dispose();
            str1.DiscardBufferedData();
            str1.Close();
        }

        void do_1()
        {
            for (int d = 0; d < 20; d++)
            {
                Stopwatch StopwatchArray = new Stopwatch();
                int[] part_of_matrix = new int[size];
                int[] res1 = new int[size];



                #region ReadingArray
                int[] array = new int[size];
                StreamReader str = new StreamReader("array.txt");
                BinaryReader binr = new BinaryReader(str.BaseStream);
                int k = 0;
                while (binr.PeekChar() > -1)
                {

                    array[k] = binr.ReadInt32();
                    k++;
                }
                binr.Close();
                str.DiscardBufferedData();
                str.Close();
                #endregion

                StopwatchArray.Start();
                for (int i = 0; i < size; i++)
                {
                    res1[i] = 0;
                    get_column(part_of_matrix, i);
                    for (int j = 0; j < size; j++)
                    {
                        res1[i] += array[j] * part_of_matrix[j];
                    }
                    Array.Clear(part_of_matrix, 0, size);
                }
                StopwatchArray.Stop();
                TimeSpan time1 = StopwatchArray.Elapsed;
                StopwatchArray.Reset();
                string elapsedTime1 = Convert.ToString(time1.Milliseconds);
                Action action = () => richTextBox2.Text += ("" + ": time for array on matrix: " + elapsedTime1 + Environment.NewLine + "");
                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
                Array.Clear(res1, 0, size);
                Action action2 = () => progressBar1.Value++;
                if (InvokeRequired)
                    Invoke(action2);
                else
                    action2();
            }
            
        }

        void do_2()
        {
            for (int d = 0; d < 20; d++)
            {
                Stopwatch StopwatchMatrix = new Stopwatch();
                int[] part_of_matrix = new int[size];
                int[] res2 = new int[size];


                #region ReadingArray
                int[] array = new int[size];
                StreamReader str = new StreamReader("array.txt");
                BinaryReader binr = new BinaryReader(str.BaseStream);
                int k = 0;
                while (binr.PeekChar() > -1)
                {

                    array[k] = binr.ReadInt32();
                    k++;
                }
                binr.Close();
                str.DiscardBufferedData();
                str.Close();
                #endregion

                StopwatchMatrix.Start();
                {
                    for (int i = 0; i < size; i++)
                    {
                        res2[i] = 0;
                        get_row(part_of_matrix, i);
                        for (int j = 0; j < size; j++)
                        {
                            res2[i] += part_of_matrix[j] * array[j];
                        }
                        Array.Clear(part_of_matrix, 0, size);
                    }
                }
                StopwatchMatrix.Stop();
                TimeSpan time2 = StopwatchMatrix.Elapsed;
                StopwatchMatrix.Reset();
                string elapsedTime2 = Convert.ToString(time2.Milliseconds);
                Action action = () => richTextBox1.Text += ("" + ": time for matrix on array: " + elapsedTime2 + Environment.NewLine + "");
                if (InvokeRequired)
                    Invoke(action);
                else
                    action();
                Array.Clear(res2, 0, size);
                Action action2 = () => progressBar2.Value++;
                if (InvokeRequired)
                    Invoke(action2);
                else
                    action2();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread mult = new Thread(do_1);
            mult.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread mult1 = new Thread(do_2);
            mult1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Filling
            int[] arraywrite = new int[size];
            for (int i = 0; i < size; i++)
            {
                arraywrite[i] = rnd.Next() % 3 - 1;
            }
            int[,] matrix = new int[size, size];
            StreamWriter streamWriter = new StreamWriter("array.txt");
            BinaryWriter binaryWriter = new BinaryWriter(streamWriter.BaseStream);
            for (int i = 0; i < size; i++)
            {
                binaryWriter.Write(arraywrite[i]);
            }
            binaryWriter.Close();
            BinaryWriter fs_matrix = new BinaryWriter(File.Open("matrix.txt", FileMode.Open));
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = rnd1.Next() % 3 - 1;
                    fs_matrix.Write(matrix[i, j]);
                }
            }
            fs_matrix.Close();
            #endregion
        }
    }
}
