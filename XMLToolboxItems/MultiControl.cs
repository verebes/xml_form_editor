using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class MultiControl: UserControl
    {
        public Label label;
        public TextBox textBox;
        public ComboBox comboBox;

        public MultiControl()
        {
            label = new Label();
            label.Parent = this;
            label.Left = 0;
            label.Top = 0;
            label.Width = Width;
            label.Height = Height;
            label.Visible = true;

            textBox = new TextBox();
            textBox.Parent = this;
            textBox.Left = 0;
            textBox.Top = 0;
            textBox.Width = Width;
            textBox.Height = Height;
            textBox.Visible = false;

            comboBox = new ComboBox();
            comboBox.Parent = this;
            comboBox.Left = 0;
            comboBox.Top = 0;
            comboBox.Width = Width;
            comboBox.Height = Height;
            comboBox.Visible = false;

            MinimumSize = new Size(0, comboBox.Height);            


            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            label.Width = Width;
            label.Height = Height;

            textBox.Width = Width;
            textBox.Height = Height;

            comboBox.Width = Width;
            comboBox.Height = Height;
        }

        public enum ControlType { Label, TextBox, ComboBox }
        
        private ControlType _controlType;
        public ControlType Type
        {
            set { 
                if (_controlType == value)
                    return;

                _controlType = value;

                label.Hide();
                textBox.Hide();
                comboBox.Hide();
                
                switch (_controlType)
                { 
                    case ControlType.Label:
                        label.Show();
                        break;
                    case ControlType.TextBox:
                        textBox.Show();
                        break;
                    case ControlType.ComboBox:
                        comboBox.Show();
                        break;
                }
            }
            get { return _controlType;}
        }

        public override string Text
        {
            get
            {
                switch (_controlType)
                {
                    case ControlType.Label:
                        return label.Text;                        
                    case ControlType.TextBox:
                        return textBox.Text;                        
                    case ControlType.ComboBox:
                        return comboBox.Text;
                }

                return base.Text;
            }
            set
            {
                base.Text = value;
                label.Text = value;
                textBox.Text = value;
                comboBox.Text = value;
            }
        }
    }
}
