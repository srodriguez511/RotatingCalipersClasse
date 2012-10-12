namespace RotatingCalipers
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ClosePolygonButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.polygonOnePoints = new System.Windows.Forms.ListBox();
            this.polygonTwoPoints = new System.Windows.Forms.ListBox();
            this.polyGonTwoPointsLabel = new System.Windows.Forms.Label();
            this.polygonOnePointsLabel = new System.Windows.Forms.Label();
            this.maxDiameterButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.minAreaButton = new System.Windows.Forms.Button();
            this.maxDiameterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.currentDiameterTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.currentAreaTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.minAreaTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.totalRotatedAngleTextBox = new System.Windows.Forms.TextBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.addPolygonButton = new System.Windows.Forms.Button();
            this.maxDist2PolyButton = new System.Windows.Forms.Button();
            this.minimumPermButton = new System.Windows.Forms.Button();
            this.minPerimTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.currentPerimTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mergeConvexHullButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.minDistanceTextBox = new System.Windows.Forms.TextBox();
            this.intersectionButton = new System.Windows.Forms.Button();
            this.minWidthButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1012, 448);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // ClosePolygonButton
            // 
            this.ClosePolygonButton.Location = new System.Drawing.Point(930, 466);
            this.ClosePolygonButton.Name = "ClosePolygonButton";
            this.ClosePolygonButton.Size = new System.Drawing.Size(94, 32);
            this.ClosePolygonButton.TabIndex = 1;
            this.ClosePolygonButton.Text = "Close Polygon";
            this.ClosePolygonButton.UseVisualStyleBackColor = true;
            this.ClosePolygonButton.Click += new System.EventHandler(this.ClosePolygonButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(930, 504);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(94, 32);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear Canvas";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // polygonOnePoints
            // 
            this.polygonOnePoints.FormattingEnabled = true;
            this.polygonOnePoints.Location = new System.Drawing.Point(1042, 28);
            this.polygonOnePoints.Name = "polygonOnePoints";
            this.polygonOnePoints.Size = new System.Drawing.Size(98, 212);
            this.polygonOnePoints.TabIndex = 4;
            // 
            // polygonTwoPoints
            // 
            this.polygonTwoPoints.FormattingEnabled = true;
            this.polygonTwoPoints.Location = new System.Drawing.Point(1042, 274);
            this.polygonTwoPoints.Name = "polygonTwoPoints";
            this.polygonTwoPoints.Size = new System.Drawing.Size(97, 186);
            this.polygonTwoPoints.TabIndex = 5;
            // 
            // polyGonTwoPointsLabel
            // 
            this.polyGonTwoPointsLabel.AutoSize = true;
            this.polyGonTwoPointsLabel.Location = new System.Drawing.Point(1039, 258);
            this.polyGonTwoPointsLabel.Name = "polyGonTwoPointsLabel";
            this.polyGonTwoPointsLabel.Size = new System.Drawing.Size(101, 13);
            this.polyGonTwoPointsLabel.TabIndex = 7;
            this.polyGonTwoPointsLabel.Text = "Polygon Two Points";
            // 
            // polygonOnePointsLabel
            // 
            this.polygonOnePointsLabel.AutoSize = true;
            this.polygonOnePointsLabel.Location = new System.Drawing.Point(1039, 12);
            this.polygonOnePointsLabel.Name = "polygonOnePointsLabel";
            this.polygonOnePointsLabel.Size = new System.Drawing.Size(100, 13);
            this.polygonOnePointsLabel.TabIndex = 8;
            this.polygonOnePointsLabel.Text = "Polygon One Points";
            // 
            // maxDiameterButton
            // 
            this.maxDiameterButton.Enabled = false;
            this.maxDiameterButton.Location = new System.Drawing.Point(12, 466);
            this.maxDiameterButton.Name = "maxDiameterButton";
            this.maxDiameterButton.Size = new System.Drawing.Size(123, 32);
            this.maxDiameterButton.TabIndex = 9;
            this.maxDiameterButton.Text = "Diameters";
            this.maxDiameterButton.UseVisualStyleBackColor = true;
            this.maxDiameterButton.Click += new System.EventHandler(this.maxDiameterButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Enabled = false;
            this.nextButton.Location = new System.Drawing.Point(710, 466);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(104, 32);
            this.nextButton.TabIndex = 10;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // minAreaButton
            // 
            this.minAreaButton.Enabled = false;
            this.minAreaButton.Location = new System.Drawing.Point(141, 466);
            this.minAreaButton.Name = "minAreaButton";
            this.minAreaButton.Size = new System.Drawing.Size(123, 32);
            this.minAreaButton.TabIndex = 11;
            this.minAreaButton.Text = "Minimum Area";
            this.minAreaButton.UseVisualStyleBackColor = true;
            this.minAreaButton.Click += new System.EventHandler(this.smallestRectangleButton_Click);
            // 
            // maxDiameterTextBox
            // 
            this.maxDiameterTextBox.Location = new System.Drawing.Point(23, 80);
            this.maxDiameterTextBox.Name = "maxDiameterTextBox";
            this.maxDiameterTextBox.Size = new System.Drawing.Size(100, 20);
            this.maxDiameterTextBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Max Distance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Current Distance";
            // 
            // currentDiameterTextbox
            // 
            this.currentDiameterTextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.currentDiameterTextbox.Location = new System.Drawing.Point(23, 33);
            this.currentDiameterTextbox.Name = "currentDiameterTextbox";
            this.currentDiameterTextbox.Size = new System.Drawing.Size(100, 20);
            this.currentDiameterTextbox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Current Area";
            // 
            // currentAreaTextBox
            // 
            this.currentAreaTextBox.Location = new System.Drawing.Point(19, 35);
            this.currentAreaTextBox.Name = "currentAreaTextBox";
            this.currentAreaTextBox.Size = new System.Drawing.Size(100, 20);
            this.currentAreaTextBox.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Min Area";
            // 
            // minAreaTextBox
            // 
            this.minAreaTextBox.ForeColor = System.Drawing.Color.Red;
            this.minAreaTextBox.Location = new System.Drawing.Point(19, 84);
            this.minAreaTextBox.Name = "minAreaTextBox";
            this.minAreaTextBox.Size = new System.Drawing.Size(100, 20);
            this.minAreaTextBox.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1169, 466);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Total Rotated Angle";
            // 
            // totalRotatedAngleTextBox
            // 
            this.totalRotatedAngleTextBox.Location = new System.Drawing.Point(1172, 482);
            this.totalRotatedAngleTextBox.Name = "totalRotatedAngleTextBox";
            this.totalRotatedAngleTextBox.Size = new System.Drawing.Size(100, 20);
            this.totalRotatedAngleTextBox.TabIndex = 23;
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(820, 504);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(104, 32);
            this.continueButton.TabIndex = 24;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // addPolygonButton
            // 
            this.addPolygonButton.Enabled = false;
            this.addPolygonButton.Location = new System.Drawing.Point(820, 466);
            this.addPolygonButton.Name = "addPolygonButton";
            this.addPolygonButton.Size = new System.Drawing.Size(104, 32);
            this.addPolygonButton.TabIndex = 25;
            this.addPolygonButton.Text = "Add Polygon";
            this.addPolygonButton.UseVisualStyleBackColor = true;
            this.addPolygonButton.Click += new System.EventHandler(this.addPolygonButton_Click);
            // 
            // maxDist2PolyButton
            // 
            this.maxDist2PolyButton.AllowDrop = true;
            this.maxDist2PolyButton.Enabled = false;
            this.maxDist2PolyButton.Location = new System.Drawing.Point(375, 466);
            this.maxDist2PolyButton.Name = "maxDist2PolyButton";
            this.maxDist2PolyButton.Size = new System.Drawing.Size(123, 32);
            this.maxDist2PolyButton.TabIndex = 26;
            this.maxDist2PolyButton.Text = "Max Distance 2 P";
            this.maxDist2PolyButton.UseVisualStyleBackColor = true;
            this.maxDist2PolyButton.Click += new System.EventHandler(this.maxDist2PolyButton_Click);
            // 
            // minimumPermButton
            // 
            this.minimumPermButton.Enabled = false;
            this.minimumPermButton.Location = new System.Drawing.Point(141, 504);
            this.minimumPermButton.Name = "minimumPermButton";
            this.minimumPermButton.Size = new System.Drawing.Size(123, 32);
            this.minimumPermButton.TabIndex = 27;
            this.minimumPermButton.Text = "Minimum Perimeter";
            this.minimumPermButton.UseVisualStyleBackColor = true;
            this.minimumPermButton.Click += new System.EventHandler(this.minimumPermButton_Click);
            // 
            // minPerimTextBox
            // 
            this.minPerimTextBox.ForeColor = System.Drawing.Color.Red;
            this.minPerimTextBox.Location = new System.Drawing.Point(19, 167);
            this.minPerimTextBox.Name = "minPerimTextBox";
            this.minPerimTextBox.Size = new System.Drawing.Size(100, 20);
            this.minPerimTextBox.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Min Perimeter";
            // 
            // currentPerimTextBox
            // 
            this.currentPerimTextBox.Location = new System.Drawing.Point(18, 128);
            this.currentPerimTextBox.Name = "currentPerimTextBox";
            this.currentPerimTextBox.Size = new System.Drawing.Size(100, 20);
            this.currentPerimTextBox.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Current Perimeter";
            // 
            // mergeConvexHullButton
            // 
            this.mergeConvexHullButton.AllowDrop = true;
            this.mergeConvexHullButton.Enabled = false;
            this.mergeConvexHullButton.Location = new System.Drawing.Point(375, 504);
            this.mergeConvexHullButton.Name = "mergeConvexHullButton";
            this.mergeConvexHullButton.Size = new System.Drawing.Size(123, 32);
            this.mergeConvexHullButton.TabIndex = 32;
            this.mergeConvexHullButton.Text = "Merge Convex Hull";
            this.mergeConvexHullButton.UseVisualStyleBackColor = true;
            this.mergeConvexHullButton.Click += new System.EventHandler(this.mergeConvexHullButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Min Distance";
            // 
            // minDistanceTextBox
            // 
            this.minDistanceTextBox.ForeColor = System.Drawing.Color.Purple;
            this.minDistanceTextBox.Location = new System.Drawing.Point(23, 129);
            this.minDistanceTextBox.Name = "minDistanceTextBox";
            this.minDistanceTextBox.Size = new System.Drawing.Size(100, 20);
            this.minDistanceTextBox.TabIndex = 34;
            // 
            // intersectionButton
            // 
            this.intersectionButton.AllowDrop = true;
            this.intersectionButton.Enabled = false;
            this.intersectionButton.Location = new System.Drawing.Point(504, 466);
            this.intersectionButton.Name = "intersectionButton";
            this.intersectionButton.Size = new System.Drawing.Size(123, 32);
            this.intersectionButton.TabIndex = 35;
            this.intersectionButton.Text = "Intersection Polygons";
            this.intersectionButton.UseVisualStyleBackColor = true;
            this.intersectionButton.Click += new System.EventHandler(this.intersectionButton_Click);
            // 
            // minWidthButton
            // 
            this.minWidthButton.Enabled = false;
            this.minWidthButton.Location = new System.Drawing.Point(12, 504);
            this.minWidthButton.Name = "minWidthButton";
            this.minWidthButton.Size = new System.Drawing.Size(123, 32);
            this.minWidthButton.TabIndex = 36;
            this.minWidthButton.Text = "Minimum Width";
            this.minWidthButton.UseVisualStyleBackColor = true;
            this.minWidthButton.Click += new System.EventHandler(this.minWidthButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.minDistanceTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.currentDiameterTextbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.maxDiameterTextBox);
            this.groupBox1.Location = new System.Drawing.Point(1149, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(141, 161);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.minPerimTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.currentPerimTextBox);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.minAreaTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.currentAreaTextBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(1149, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 200);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 541);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.minWidthButton);
            this.Controls.Add(this.intersectionButton);
            this.Controls.Add(this.mergeConvexHullButton);
            this.Controls.Add(this.minimumPermButton);
            this.Controls.Add(this.maxDist2PolyButton);
            this.Controls.Add(this.addPolygonButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.totalRotatedAngleTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.minAreaButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.maxDiameterButton);
            this.Controls.Add(this.polygonOnePointsLabel);
            this.Controls.Add(this.polyGonTwoPointsLabel);
            this.Controls.Add(this.polygonTwoPoints);
            this.Controls.Add(this.polygonOnePoints);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ClosePolygonButton);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button ClosePolygonButton;
        public System.Windows.Forms.Button ClearButton;
        public System.Windows.Forms.ListBox polygonOnePoints;
        public System.Windows.Forms.ListBox polygonTwoPoints;
        public System.Windows.Forms.Label polyGonTwoPointsLabel;
        public System.Windows.Forms.Label polygonOnePointsLabel;
        public System.Windows.Forms.Button maxDiameterButton;
        public System.Windows.Forms.Button nextButton;
        public System.Windows.Forms.Button minAreaButton;
        public System.Windows.Forms.TextBox maxDiameterTextBox;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox currentDiameterTextbox;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox currentAreaTextBox;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox minAreaTextBox;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox totalRotatedAngleTextBox;
        public System.Windows.Forms.Button continueButton;
        public System.Windows.Forms.Button addPolygonButton;
        public System.Windows.Forms.Button maxDist2PolyButton;
        public System.Windows.Forms.Button minimumPermButton;
        public System.Windows.Forms.TextBox minPerimTextBox;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox currentPerimTextBox;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Button mergeConvexHullButton;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox minDistanceTextBox;
        public System.Windows.Forms.Button intersectionButton;
        public System.Windows.Forms.Button minWidthButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

