using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    public class Polygon
    {
        public List<PointF> points = new List<PointF>(); //all the points
        private int currentPointIndex = -1; // index number into the last point entered

        //is this polygon closed?
        public bool CompletePolygon { get; set; }

        //previous point
        public PointF PreviousPoint
        {
            get
            {
                return points[currentPointIndex - 1];
            }
        }

        //Current Point
        public PointF CurrentPoint
        {
            get
            {
                return points[currentPointIndex];
            }
        }

        //First added point to close polygon
        public PointF FirstPoint
        {
            get
            {
                return points[0];
            }
        }

        //total number of points
        public int PointsInPolygon
        {
            get
            {
                return points.Count;
            }
        }

        //Safe way to increment to the next point in the polygon
        public int IncrementIndex(int currIndex)
        {
            if ((currIndex + 1) == points.Count)
            {
                return 0;
            }
            else 
                return (currIndex + 1);
        }

        //Safe way to decrement
        public int DecrementIndex(int currIndex)
        {
            if (currIndex == 0)
            {
                return (points.Count - 1);
            }
            else
                return (currIndex - 1);
        }

        //largest diamater
        public double LargestDiameter { get; set; }

        //index of the max and min y points
        public Point MinYPoint { get; set; }
        public Point MaxYPoint { get; set; }
        public int indexMinYPoint { get; set; }
        public int indexMaxYPoint { get; set; }
        private int minYValue = int.MaxValue;
        private int maxYValue = int.MinValue;

        //index of the max and min X points
        public Point MinXPoint { get; set; }
        public Point MaxXPoint { get; set; }
        public int indexMinXPoint { get; set; }
        public int indexMaxXPoint { get; set; }
        private int minXValue = int.MaxValue;
        private int maxXValue = int.MinValue;

        public Polygon()
        {
            
        }

        //Clear out everything for the polygon
        public void ClearPolygon()
        {
            points.Clear();
            this.CompletePolygon = false;
            currentPointIndex = -1;
            maxYValue = int.MinValue;
            minYValue = int.MaxValue;
            minXValue = int.MaxValue;
            maxXValue = int.MinValue;
        }

        //simply add a point
        public bool AddPoint(Point pointToBeAdded)
        {
            //The form will set this to true when the user clicks
            //close polygon
            if (!this.CompletePolygon)
            {
                points.Add(pointToBeAdded);
                currentPointIndex++;

                //The Y coordinate system in C# is top to bottom 0 to max

                //is this the min or max y value?
                if (pointToBeAdded.Y < minYValue)
                {
                    MaxYPoint = pointToBeAdded;
                    minYValue = pointToBeAdded.Y; 
                    indexMaxYPoint = currentPointIndex;
                }
                if (pointToBeAdded.Y > maxYValue)
                {
                    MinYPoint = pointToBeAdded;
                    maxYValue = pointToBeAdded.Y;
                    indexMinYPoint = currentPointIndex;
                    
                }

                //is this the min or max x value?
                if (pointToBeAdded.X < minXValue)
                {
                    MinXPoint = pointToBeAdded;
                    minXValue = pointToBeAdded.X;
                    indexMinXPoint = currentPointIndex;
                }
                if (pointToBeAdded.X > maxXValue)
                {
                    MaxXPoint = pointToBeAdded;
                    maxXValue = pointToBeAdded.X;
                    indexMaxXPoint = currentPointIndex;
                }

                return true;
            }
            return false;
        }

    }
}
