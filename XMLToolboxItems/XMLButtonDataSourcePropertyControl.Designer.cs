namespace XMLFormEditor
{
    partial class XMLButtonDataSourcePropertyControl
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
            this.richTextBoxInsertXml = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // bApply
            // 
            this.bApply.Location = new System.Drawing.Point(158, 323);
            // 
            // richTextBoxInsertXml
            // 
            this.richTextBoxInsertXml.Location = new System.Drawing.Point(6, 134);
            this.richTextBoxInsertXml.Name = "richTextBoxInsertXml";
            this.richTextBoxInsertXml.Size = new System.Drawing.Size(227, 183);
            this.richTextBoxInsertXml.TabIndex = 9;
            this.richTextBoxInsertXml.Text = "";
            // 
            // XMLButtonDataSourcePropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.richTextBoxInsertXml);
            this.Name = "XMLButtonDataSourcePropertyControl";
            this.Size = new System.Drawing.Size(236, 349);
            this.Controls.SetChildIndex(this.richTextBoxInsertXml, 0);
            this.Controls.SetChildIndex(this.bApply, 0);
            this.Controls.SetChildIndex(this.title, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.RichTextBox richTextBoxInsertXml;

    }
}
