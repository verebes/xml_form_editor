namespace XMLFormEditor
{
    partial class EditorForm
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.applyXSLTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.snapToGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setGridSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonRemovePage = new System.Windows.Forms.Button();
            this.buttonAddPage = new System.Windows.Forms.Button();
            this.listBoxPageNames = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitter5 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonNewDocument = new System.Windows.Forms.Button();
            this.buttonRemoveDocument = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAddDocument = new System.Windows.Forms.Button();
            this.listBoxDocumentSources = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.toolBoxControl1 = new XMLFormEditor.XmlEditorToolBox();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(200, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 553);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(694, 24);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(5, 553);
            this.splitter2.TabIndex = 10;
            this.splitter2.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(489, 553);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(481, 527);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(481, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadLayoutToolStripMenuItem,
            this.saveLayoutToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.applyXSLTToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadLayoutToolStripMenuItem
            // 
            this.loadLayoutToolStripMenuItem.Name = "loadLayoutToolStripMenuItem";
            this.loadLayoutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadLayoutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadLayoutToolStripMenuItem.Text = "&Open";
            this.loadLayoutToolStripMenuItem.Click += new System.EventHandler(this.openLayoutToolStripMenuItem_Click);
            // 
            // saveLayoutToolStripMenuItem
            // 
            this.saveLayoutToolStripMenuItem.Name = "saveLayoutToolStripMenuItem";
            this.saveLayoutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveLayoutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveLayoutToolStripMenuItem.Text = "&Save";
            this.saveLayoutToolStripMenuItem.Click += new System.EventHandler(this.saveLayoutToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // applyXSLTToolStripMenuItem
            // 
            this.applyXSLTToolStripMenuItem.Name = "applyXSLTToolStripMenuItem";
            this.applyXSLTToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.applyXSLTToolStripMenuItem.Text = "Import";
            this.applyXSLTToolStripMenuItem.Click += new System.EventHandler(this.applyXSLTToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem1,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.selectAllToolStripMenuItem,
            this.clearSelectionToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem1
            // 
            this.cutToolStripMenuItem1.Name = "cutToolStripMenuItem1";
            this.cutToolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
            this.cutToolStripMenuItem1.Text = "Cut";
            this.cutToolStripMenuItem1.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.clearSelectionToolStripMenuItem.Text = "Clear selection";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arrangeLeftToolStripMenuItem,
            this.arrangeRightToolStripMenuItem,
            this.toolStripMenuItem3,
            this.snapToGridToolStripMenuItem,
            this.drawGridToolStripMenuItem,
            this.setGridSizeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // arrangeLeftToolStripMenuItem
            // 
            this.arrangeLeftToolStripMenuItem.Name = "arrangeLeftToolStripMenuItem";
            this.arrangeLeftToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.arrangeLeftToolStripMenuItem.Text = "Arrange left";
            this.arrangeLeftToolStripMenuItem.Click += new System.EventHandler(this.arrangeLeftToolStripMenuItem_Click);
            // 
            // arrangeRightToolStripMenuItem
            // 
            this.arrangeRightToolStripMenuItem.Name = "arrangeRightToolStripMenuItem";
            this.arrangeRightToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.arrangeRightToolStripMenuItem.Text = "Arrange right";
            this.arrangeRightToolStripMenuItem.Click += new System.EventHandler(this.arrangeRightToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(146, 6);
            // 
            // snapToGridToolStripMenuItem
            // 
            this.snapToGridToolStripMenuItem.Checked = true;
            this.snapToGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.snapToGridToolStripMenuItem.Name = "snapToGridToolStripMenuItem";
            this.snapToGridToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.snapToGridToolStripMenuItem.Text = "Snap to grid";
            this.snapToGridToolStripMenuItem.Click += new System.EventHandler(this.snapToGridToolStripMenuItem_Click);
            // 
            // drawGridToolStripMenuItem
            // 
            this.drawGridToolStripMenuItem.Checked = true;
            this.drawGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drawGridToolStripMenuItem.Name = "drawGridToolStripMenuItem";
            this.drawGridToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.drawGridToolStripMenuItem.Text = "Draw grid";
            this.drawGridToolStripMenuItem.Click += new System.EventHandler(this.drawGridToolStripMenuItem_Click);
            // 
            // setGridSizeToolStripMenuItem
            // 
            this.setGridSizeToolStripMenuItem.Name = "setGridSizeToolStripMenuItem";
            this.setGridSizeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.setGridSizeToolStripMenuItem.Text = "Options";
            this.setGridSizeToolStripMenuItem.Click += new System.EventHandler(this.setGridSizeToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.splitter3);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.splitter5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(699, 24);
            this.panel1.MinimumSize = new System.Drawing.Size(220, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 553);
            this.panel1.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 253);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(249, 300);
            this.panel4.TabIndex = 10;
            // 
            // splitter3
            // 
            this.splitter3.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(0, 250);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(249, 3);
            this.splitter3.TabIndex = 9;
            this.splitter3.TabStop = false;
            this.splitter3.Paint += new System.Windows.Forms.PaintEventHandler(this.splitter5_Paint);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.buttonRemovePage);
            this.panel5.Controls.Add(this.buttonAddPage);
            this.panel5.Controls.Add(this.listBoxPageNames);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 164);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(249, 86);
            this.panel5.TabIndex = 0;
            // 
            // buttonRemovePage
            // 
            this.buttonRemovePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemovePage.Enabled = false;
            this.buttonRemovePage.Location = new System.Drawing.Point(179, 54);
            this.buttonRemovePage.Name = "buttonRemovePage";
            this.buttonRemovePage.Size = new System.Drawing.Size(57, 23);
            this.buttonRemovePage.TabIndex = 6;
            this.buttonRemovePage.Text = "Remove";
            this.buttonRemovePage.UseVisualStyleBackColor = true;
            this.buttonRemovePage.Click += new System.EventHandler(this.buttonRemovePage_Click);
            // 
            // buttonAddPage
            // 
            this.buttonAddPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddPage.Location = new System.Drawing.Point(179, 24);
            this.buttonAddPage.Name = "buttonAddPage";
            this.buttonAddPage.Size = new System.Drawing.Size(57, 23);
            this.buttonAddPage.TabIndex = 5;
            this.buttonAddPage.Text = "Add";
            this.buttonAddPage.UseVisualStyleBackColor = true;
            this.buttonAddPage.Click += new System.EventHandler(this.buttonAddPage_Click);
            // 
            // listBoxPageNames
            // 
            this.listBoxPageNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPageNames.FormattingEnabled = true;
            this.listBoxPageNames.Location = new System.Drawing.Point(15, 24);
            this.listBoxPageNames.Name = "listBoxPageNames";
            this.listBoxPageNames.Size = new System.Drawing.Size(158, 56);
            this.listBoxPageNames.TabIndex = 4;
            this.listBoxPageNames.SelectedIndexChanged += new System.EventHandler(this.listBoxPageNames_SelectedIndexChanged);
            this.listBoxPageNames.DoubleClick += new System.EventHandler(this.listBoxPageNames_DoubleClick);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pages";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // splitter5
            // 
            this.splitter5.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter5.Location = new System.Drawing.Point(0, 161);
            this.splitter5.Name = "splitter5";
            this.splitter5.Size = new System.Drawing.Size(249, 3);
            this.splitter5.TabIndex = 0;
            this.splitter5.TabStop = false;
            this.splitter5.Paint += new System.Windows.Forms.PaintEventHandler(this.splitter5_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonNewDocument);
            this.panel2.Controls.Add(this.buttonRemoveDocument);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.buttonAddDocument);
            this.panel2.Controls.Add(this.listBoxDocumentSources);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.MinimumSize = new System.Drawing.Size(220, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 161);
            this.panel2.TabIndex = 6;
            // 
            // buttonNewDocument
            // 
            this.buttonNewDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNewDocument.Location = new System.Drawing.Point(170, 123);
            this.buttonNewDocument.Name = "buttonNewDocument";
            this.buttonNewDocument.Size = new System.Drawing.Size(66, 23);
            this.buttonNewDocument.TabIndex = 4;
            this.buttonNewDocument.Text = "New";
            this.buttonNewDocument.UseVisualStyleBackColor = true;
            this.buttonNewDocument.Click += new System.EventHandler(this.buttonNewDocument_Click);
            // 
            // buttonRemoveDocument
            // 
            this.buttonRemoveDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveDocument.Location = new System.Drawing.Point(35, 123);
            this.buttonRemoveDocument.Name = "buttonRemoveDocument";
            this.buttonRemoveDocument.Size = new System.Drawing.Size(56, 23);
            this.buttonRemoveDocument.TabIndex = 3;
            this.buttonRemoveDocument.Text = "Remove";
            this.buttonRemoveDocument.UseVisualStyleBackColor = true;
            this.buttonRemoveDocument.Click += new System.EventHandler(this.buttonDocumentSourceRemove_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Xml documents";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAddDocument
            // 
            this.buttonAddDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddDocument.Location = new System.Drawing.Point(97, 123);
            this.buttonAddDocument.Name = "buttonAddDocument";
            this.buttonAddDocument.Size = new System.Drawing.Size(67, 23);
            this.buttonAddDocument.TabIndex = 1;
            this.buttonAddDocument.Text = "Add document";
            this.buttonAddDocument.UseVisualStyleBackColor = true;
            this.buttonAddDocument.Click += new System.EventHandler(this.buttonAddDocument_Click);
            // 
            // listBoxDocumentSources
            // 
            this.listBoxDocumentSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDocumentSources.FormattingEnabled = true;
            this.listBoxDocumentSources.HorizontalScrollbar = true;
            this.listBoxDocumentSources.Location = new System.Drawing.Point(6, 25);
            this.listBoxDocumentSources.Name = "listBoxDocumentSources";
            this.listBoxDocumentSources.Size = new System.Drawing.Size(231, 82);
            this.listBoxDocumentSources.TabIndex = 0;
            this.listBoxDocumentSources.DoubleClick += new System.EventHandler(this.listBoxDocumentSources_DoubleClick);
            this.listBoxDocumentSources.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.toolBoxControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 553);
            this.panel3.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tabControl1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(205, 24);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(489, 553);
            this.panel6.TabIndex = 15;
            // 
            // toolBoxControl1
            // 
            this.toolBoxControl1.AutoScroll = true;
            this.toolBoxControl1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.toolBoxControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.toolBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolBoxControl1.Editor = null;
            this.toolBoxControl1.Location = new System.Drawing.Point(0, 0);
            this.toolBoxControl1.Name = "toolBoxControl1";
            this.toolBoxControl1.Size = new System.Drawing.Size(200, 553);
            this.toolBoxControl1.TabIndex = 1;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 577);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EditorForm";
            this.Text = "Xml Form Editor";
            this.Shown += new System.EventHandler(this.EditorForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private XmlEditorToolBox toolBoxControl1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem saveLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.Button buttonRemoveDocument;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddDocument;
        private System.Windows.Forms.ListBox listBoxDocumentSources;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Splitter splitter5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRemovePage;
        private System.Windows.Forms.Button buttonAddPage;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button buttonNewDocument;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrangeLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem arrangeRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem snapToGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setGridSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyXSLTToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ListBox listBoxPageNames;

    }
}

