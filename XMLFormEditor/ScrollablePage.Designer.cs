namespace XMLFormEditor
{
    partial class ScrollablePage
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
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.documentForm = new XMLFormEditor.DocumentFormVisualizer();
            this.SuspendLayout();
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.LargeChange = 20;
            this.hScrollBar.Location = new System.Drawing.Point(0, 424);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(394, 17);
            this.hScrollBar.SmallChange = 20;
            this.hScrollBar.TabIndex = 14;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBar_Scroll);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.LargeChange = 20;
            this.vScrollBar.Location = new System.Drawing.Point(391, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(19, 424);
            this.vScrollBar.SmallChange = 20;
            this.vScrollBar.TabIndex = 13;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBar_Scroll);
            // 
            // documentForm
            // 
            this.documentForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.documentForm.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.documentForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.documentForm.Cursor = System.Windows.Forms.Cursors.Default;
            this.documentForm.DocumentLayout = null;
            this.documentForm.DrawGrid = false;
            this.documentForm.Location = new System.Drawing.Point(0, 0);
            this.documentForm.Name = "documentForm";
            this.documentForm.Size = new System.Drawing.Size(388, 424);
            this.documentForm.TabIndex = 15;
            this.documentForm.ViewLocation = new System.Drawing.Point(0, 0);
            this.documentForm.ViewRectangle = new System.Drawing.Rectangle(0, 0, 388, 424);
            // 
            // ScrollablePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.documentForm);
            this.Name = "ScrollablePage";
            this.Size = new System.Drawing.Size(410, 441);
            this.Resize += new System.EventHandler(this.scrollablePage_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private DocumentFormVisualizer documentForm;

    }
}
