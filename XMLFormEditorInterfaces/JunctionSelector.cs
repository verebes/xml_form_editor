using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XMLFormEditor
{
    public partial class JunctionSelector : Form
    {

        public JunctionSelector() {
            InitializeComponent();
        }

        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parameters = base.CreateParams;
                if (OSFeature.IsPresent(SystemParameter.DropShadow))
                {
                    parameters.ClassStyle |= CS_DROPSHADOW;
                }
                return parameters;
            }
        }
        
        public event System.EventHandler OnJunctionTypeSelected = delegate {
        };


        public void showSelector( Point junctionLocation ) {
            this.Show();
            Location =  new Point(junctionLocation.X - bCross.Left - bCross.Width / 2, junctionLocation.Y - bCross.Top - bCross.Height / 2);
            selectedJunction.type = LineDrawer.Junction.Type.Invalid;
            Capture = true;
        }

        LineDrawer.Junction selectedJunction = new LineDrawer.Junction();
        public LineDrawer.Junction SelectedJunction {
            get {
                return selectedJunction;
            }
        }

        private void bLeft_Click(object sender, EventArgs e) {
            SelectJunction(sender);
        }

        private void SelectJunction(object sender)
        {
            selectedJunction.type = LineDrawer.Junction.Type.Invalid;
            if (sender == bLeft)
                selectedJunction.type = LineDrawer.Junction.Type.Left;
            if (sender == bRight)
                selectedJunction.type = LineDrawer.Junction.Type.Right;
            if (sender == bDown)
                selectedJunction.type = LineDrawer.Junction.Type.Down;
            if (sender == bUp)
                selectedJunction.type = LineDrawer.Junction.Type.Up;
            if (sender == bDownLeft)
                selectedJunction.type = LineDrawer.Junction.Type.DownLeft;
            if (sender == bDownRight)
                selectedJunction.type = LineDrawer.Junction.Type.DownRight;
            if (sender == bUpLeft)
                selectedJunction.type = LineDrawer.Junction.Type.UpLeft;
            if (sender == bUpRight)
                selectedJunction.type = LineDrawer.Junction.Type.UpRight;
            if (sender == bCross)
                selectedJunction.type = LineDrawer.Junction.Type.Cross;
            if (sender == bTLeft)
                selectedJunction.type = LineDrawer.Junction.Type.TLeft;
            if (sender == bTRight)
                selectedJunction.type = LineDrawer.Junction.Type.TRight;
            if (sender == bTDown)
                selectedJunction.type = LineDrawer.Junction.Type.TDown;
            if (sender == bTUp)
                selectedJunction.type = LineDrawer.Junction.Type.TUp;
            if (sender == bDelete)
                selectedJunction.type = LineDrawer.Junction.Type.Invalid;

            OnJunctionTypeSelected(this, null);
            Hide();
        }

        private void bDownRight_MouseUp(object sender, MouseEventArgs e)
        {            
            SelectJunction(GetChildAtPoint(e.Location));
        }
    }
}