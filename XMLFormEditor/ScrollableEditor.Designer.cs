namespace XMLFormEditor
{
    partial class ScrollableEditor
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
            this.documentEditor = new XMLFormEditor.DocumentEditorVisualizer();
            this.SuspendLayout();
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hScrollBar.LargeChange = 20;
            this.hScrollBar.Location = new System.Drawing.Point(0, 424);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(394, 17);
            this.hScrollBar.SmallChange = 20;
            this.hScrollBar.TabIndex = 11;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBar_Scroll);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.vScrollBar.LargeChange = 20;
            this.vScrollBar.Location = new System.Drawing.Point(391, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(19, 424);
            this.vScrollBar.SmallChange = 20;
            this.vScrollBar.TabIndex = 10;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBar_Scroll);
            // 
            // documentEditor
            // 
            this.documentEditor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.documentEditor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.documentEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.documentEditor.Cursor = System.Windows.Forms.Cursors.Default;
            this.documentEditor.DocumentLayout = null;
            this.documentEditor.GridSize = 20;
            this.documentEditor.Location = new System.Drawing.Point(0, 0);
            this.documentEditor.Name = "documentEditor";
            this.documentEditor.PropertyWindowPlace = null;
            this.documentEditor.PropertyWindowVisible = false;
            this.documentEditor.Size = new System.Drawing.Size(388, 424);
            this.documentEditor.SnapToGrid = true;            
            this.documentEditor.TabIndex = 12;
            this.documentEditor.ViewLocation = new System.Drawing.Point(0, 0);
            this.documentEditor.ViewRectangle = new System.Drawing.Rectangle(0, 0, 388, 424);
            // 
            // ScrollableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.documentEditor);
            this.Name = "ScrollableEditor";
            this.Size = new System.Drawing.Size(410, 441);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private DocumentEditorVisualizer documentEditor;
    }
}
