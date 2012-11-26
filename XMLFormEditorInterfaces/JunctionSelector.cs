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
        
        public event System.EventHandler OnJunctionTypeSelected = delegate {
        };


        public void showSelector( Point junctionLocation ) {
            selectedJunction.position = junctionLocation;
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

            OnJunctionTypeSelected(this, null);
            Hide();
        }

        private void bDownRight_MouseUp(object sender, MouseEventArgs e)
        {            
            SelectJunction(GetChildAtPoint(e.Location));
        }

        private void bDownRight_MouseEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;
        }

    }
}