namespace XMLFormEditor
{
    partial class XMLListDataSourcePropertyControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxList = new System.Windows.Forms.TextBox();
            this.textBoxCaption = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbListSourceDocument = new System.Windows.Forms.ComboBox();
            this.bListExpression = new System.Windows.Forms.Button();
            this.bCaptionExpression = new System.Windows.Forms.Button();
            this.bValueExpression = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bApply
            // 
            this.bApply.Location = new System.Drawing.Point(133, 338);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "List expression";
            // 
            // textBoxList
            // 
            this.textBoxList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxList.Location = new System.Drawing.Point(6, 226);
            this.textBoxList.Name = "textBoxList";
            this.textBoxList.Size = new System.Drawing.Size(202, 20);
            this.textBoxList.TabIndex = 10;
            // 
            // textBoxCaption
            // 
            this.textBoxCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCaption.Location = new System.Drawing.Point(36, 268);
            this.textBoxCaption.Name = "textBoxCaption";
            this.textBoxCaption.Size = new System.Drawing.Size(172, 20);
            this.textBoxCaption.TabIndex = 12;
            this.textBoxCaption.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Caption exprerssion";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Value expression";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(36, 312);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(172, 20);
            this.textBoxValue.TabIndex = 14;
            this.textBoxValue.Text = ".";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "List source ducment";
            // 
            // cbListSourceDocument
            // 
            this.cbListSourceDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbListSourceDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbListSourceDocument.FormattingEnabled = true;
            this.cbListSourceDocument.Location = new System.Drawing.Point(6, 170);
            this.cbListSourceDocument.Name = "cbListSourceDocument";
            this.cbListSourceDocument.Size = new System.Drawing.Size(227, 21);
            this.cbListSourceDocument.TabIndex = 16;
            // 
            // bListExpression
            // 
            this.bListExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bListExpression.Location = new System.Drawing.Point(210, 226);
            this.bListExpression.Name = "bListExpression";
            this.bListExpression.Size = new System.Drawing.Size(23, 23);
            this.bListExpression.TabIndex = 17;
            this.bListExpression.Text = "...";
            this.bListExpression.UseVisualStyleBackColor = true;
            this.bListExpression.Click += new System.EventHandler(this.bListExpression_Click);
            // 
            // bCaptionExpression
            // 
            this.bCaptionExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bCaptionExpression.Location = new System.Drawing.Point(210, 268);
            this.bCaptionExpression.Name = "bCaptionExpression";
            this.bCaptionExpression.Size = new System.Drawing.Size(23, 23);
            this.bCaptionExpression.TabIndex = 18;
            this.bCaptionExpression.Text = "...";
            this.bCaptionExpression.UseVisualStyleBackColor = true;
            this.bCaptionExpression.Click += new System.EventHandler(this.bCaptionExpression_Click);
            // 
            // bValueExpression
            // 
            this.bValueExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bValueExpression.Location = new System.Drawing.Point(210, 312);
            this.bValueExpression.Name = "bValueExpression";
            this.bValueExpression.Size = new System.Drawing.Size(23, 23);
            this.bValueExpression.TabIndex = 19;
            this.bValueExpression.Text = "...";
            this.bValueExpression.UseVisualStyleBackColor = true;
            this.bValueExpression.Click += new System.EventHandler(this.bValueExpression_Click);
            // 
            // XMLListDataSourcePropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.bValueExpression);
            this.Controls.Add(this.bCaptionExpression);
            this.Controls.Add(this.cbListSourceDocument);
            this.Controls.Add(this.bListExpression);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxCaption);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxList);
            this.Controls.Add(this.label4);
            this.Name = "XMLListDataSourcePropertyControl";
            this.Size = new System.Drawing.Size(236, 380);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.textBoxList, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.textBoxValue, 0);
            this.Controls.SetChildIndex(this.textBoxCaption, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.bApply, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.bListExpression, 0);
            this.Controls.SetChildIndex(this.cbListSourceDocument, 0);
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.bCaptionExpression, 0);
            this.Controls.SetChildIndex(this.bValueExpression, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox textBoxList;
        internal System.Windows.Forms.TextBox textBoxCaption;
        internal System.Windows.Forms.TextBox textBoxValue;
        internal System.Windows.Forms.ComboBox cbListSourceDocument;
        private System.Windows.Forms.Button bListExpression;
        private System.Windows.Forms.Button bCaptionExpression;
        private System.Windows.Forms.Button bValueExpression;
    }
}
