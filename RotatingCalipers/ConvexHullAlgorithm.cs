using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace RotatingCalipers
{
    public class ConvexHullAlgorithm : CalipersBaseClass
    {
        //Convex hull 
        private ConvexHullBridge convexHullBridge = new ConvexHullBridge();

        public ConvexHullAlgorithm(Form1 form)
            : base(form)
        {
        }

        public override void StartAlgorithm()
        {
            form.mergeConvexHullButton.Enabled = false;
            form.maxDist2PolyButton.Enabled = false;
            form.intersectionButton.Enabled = false;
            form.continueButton.Enabled = false;
            form.nextButton.Enabled = true;

            InitialLines();

            //Is one of them flush with an edge?
            //Co-Podal pair

            if (angleA == 0 || angleB == 0)
            {
                //check if P-1, P+1, Q-1 and Q+1 are to the right of the line
                //P to Q
                double pm1 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.DecrementIndex(indexOne)]);
                double pm2 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);

                double qm1 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.DecrementIndex(indexTwo)]);
                double qm2 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.IncrementIndex(indexTwo)]);

                //all must be negative or positive
                if ((pm1 > 0 && pm2 > 0 && qm1 > 0 && qm2 > 0) || (pm1 < 0 && pm2 < 0 && qm1 < 0 && qm2 < 0))
                {
                    convexHullBridge.BridgePoints.Add(CaliperA.CenterPoint);
                    convexHullBridge.BridgePoints.Add(CaliperB.CenterPoint);
                    form.g.DrawLine(Utility.blackPen, CaliperA.CenterPoint, CaliperB.CenterPoint);
                }


            }
        }

        /// <summary>
        /// Ugly code for the part that drawsthe merged hull
        /// Basically want to walk around the bridge starting at the highest Y point.
        /// Should be rewritten
        /// </summary>
        public override void NextButton()
        {
            if (TotalRotatedAngleAmount < 360)
            {
                RotateCalipers();
                DoWork();
            }
            else
            {
                form.nextButton.Enabled = false;
                form.mergeConvexHullButton.Enabled = false;

                form.g.Clear(Color.White);
                Console.WriteLine(convexHullBridge);

                form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
                form.g.DrawPolygon(Utility.bluePen, form.secondPolygon.points.ToArray());

                Thread.Sleep(2000);


                for (int i = 0; i < convexHullBridge.BridgePoints.Count; i = i + 2)
                {
                    form.g.DrawLine(Utility.blackPen, convexHullBridge.BridgePoints[i], convexHullBridge.BridgePoints[i + 1]);
                }

                Thread.Sleep(2000);

                PointF startPoint;
                PointF currentPoint;

                Polygon currentPolygon; //which polygon are we looking at
                int currentIndex = 0; //the index of the polygon we are at could first or second polygon
                int bridgePointIndex = 0;

                if (form.firstPolygon.MaxYPoint.Y < form.secondPolygon.MaxYPoint.Y)
                {
                    startPoint = form.firstPolygon.MaxYPoint;
                    currentIndex = form.firstPolygon.points.IndexOf(startPoint);
                    currentPolygon = form.firstPolygon;
                   // MessageBox.Show("First polygon starts");
                }
                else
                {
                    startPoint = form.secondPolygon.MaxYPoint;
                    currentIndex = form.secondPolygon.points.IndexOf(startPoint);
                    currentPolygon = form.secondPolygon;
                    //MessageBox.Show("secondPolygon starts");
                    
                    //Handles the case where the first point is on Y but is also a bridg epoint
                    //in this case lets strat at the bridge point on P instgead of on Q
                    if (convexHullBridge.BridgePoints.Contains(startPoint))
                    {
                        bridgePointIndex = convexHullBridge.BridgePoints.IndexOf(startPoint);
                        startPoint = convexHullBridge.BridgePoints[bridgePointIndex - 1];
                        currentIndex = form.firstPolygon.points.IndexOf(startPoint);
                        currentPolygon = form.firstPolygon;
                    }
                }

                form.g.DrawEllipse(Utility.redPen, startPoint.X - 5, startPoint.Y - 5, 10, 10);
                Thread.Sleep(1000);
                currentPoint = startPoint;

                //are we checking points on Q or checking points on P
                do
                {
                    //check if the point alreayd exists so we dont throw an exceptoin
                    if(!convexHullBridge.ConvexHull.Contains(currentPoint))
                    {
                        //add the current point
                        convexHullBridge.ConvexHull.Add(currentPoint);
                        //MessageBox.Show("Adding point to convex hull");
                    }

                    //now lets check if this point is a bridge point
                    if (convexHullBridge.BridgePoints.Contains(currentPoint))
                    {
                        //MessageBox.Show("Bridge Point");
                        bridgePointIndex = convexHullBridge.BridgePoints.IndexOf(currentPoint);

                        //this is a bridge point which means we have to add this bridge point and the next one
                        //either p then q or q then p
                        //the bridge Points list contains them in P then Q order
                        //also chnged the curent index to the correct one the second oplygon
                        if (currentPolygon == form.firstPolygon)
                        {
                           // MessageBox.Show("Bridge point firstpolygon ");
                            //check if the point alreayd exists so we dont throw an exceptoin
                            if (!convexHullBridge.ConvexHull.Contains(convexHullBridge.BridgePoints[bridgePointIndex + 1]))
                            {
                                //add the current point
                                convexHullBridge.ConvexHull.Add(convexHullBridge.BridgePoints[bridgePointIndex + 1]);
                                //MessageBox.Show("Adding point to convex hull");
                                form.g.DrawEllipse(Utility.redPen, convexHullBridge.BridgePoints[bridgePointIndex + 1].X - 5, convexHullBridge.BridgePoints[bridgePointIndex + 1].Y - 5, 10, 10);
                            }
                            currentPolygon = form.secondPolygon;
                            currentIndex = currentPolygon.points.IndexOf(convexHullBridge.BridgePoints[bridgePointIndex + 1]);
                        }
                        else
                        {
                            //MessageBox.Show("Bridge point second polygon ");
                            //check if the point alreayd exists so we dont throw an exceptoin
                            if (!convexHullBridge.ConvexHull.Contains(convexHullBridge.BridgePoints[bridgePointIndex - 1]))
                            {
                                //add the current point
                                convexHullBridge.ConvexHull.Add(convexHullBridge.BridgePoints[bridgePointIndex - 1]);
                                //MessageBox.Show("Adding point to convex hull");
                                form.g.DrawEllipse(Utility.redPen, convexHullBridge.BridgePoints[bridgePointIndex - 1].X - 5, convexHullBridge.BridgePoints[bridgePointIndex - 1].Y - 5, 10, 10);
                            }
                            currentPolygon = form.firstPolygon;
                            currentIndex = currentPolygon.points.IndexOf(convexHullBridge.BridgePoints[bridgePointIndex - 1]);
                        }
                        Thread.Sleep(1000);
                    }

                    form.g.DrawEllipse(Utility.redPen, currentPoint.X - 5, currentPoint.Y - 5, 10, 10);
                    Thread.Sleep(1000);

                    //incrment index. if it was ab ridge point we needto increment one past it anyway
                    currentIndex = currentPolygon.IncrementIndex(currentIndex);
                    //update the current point
                    currentPoint = currentPolygon.points[currentIndex];

                } while ( (currentPoint != startPoint));
                //MessageBox.Show("Done");

                foreach (PointF p in convexHullBridge.ConvexHull)
                {
                    form.g.DrawEllipse(Utility.blackPen, p.X - 5, p.Y - 5, 10, 10);
                }

                Thread.Sleep(2000);

                form.g.Clear(Color.White);
                form.g.DrawPolygon(Utility.blackPen, convexHullBridge.ConvexHull.ToArray());
                form.continueButton.Enabled = true;
                convexHullBridge.BridgePoints.Clear();
                convexHullBridge.ConvexHull.Clear();
            }
        }

        protected override void InitialLines()
        {
            //P is the max for polygon One
            //Q is the max for polygon Two
            indexOne = form.firstPolygon.indexMaxYPoint;
            indexTwo = form.secondPolygon.indexMaxYPoint;

            //Initialize them should be two horizontal lines
            DrawInitialSupportLines();

            //get the vectors associated with the initial caliper A (P)
            calAVector1 = new Vector(CaliperA.CenterPoint, CaliperA.P1);
            calAVector2 = new Vector(CaliperA.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);


            //Get the angle between them
            angleA = Utility.GetAngle(calAVector1, calAVector2);
            Console.WriteLine("Angle between first caliper is: {0}", angleA);

            //Get the vectors associated with the initial caliper B (Q)
            calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
            calBVector2 = new Vector(CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.IncrementIndex(indexTwo)]);

            angleB = Utility.GetAngle(calBVector1, calBVector2);
            Console.WriteLine("Angle between second caliper is: {0}", angleB);
        }

        protected override void DrawInitialSupportLines()
        {
            //P (poly 1) is min Q (poly 2)is max

            //first the max Y point for P
            form.g.DrawLine(Utility.redPen, form.firstPolygon.MaxYPoint.X, form.firstPolygon.MaxYPoint.Y, 999999, form.firstPolygon.MaxYPoint.Y);
            //now max y point to the left
            form.g.DrawLine(Utility.redPen, form.firstPolygon.MaxYPoint.X, form.firstPolygon.MaxYPoint.Y, -999999, form.firstPolygon.MaxYPoint.Y);
            CaliperA.CenterPoint = form.firstPolygon.MaxYPoint;
            CaliperA.P1 = new PointF(999999, form.firstPolygon.MaxYPoint.Y);
            CaliperA.P2 = new PointF(-999999, form.firstPolygon.MaxYPoint.Y);
            Console.WriteLine("CaliperA: {0}", CaliperA);

            //maxx Y point for Q
            form.g.DrawLine(Utility.greenPen, form.secondPolygon.MaxYPoint.X, form.secondPolygon.MaxYPoint.Y, 999999, form.secondPolygon.MaxYPoint.Y);
            //now max y point to secondPolygon left
            form.g.DrawLine(Utility.greenPen, form.secondPolygon.MaxYPoint.X, form.secondPolygon.MaxYPoint.Y, -999999, form.secondPolygon.MaxYPoint.Y);
            CaliperB.CenterPoint = form.secondPolygon.MaxYPoint;
            CaliperB.P1 = new PointF(999999, form.secondPolygon.MaxYPoint.Y);
            CaliperB.P2 = new PointF(-999999, form.secondPolygon.MaxYPoint.Y);
            Console.WriteLine("CaliperB: {0}", CaliperB);
        }

        protected override void NewCalipers(int choice)
        {
            PointF newPoint1 = new PointF();
            PointF newPoint2 = new PointF();

            form.g.Clear(Color.White);
            form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
            form.g.DrawPolygon(Utility.bluePen, form.secondPolygon.points.ToArray());

            //========================================================
            //Rotate A
            newPoint1.X = (float)(CaliperA.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.X - CaliperA.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.Y - CaliperA.CenterPoint.Y)));

            newPoint1.Y = (float)(CaliperA.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.X - CaliperA.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P1.Y - CaliperA.CenterPoint.Y)));

            newPoint2.X = (float)(CaliperA.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.X - CaliperA.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.Y - CaliperA.CenterPoint.Y)));

            newPoint2.Y = (float)(CaliperA.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.X - CaliperA.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperA.P2.Y - CaliperA.CenterPoint.Y)));

            CaliperA.P1 = newPoint1;
            CaliperA.P2 = newPoint2;

            //Only get new center if this is A asking
            if (choice == 1)
            {
                indexOne = form.firstPolygon.IncrementIndex(indexOne);
                CaliperA.CenterPoint = form.firstPolygon.points[indexOne];
            }

            form.g.DrawLine(Utility.redPen, CaliperA.CenterPoint, newPoint1);
            form.g.DrawLine(Utility.redPen, CaliperA.CenterPoint, newPoint2);


            //=================================================
            //Rotate B
            newPoint1.X = (float)(CaliperB.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.X - CaliperB.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.Y - CaliperB.CenterPoint.Y)));

            newPoint1.Y = (float)(CaliperB.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.X - CaliperB.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P1.Y - CaliperB.CenterPoint.Y)));

            newPoint2.X = (float)(CaliperB.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.X - CaliperB.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.Y - CaliperB.CenterPoint.Y)));

            newPoint2.Y = (float)(CaliperB.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.X - CaliperB.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperB.P2.Y - CaliperB.CenterPoint.Y)));

            CaliperB.P1 = newPoint1;
            CaliperB.P2 = newPoint2;

            //Only get new center of this is B asking
            if (choice == 2)
            {
                indexTwo = form.secondPolygon.IncrementIndex(indexTwo);
                CaliperB.CenterPoint = form.secondPolygon.points[indexTwo];
            }

            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P1);
            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P2);
        }

        protected override void RotateCalipers()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //A < B
            if (angleA < angleB)
            {
                Console.WriteLine("A < B");
                //NewCalipers for A < B
                minAngle = angleA;
                NewCalipers(1);
                //new angle B
                angleB = angleB - angleA;
                TotalRotatedAngleAmount += angleA;

                calAVector1 = new Vector(CaliperA.CenterPoint, CaliperA.P1);
                calAVector2 = new Vector(CaliperA.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);
                Console.WriteLine("CalAvector1: {0}", calAVector1);
                Console.WriteLine("CalAvector2: {0}", calAVector2);
                angleA = Utility.GetAngle(calAVector1, calAVector2);
            }
            //B < A
            else
            {
                Console.WriteLine("B < A");
                //New calipers for B < A
                minAngle = angleB;
                NewCalipers(2);
                //new angle A
                angleA = angleA - angleB;
                TotalRotatedAngleAmount += angleB;

                calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
                calBVector2 = new Vector(CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.IncrementIndex(indexTwo)]);
                Console.WriteLine("CalBvector1: {0}", calBVector1);
                Console.WriteLine("CalBvector2: {0}", calBVector2);
                angleB = Utility.GetAngle(calBVector1, calBVector2);
            }

            Console.WriteLine("CaliperA: {0}", CaliperA);
            Console.WriteLine("CaliperB: {0}", CaliperB);
            Console.WriteLine("Angle between first caliper is: {0}", angleA);
            Console.WriteLine("Angle between second caliper is: {0}", angleB);
            Console.WriteLine("Total rotated angle: {0}", TotalRotatedAngleAmount);
            form.totalRotatedAngleTextBox.Text = TotalRotatedAngleAmount.ToString();
        }

        protected override void DoWork()
        {
            double pm1 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.DecrementIndex(indexOne)]);
            double pm2 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);

            double qm1 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.DecrementIndex(indexTwo)]);
            double qm2 = Utility.PointToTheSide(CaliperA.CenterPoint, CaliperB.CenterPoint, form.secondPolygon.points[form.secondPolygon.IncrementIndex(indexTwo)]);

            //draw what we are currently looking at
            form.g.DrawLine(Utility.brownPen, CaliperA.CenterPoint, CaliperB.CenterPoint);

            //all must be negative or positive
            //convex hull point
            if ((pm1 > 0 && pm2 > 0 && qm1 > 0 && qm2 > 0) || (pm1 < 0 && pm2 < 0 && qm1 < 0 && qm2 < 0))
            {
                try
                {
                    convexHullBridge.BridgePoints.Add(CaliperA.CenterPoint);
                    convexHullBridge.BridgePoints.Add(CaliperB.CenterPoint);
                    form.g.DrawLine(Utility.blackPen, CaliperA.CenterPoint, CaliperB.CenterPoint);
                }
                catch(Exception)
                {
                    //dont care means point was alreayd added
                }
            }
        }
    }
}
