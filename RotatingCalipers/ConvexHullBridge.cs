using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{

    /// <summary>
    /// Holds the bridge points possible when merging the convex hull
    /// if disjoint only 2
    /// if interesect more
    /// </summary>
    class ConvexHullBridge
    {
        public List<PointF> BridgePoints = new List<PointF>();
        public List<PointF> ConvexHull = new List<PointF>();

        public ConvexHullBridge()
        {

        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("\n Convex Hull Bridge Points: \n");
            if (BridgePoints != null)
            {
                foreach (PointF p in BridgePoints)
                {
                    s.Append("Point: " + p + " \n");
                }

                s.Append("Convex Hull \n");

                foreach (PointF p in ConvexHull)
                {
                    s.Append("Point: " + p + " \n");
                }
            }
            return s.ToString();
        }
    }
}
