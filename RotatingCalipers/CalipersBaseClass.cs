using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RotatingCalipers
{
    //all of the caliper algorithms must implement these functions
    abstract public class CalipersBaseClass
    {
        //parent form
        protected Form1 form;

        //temp variables
        protected double tempDistance;

        //Calipers and caliper vectors
        protected Caliper CaliperA = new Caliper();
        protected Caliper CaliperB = new Caliper();
        protected Caliper CaliperC = new Caliper();
        protected Caliper CaliperD = new Caliper();
        protected Vector calAVector1;
        protected Vector calAVector2;
        protected Vector calBVector1;
        protected Vector calBVector2;
        protected Vector calCVector1;
        protected Vector calCVector2;
        protected Vector calDVector1;
        protected Vector calDVector2;

        //indexes into the polygon positions for the calipers
        protected int indexOne;
        protected int indexTwo;
        protected int indexThree;
        protected int indexFour;

        //angle between calipers and total angle rotated
        public static double TotalRotatedAngleAmount { get; set; }

        protected double angleA;
        protected double angleB;
        protected double angleC;
        protected double angleD;
        protected double minAngle;


        public CalipersBaseClass(Form1 form)
        {
            this.form = form;
        }
        
        /// <summary>
        /// This is called when they click the button pertaining to that algorithm
        /// </summary>
        abstract public void StartAlgorithm();
        /// <summary>
        /// This is called when they click the next button
        /// </summary>
        abstract public void NextButton();

        /// <summary>
        /// All of the algorithms will follow these basic steps
        /// </summary>
        abstract protected void InitialLines(); // draw the intial calipers
        abstract protected void DrawInitialSupportLines(); 
        abstract protected void NewCalipers(int choice); 
        abstract protected void RotateCalipers();
        abstract protected void DoWork();
    }

    
}
