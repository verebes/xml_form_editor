using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XMLFormEditor
{
    public partial class XMLTreeDialog : Form
    {
        public XMLTreeDialog()
        {
            InitializeComponent();
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);

            ImageList iconList = new ImageList();
            iconList.Images.Add(XMLToolboxItems.Properties.Resources.attribute);
            iconList.Images.Add(XMLToolboxItems.Properties.Resources.node);
            iconList.Images.Add(XMLToolboxItems.Properties.Resources.text);
            iconList.Images.Add(XMLToolboxItems.Properties.Resources.document);

            treeView1.ImageList = iconList;
            treeView1.LabelEdit = true;
            updateTree();
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
        }

        private string GenerateXPath(TreeNode node) {
            selection = "";
            while (node != null) {
                selection = "/" + node.Text + selection;
                node = node.Parent;
            }
            return selection;
        }


        
        protected TreeNode getTreeNode(XmlNode xmlNode) {
            if (xmlNode == document) {
                return treeView1.TopNode;
            }

            Stack stack = new Stack();

            foreach (TreeNode treeNode in treeView1.Nodes) {
                stack.Push(treeNode);
            }
            

            while ( stack.Count > 0 ) {
                TreeNode node = stack.Pop() as TreeNode;
                if (node == null || node.Tag == null) {
                    continue;                    
                }
                XmlNode currentXmlNode = node.Tag as XmlNode;
                if (currentXmlNode == xmlNode) {
                    return node;
                }

                foreach (TreeNode treeNode in node.Nodes) {
                    stack.Push(treeNode);
                }
       
            }
            return null;

        }

        public void selectNodeByXPath (string xpathExpression ) {
            try {
                XmlNode node = document.SelectSingleNode(xpathExpression);

                TreeNode treeNode = getTreeNode(node);
                if (treeNode == null)
                    return;

                treeView1.SelectedNode = treeNode;
                treeNode.ExpandAll();

            } catch (System.Xml.XmlException e) {
                return;
            }
        }
        

        protected string selection;
        public string Selection 
        {
            get { return selection; }
        }

        protected XmlDocument document;
        public XmlDocument Document
        {
            set
            {
                document = value;
                updateTree();
            }
        }
        
        
        private class StackItem 
        {
            public StackItem ( TreeNode treeNode, XmlNode xmlNode ) {
                this.treeNode = treeNode;
                this.xmlNode = xmlNode;
            }
            public TreeNode treeNode;
            public XmlNode xmlNode;
        }
        private System.Collections.Stack stack = new System.Collections.Stack();


        protected void updateTree()
        {
            if (document == null) {
                return;
            }

            stack.Clear();
            treeView1.Nodes.Clear();

            if ( document.FirstChild == null) {
                return;
            }

            TreeNode rootNode = treeView1.Nodes.Add(document.FirstChild.Name);

            rootNode.Tag = document.FirstChild;
            rootNode.ImageIndex = 3;
            rootNode.SelectedImageIndex = 3;

            foreach (XmlNode child in document.FirstChild)
            {
                stack.Push(new StackItem(rootNode, child));                
            }

            while (stack.Count != 0 ) {
                StackItem item = stack.Pop() as StackItem;

                if (item.xmlNode.NodeType != XmlNodeType.Element
                    && item.xmlNode.NodeType != XmlNodeType.Document
                    && item.xmlNode.NodeType != XmlNodeType.Attribute )
                {
                    continue;
                }

                TreeNode treeNode = null;

                switch ( item.xmlNode.NodeType )
                {
                    case XmlNodeType.Document:
                        treeNode = rootNode;
                        break;
                    case XmlNodeType.Attribute:
                        treeNode = item.treeNode.Nodes.Insert(0, item.xmlNode.Name);
                        treeNode.ImageIndex = 0;
                        treeNode.SelectedImageIndex = 0;
                        treeNode.Tag = item.xmlNode;

                        
                        TreeNode value = treeNode.Nodes.Add(item.xmlNode.Value);
                        
                        value.ImageIndex = 2;
                        value.SelectedImageIndex = 2;
                        value.Tag = null;

                        break;
                    case XmlNodeType.Element:
                        treeNode = item.treeNode.Nodes.Insert(0,item.xmlNode.Name);
                        treeNode.ImageIndex = 1;
                        treeNode.SelectedImageIndex = 1;
                        treeNode.Tag = item.xmlNode;
                        break;
                    default:
                        continue;
                }

                foreach (XmlNode child in item.xmlNode.ChildNodes)
                {
                    stack.Push(new StackItem(treeNode, child));
                }

                if (item.xmlNode.Attributes == null)
                    continue;

                foreach (XmlNode attributes in item.xmlNode.Attributes)
                {
                    stack.Push(new StackItem(treeNode, attributes));
                }

            }
        }

        private void treeView1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void treeView1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Insert && e.Control) {
                insertChildNode();
                return;
            }

            if (e.KeyCode == Keys.Insert)
            {
                insertNode();
            }

            if ( e.KeyCode == Keys.F2 )
            {
                editNode();
            }

            if (e.KeyCode == Keys.Delete)
            {
                deleteNode();
            }

        }

        private void renameXmlElement(XmlElement xmlElement, string label)
        {            
            XmlElement newElement = document.CreateElement(label);
            while ( xmlElement.HasChildNodes ) {
                newElement.AppendChild(xmlElement.FirstChild);
            }
            while ( xmlElement.HasAttributes) {
                newElement.Attributes.Append( xmlElement.Attributes[0]);
            }
            xmlElement.ParentNode.ReplaceChild(newElement, xmlElement);
        }

        private void deleteNode()
        {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            XmlElement xmlElement = treeNode.Tag as XmlElement;
            if (xmlElement == null)
                return;

            const string question = "Do you really want to delete the selected node and all of it's descendants?";
            const string caption = "Confirm delete node";
            if ( MessageBox.Show(question,caption, MessageBoxButtons.YesNo) == DialogResult.Yes )
            {
                treeNode.Remove();
                xmlElement.RemoveAll();
            }            
        }

        private void editNode() {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            XmlElement xmlElement = treeNode.Tag as XmlElement;
            if (xmlElement == null)
                return;


            if (xmlElement.NodeType == XmlNodeType.Element)
            {                
                treeNode.BeginEdit();
            }
        }

        private void insertNode() {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            XmlElement xmlElement = treeNode.Tag as XmlElement;
            if (xmlElement == null)
                return;

            if (xmlElement.ParentNode == null || xmlElement.ParentNode == document) {
                return;
            }

            if (xmlElement.NodeType == XmlNodeType.Element)
            {
                XmlElement newElement = document.CreateElement("New");
                xmlElement.ParentNode.InsertAfter(newElement, xmlElement);

                TreeNode newTreeNode = treeNode.Parent.Nodes.Insert(treeNode.Index +1, newElement.Name);                
                newTreeNode.ImageIndex = 1;
                newTreeNode.SelectedImageIndex = 1;
                newTreeNode.Tag = newElement;
            }
        }

        private void insertChildNode() {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            XmlElement xmlElement = treeNode.Tag as XmlElement;
            if (xmlElement == null)
                return;


            if ( xmlElement.NodeType == XmlNodeType.Element ) {
                XmlElement newElement = document.CreateElement("New");              
                xmlElement.AppendChild(newElement);

                TreeNode newTreeNode = treeNode.Nodes.Insert(0, newElement.Name);
                newTreeNode.ImageIndex = 1;
                newTreeNode.SelectedImageIndex = 1;
                newTreeNode.Tag = newElement;                
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // should be checked by regular expression according to the xml specification:]
            // http://www.w3.org/TR/2008/REC-xml-20081126/#charsets 


            if ( e.Label == null ||  e.Label.Trim() == ""  ) {
                e.CancelEdit = true;
                return;
            }

            XmlElement xmlElement = e.Node.Tag as XmlElement;
            
            if (xmlElement == null) {
                e.CancelEdit = true;
                return;
            }

            renameXmlElement(xmlElement,e.Label);
            e.Node.Tag = xmlElement;
        }


        protected override void OnClosing(CancelEventArgs e) {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode != null) {
                selection = GenerateXPath(treeNode);
            }
                
            base.OnClosing(e);
        }

        private void insertNodeToolStripMenuItem_Click(object sender, EventArgs e) {
            insertNode();
        }

        private void inserChildNodeToolStripMenuItem_Click(object sender, EventArgs e) {
            insertChildNode();
        }

        private void deleteNodeToolStripMenuItem_Click(object sender, EventArgs e) {
            deleteNode();
        }

        private void editNodeToolStripMenuItem_Click(object sender, EventArgs e) {
            editNode();
        }

    }
}

