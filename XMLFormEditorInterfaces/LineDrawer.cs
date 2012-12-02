using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;

namespace XMLFormEditor
{
    public class LineDrawer
    {
        public class Junction {
            public class Type : ICloneable
            {
                public Type(bool up, bool down, bool left, bool right )
                {
                    this.up = up;
                    this.down = down;
                    this.left = left;
                    this.right = right;
                }

                public Object Clone() {
                    Type t = new Type(up, down, left, right);
                    return t;
                }

                public bool up = false;
                public bool down = false;
                public bool left = false;
                public bool right = false;

                public bool isValid()
                {
                    return up || down || left || right;
                }

                public static Type Invalid = new Type(false, false, false, false);
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

            public Junction() {
                type = Type.Invalid.Clone() as Type;
                this.position = new Point(0, 0);
            }
            public Junction(Type type, Point point) {
                this.type = type.Clone() as Type;
                this.position = new Point(point.X, point.Y);
            }

            public Junction Union(Junction junction){
                type.left = type.left | junction.type.left;
                type.right = type.right | junction.type.right;
                type.up = type.up | junction.type.up;
                type.down = type.down | junction.type.down;

                return this;
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


            //Line point distance
            public double disctance( Point p ) 
            {
                PointF pm;
                if (p1.X == p2.X && p1.Y == p2.Y ) {
                    pm = p1;                    
                } else {
                    PointF v = new PointF(p2.X - p1.X, p2.Y - p1.Y);
                    PointF vv = new PointF(v.Y, - v.X);
                    PointF u = new PointF(p.X - p1.X, p.Y - p1.Y);

                    float s = (u.X * v.Y - u.Y * v.X) / ( v.X*vv.Y - vv.X * v.Y );


                    pm = new PointF();
                    if (v.X != 0) {
                        float t = (u.X + s * vv.X) / v.X;

                        pm.X = p1.X + t * v.X;
                        pm.Y = p1.Y + t * v.Y;
                    } else {
                        pm.X = p.X + s * vv.X;
                        pm.Y = p.Y + s * vv.Y;
                    }
                }

                double x = pm.X - p.X;
                double y = pm.Y - p.Y;
                double d = x*x + y*y;
                return Math.Sqrt( d  );
            }            
        }


        public LineDrawer()
        {
            UpdateSectionList();
        }


        List<Junction> junctions = new List<Junction>();
        List<HalfSection> halfSections = new List<HalfSection>();
        List<Section> sections = new List<Section>();        

        public void AddJunction(Junction junction) { 
            AddJunction(junction, false);
        }

        public void AddJunction( Junction junction, bool unionJunction) {

            List<Junction> toDelete = new List<Junction>();
            foreach ( Junction j in junctions)
            {
                if ( j.position == junction.position) {
                    toDelete.Add(j);
                }
            }


            Junction jUnioun = new Junction(junction.type, junction.position);
            foreach (Junction j in toDelete) {
                jUnioun = jUnioun.Union(j);
            }


            foreach ( Junction j in toDelete) 
            {
                junctions.Remove(j);
            }

            if (junction.type != Junction.Type.Invalid)
            {
                if (unionJunction) {                    
                    junctions.Add(jUnioun);
                } else {
                    junctions.Add(junction);
                }
            }
        }


        public void AddRectange(Rectangle r)
        {                        
            AddJunction(new Junction(Junction.Type.Cross, new Point(r.Location.X, r.Location.Y)),true);
            AddJunction(new Junction(Junction.Type.Cross, new Point(r.Location.X + r.Width, r.Location.Y)), true);
            AddJunction(new Junction(Junction.Type.Cross, new Point(r.Location.X, r.Location.Y + r.Height)), true);
            AddJunction(new Junction(Junction.Type.Cross, new Point(r.Location.X + r.Width, r.Location.Y + r.Height)), true);
        }

