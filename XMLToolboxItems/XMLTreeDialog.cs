using System;
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
            updateTree();
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            selection = "";
            while (node != null)
            {
                selection = node.Text + "/" + selection;
                node = node.Parent;
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

            TreeNode rootNode = treeView1.Nodes.Add("/");            
            stack.Push(new StackItem( rootNode, document ));

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
                        treeNode.Nodes.Add(item.xmlNode.Value);
                        break;
                    case XmlNodeType.Element:
                        treeNode = item.treeNode.Nodes.Insert(0,item.xmlNode.Name);
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

    }
}
