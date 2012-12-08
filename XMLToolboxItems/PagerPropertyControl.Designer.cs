namespace XMLFormEditor
{
    partial class PagerPropertyControl
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
            this.textBoxPageCountXPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPageCountDocument = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bApply
            // 
            this.bApply.Location = new System.Drawing.Point(158, 258);
            this.bApply.TabIndex = 5;
            // 
            // textBoxPageCountXPath
            // 
            this.textBoxPageCountXPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPageCountXPath.Location = new System.Drawing.Point(6, 220);
            this.textBoxPageCountXPath.Name = "textBoxPageCountXPath";
            this.textBoxPageCountXPath.Size = new System.Drawing.Size(202, 20);
            this.textBoxPageCountXPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Page count XPath expression";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Page count XPath Document";
            // 
            // cbPageCountDocument
            // 
            this.cbPageCountDocument.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPageCountDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPageCountDocument.FormattingEnabled = true;
            this.cbPageCountDocument.Location = new System.Drawing.Point(6, 165);
            this.cbPageCountDocument.Name = "cbPageCountDocument";
            this.cbPageCountDocument.Size = new System.Drawing.Size(227, 21);
            this.cbPageCountDocument.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(210, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PagerPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbPageCountDocument);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPageCountXPath);
            this.Controls.Add(this.label1);
            this.Name = "PagerPropertyControl";
            this.Size = new System.Drawing.Size(236, 284);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textBoxPageCountXPath, 0);
            this.Controls.SetChildIndex(this.bApply, 0);
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cbPageCountDocument, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox textBoxPageCountXPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox cbPageCountDocument;
        private System.Windows.Forms.Button button1;
    }
}
