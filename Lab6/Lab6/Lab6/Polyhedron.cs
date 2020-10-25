﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Polyhedron
    {
        public List<Polygon> Faces { get; set; } = null;
        public Point3D Center { get; set; } = new Point3D(0, 0, 0);
        public Polyhedron(List<Polygon> fs = null)
        {
            if (fs != null)
            {
                Faces = fs.Select(face => new Polygon(face)).ToList();
                find_center();
            }
        }

        public Polyhedron(Polyhedron polyhedron)
        {
            Faces = polyhedron.Faces.Select(face => new Polygon(face)).ToList();
            Center = new Point3D(polyhedron.Center);
        }

        private void find_center()
        {
            Center.X = 0;
            Center.Y = 0;
            Center.Z = 0;
            foreach (Polygon f in Faces)
            {
                Center.X += f.Center.X;
                Center.Y += f.Center.Y;
                Center.Z += f.Center.Z;
            }
            Center.X /= Faces.Count;
            Center.Y /= Faces.Count;
            Center.Z /= Faces.Count;
        }

        public void show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            foreach (Polygon f in Faces)
                f.show(g, pr, pen);
        }

        public void make_hexahedron(float cube_half_size = 50)
        {
            Polygon f = new Polygon(
                new List<Point3D>
                {
                    new Point3D(-cube_half_size, cube_half_size, cube_half_size),
                    new Point3D(cube_half_size, cube_half_size, cube_half_size),
                    new Point3D(cube_half_size, -cube_half_size, cube_half_size),
                    new Point3D(-cube_half_size, -cube_half_size, cube_half_size)
                }
            );


            Faces = new List<Polygon> { f };

            List<Point3D> l1 = new List<Point3D>();
            foreach (var point in f.Points)
            {
                l1.Add(new Point3D(point.X, point.Y, point.Z - 2 * cube_half_size));
            }
            Polygon f1 = new Polygon(
                    new List<Point3D>
                    {
                        new Point3D(-cube_half_size, cube_half_size, -cube_half_size),
                        new Point3D(-cube_half_size, -cube_half_size, -cube_half_size),
                        new Point3D(cube_half_size, -cube_half_size, -cube_half_size),
                        new Point3D(cube_half_size, cube_half_size, -cube_half_size)
                    });

            Faces.Add(f1);

            List<Point3D> l2 = new List<Point3D>
            {
                new Point3D(f.Points[2]),
                new Point3D(f1.Points[2]),
                new Point3D(f1.Points[1]),
                new Point3D(f.Points[3]),
            };
            Polygon f2 = new Polygon(l2);
            Faces.Add(f2);

            List<Point3D> l3 = new List<Point3D>
            {
                new Point3D(f1.Points[0]),
                new Point3D(f1.Points[3]),
                new Point3D(f.Points[1]),
                new Point3D(f.Points[0]),
            };
            Polygon f3 = new Polygon(l3);
            Faces.Add(f3);

            List<Point3D> l4 = new List<Point3D>
            {
                new Point3D(f1.Points[0]),
                new Point3D(f.Points[0]),
                new Point3D(f.Points[3]),
                new Point3D(f1.Points[1])
            };
            Polygon f4 = new Polygon(l4);
            Faces.Add(f4);

            List<Point3D> l5 = new List<Point3D>
            {
                new Point3D(f1.Points[3]),
                new Point3D(f1.Points[2]),
                new Point3D(f.Points[2]),
                new Point3D(f.Points[1])
            };
            Polygon f5 = new Polygon(l5);
            Faces.Add(f5);

            find_center();
        }     
    }
}