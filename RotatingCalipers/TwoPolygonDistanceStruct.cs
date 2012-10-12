using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    /// <summary>
    /// Struct to help hold min and max distances as well as points that represent them. variou algorithms use this
    /// </summary>
    class TwoPolygonDistanceStruct
    {
        public double MaxDistance { get; set; }
        public double MinDistance { get; set; }
        public PointF MaxP1 { get; set; }
        public PointF MaxP2 { get; set; }
        public PointF MinP1 { get; set; }
        public PointF MinP2 { get; set; }

        public TwoPolygonDistanceStruct(PointF P1, PointF P2, double MaxDistance, PointF P3, PointF P4, double MinDistance)
        {
            this.MaxP1 = P1;
            this.MaxP2 = P2;
            this.MinP1 = P3;
            this.MinP2 = P4;
            this.MaxDistance = MaxDistance;
            this.MinDistance = MinDistance;
        }
    }
}
