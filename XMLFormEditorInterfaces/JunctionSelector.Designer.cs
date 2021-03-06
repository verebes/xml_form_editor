namespace XMLFormEditor
{
    partial class JunctionSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JunctionSelector));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bLeft = new System.Windows.Forms.Button();
            this.bDownRight = new System.Windows.Forms.Button();
            this.bTDown = new System.Windows.Forms.Button();
            this.bDownLeft = new System.Windows.Forms.Button();
            this.bRight = new System.Windows.Forms.Button();
            this.bTRight = new System.Windows.Forms.Button();
            this.bCross = new System.Windows.Forms.Button();
            this.bTLeft = new System.Windows.Forms.Button();
            this.bDown = new System.Windows.Forms.Button();
            this.bUpRight = new System.Windows.Forms.Button();
            this.bTUp = new System.Windows.Forms.Button();
            this.bUpLeft = new System.Windows.Forms.Button();
            this.bUp = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Cross.ico");
            this.imageList1.Images.SetKeyName(1, "Down.ico");
            this.imageList1.Images.SetKeyName(2, "DownLeft.ico");
            this.imageList1.Images.SetKeyName(3, "DownRight.ico");
            this.imageList1.Images.SetKeyName(4, "Left.ico");
            this.imageList1.Images.SetKeyName(5, "Right.ico");
            this.imageList1.Images.SetKeyName(6, "TDown.ico");
            this.imageList1.Images.SetKeyName(7, "TLeft.ico");
            this.imageList1.Images.SetKeyName(8, "TRight.ico");
            this.imageList1.Images.SetKeyName(9, "TUp.ico");
            this.imageList1.Images.SetKeyName(10, "Up.ico");
            this.imageList1.Images.SetKeyName(11, "UpLeft.ico");
            this.imageList1.Images.SetKeyName(12, "UpRight.ico");
            // 
            // bLeft
            // 
            this.bLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bLeft.ImageIndex = 4;
            this.bLeft.ImageList = this.imageList1;
            this.bLeft.Location = new System.Drawing.Point(138, 12);
            this.bLeft.Name = "bLeft";
            this.bLeft.Size = new System.Drawing.Size(23, 23);
            this.bLeft.TabIndex = 14;
            this.bLeft.UseVisualStyleBackColor = true;
            this.bLeft.Click += new System.EventHandler(this.bLeft_Click);
            this.bLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bDownRight
            // 
            this.bDownRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDownRight.ImageIndex = 3;
            this.bDownRight.ImageList = this.imageList1;
            this.bDownRight.Location = new System.Drawing.Point(12, 12);
            this.bDownRight.Name = "bDownRight";
            this.bDownRight.Size = new System.Drawing.Size(23, 23);
            this.bDownRight.TabIndex = 21;
            this.bDownRight.UseVisualStyleBackColor = true;
            this.bDownRight.Click += new System.EventHandler(this.bLeft_Click);
            this.bDownRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bTDown
            // 
            this.bTDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bTDown.ImageIndex = 6;
            this.bTDown.ImageList = this.imageList1;
            this.bTDown.Location = new System.Drawing.Point(41, 12);
            this.bTDown.Name = "bTDown";
            this.bTDown.Size = new System.Drawing.Size(23, 23);
            this.bTDown.TabIndex = 25;
            this.bTDown.UseVisualStyleBackColor = true;
            this.bTDown.Click += new System.EventHandler(this.bLeft_Click);
            this.bTDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bDownLeft
            // 
            this.bDownLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDownLeft.ImageIndex = 2;
            this.bDownLeft.ImageList = this.imageList1;
            this.bDownLeft.Location = new System.Drawing.Point(70, 12);
            this.bDownLeft.Name = "bDownLeft";
            this.bDownLeft.Size = new System.Drawing.Size(23, 23);
            this.bDownLeft.TabIndex = 19;
            this.bDownLeft.UseVisualStyleBackColor = true;
            this.bDownLeft.Click += new System.EventHandler(this.bLeft_Click);
            this.bDownLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bRight
            // 
            this.bRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bRight.ImageIndex = 5;
            this.bRight.ImageList = this.imageList1;
            this.bRight.Location = new System.Drawing.Point(109, 12);
            this.bRight.Name = "bRight";
            this.bRight.Size = new System.Drawing.Size(23, 23);
            this.bRight.TabIndex = 15;
            this.bRight.UseVisualStyleBackColor = true;
            this.bRight.Click += new System.EventHandler(this.bLeft_Click);
            this.bRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bTRight
            // 
            this.bTRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bTRight.ImageIndex = 8;
            this.bTRight.ImageList = this.imageList1;
            this.bTRight.Location = new System.Drawing.Point(12, 41);
            this.bTRight.Name = "bTRight";
            this.bTRight.Size = new System.Drawing.Size(23, 23);
            this.bTRight.TabIndex = 26;
            this.bTRight.UseVisualStyleBackColor = true;
            this.bTRight.Click += new System.EventHandler(this.bLeft_Click);
            this.bTRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bCross
            // 
            this.bCross.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bCross.ImageIndex = 0;
            this.bCross.ImageList = this.imageList1;
            this.bCross.Location = new System.Drawing.Point(41, 41);
            this.bCross.Name = "bCross";
            this.bCross.Size = new System.Drawing.Size(23, 23);
            this.bCross.TabIndex = 22;
            this.bCross.UseVisualStyleBackColor = true;
            this.bCross.Click += new System.EventHandler(this.bLeft_Click);
            this.bCross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bTLeft
            // 
            this.bTLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bTLeft.ImageIndex = 7;
            this.bTLeft.ImageList = this.imageList1;
            this.bTLeft.Location = new System.Drawing.Point(70, 41);
            this.bTLeft.Name = "bTLeft";
            this.bTLeft.Size = new System.Drawing.Size(23, 23);
            this.bTLeft.TabIndex = 24;
            this.bTLeft.UseVisualStyleBackColor = true;
            this.bTLeft.Click += new System.EventHandler(this.bLeft_Click);
            this.bTLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bDown
            // 
            this.bDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDown.ImageIndex = 1;
            this.bDown.ImageList = this.imageList1;
            this.bDown.Location = new System.Drawing.Point(109, 41);
            this.bDown.Name = "bDown";
            this.bDown.Size = new System.Drawing.Size(23, 23);
            this.bDown.TabIndex = 17;
            this.bDown.UseVisualStyleBackColor = true;
            this.bDown.Click += new System.EventHandler(this.bLeft_Click);
            this.bDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bUpRight
            // 
            this.bUpRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bUpRight.ImageIndex = 12;
            this.bUpRight.ImageList = this.imageList1;
            this.bUpRight.Location = new System.Drawing.Point(12, 70);
            this.bUpRight.Name = "bUpRight";
            this.bUpRight.Size = new System.Drawing.Size(23, 23);
            this.bUpRight.TabIndex = 20;
            this.bUpRight.UseVisualStyleBackColor = true;
            this.bUpRight.Click += new System.EventHandler(this.bLeft_Click);
            this.bUpRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bTUp
            // 
            this.bTUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bTUp.ImageIndex = 9;
            this.bTUp.ImageList = this.imageList1;
            this.bTUp.Location = new System.Drawing.Point(41, 70);
            this.bTUp.Name = "bTUp";
            this.bTUp.Size = new System.Drawing.Size(23, 23);
            this.bTUp.TabIndex = 23;
            this.bTUp.UseVisualStyleBackColor = true;
            this.bTUp.Click += new System.EventHandler(this.bLeft_Click);
            this.bTUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bUpLeft
            // 
            this.bUpLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bUpLeft.ImageIndex = 11;
            this.bUpLeft.ImageList = this.imageList1;
            this.bUpLeft.Location = new System.Drawing.Point(70, 70);
            this.bUpLeft.Name = "bUpLeft";
            this.bUpLeft.Size = new System.Drawing.Size(23, 23);
            this.bUpLeft.TabIndex = 18;
            this.bUpLeft.UseVisualStyleBackColor = true;
            this.bUpLeft.Click += new System.EventHandler(this.bLeft_Click);
            this.bUpLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bUp
            // 
            this.bUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bUp.ImageIndex = 10;
            this.bUp.ImageList = this.imageList1;
            this.bUp.Location = new System.Drawing.Point(109, 70);
            this.bUp.Name = "bUp";
            this.bUp.Size = new System.Drawing.Size(23, 23);
            this.bUp.TabIndex = 16;
            this.bUp.UseVisualStyleBackColor = true;
            this.bUp.Click += new System.EventHandler(this.bLeft_Click);
            this.bUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // bDelete
            // 
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.bDelete.ImageList = this.imageList1;
            this.bDelete.Location = new System.Drawing.Point(138, 70);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(23, 23);
            this.bDelete.TabIndex = 27;
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bLeft_Click);
            this.bDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            // 
            // JunctionSelector
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 108);
            this.ControlBox = false;
            this.Controls.Add(this.bLeft);
            this.Controls.Add(this.bDownRight);
            this.Controls.Add(this.bTDown);
            this.Controls.Add(this.bDownLeft);
            this.Controls.Add(this.bRight);
            this.Controls.Add(this.bTRight);
            this.Controls.Add(this.bCross);
            this.Controls.Add(this.bTLeft);
            this.Controls.Add(this.bDown);
            this.Controls.Add(this.bUpRight);
            this.Controls.Add(this.bTUp);
            this.Controls.Add(this.bUpLeft);
            this.Controls.Add(this.bUp);
            this.Controls.Add(this.bDelete);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JunctionSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "JunctionSelector";
            this.TopMost = true;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bDownRight_MouseUp);
            this.Click += new System.EventHandler(this.bLeft_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button bLeft;
        private System.Windows.Forms.Button bDownRight;
        private System.Windows.Forms.Button bTDown;
        private System.Windows.Forms.Button bDownLeft;
        private System.Windows.Forms.Button bRight;
        private System.Windows.Forms.Button bTRight;
        private System.Windows.Forms.Button bCross;
        private System.Windows.Forms.Button bTLeft;
        private System.Windows.Forms.Button bDown;
        private System.Windows.Forms.Button bUpRight;
        private System.Windows.Forms.Button bTUp;
        private System.Windows.Forms.Button bUpLeft;
        private System.Windows.Forms.Button bUp;
        private System.Windows.Forms.Button bDelete;
    }
}