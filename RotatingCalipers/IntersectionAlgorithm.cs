using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace RotatingCalipers
{
    /// <summary>
    /// Essentially just go around and grab the bridge points
    /// The processing of the intersection happens after the rotating calipers is complete
    /// </summary>
    public class IntersectionAlgorithm : CalipersBaseClass
    {
        private List<PointF> BridgePoints = new List<PointF>();
        private List<PointF> beyondIntersectionPointList = new List<PointF>(); //contains the points that make up the point just outside the intersection point. basically tells us where to stop when going around to make up
        //the intersecting polygon
        private Dictionary<PointF, int> IntersectionPoints = new Dictionary<PointF, int>();
        private List<PointF> FinalPolygon = new List<PointF>();
        public bool PToQ { get; set; } // Determine if the bridge point is connecting P to Q or Q to P only need to know this for the first bridge point added
        private bool firstTime = true;
        private int startPorQ = 0; //1 = P 2 = Q where do we start building the intesrction polygon

        public IntersectionAlgorithm(Form1 form)
            : base(form)
        {
        }

        public override void StartAlgorithm()
        {
            form.mergeConvexHullButton.Enabled = false;
            form.maxDist2PolyButton.Enabled = false;
            form.intersectionButton.Enabled = false;
            form.nextButton.Enabled = true;
            form.continueButton.Enabled = false;

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
                    BridgePoints.Add(CaliperA.CenterPoint);
                    BridgePoints.Add(CaliperB.CenterPoint);
                    form.g.DrawLine(Utility.blackPen, CaliperA.CenterPoint, CaliperB.CenterPoint);
                }

            }
        }

        public override void NextButton()
        {
            if (TotalRotatedAngleAmount < 360)
            {
                RotateCalipers();
                DoWork();
            }
            //Now we have all the bridge points the intersection work can begin
            else
            {

                //First drwa teh polygon and the bridge points
                form.nextButton.Enabled = false;
                form.mergeConvexHullButton.Enabled = false;

                form.g.Clear(Color.White);

                form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
                form.g.DrawPolygon(Utility.bluePen, form.secondPolygon.points.ToArray());

                Thread.Sleep(2000);


                for (int i = 0; i < BridgePoints.Count; i = i + 2)
                {
                    form.g.DrawLine(Utility.blackPen, BridgePoints[i], BridgePoints[i + 1]);
                }

                Thread.Sleep(2000);


                //Ugly way of doing this. We have to determine if we are going from P to Q or Q to P. This alternates the algorithm
                //it assumes that the points for Q go CCW but we have them as CW. Therefore depending on P to Q or Q to P we have 
                //to decrement or increment accordingly

                //==================Process the intersection
                //Algorithm to find the intersection from T.
                /*i <- 1; j <- 1;
                repeat 
                    finished <- true; 
                    while ( leftTurn( p(i), p(i+1), q(j+1) ) ) do 
                    begin 
                        j <- j + 1; 
                        finished <- false; 
                    end 
                    while ( rightTurn( q(j), q(j+1), p(i+1) ) ) do 
                    begin 
                        i <- i + 1; 
                        finished <- false; 
                    end 
                until finished*/

                //for all the bridge points need to find the intersection point
                for (int i = 0; i < BridgePoints.Count; i = i + 2)
                {

                    if (PToQ)
                    {
                        //Bridge points are always add P then Q
                        //Find these points in the P and Q points
                        int iIndex = form.firstPolygon.points.IndexOf(BridgePoints[i]);
                        int iIndexPlusOne = form.firstPolygon.IncrementIndex(iIndex);
                        int jIndex = form.secondPolygon.points.IndexOf(BridgePoints[i + 1]);
                        int jIndexPlusOne = form.secondPolygon.DecrementIndex(jIndex);

                        bool finished = false;

                        while (!finished)
                        {
                            finished = true;
                            //left turn part on P
                            while (Utility.WhichSide(form.firstPolygon.points[iIndex], form.firstPolygon.points[iIndexPlusOne],
                                form.secondPolygon.points[jIndexPlusOne]) == -1)
                            {
                                jIndex = form.secondPolygon.DecrementIndex(jIndex);
                                jIndexPlusOne = form.secondPolygon.DecrementIndex(jIndex);
                                finished = false;
                            }
                            //right turn part on Q
                            while (Utility.WhichSide(form.secondPolygon.points[jIndex], form.secondPolygon.points[jIndexPlusOne],
                                form.firstPolygon.points[iIndexPlusOne]) == 1)
                            {
                                iIndex = form.firstPolygon.IncrementIndex(iIndex);
                                iIndexPlusOne = form.firstPolygon.IncrementIndex(iIndex);
                                finished = false;
                            }

                        }

                        //form.g.DrawEllipse(Utility.blackPen, form.firstPolygon.points[iIndex].X - 5, form.firstPolygon.points[iIndex].Y - 5, 10, 10);
                        //form.g.DrawEllipse(Utility.greenPen, form.secondPolygon.points[jIndex].X - 5, form.secondPolygon.points[jIndex].Y - 5, 10, 10);
                        //form.g.DrawEllipse(Utility.blackPen, form.firstPolygon.points[form.firstPolygon.IncrementIndex(iIndex)].X - 5, form.firstPolygon.points[form.firstPolygon.IncrementIndex(iIndex)].Y - 5,
                        //    10, 10);
                        //form.g.DrawEllipse(Utility.blackPen, form.secondPolygon.points[form.secondPolygon.DecrementIndex(jIndex)].X - 5, form.secondPolygon.points[form.secondPolygon.DecrementIndex(jIndex)].Y - 5,
                        //        10, 10);

                        PToQ = false;

                        PointF intersectionPoint = Utility.IntersectionPoint(form.firstPolygon.points[iIndex], form.firstPolygon.points[form.firstPolygon.IncrementIndex(iIndex)],
                            form.secondPolygon.points[jIndex], form.secondPolygon.points[form.secondPolygon.DecrementIndex(jIndex)]);

                        form.g.DrawEllipse(Utility.redPen, intersectionPoint.X, intersectionPoint.Y, 10, 10);
                        if (!IntersectionPoints.ContainsKey(intersectionPoint))
                        {
                            IntersectionPoints.Add(intersectionPoint, form.firstPolygon.IncrementIndex(iIndex)); //index of where the first point of the P starts
                        }
                        if (startPorQ == 0)
                        {
                            startPorQ = 1;
                        }
                        if (!beyondIntersectionPointList.Contains(form.secondPolygon.points[jIndex]))
                        {
                            beyondIntersectionPointList.Add(form.secondPolygon.points[jIndex]);
                        }
                    }
                    else
                    {
                        //flip the algorithm
                        //Bridge points are always add P then Q
                        //Find these points in the P and Q points
                        int iIndex = form.firstPolygon.points.IndexOf(BridgePoints[i]);
                        int iIndexPlusOne = form.firstPolygon.DecrementIndex(iIndex);
                        int jIndex = form.secondPolygon.points.IndexOf(BridgePoints[i + 1]);
                        int jIndexPlusOne = form.secondPolygon.IncrementIndex(jIndex);

                        bool finished = false;

                        while (!finished)
                        {
                            finished = true;
                            //left turn part on P
                            while (Utility.WhichSide(form.firstPolygon.points[iIndex], form.firstPolygon.points[iIndexPlusOne],
                                form.secondPolygon.points[jIndexPlusOne]) == 1)
                            {
                                jIndex = form.secondPolygon.IncrementIndex(jIndex);
                                jIndexPlusOne = form.secondPolygon.IncrementIndex(jIndex);
                                finished = false;
                            }
                            //right turn part on Q
                            while (Utility.WhichSide(form.secondPolygon.points[jIndex], form.secondPolygon.points[jIndexPlusOne],
                                form.firstPolygon.points[iIndexPlusOne]) == -1)
                            {
                                iIndex = form.firstPolygon.DecrementIndex(iIndex);
                                iIndexPlusOne = form.firstPolygon.DecrementIndex(iIndex);
                                finished = false;
                            }

                        }

                        //form.g.DrawEllipse(Utility.bluePen, form.firstPolygon.points[iIndex].X - 5, form.firstPolygon.points[iIndex].Y - 5, 10, 10);
                        //form.g.DrawEllipse(Utility.brownPen, form.secondPolygon.points[jIndex].X - 5, form.secondPolygon.points[jIndex].Y - 5, 10, 10);
                        //form.g.DrawEllipse(Utility.brownPen, form.firstPolygon.points[form.firstPolygon.DecrementIndex(iIndex)].X - 5, form.firstPolygon.points[form.firstPolygon.DecrementIndex(iIndex)].Y - 5,
                        //    10, 10);
                        //form.g.DrawEllipse(Utility.brownPen, form.secondPolygon.points[form.secondPolygon.IncrementIndex(jIndex)].X - 5, form.secondPolygon.points[form.secondPolygon.IncrementIndex(jIndex)].Y - 5,
                        //        10, 10);

                        PToQ = true;

                        PointF intersectionPoint = Utility.IntersectionPoint(form.firstPolygon.points[iIndex], form.firstPolygon.points[form.firstPolygon.DecrementIndex(iIndex)],
                            form.secondPolygon.points[jIndex], form.secondPolygon.points[form.secondPolygon.IncrementIndex(jIndex)]);

                        form.g.DrawEllipse(Utility.redPen, intersectionPoint.X, intersectionPoint.Y, 10, 10);
                        if (!IntersectionPoints.ContainsKey(intersectionPoint))
                        {
                            IntersectionPoints.Add(intersectionPoint, form.secondPolygon.IncrementIndex(jIndex));//index of where the first point of the P starts
                        }
                        if (startPorQ == 0)
                        {
                            startPorQ = 2;
                        }
                        if (!beyondIntersectionPointList.Contains(form.firstPolygon.points[iIndex]))
                        {
                            beyondIntersectionPointList.Add(form.firstPolygon.points[iIndex]);
                        }
                    }
                }

                DetermineIntersectingPolygon();


                form.g.Clear(Color.White);
                form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
                form.g.DrawPolygon(Utility.bluePen, form.secondPolygon.points.ToArray());
                form.g.DrawPolygon(Utility.redPen, FinalPolygon.ToArray());

                form.continueButton.Enabled = true;

                //Clear the points
                BridgePoints.Clear();
            }
        }

        /// <summary>
        /// Now we hvae all the intersection points build the intersection polygon
        /// </summary>
        private void DetermineIntersectingPolygon()
        {
            Thread.Sleep(3000);
            form.g.Clear(Color.White);
            form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());
            form.g.DrawPolygon(Utility.bluePen, form.secondPolygon.points.ToArray());

            //Must go over every intersection point
            foreach (KeyValuePair<PointF, int> pair in IntersectionPoints)
            {
                //add this point to the final polygon list
                FinalPolygon.Add(pair.Key);
                form.g.DrawEllipse(Utility.redPen, pair.Key.X - 5, pair.Key.Y - 5, 10, 10);
                int index = pair.Value;
                //do we add points in p or points in q?
                if (startPorQ == 1) //p
                {
                    startPorQ = 2;
                    while(true)
                    {
                        //is the point we are supposedto start on an intersection point?
                        form.g.DrawEllipse(Utility.bluePen, form.firstPolygon.points[index].X - 5, form.firstPolygon.points[index].Y - 5, 10, 10);
                        if (!beyondIntersectionPointList.Contains(form.firstPolygon.points[index])) //check that the point we are curretnly on
                            //is not part of the list of pointstaht extend beyound the interior polygon
                        {
                            FinalPolygon.Add(form.firstPolygon.points[index]);
                            form.g.DrawEllipse(Utility.redPen, form.firstPolygon.points[index].X - 5, form.firstPolygon.points[index].Y - 5, 10, 10);
                            index = form.firstPolygon.IncrementIndex(index);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else // Q
                {
                    startPorQ = 1;
                    while(true)
                    {
                        //is the point we are supposedto start on an intersection point?
                        form.g.DrawEllipse(Utility.bluePen, form.secondPolygon.points[index].X - 5, form.secondPolygon.points[index].Y - 5, 10, 10);
                        if (!beyondIntersectionPointList.Contains(form.secondPolygon.points[index]))
                        {
                            FinalPolygon.Add(form.secondPolygon.points[index]);
                            form.g.DrawEllipse(Utility.redPen, form.secondPolygon.points[index].X - 5, form.secondPolygon.points[index].Y - 5, 10, 10);
                            index = form.secondPolygon.IncrementIndex(index);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            form.g.DrawPolygon(Utility.blackPen, FinalPolygon.ToArray());
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
                //The first bridge point is P connecting Q or Q connecting P this helps us with the intersection algorithm to flip the decrement increment
                if (firstTime)
                {
                    if (pm1 > 0)
                        PToQ = true;
                    else
                        PToQ = false;

                    firstTime = false;
                }

                BridgePoints.Add(CaliperA.CenterPoint);
                BridgePoints.Add(CaliperB.CenterPoint);
                form.g.DrawLine(Utility.blackPen, CaliperA.CenterPoint, CaliperB.CenterPoint);

            }
        }
    }
}
