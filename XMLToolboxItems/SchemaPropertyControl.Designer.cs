namespace XMLFormEditor
{
    partial class SchemaPropertyControl
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
            this.labelSchemaName = new System.Windows.Forms.Label();
            this.cbSchemaName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bApply
            // 
            this.bApply.Location = new System.Drawing.Point(158, 207);
            // 
            // labelSchemaName
            // 
            this.labelSchemaName.AutoSize = true;
            this.labelSchemaName.Location = new System.Drawing.Point(3, 152);
            this.labelSchemaName.Name = "labelSchemaName";
            this.labelSchemaName.Size = new System.Drawing.Size(75, 13);
            this.labelSchemaName.TabIndex = 9;
            this.labelSchemaName.Text = "Schema name";
            // 
            // cbSchemaName
            // 
            this.cbSchemaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSchemaName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSchemaName.FormattingEnabled = true;
            this.cbSchemaName.Location = new System.Drawing.Point(6, 168);
            this.cbSchemaName.Name = "cbSchemaName";
            this.cbSchemaName.Size = new System.Drawing.Size(227, 21);
            this.cbSchemaName.TabIndex = 10;
            // 
            // SchemaPropertyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.labelSchemaName);
            this.Controls.Add(this.cbSchemaName);
            this.Name = "SchemaPropertyControl";
            this.Size = new System.Drawing.Size(236, 233);
            this.Controls.SetChildIndex(this.cbSchemaName, 0);
            this.Controls.SetChildIndex(this.labelSchemaName, 0);
            this.Controls.SetChildIndex(this.bApply, 0);
            this.Controls.SetChildIndex(this.title, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSchemaName;
        internal System.Windows.Forms.ComboBox cbSchemaName;
    }
}