        public void RemoveJunction ( Junction junction )
        {
            // when we add an invalid junction to a position it will remove all junctions from there
            Junction j = new Junction(Junction.Type.Invalid, junction.position);
            AddJunction(j);
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
                    Section newSection = new Section(closestHalfSection.p, halfSection.p);
                    sections.Add(newSection);
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
                if (w.x * v.y - w.y * v.x != 0)
                    continue;

                // Junction is on the line of the half section
                
                double lengthW = Math.Sqrt(w.x * w.x + w.y*w.y);
                Vector2d normalizedW = new Vector2d(w.x / lengthW, w.y / lengthW );
                if (normalizedW.x != halfSection.vector.x ||
                     normalizedW.y != halfSection.vector.y)
                    continue;
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


        public bool getSmallestBoundingRectangle( Point p, ref Rectangle rect  ) {
            List<Section> leftSections = new List<Section>();
            List<Section> rightSections = new List<Section>();
            List<Section> upSections = new List<Section>();
            List<Section> downSections = new List<Section>();


            UpdateSectionList();

            foreach ( Section section in sections)
            {
                if (section.p1.X == section.p2.X) {
                    //vertical section
                    int x = section.p1.X;
                    int up = Math.Min(section.p1.Y, section.p2.Y);
                    int down = Math.Max(section.p1.Y, section.p2.Y);

                    if (p.Y < up || p.Y > down)
                        continue;

                    if (x > p.X ) {
                        rightSections.Add(section);
                    } else {
                        leftSections.Add(section);
                    }

                    continue;
                }

                if (section.p1.Y == section.p2.Y) {
                    //horizontal section 
                    int y = section.p1.Y;
                    int left = Math.Min(section.p1.X, section.p2.X);
                    int right = Math.Max(section.p1.X, section.p2.X);

                    if (p.X < left || p.X > right)
                        continue;

                    if (y < p.Y) {
                        upSections.Add(section);
                    } else {                        
                        downSections.Add(section);
                    }

                    continue;
                }                

                // this should not happen since we have only horizontal and vertical sections
                return false;
            }            

            SectionPointDistanceComparer comparer = new SectionPointDistanceComparer(p);
            upSections.Sort(comparer);
            downSections.Sort(comparer);
            leftSections.Sort(comparer);
            rightSections.Sort(comparer);

            //bool found = false;
            //Section[] upV =  upSections.ToArray();
            //Section[] downV = downSections.ToArray();
            //Section[] leftSectionsV = leftSections.ToArray();
            //Section[] rightV = rightSections.ToArray();

            if (upSections.Count == 0 || downSections.Count == 0 || leftSections.Count == 0 || rightSections.Count == 0)
                return false;

            Section u = upSections[0];
            Section d = downSections[0];
            Section l = leftSections[0];
            Section r = rightSections[0];

            int[] xx = { u.p1.X, u.p2.X, l.p1.X, l.p2.X, d.p1.X, d.p2.X, r.p1.X, r.p2.X };
            int[] yy = { d.p1.Y, d.p2.Y, l.p1.Y, l.p2.Y, r.p1.Y, r.p2.Y, u.p1.Y, u.p2.Y };
            int minX = xx[0];
            int minY = yy[0];
            int maxX = xx[0];
            int maxY = yy[0];
            for (int i = 1; i < xx.Length; ++i) {
                if (xx[i] < minX)
                    minX = xx[i];

                if (xx[i] > maxX)
                    maxX = xx[i];

                if (yy[i] < minY)
                    minY = yy[i];

                if (yy[i] > maxY)
                    maxY = yy[i];
            }
            

            rect.X = minX;
            rect.Y = minY;
            rect.Width = maxX - minX;
            rect.Height = maxY - minY;

            rect.X += 1;
            rect.Y += 1;
            rect.Width -= 2;
            rect.Height -= 2;

            return true;
        }

        class SectionPointDistanceComparer: IComparer<Section>
        {

            private Point p;
            public SectionPointDistanceComparer(Point p)
            {
                this.p = p;
            }
            public int Compare(Section x, Section y) {
                if (x == y)
                    return 0;

                double dx = x.disctance(p);
                double dy = y.disctance(p);

                return  Math.Sign( dx - dy );
            }
        }


        public XmlElement serializeToXml(XmlDocument document) {

            XmlElement junctionElement = document.CreateElement("Junctions");            

            foreach ( Junction j in junctions)
            {
                XmlElement element = document.CreateElement("Junction");
                String type = String.Format("{0},{1},{2},{3}", j.type.up, j.type.down, j.type.left, j.type.right);                
                element.SetAttribute("Type", type);
                element.SetAttribute("X", j.position.X.ToString());
                element.SetAttribute("Y", j.position.Y.ToString());
                junctionElement.AppendChild(element);
            }
            return junctionElement;
        }

        public void deserializeFromXml(XmlNode element) {
            foreach (XmlNode node in element.ChildNodes) {

                Junction j = new Junction();
                
                String type = node.Attributes["Type"].Value;
                String[] s = type.Split(',');
                Junction.Type t = new Junction.Type(
                    Convert.ToBoolean(s[0]),
                    Convert.ToBoolean(s[1]),
                    Convert.ToBoolean(s[2]),
                    Convert.ToBoolean(s[3]));

                j.type = t;
                j.position.X = Convert.ToInt32(node.Attributes["X"].Value);
                j.position.Y = Convert.ToInt32(node.Attributes["Y"].Value);

                junctions.Add(j);
            }
            UpdateSectionList();
        }
    }
}
