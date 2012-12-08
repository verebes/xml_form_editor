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
            splitContainer1.Panel2Collapsed = true;            

            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            treeView1.DragDrop += new DragEventHandler(treeView1_DragDrop);
            treeView1.ItemDrag += new ItemDragEventHandler(treeView1_ItemDrag);
            treeView1.DragOver += new DragEventHandler(treeView1_DragOver);            

            ImageList iconList = new ImageList();
            iconList.Images.Add(Icons.attribute);
            iconList.Images.Add(Icons.node);
            iconList.Images.Add(Icons.text);
            iconList.Images.Add(Icons.document);
            iconList.Images.Add(Icons.node_text);
            
            treeView1.ImageList = iconList;
            treeView1.LabelEdit = true;
            treeView1.AllowDrop = true;
            updateTree();
        }        

        void treeView1_DragOver(object sender, DragEventArgs e) {
            Point p = new Point(e.X, e.Y);
            Point pLocal = PointToClient(p);
            TreeNode node = treeView1.GetNodeAt(pLocal.X, pLocal.Y);
                        
            e.Effect = DragDropEffects.None;
            if ( node == null) {
                return;
            }
        
            // node can not be moved under it's child nodes
            // so we check if the node we drag is an ascendant of the target node
            TreeNode tmp = node;
            bool descendant = false;
            while (!descendant && tmp != null) {
                descendant = (draggedNode == tmp);
                tmp = tmp.Parent;
            }
            if (!descendant && 
                node.Tag != null && node.Tag is XmlElement &&
                draggedNode.Tag != null && draggedNode.Tag is XmlElement
                ) {
                e.Effect = DragDropEffects.Move;            
            }
            return;
        }


        void treeView1_DragDrop(object sender, DragEventArgs e) {
            Point p = new Point(e.X, e.Y);
            Point pLocal = PointToClient(p);
            TreeNode node = treeView1.GetNodeAt(pLocal.X, pLocal.Y);
            if (node == null) {
                return;
            }
            if (draggedNode == null) {
                return;
            }

            if ( draggedNode == treeView1.TopNode )  {
                return;
            }

            move(draggedNode, node);
        }

        private void  move(TreeNode node, TreeNode newParent) {

            XmlElement xmlElement = node.Tag as XmlElement;
            XmlElement newParentXmlNode = newParent.Tag as XmlElement;

            if (xmlElement == null || newParentXmlNode == null) {
                return;
            }

            XmlElement newElement = document.CreateElement(xmlElement.Name);
            while (xmlElement.HasChildNodes) {
                newElement.AppendChild(xmlElement.FirstChild);
            }

            while (xmlElement.HasAttributes ) {
                newElement.Attributes.Append(xmlElement.Attributes[0]);
            }

            node.Tag = newElement;
            node.Parent.Nodes.Remove(node);
            newParent.Nodes.Add(node);
            xmlElement.ParentNode.RemoveChild(xmlElement);
            newParentXmlNode.AppendChild(newElement);            
        }

        TreeNode draggedNode = null;
        void treeView1_ItemDrag(object sender, ItemDragEventArgs e) {
            draggedNode = e.Item as TreeNode;
            DoDragDrop(e.Item.ToString(), DragDropEffects.Move);
            if (draggedNode == null)
                return;            
        }


        private bool hasChildElement(XmlNode node)
        {            
            foreach ( XmlNode child in node.ChildNodes   )
            {
                if (child.NodeType != XmlNodeType.Text)
                    return true;
            }
            return false;

        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;

            XmlElement xmlElement = node.Tag as XmlElement;
            if (xmlElement == null || hasChildElement(xmlElement)) {
                splitContainer1.Panel2Collapsed = true;
                textBox1.Text = "";
            } else {
                splitContainer1.Panel2Collapsed = false;
                textBox1.Text = xmlElement.InnerText;                
            }

        }

        private string GenerateXPath(TreeNode node) {
            selection = "";
            TreeNode common = commonAscendant(node, markedNode);
            while (node != null && node != common) {
                if (node.Tag == null) {
                    //attribute's text case
                } else if (node.Tag is XmlAttribute) {
                    selection = "/@" + node.Text + selection;
                } else if (node.Tag is XmlElement) {
                    int index = 0;
                    int count = 0;
                    NodeIndexForXPath(node.Tag as XmlElement, ref count, ref index);
                    if (count < 2) {
                        selection = "/" + node.Text + selection;
                    } else {
                        selection = "/" + node.Text + "[" + index.ToString() + "]" + selection;
                    }
                }

                node = node.Parent;
            }

            if ( markedNode != null && common == markedNode) {
                selection = selection.TrimStart('/');
            }

            node = markedNode;
            string pathBack = "";
            while (node != null && node != common)
            {
                pathBack += "../";
                node = node.Parent;
            }
            pathBack = pathBack.TrimEnd('/');
            selection = pathBack + selection;

            if (selection == "") {
                selection = ".";
            }

            return selection;
        }


        private TreeNode commonAscendant(TreeNode nodeA, TreeNode nodeB) {
            List<TreeNode> pathToNodeA = new List<TreeNode>();
            TreeNode tmpA = nodeA;            
            while (tmpA != null) {
                TreeNode tmpB = nodeB;
                while (tmpB != null) {
                    if (tmpA == tmpB) {
                        return tmpA;
                    }
                    tmpB = tmpB.Parent;
                }
                tmpA = tmpA.Parent;
            }
            return null;
        }

        private void NodeIndexForXPath(XmlElement element, ref int count, ref int index) {
            count = 0;
            index = -1;

            XmlNode parent = element.ParentNode;
            if (parent == null) {
                //might be the parent node
                return;
            }
            
            foreach ( XmlNode child in parent.ChildNodes )
            {
                if (child.Name == element.Name) {
                    count++;
                }
                if (child == element) {
                    index = count;
                }
            }
            return;
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

        public XmlNode selectNodeByXPath(string xpathExpression)
        {
            return selectNodeByXPath(xpathExpression, document);
        }

        public XmlNode selectNodeByXPath(string xpathExpression, XmlNode node)
        {
            if (node == null)
                return null;

            try {                
                XmlNode selectedNode = node.SelectSingleNode(xpathExpression);

                TreeNode treeNode = getTreeNode(selectedNode);
                if (treeNode == null)
                    return null;

                treeView1.SelectedNode = treeNode;
                treeNode.ExpandAll();
                return selectedNode;
            } catch (System.Xml.XmlException e) {
                return null;
            } catch (System.Xml.XPath.XPathException e) {
                return null;
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

        private TreeNode markedNode;
        private Color prevMarkedColor;

        public void markNode() {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            if (treeNode == markedNode)
                return;

            if (markedNode != null) {
                markedNode.BackColor = prevMarkedColor;
            }
            
            prevMarkedColor = treeNode.BackColor;
            treeNode.BackColor = Color.Yellow;

            markedNode = treeNode;
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


        private class TreeIcon
        {
            public const int Attribute =0 ;
            public const int Node = 1;
            public const int Text = 2;
            public const int Document = 3;
            public const int NodeText = 4;
        };


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
            rootNode.ImageIndex = TreeIcon.Document;
            rootNode.SelectedImageIndex = TreeIcon.Document;            

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
                        treeNode.ImageIndex = TreeIcon.Attribute;
                        treeNode.SelectedImageIndex = TreeIcon.Attribute;
                        treeNode.Tag = item.xmlNode;

                        
                        TreeNode value = treeNode.Nodes.Add(item.xmlNode.Value);
                        
                        value.ImageIndex = TreeIcon.Text;
                        value.SelectedImageIndex = TreeIcon.Text;
                        value.Tag = null;

                        break;
                    case XmlNodeType.Element:
                        treeNode = item.treeNode.Nodes.Insert(0,item.xmlNode.Name);
                        treeNode.ImageIndex = TreeIcon.Node;
                        treeNode.SelectedImageIndex = TreeIcon.Node;
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

            if (e.KeyCode == Keys.Insert && e.Shift) {
                insertAttribute();
            }

            if (e.KeyCode == Keys.Insert)
            {
                insertNode();
            }

            if ( e.KeyCode == Keys.F2 )
            {
                editNode();
            }

            if (e.KeyCode == Keys.F3) {
                markNode();
            }

            if (e.KeyCode == Keys.Delete)
            {
                deleteNode();
            }

        }

        private XmlElement renameXmlElement(XmlElement xmlElement, string label)
        {            
            XmlElement newElement = document.CreateElement(label);
            while ( xmlElement.HasChildNodes ) {
                newElement.AppendChild(xmlElement.FirstChild);
            }
            while ( xmlElement.HasAttributes) {
                newElement.Attributes.Append( xmlElement.Attributes[0]);
            }

            if (xmlElement.ParentNode == null) {
                document.ReplaceChild(newElement, document.FirstChild);
            } else {
                xmlElement.ParentNode.ReplaceChild(newElement, xmlElement);                
            }

            return newElement;
        }

        private void deleteNode()
        {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            XmlElement xmlElement = treeNode.Tag as XmlElement;
            if (xmlElement == null)
                return;

            if (treeNode == treeView1.TopNode) {
                MessageBox.Show("Can't delete the root node", "Error", MessageBoxButtons.OK);               
                return;
            }

            const string question = "Do you really want to delete the selected node and all of it's descendants?";
            const string caption = "Confirm delete node";
            if ( MessageBox.Show(question,caption, MessageBoxButtons.YesNo) == DialogResult.Yes )
            {
                treeNode.Remove();
                xmlElement.RemoveAll();
                xmlElement.ParentNode.RemoveChild(xmlElement);
            }            
        }

        private void editNode() {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            XmlElement xmlElement = treeNode.Tag as XmlElement;

            // xmlElement is null if we edit the value of an attribute
            if (xmlElement == null ||
                xmlElement.NodeType == XmlNodeType.Element ||
                xmlElement.NodeType == XmlNodeType.Attribute )
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
                newTreeNode.ImageIndex = TreeIcon.Node;
                newTreeNode.SelectedImageIndex = TreeIcon.Node;
                newTreeNode.Tag = newElement;

                treeView1.SelectedNode = newTreeNode;
                newTreeNode.BeginEdit();
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
                XmlNode newChildNode = xmlElement.AppendChild(newElement);

                TreeNode newTreeNode = treeNode.Nodes.Insert(0, newElement.Name);
                newTreeNode.ImageIndex = TreeIcon.Node;
                newTreeNode.SelectedImageIndex = TreeIcon.Node;
                newTreeNode.Tag = newChildNode;

                treeView1.SelectedNode = newTreeNode;
                newTreeNode.BeginEdit();
            }
        }

        private void insertAttribute() {
            TreeNode treeNode = treeView1.SelectedNode;
            if (treeNode == null)
                return;

            XmlElement xmlElement = treeNode.Tag as XmlElement;
            if (xmlElement == null)
                return;


            if (xmlElement.NodeType == XmlNodeType.Element) {
                XmlAttribute newAttribute = document.CreateAttribute("new");
                xmlElement.Attributes.Append(newAttribute);                

                TreeNode newTreeNode = treeNode.Nodes.Insert(0, newAttribute.Name);
                newTreeNode.ImageIndex = TreeIcon.Attribute;
                newTreeNode.SelectedImageIndex = TreeIcon.Attribute;
                newTreeNode.Tag = newAttribute;

                TreeNode value = newTreeNode.Nodes.Add(newAttribute.Value);
                value.ImageIndex = TreeIcon.Text;
                value.SelectedImageIndex = TreeIcon.Text;
                value.Tag = null;
                
                treeView1.SelectedNode = newTreeNode;
                newTreeNode.BeginEdit();
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

                XmlAttribute xmlAttribute = e.Node.Tag as XmlAttribute;                
                if (xmlAttribute != null) {
                    //we edited an attributes's name
                    if ( e.Label == xmlAttribute.Name ) {
                        // not really renamed
                        e.CancelEdit = true;
                        return;
                    }
                    // here we should check if it is a duplicated attriubute name

                    XmlAttribute newAttribute =  document.CreateAttribute(e.Label);
                    newAttribute.Value = xmlAttribute.Value;                                        
                    xmlAttribute.OwnerElement.Attributes.InsertBefore(newAttribute, xmlAttribute);
                    xmlAttribute.OwnerElement.Attributes.Remove(xmlAttribute);
                    e.Node.Tag = newAttribute;
                    return;
                }

                if (e.Node.Parent == null) { // error case
                    e.CancelEdit = true;
                    return;
                }                
                
                xmlAttribute = e.Node.Parent.Tag as XmlAttribute;
                if (xmlAttribute != null) {
                    // we edited an attribute's value
                    xmlAttribute.Value = e.Label;
                    return;
                }

                e.CancelEdit = true;                
                return;
            }
            
            XmlElement renamedElement = renameXmlElement(xmlElement,e.Label);
            if (renamedElement.ParentNode == null) {
                e.Node.Tag = document.FirstChild; // in case we edited the topmost node
            } else {
                e.Node.Tag = renamedElement;
            }
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

        private void textBox1_TextChanged(object sender, EventArgs e) {
            if (treeView1.SelectedNode == null)
                return;

            XmlElement xmlElement = treeView1.SelectedNode.Tag as XmlElement;
            if (xmlElement == null)
                return;
            
            if ( hasChildElement(xmlElement))
                return;
            
            xmlElement.InnerText = textBox1.Text;
        }

        private void XMLTreeDialog_Shown(object sender, EventArgs e) {
            treeView1.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Control | Keys.Right:
                    if ( !splitContainer1.Panel2Collapsed )
                        textBox1.Focus();    
                    return true;
                case Keys.Control | Keys.Left:
                    treeView1.Focus();    
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static string cutLastBracketIfExists(string str)
        {
            string result = str;
            int lastBracket = result.LastIndexOf('[');
            int lastSlash = result.LastIndexOf('/');
            if (lastBracket > lastSlash)
            {
                result = result.Substring(0, lastBracket);
            }
            return result;
        }


        public static string between(string before, string str, string after)
        {
            int bl = before.Length;
            int al = after.Length;
            int sl = str.Length;


            if (sl > (bl + al) &&
                str.Substring(0, bl).ToLower() == before.ToLower() &&
                str.Substring(sl - al, al).ToLower() == after.ToLower())
            {
                return str.Substring(bl, sl - (bl + al));
            }
            return "";
        }

    }
}

