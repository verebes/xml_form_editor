namespace XMLFormEditor
{
    partial class SingleDataSourcePropertyControl
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
            this.textBoxXPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bApply = new System.Windows.Forms.Button();
            this.cbSourceDocuments = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.Size = new System.Drawing.Size(236, 21);
            // 
            // textBoxXPath
            // 
            this.textBoxXPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXPath.Location = new System.Drawing.Point(6, 108);
            this.textBoxXPath.Name = "textBoxXPath";
            this.textBoxXPath.Size = new System.Drawing.Size(227, 20);
            this.textBoxXPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "XPath expression";
            // 
            // bApply
            // 
            this.bApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bApply.Location = new System.Drawing.Point(158, 147);
            this.bApply.Name = "bApply";
            this.bApply.Size = new System.Drawing.Size(75, 23);
            this.bApply.TabIndex = 6;
            this.bApply.Text = "Apply";
            this.bApply.UseVisualStyleBackColor = true;
            this.bApply.Click += new System.EventHandler(this.bApply_Click);
            // 
            // cbSourceDocuments
            // 
            this.cbSourceDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSourceDocuments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSourceDocuments.FormattingEnabled = true;
            this.cbSourceDocuments.Location = new System.Drawing.Point(6, 48);
            this.cbSourceDocuments.Name = "cbSourceDocuments";
            this.cbSourceDocuments.Size = new System.Drawing.Size(227, 21);
            this.cbSourceDocuments.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Source document";
            // 
            // SingleDataSourcePropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbSourceDocuments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bApply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxXPath);
            this.Name = "SingleDataSourcePropertyControl";
            this.Size = new System.Drawing.Size(236, 173);
            this.Controls.SetChildIndex(this.title, 0);
            this.Controls.SetChildIndex(this.textBoxXPath, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.bApply, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cbSourceDocuments, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBoxXPath;
        internal System.Windows.Forms.ComboBox cbSourceDocuments;
        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Button bApply;
    }
}
