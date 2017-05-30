using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NearestNeighborSearch;

namespace Main
{
    public partial class Form1 : Form
    {
        private KdTreeTest2D kdTest;
        private IPoint[] searchPoints = null;
        private Bitmap backgroundBuffer;
        private Graphics backgroundGrx;

        public Form1()
        {
            InitializeComponent();

            //MessageBox.Show(KdSplitFunctionsTest.StartAllTests());
            //MessageBox.Show(SortedArrayTest.RunAllTests());
            //MessageBox.Show(PriorityQueueTest.RunAllTests());

            //string result = PointSearchTest.TestAllMethods(new int[] { this.Width, this.Height/*, this.Height*/ }, new int[] { 1000, 5000, 10000 }, 100, 10, 10);
            //MessageBox.Show(result);

            CreateNewRound();
            MyPaint();
        }

        private void CreateNewRound()
        {
            this.kdTest = new KdTreeTest2D(100000, this.Width, this.Height);
            this.backgroundBuffer = new Bitmap(this.Width, this.Height);
            this.backgroundGrx = Graphics.FromImage(this.backgroundBuffer);
        }

        private void MyPaint()
        {
            this.backgroundGrx.DrawImage(this.kdTest.GetImage(), 0, 0);

            if (this.searchPoints != null)
            {
                foreach (var p in this.searchPoints)
                {
                    this.backgroundGrx.DrawEllipse(Pens.Red, p[0] - 2, p[1] - 2, 4, 4);
                }
            }

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(this.backgroundBuffer, 0, 0);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DateTime start = DateTime.Now;
                //this.searchPoints = this.kdTest.ApproximateKNearestNeighborSearch(e.X, e.Y, 5);
                this.searchPoints = this.kdTest.PriorityKNearestNeighborSearch(e.X, e.Y, 5);
                //this.searchPoints = this.kdTest.FixedRadiusSearchForKNearestNeighbors(e.X, e.Y, 10, 5);
                DateTime stop = DateTime.Now;
                this.Text = ((stop - start).TotalMilliseconds).ToString();

                MyPaint();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.searchPoints = null;
                MyPaint();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                CreateNewRound();
                MyPaint();
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Form1_MouseClick(sender, e);
        }



    }
}
