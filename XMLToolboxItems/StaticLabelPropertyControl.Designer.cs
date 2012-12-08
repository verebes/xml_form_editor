namespace XMLFormEditor
{
    partial class StaticLabelPropertyControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCaption = new System.Windows.Forms.TextBox();
            this.gbHorizontal = new System.Windows.Forms.GroupBox();
            this.rbRight = new System.Windows.Forms.RadioButton();
            this.rbCenter = new System.Windows.Forms.RadioButton();
            this.rbLeft = new System.Windows.Forms.RadioButton();
            this.gbVerticalAlignment = new System.Windows.Forms.GroupBox();
            this.rbBottom = new System.Windows.Forms.RadioButton();
            this.rbMiddle = new System.Windows.Forms.RadioButton();
            this.rbTop = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bFontSelect = new System.Windows.Forms.Button();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelColor = new System.Windows.Forms.Panel();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.bReset = new System.Windows.Forms.Button();
            this.gbHorizontal.SuspendLayout();
            this.gbVerticalAlignment.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Size = new System.Drawing.Size(235, 21);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(157, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Caption";
            // 
            // textBoxCaption
            // 
            this.textBoxCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCaption.Location = new System.Drawing.Point(7, 62);
            this.textBoxCaption.Multiline = true;
            this.textBoxCaption.Name = "textBoxCaption";
            this.textBoxCaption.Size = new System.Drawing.Size(225, 80);
            this.textBoxCaption.TabIndex = 3;
            // 
            // gbHorizontal
            // 
            this.gbHorizontal.Controls.Add(this.rbRight);
            this.gbHorizontal.Controls.Add(this.rbCenter);
            this.gbHorizontal.Controls.Add(this.rbLeft);
            this.gbHorizontal.Location = new System.Drawing.Point(7, 148);
            this.gbHorizontal.Name = "gbHorizontal";
            this.gbHorizontal.Size = new System.Drawing.Size(225, 46);
            this.gbHorizontal.TabIndex = 7;
            this.gbHorizontal.TabStop = false;
            this.gbHorizontal.Text = "Horizontal";
            // 
            // rbRight
            // 
            this.rbRight.AutoSize = true;
            this.rbRight.Location = new System.Drawing.Point(119, 23);
            this.rbRight.Name = "rbRight";
            this.rbRight.Size = new System.Drawing.Size(50, 17);
            this.rbRight.TabIndex = 9;
            this.rbRight.Text = "Right";
            this.rbRight.UseVisualStyleBackColor = true;
            this.rbRight.CheckedChanged += new System.EventHandler(this.rbTop_CheckedChanged);
            // 
            // rbCenter
            // 
            this.rbCenter.AutoSize = true;
            this.rbCenter.Location = new System.Drawing.Point(57, 23);
            this.rbCenter.Name = "rbCenter";
            this.rbCenter.Size = new System.Drawing.Size(56, 17);
            this.rbCenter.TabIndex = 8;
            this.rbCenter.Text = "Center";
            this.rbCenter.UseVisualStyleBackColor = true;
            this.rbCenter.CheckedChanged += new System.EventHandler(this.rbTop_CheckedChanged);
            // 
            // rbLeft
            // 
            this.rbLeft.AutoSize = true;
            this.rbLeft.Checked = true;
            this.rbLeft.Location = new System.Drawing.Point(8, 23);
            this.rbLeft.Name = "rbLeft";
            this.rbLeft.Size = new System.Drawing.Size(43, 17);
            this.rbLeft.TabIndex = 7;
            this.rbLeft.TabStop = true;
            this.rbLeft.Text = "Left";
            this.rbLeft.UseVisualStyleBackColor = true;
            this.rbLeft.CheckedChanged += new System.EventHandler(this.rbTop_CheckedChanged);
            // 
            // gbVerticalAlignment
            // 
            this.gbVerticalAlignment.Controls.Add(this.rbBottom);
            this.gbVerticalAlignment.Controls.Add(this.rbMiddle);
            this.gbVerticalAlignment.Controls.Add(this.rbTop);
            this.gbVerticalAlignment.Location = new System.Drawing.Point(7, 200);
            this.gbVerticalAlignment.Name = "gbVerticalAlignment";
            this.gbVerticalAlignment.Size = new System.Drawing.Size(225, 46);
            this.gbVerticalAlignment.TabIndex = 10;
            this.gbVerticalAlignment.TabStop = false;
            this.gbVerticalAlignment.Text = "Vertical";
            // 
            // rbBottom
            // 
            this.rbBottom.AutoSize = true;
            this.rbBottom.Location = new System.Drawing.Point(119, 23);
            this.rbBottom.Name = "rbBottom";
            this.rbBottom.Size = new System.Drawing.Size(58, 17);
            this.rbBottom.TabIndex = 9;
            this.rbBottom.Text = "Bottom";
            this.rbBottom.UseVisualStyleBackColor = true;
            this.rbBottom.CheckedChanged += new System.EventHandler(this.rbTop_CheckedChanged);
            // 
            // rbMiddle
            // 
            this.rbMiddle.AutoSize = true;
            this.rbMiddle.Checked = true;
            this.rbMiddle.Location = new System.Drawing.Point(57, 23);
            this.rbMiddle.Name = "rbMiddle";
            this.rbMiddle.Size = new System.Drawing.Size(56, 17);
            this.rbMiddle.TabIndex = 8;
            this.rbMiddle.TabStop = true;
            this.rbMiddle.Text = "Middle";
            this.rbMiddle.UseVisualStyleBackColor = true;
            this.rbMiddle.CheckedChanged += new System.EventHandler(this.rbTop_CheckedChanged);
            // 
            // rbTop
            // 
            this.rbTop.AutoSize = true;
            this.rbTop.Location = new System.Drawing.Point(8, 23);
            this.rbTop.Name = "rbTop";
            this.rbTop.Size = new System.Drawing.Size(44, 17);
            this.rbTop.TabIndex = 7;
            this.rbTop.Text = "Top";
            this.rbTop.UseVisualStyleBackColor = true;
            this.rbTop.CheckedChanged += new System.EventHandler(this.rbTop_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.bFontSelect);
            this.groupBox1.Controls.Add(this.numFontSize);
            this.groupBox1.Location = new System.Drawing.Point(7, 252);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 51);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Font size";
            // 
            // bFontSelect
            // 
            this.bFontSelect.Location = new System.Drawing.Point(113, 19);
            this.bFontSelect.Name = "bFontSelect";
            this.bFontSelect.Size = new System.Drawing.Size(23, 20);
            this.bFontSelect.TabIndex = 16;
            this.bFontSelect.Text = "...";
            this.bFontSelect.UseVisualStyleBackColor = true;
            this.bFontSelect.Click += new System.EventHandler(this.bFontSelect_Click);
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(59, 19);
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(48, 20);
            this.numFontSize.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelBackground);
            this.groupBox2.Controls.Add(this.panelColor);
            this.groupBox2.Location = new System.Drawing.Point(7, 309);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 64);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color";
            // 
            // panelColor
            // 
            this.panelColor.BackColor = System.Drawing.SystemColors.ControlText;
            this.panelColor.Location = new System.Drawing.Point(8, 19);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(49, 32);
            this.panelColor.TabIndex = 0;
            this.panelColor.TabStop = true;
            this.panelColor.Click += new System.EventHandler(this.panel1_Click);
            // 
            // panelBackground
            // 
            this.panelBackground.BackColor = System.Drawing.SystemColors.Window;
            this.panelBackground.Location = new System.Drawing.Point(64, 19);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(49, 32);
            this.panelBackground.TabIndex = 1;
            this.panelBackground.TabStop = true;
            this.panelBackground.Click += new System.EventHandler(this.panel1_Click);
            // 
            // bReset
            // 
            this.bReset.Location = new System.Drawing.Point(7, 382);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(75, 23);
            this.bReset.TabIndex = 16;
            this.bReset.Text = "Reset";
            this.bReset.UseVisualStyleBackColor = true;
            // 
            // StaticLabelPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.textBoxCaption);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbVerticalAlignment);
            this.Controls.Add(this.gbHorizontal);
            this.Controls.Add(this.button1);
            this.Name = "StaticLabelPropertyControl";
            this.Size = new System.Drawing.Size(235, 421);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.gbHorizontal, 0);
            this.Controls.SetChildIndex(this.gbVerticalAlignment, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.bReset, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.textBoxCaption, 0);
            this.Controls.SetChildIndex(this.title, 0);
            this.gbHorizontal.ResumeLayout(false);
            this.gbHorizontal.PerformLayout();
            this.gbVerticalAlignment.ResumeLayout(false);
            this.gbVerticalAlignment.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxCaption;
        private System.Windows.Forms.GroupBox gbHorizontal;
        private System.Windows.Forms.RadioButton rbRight;
        private System.Windows.Forms.RadioButton rbCenter;
        private System.Windows.Forms.RadioButton rbLeft;
        private System.Windows.Forms.GroupBox gbVerticalAlignment;
        private System.Windows.Forms.RadioButton rbBottom;
        private System.Windows.Forms.RadioButton rbMiddle;
        private System.Windows.Forms.RadioButton rbTop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bFontSelect;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.Button bReset;
    }
}
