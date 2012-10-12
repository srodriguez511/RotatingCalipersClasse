using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    class MinimumAreaAlgorithm : CalipersBaseClass
    {
        private double TempArea { get; set; }
        private RectangleStruct AreaStruct;

        public MinimumAreaAlgorithm(Form1 form)
            : base(form)
        {
        }

        public override void StartAlgorithm()
        {
            form.nextButton.Enabled = true;

            form.minAreaButton.Enabled = false;
            form.maxDiameterButton.Enabled = false;
            form.minimumPermButton.Enabled = false;
            form.addPolygonButton.Enabled = false;
            form.minWidthButton.Enabled = false;
            form.continueButton.Enabled = false;

            //Get the initial angles
            InitialLines();

            TempArea = Utility.Area(CaliperA, CaliperB, CaliperC, CaliperD);
            AreaStruct = new RectangleStruct(CaliperA, CaliperB, CaliperC, CaliperD, TempArea);

            form.currentAreaTextBox.Text = AreaStruct.MinimumArea.ToString();
            form.minAreaTextBox.Text = AreaStruct.MinimumArea.ToString();

            Console.WriteLine("MinArea: {0}", AreaStruct.MinimumArea.ToString());
        }

        protected override void InitialLines()
        {
            indexOne = form.firstPolygon.indexMaxYPoint;
            indexTwo = form.firstPolygon.indexMinYPoint;
            indexThree = form.firstPolygon.indexMaxXPoint;
            indexFour = form.firstPolygon.indexMinXPoint;

            //Initialize them to the top and bottom horizontal lines
            DrawInitialSupportLines();

            //====================================

            //get the vectors associated with the initial caliper A
            calAVector1 = new Vector(CaliperA.CenterPoint, CaliperA.P1);
            calAVector2 = new Vector(CaliperA.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);


            //Get the angle between them
            angleA = Utility.GetAngle(calAVector1, calAVector2);
            Console.WriteLine("Angle between first caliper Y is: {0}", angleA);

            //Get the vectors associated with the initial caliper B
            calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
            calBVector2 = new Vector(CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexTwo)]);

            angleB = Utility.GetAngle(calBVector1, calBVector2);
            Console.WriteLine("Angle between second caliper Y is: {0}", angleB);

            //Get the vectors associated with the initial caliper C
            calCVector1 = new Vector(CaliperC.CenterPoint, CaliperC.P1);
            calCVector2 = new Vector(CaliperC.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexThree)]);

            angleC = Utility.GetAngle(calCVector1, calCVector2);
            Console.WriteLine("Angle between first caliper X is: {0}", angleC);

            //Get the vectors associated with the initial caliper D
            calDVector1 = new Vector(CaliperD.CenterPoint, CaliperD.P1);
            calDVector2 = new Vector(CaliperD.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexFour)]);

            angleD = Utility.GetAngle(calDVector1, calDVector2);
            Console.WriteLine("Angle between second caliper X is: {0}", angleD);
        }

        protected override void DrawInitialSupportLines()
        {
            //First Draw the horizontal support lines using the min and max Y coordinates
            //Draw 2 lines for each support, from the point to he left and to the right

            //first the max Y point to the right
            form.g.DrawLine(Utility.redPen, form.firstPolygon.MaxYPoint.X, form.firstPolygon.MaxYPoint.Y, 999999, form.firstPolygon.MaxYPoint.Y);
            //now max y point to the left
            form.g.DrawLine(Utility.redPen, form.firstPolygon.MaxYPoint.X, form.firstPolygon.MaxYPoint.Y, -999999, form.firstPolygon.MaxYPoint.Y);
            CaliperA.CenterPoint = form.firstPolygon.MaxYPoint;
            CaliperA.P1 = new PointF(999999, form.firstPolygon.MaxYPoint.Y);
            CaliperA.P2 = new PointF(-999999, form.firstPolygon.MaxYPoint.Y);
            Console.WriteLine("CaliperA: {0}", CaliperA);

            //firs the min Y point to the right
            form.g.DrawLine(Utility.greenPen, form.firstPolygon.MinYPoint.X, form.firstPolygon.MinYPoint.Y, -999999, form.firstPolygon.MinYPoint.Y);
            //now min y point to the left
            form.g.DrawLine(Utility.greenPen, form.firstPolygon.MinYPoint.X, form.firstPolygon.MinYPoint.Y, 999999, form.firstPolygon.MinYPoint.Y);
            CaliperB.CenterPoint = form.firstPolygon.MinYPoint;
            CaliperB.P1 = new PointF(-999999, form.firstPolygon.MinYPoint.Y);
            CaliperB.P2 = new PointF(999999, form.firstPolygon.MinYPoint.Y);
            Console.WriteLine("CaliperB: {0}", CaliperB);


            //Now draw the vertical linse using the min and max X coordinates
            //First Draw the horizontal support lines using the min and max Y coordinates
            //Draw 2 lines for each support, from the point to he left and to the right

            //first the max X point to the top
            form.g.DrawLine(Utility.orangePen, form.firstPolygon.MaxXPoint.X, form.firstPolygon.MaxYPoint.Y, form.firstPolygon.MaxXPoint.X, 999999);
            //now max X point to the Bottom
            form.g.DrawLine(Utility.orangePen, form.firstPolygon.MaxXPoint.X, form.firstPolygon.MaxXPoint.Y, form.firstPolygon.MaxXPoint.X, -999999);
            CaliperC.CenterPoint = form.firstPolygon.MaxXPoint;
            CaliperC.P1 = new PointF(form.firstPolygon.MaxXPoint.X, 999999);
            CaliperC.P2 = new PointF(form.firstPolygon.MaxXPoint.X, -999999);
            Console.WriteLine("CaliperC: {0}", CaliperC);

            //firs the min Y point to the right
            form.g.DrawLine(Utility.purplePen, form.firstPolygon.MinXPoint.X, form.firstPolygon.MinXPoint.Y, form.firstPolygon.MinXPoint.X, -999999);
            //now min y point to the left
            form.g.DrawLine(Utility.purplePen, form.firstPolygon.MinXPoint.X, form.firstPolygon.MinXPoint.Y, form.firstPolygon.MinXPoint.X, 999999);
            CaliperD.CenterPoint = form.firstPolygon.MinXPoint;
            CaliperD.P1 = new PointF(form.firstPolygon.MinXPoint.X, -999999);
            CaliperD.P2 = new PointF(form.firstPolygon.MinXPoint.X, 999999);
            Console.WriteLine("CaliperD: {0}", CaliperD);
        }

        public override void NextButton()
        {
            //All we need is 90 to determine a full sweep
            if (TotalRotatedAngleAmount < 90)
            {
                RotateCalipers();
                DoWork();
            }
            else
            {
                form.g.Clear(Color.White);
                form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());

                //elipses are drawn to the riform.ght of wher eyou click not over where you click   
                form.g.DrawEllipse(Utility.blackPen, AreaStruct.GetC1().CenterPoint.X - 5, AreaStruct.GetC1().CenterPoint.Y - 5, 10, 10);
                form.g.DrawEllipse(Utility.blackPen, AreaStruct.GetC2().CenterPoint.X - 5, AreaStruct.GetC2().CenterPoint.Y - 5, 10, 10);
                form.g.DrawEllipse(Utility.blackPen, AreaStruct.GetC3().CenterPoint.X - 5, AreaStruct.GetC3().CenterPoint.Y - 5, 10, 10);
                form.g.DrawEllipse(Utility.blackPen, AreaStruct.GetC4().CenterPoint.X - 5, AreaStruct.GetC4().CenterPoint.Y - 5, 10, 10);

                //Redraw the rectanform.gle
                form.g.DrawLine(Utility.redPen, AreaStruct.GetC1().P1, AreaStruct.GetC1().CenterPoint);
                form.g.DrawLine(Utility.redPen, AreaStruct.GetC1().CenterPoint, AreaStruct.GetC1().P2);

                form.g.DrawLine(Utility.redPen, AreaStruct.GetC2().P1, AreaStruct.GetC2().CenterPoint);
                form.g.DrawLine(Utility.redPen, AreaStruct.GetC2().CenterPoint, AreaStruct.GetC2().P2);

                form.g.DrawLine(Utility.redPen, AreaStruct.GetC3().P1, AreaStruct.GetC3().CenterPoint);
                form.g.DrawLine(Utility.redPen, AreaStruct.GetC3().CenterPoint, AreaStruct.GetC3().P2);

                form.g.DrawLine(Utility.redPen, AreaStruct.GetC4().P1, AreaStruct.GetC4().CenterPoint);
                form.g.DrawLine(Utility.redPen, AreaStruct.GetC4().CenterPoint, AreaStruct.GetC4().P2);

                form.nextButton.Enabled = false;
                form.continueButton.Enabled = true;
            }
        }


        protected override void RotateCalipers()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            if (angleA < angleB && angleA < angleC && angleA < angleD)
            {
                Console.WriteLine("A is min");
                minAngle = angleA;
                NewCalipers(1);
                //new angle B, C and D
                angleB = angleB - angleA;
                angleC = angleC - angleA;
                angleD = angleD - angleA;

                TotalRotatedAngleAmount += angleA;

                calAVector1 = new Vector(CaliperA.CenterPoint, CaliperA.P1);
                calAVector2 = new Vector(CaliperA.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexOne)]);
                Console.WriteLine("CalAvector1: {0}", calAVector1);
                Console.WriteLine("CalAvector2: {0}", calAVector2);
                angleA = Utility.GetAngle(calAVector1, calAVector2);
            }
            else if (angleB < angleA && angleB < angleC && angleB < angleD)
            {
                Console.WriteLine("B is min");
                minAngle = angleB;
                NewCalipers(2);
                //new angle A, C and D
                angleA = angleA - angleB;
                angleC = angleC - angleB;
                angleD = angleD - angleB;

                TotalRotatedAngleAmount += angleB;

                calBVector1 = new Vector(CaliperB.CenterPoint, CaliperB.P1);
                calBVector2 = new Vector(CaliperB.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexTwo)]);
                Console.WriteLine("CalBvector1: {0}", calBVector1);
                Console.WriteLine("CalBvector2: {0}", calBVector2);
                angleB = Utility.GetAngle(calBVector1, calBVector2);
            }
            else if (angleC < angleA && angleC < angleB && angleC < angleD)
            {
                Console.WriteLine("C is min");
                minAngle = angleC;
                NewCalipers(3);
                //new angle A, B and D
                angleA = angleA - angleC;
                angleB = angleB - angleC;
                angleD = angleD - angleC;

                TotalRotatedAngleAmount += angleC;

                calCVector1 = new Vector(CaliperC.CenterPoint, CaliperC.P1);
                calCVector2 = new Vector(CaliperC.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexThree)]);
                Console.WriteLine("CalCvector1: {0}", calCVector1);
                Console.WriteLine("CalCvector2: {0}", calCVector2);
                angleC = Utility.GetAngle(calCVector1, calCVector2);
            }
            else
            {
                Console.WriteLine("D is min");
                minAngle = angleD;
                NewCalipers(4);
                //new angle A, B, C
                angleA = angleA - angleD;
                angleB = angleB - angleD;
                angleC = angleC - angleD;

                TotalRotatedAngleAmount += angleD;

                calDVector1 = new Vector(CaliperD.CenterPoint, CaliperD.P1);
                calDVector2 = new Vector(CaliperD.CenterPoint, form.firstPolygon.points[form.firstPolygon.IncrementIndex(indexFour)]);
                Console.WriteLine("CalDvector1: {0}", calDVector1);
                Console.WriteLine("CalDvector2: {0}", calDVector2);
                angleD = Utility.GetAngle(calDVector1, calDVector2);
            }

            Console.WriteLine("CaliperA: {0}", CaliperA);
            Console.WriteLine("CaliperB: {0}", CaliperB);
            Console.WriteLine("CaliperC: {0}", CaliperC);
            Console.WriteLine("CaliperD: {0}", CaliperD);
            Console.WriteLine("Angle between first caliper is: {0}", angleA);
            Console.WriteLine("Angle between second caliper is: {0}", angleB);
            Console.WriteLine("Angle between third caliper is: {0}", angleC);
            Console.WriteLine("Angle between fourth caliper is: {0}", angleD);
            Console.WriteLine("Total rotated angle: {0}", TotalRotatedAngleAmount);
            form.totalRotatedAngleTextBox.Text = TotalRotatedAngleAmount.ToString();
        }

        protected override void NewCalipers(int choice)
        {
            PointF newPoint1 = new PointF();
            PointF newPoint2 = new PointF();

            form.g.Clear(Color.White);
            form.g.DrawPolygon(Utility.bluePen, form.firstPolygon.points.ToArray());


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
                indexTwo = form.firstPolygon.IncrementIndex(indexTwo);
                CaliperB.CenterPoint = form.firstPolygon.points[indexTwo];
            }

            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P1);
            form.g.DrawLine(Utility.greenPen, CaliperB.CenterPoint, CaliperB.P2);

            //=================================================
            //Rotate C
            newPoint1.X = (float)(CaliperC.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperC.P1.X - CaliperC.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperC.P1.Y - CaliperC.CenterPoint.Y)));

            newPoint1.Y = (float)(CaliperC.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperC.P1.X - CaliperC.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperC.P1.Y - CaliperC.CenterPoint.Y)));

            newPoint2.X = (float)(CaliperC.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperC.P2.X - CaliperC.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperC.P2.Y - CaliperC.CenterPoint.Y)));

            newPoint2.Y = (float)(CaliperC.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperC.P2.X - CaliperC.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperC.P2.Y - CaliperC.CenterPoint.Y)));

            CaliperC.P1 = newPoint1;
            CaliperC.P2 = newPoint2;

            //Only get new center of this is B asking
            if (choice == 3)
            {
                indexThree = form.firstPolygon.IncrementIndex(indexThree);
                CaliperC.CenterPoint = form.firstPolygon.points[indexThree];
            }

            form.g.DrawLine(Utility.orangePen, CaliperC.CenterPoint, CaliperC.P1);
            form.g.DrawLine(Utility.orangePen, CaliperC.CenterPoint, CaliperC.P2);

            //=================================================
            //Rotate D
            newPoint1.X = (float)(CaliperD.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperD.P1.X - CaliperD.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperD.P1.Y - CaliperD.CenterPoint.Y)));

            newPoint1.Y = (float)(CaliperD.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperD.P1.X - CaliperD.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperD.P1.Y - CaliperD.CenterPoint.Y)));

            newPoint2.X = (float)(CaliperD.CenterPoint.X + (Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperD.P2.X - CaliperD.CenterPoint.X) -
                            Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperD.P2.Y - CaliperD.CenterPoint.Y)));

            newPoint2.Y = (float)(CaliperD.CenterPoint.Y + (Math.Sin(Utility.DegreesToRadians(minAngle)) * (CaliperD.P2.X - CaliperD.CenterPoint.X) +
                            Math.Cos(Utility.DegreesToRadians(minAngle)) * (CaliperD.P2.Y - CaliperD.CenterPoint.Y)));

            CaliperD.P1 = newPoint1;
            CaliperD.P2 = newPoint2;

            //Only get new center of this is B asking
            if (choice == 4)
            {
                indexFour = form.firstPolygon.IncrementIndex(indexFour);
                CaliperD.CenterPoint = form.firstPolygon.points[indexFour];
            }

            form.g.DrawLine(Utility.purplePen, CaliperD.CenterPoint, CaliperD.P1);
            form.g.DrawLine(Utility.purplePen, CaliperD.CenterPoint, CaliperD.P2);           
        }


        protected override void DoWork()
        {
            TempArea = Utility.Area(CaliperA, CaliperB, CaliperC, CaliperD);

            if (TempArea < AreaStruct.MinimumArea)
            {
                Console.WriteLine("Set new min struct");
                AreaStruct.MinimumArea = TempArea;
                AreaStruct.SetC1(CaliperA);
                AreaStruct.SetC2(CaliperB);
                AreaStruct.SetC3(CaliperC);
                AreaStruct.SetC4(CaliperD);
            }

            form.currentAreaTextBox.Text = TempArea.ToString();
            form.minAreaTextBox.Text = AreaStruct.MinimumArea.ToString();
            Console.WriteLine("Temp Area: {0}", TempArea);
            Console.WriteLine("Area Struct {0}", AreaStruct);
        }
    }
}
