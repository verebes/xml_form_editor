namespace XMLFormEditor
{
    partial class XMLEditorPropertyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLeft = new System.Windows.Forms.TextBox();
            this.textBoxTop = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxBottom = new System.Windows.Forms.TextBox();
            this.textBoxRight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBackgroundImage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bFileSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Size = new System.Drawing.Size(211, 21);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Left";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Top";
            // 
            // textBoxLeft
            // 
            this.textBoxLeft.Location = new System.Drawing.Point(56, 71);
            this.textBoxLeft.Name = "textBoxLeft";
            this.textBoxLeft.Size = new System.Drawing.Size(68, 20);
            this.textBoxLeft.TabIndex = 3;
            this.textBoxLeft.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // textBoxTop
            // 
            this.textBoxTop.Location = new System.Drawing.Point(56, 98);
            this.textBoxTop.Name = "textBoxTop";
            this.textBoxTop.Size = new System.Drawing.Size(68, 20);
            this.textBoxTop.TabIndex = 4;
            this.textBoxTop.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Minimal rectangle";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // textBoxBottom
            // 
            this.textBoxBottom.Location = new System.Drawing.Point(56, 151);
            this.textBoxBottom.Name = "textBoxBottom";
            this.textBoxBottom.Size = new System.Drawing.Size(68, 20);
            this.textBoxBottom.TabIndex = 10;
            this.textBoxBottom.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // textBoxRight
            // 
            this.textBoxRight.Location = new System.Drawing.Point(56, 124);
            this.textBoxRight.Name = "textBoxRight";
            this.textBoxRight.Size = new System.Drawing.Size(68, 20);
            this.textBoxRight.TabIndex = 9;
            this.textBoxRight.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Bottom";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Right";
            // 
            // textBoxBackgroundImage
            // 
            this.textBoxBackgroundImage.Location = new System.Drawing.Point(10, 208);
            this.textBoxBackgroundImage.Name = "textBoxBackgroundImage";
            this.textBoxBackgroundImage.Size = new System.Drawing.Size(150, 20);
            this.textBoxBackgroundImage.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 192);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Background image";
            // 
            // bFileSelect
            // 
            this.bFileSelect.Location = new System.Drawing.Point(167, 204);
            this.bFileSelect.Name = "bFileSelect";
            this.bFileSelect.Size = new System.Drawing.Size(25, 23);
            this.bFileSelect.TabIndex = 13;
            this.bFileSelect.Text = "...";
            this.bFileSelect.UseVisualStyleBackColor = true;
            this.bFileSelect.Click += new System.EventHandler(this.bFileSelect_Click);
            // 
            // XMLEditorPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bFileSelect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxBackgroundImage);
            this.Controls.Add(this.textBoxBottom);
            this.Controls.Add(this.textBoxRight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxLeft);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "XMLEditorPropertyControl";
            this.Size = new System.Drawing.Size(211, 314);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.textBoxLeft, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.textBoxTop, 0);
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.textBoxRight, 0);
            this.Controls.SetChildIndex(this.textBoxBottom, 0);
            this.Controls.SetChildIndex(this.textBoxBackgroundImage, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.bFileSelect, 0);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxLeft;
        public System.Windows.Forms.TextBox textBoxTop;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.TextBox textBoxBottom;
        public System.Windows.Forms.TextBox textBoxRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBoxBackgroundImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bFileSelect;
    }
}
