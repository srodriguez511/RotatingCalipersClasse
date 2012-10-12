//Steven Rodriguez
//0298456
//Rotating Calipers Algorithm
//http://cgm.cs.mcgill.ca/~orm/welcome.html for general algorithm rules see link

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RotatingCalipers
{
    public partial class Form1 : Form
    {
        #region Variables

        #region Properties
        //current x and y pos on the mouse
        public int Xpos { get; set; }
        public int Ypos { get; set; }
        #endregion


        //holds the current algorithm to run
        CalipersBaseClass cb;

        //First user entered polygon
        public Polygon firstPolygon = new Polygon();
        //Second user entered polygon
        public Polygon secondPolygon = new Polygon();
        //Which polygon are we on now 1 or 2
        //1 by default;
        public Polygon currentPolygon;
        //total created
        private static int totalPolygons = 0;

        // the graphics object for the picture box
        public System.Drawing.Graphics g;

        #endregion

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();

            //by default first is current
            currentPolygon = firstPolygon;
        }


        #region EventHandlers

        /// <summary>
        /// When the user clicks draw an ellipse and check if we need to draw a line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Point currentPoint = new Point(Xpos, Ypos);
            if (!currentPolygon.AddPoint(currentPoint))
            {
                //We may want to validate points later for now just ignore
            }
            else
            {
                //draw the curent point
                g.DrawEllipse(Utility.redPen, currentPoint.X, currentPoint.Y, 5, 2);

                if (currentPolygon.PointsInPolygon > 1)
                {
                    g.DrawLine(Utility.bluePen, currentPolygon.PreviousPoint, currentPolygon.CurrentPoint);
                }

                Console.WriteLine("Point: {0}", currentPoint);

                //Add it to the list box
                string s = "{" + Xpos + "," + Ypos + "}";

                if (currentPolygon == firstPolygon)
                {
                    this.polygonOnePoints.Items.Add(s);
                }
                else
                {
                    this.polygonTwoPoints.Items.Add(s);
                }
            }
        }

        /// <summary>
        /// Get the user mouse position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Xpos = e.X;
            Ypos = e.Y;
        }

        /// <summary>
        /// Remove the current algorithm but leave the polygons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueButton_Click(object sender, EventArgs e)
        {
            if (totalPolygons == 1)
            {
                g.Clear(Color.White);
                maxDiameterButton.Enabled = true;
                minAreaButton.Enabled = true;
                nextButton.Enabled = false;
                maxDist2PolyButton.Enabled = false;
                addPolygonButton.Enabled = false;
                ClosePolygonButton.Enabled = false;
                intersectionButton.Enabled = false;
                minimumPermButton.Enabled = true;
                minWidthButton.Enabled = true;

                CalipersBaseClass.TotalRotatedAngleAmount = 0;
                g.DrawPolygon(Utility.bluePen, firstPolygon.points.ToArray());

            }
            else if (totalPolygons == 2)
            {
                g.Clear(Color.White);

                maxDiameterButton.Enabled = false;
                minWidthButton.Enabled = false;
                minAreaButton.Enabled = false;
                nextButton.Enabled = false;
                maxDist2PolyButton.Enabled = true;
                addPolygonButton.Enabled = false;
                ClosePolygonButton.Enabled = false;
                minimumPermButton.Enabled = false;
                mergeConvexHullButton.Enabled = true;
                intersectionButton.Enabled = true;

                CalipersBaseClass.TotalRotatedAngleAmount = 0;
                g.DrawPolygon(Utility.bluePen, firstPolygon.points.ToArray());
                g.DrawPolygon(Utility.bluePen, secondPolygon.points.ToArray());
            }
        }

        /// <summary>
        /// The user clicked the next button determine what to do
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, EventArgs e)
        {
            cb.NextButton();
        }

        /// <summary>
        /// Closes the polygon by connecting the last points to the first point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosePolygonButton_Click(object sender, EventArgs e)
        {
            if (currentPolygon.PointsInPolygon < 3)
            {
                MessageBox.Show("You need more than 3 points", "Invalid Polygon");
            }
            else if (currentPolygon.PointsInPolygon > 1)
            {
                //Draw a line from the last point to the first point
                g.DrawLine(Utility.bluePen, currentPolygon.CurrentPoint, currentPolygon.FirstPoint);
                currentPolygon.CompletePolygon = true;
            }

            CalipersBaseClass.TotalRotatedAngleAmount = 0;
            totalRotatedAngleTextBox.Text = CalipersBaseClass.TotalRotatedAngleAmount.ToString();

            maxDiameterButton.Enabled = true;
            minAreaButton.Enabled = true;
            ClosePolygonButton.Enabled = false;
            minimumPermButton.Enabled = true;
            addPolygonButton.Enabled = true;
            minWidthButton.Enabled = true;
            totalPolygons++;

            //operations not  allowed for two polygons
            if (totalPolygons >= 2)
            {
                addPolygonButton.Enabled = false;
                maxDiameterButton.Enabled = false;
                minAreaButton.Enabled = false;
                maxDist2PolyButton.Enabled = true;
                mergeConvexHullButton.Enabled = true;
                minimumPermButton.Enabled = false;
                intersectionButton.Enabled = true;
                minWidthButton.Enabled = false;
            }
        }


        /// <summary>
        /// Add a second polygon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPolygonButton_Click(object sender, EventArgs e)
        {
            minimumPermButton.Enabled = false;
            maxDiameterButton.Enabled = false;
            minAreaButton.Enabled = false;
            nextButton.Enabled = false;
            minWidthButton.Enabled = false;

            ClosePolygonButton.Enabled = true;

            currentPolygon = secondPolygon;
        }

        /// <summary>
        /// Clears the screen and empties the points list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            currentPolygon = firstPolygon;
            g.Clear(Color.White);

            polygonOnePoints.Items.Clear();
            polygonTwoPoints.Items.Clear();
            secondPolygon.ClearPolygon();
            firstPolygon.ClearPolygon();
            totalPolygons = 0;

            ClosePolygonButton.Enabled = true;
            maxDiameterButton.Enabled = false;
            nextButton.Enabled = false;
            addPolygonButton.Enabled = false;
            minAreaButton.Enabled = false;
            minimumPermButton.Enabled = false;
            mergeConvexHullButton.Enabled = false;
            maxDist2PolyButton.Enabled = false;
            intersectionButton.Enabled = false;
            minWidthButton.Enabled = false;

            CalipersBaseClass.TotalRotatedAngleAmount = 0;
            totalRotatedAngleTextBox.Text = CalipersBaseClass.TotalRotatedAngleAmount.ToString();
        }

        /// <summary>
        /// Computes the maximum diameter in O(n) time using Shamox rotating calipers
        /// Use max and min Y values as first pair of support lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maxDiameterButton_Click(object sender, EventArgs e)
        {
            cb = new DiametersAlgorithm(this);
            cb.StartAlgorithm();
        }


        /// <summary>
        /// Max distance between two two polygons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maxDist2PolyButton_Click(object sender, EventArgs e)
        {
            cb = new MaxDistance2PAlgorithm(this);
            cb.StartAlgorithm();
        }

        /// <summary>
        /// Merge the convex hull of the two polygons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mergeConvexHullButton_Click(object sender, EventArgs e)
        {
            cb = new ConvexHullAlgorithm(this);
            cb.StartAlgorithm();
        }

        /// <summary>
        /// Smallest Rectangle event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smallestRectangleButton_Click(object sender, EventArgs e)
        {
            cb = new MinimumAreaAlgorithm(this);
            cb.StartAlgorithm();
        }

        /// <summary>
        /// Smllest Perimeter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minimumPermButton_Click(object sender, EventArgs e)
        {
            cb = new MinimumPerimeterAlgorithm(this);
            cb.StartAlgorithm();
        }

        /// <summary>
        /// Intersection of two convex polygons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void intersectionButton_Click(object sender, EventArgs e)
        {
            cb = new IntersectionAlgorithm(this);
            cb.StartAlgorithm();
        }

        /// <summary>
        /// Minimum Width Algorithm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minWidthButton_Click(object sender, EventArgs e)
        {
            cb = new MinimumWidthAlgorithm(this);
            cb.StartAlgorithm();
        }

        #endregion
    }
}
