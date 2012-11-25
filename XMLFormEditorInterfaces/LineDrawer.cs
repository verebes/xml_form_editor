using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace XMLFormEditor
{
    public class LineDrawer
    {
        public class Junction {
            public class Type
            {
                public Type(bool up, bool down, bool left, bool right )
                {
                    this.up = up;
                    this.down = down;
                    this.left = left;
                    this.right = right;
                }

                public bool up = false;
                public bool down = false;
                public bool left = false;
                public bool right = false;

                public bool isValid()
                {
                    return up || down || left || right;
                }

                public static Type Up = new Type(true, false, false, false);
                public static Type Down = new Type(false, true, false, false);
                public static Type Left = new Type(false, false, true, false);
                public static Type Right = new Type(false, false, false, true);
                public static Type UpLeft = new Type(true, false, true, false);
                public static Type DownLeft = new Type(false, true, true, false);
                public static Type UpRight = new Type(true, false, false, true);
                public static Type DownRight = new Type(false, true, false, true);
                public static Type Cross = new Type(true, true, true, true);
                public static Type TUp = new Type(true, false, true, true);
                public static Type TDown = new Type(false, true, true, true);
                public static Type TLeft = new Type(true, true, true, false);
                public static Type TRight = new Type(true, true, false, true);
            }

            public Junction(Type type, Point point) {
                this.type = type;
                this.position = point;
            }

            public Type type;
            public Point position;
        }

        public class Vector2d 
        {
            public Vector2d( double x, double y ){
                this.x = x;
                this.y = y;
            }
            
            public double x;
            public double y;
        };

        public class HalfSection
        {
            public HalfSection ( Point p, Vector2d vector )
            {
                this.p = p;
                this.vector = vector;
            }
            public Point p;
            public Vector2d vector;
        }

        public class Section
        {
            public Section(Point p1, Point p2) 
            {
                this.p1 = p1;
                this.p2 = p2;
            }
            public Point p1;
            public Point p2;
        }


        public LineDrawer()
        {
            AddJunction(new Junction(Junction.Type.DownRight, new Point(0, 50)));
            AddJunction(new Junction(Junction.Type.UpLeft, new Point(120, 50)));
            AddJunction(new Junction(Junction.Type.DownRight, new Point(120, 0)));
            AddJunction(new Junction(Junction.Type.UpLeft, new Point(200, 100)));
            AddJunction(new Junction(Junction.Type.Cross, new Point(100, 100)));
            AddJunction(new Junction(Junction.Type.UpLeft, new Point(100, 200)));
            AddJunction(new Junction(Junction.Type.UpRight, new Point(0, 200)));
            AddJunction(new Junction(Junction.Type.DownLeft, new Point(200, 0)));

            AddJunction(new Junction(Junction.Type.TRight, new Point(0, 80)));
            AddJunction(new Junction(Junction.Type.DownLeft, new Point(100, 80)));


            //AddJunction(new Junction( Junction.Type.DownRight, new Point( 100,100) ));
            //AddJunction(new Junction(Junction.Type.DownLeft, new Point(200, 100)));
            //AddJunction(new Junction(Junction.Type.UpLeft, new Point(200, 200)));
            //AddJunction(new Junction(Junction.Type.UpRight, new Point(100, 200)));

            UpdateSectionList();
        }


        List<Junction> junctions = new List<Junction>();
        List<HalfSection> halfSections = new List<HalfSection>();
        List<Section> sections = new List<Section>();

        public void AddJunction( Junction junction) {

            List<Junction> toDelete = new List<Junction>();
            foreach ( Junction j in junctions)
            {
                if ( j.position == junction.position) {
                    toDelete.Add(j);
                }
            }
            foreach ( Junction j in toDelete) 
            {
                junctions.Remove(j);
            }
            
            junctions.Add(junction);
        }

        public void UpdateSectionList () {
            
            sections.Clear();

            FillUpHalfSections();

            foreach ( HalfSection halfSection in halfSections )
            {
                HalfSection closestHalfSection = null;
                bool found = false;
                findClosestJunction(halfSection, ref closestHalfSection, ref found);
                if ( found  ) {
                    sections.Add(new Section(closestHalfSection.p, halfSection.p));
                }
            }
        }

        private void findClosestJunction(HalfSection halfSection, ref HalfSection junction, ref bool found)
        {            
            double minDistant = Double.MaxValue;
            foreach (HalfSection j in halfSections)
            {
                if ( j.p == halfSection.p ) {
                    continue;
                }

                Vector2d w = new Vector2d(j.p.X - halfSection.p.X, j.p.Y - halfSection.p.Y);
                Vector2d v = halfSection.vector;
                if ( w.x * v.y - w.y * v.x == 0) {
                    // Junction is on the line of the half section
                    
                    double lengthW = Math.Sqrt(w.x * w.x + w.y*w.y);
                    Vector2d normalizedW = new Vector2d(w.x / lengthW, w.y / lengthW );
                    if ( normalizedW.x == halfSection.vector.x &&
                         normalizedW.y == halfSection.vector.y)
                    {
                        // we are on the good half of the line
                        {                            

                            found = true;

                            double a = (j.p.X - halfSection.p.X);
                            double b = (j.p.Y - halfSection.p.Y);
                            double distant = Math.Sqrt(a * a + b * b);
                            if (distant < minDistant
                                ||( distant == minDistant && 
                                (j.vector.x == halfSection.vector.x * -1
                                && j.vector.y == halfSection.vector.y * -1)))                                
                            {
                                // the two half section facing to each other has higher priority
                                // because there might be more than one halfsection starting from a point
                                minDistant = distant;
                                junction = j;
                            }
                        }
                    }
                }
            }

            // the two half section facing to each other
            found = found && (junction.vector.x == halfSection.vector.x * -1
            && junction.vector.y == halfSection.vector.y * -1);

        }

        private void FillUpHalfSections()
        {
            halfSections.Clear();

            foreach (Junction junction in junctions)
            {
                if (junction.type.up)
                {
                    halfSections.Add(new HalfSection(junction.position, new Vector2d(0, -1)));
                }

                if (junction.type.down)
                {
                    halfSections.Add(new HalfSection(junction.position, new Vector2d(0, 1)));
                }

                if (junction.type.right)
                {
                    halfSections.Add(new HalfSection(junction.position, new Vector2d(1, 0)));
                }

                if (junction.type.left)
                {
                    halfSections.Add(new HalfSection(junction.position, new Vector2d(-1, 0)));
                }
            }
        }

        public List<Section> getSectionList() {
            return sections;
        }

        public List<Junction> getJunctionList() {
            return junctions;
        }

    }
}
