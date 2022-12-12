using System;
using System.Diagnostics;

namespace Shapes
{
    public class Shape
    {
        protected double surfaceArea = 0.0; 

        public double GetArea()
        { 
            return surfaceArea;
        }

        public void WriteArea()
        {
            Console.WriteLine("Surface area is " + GetArea().ToString());
        }

        public virtual void WriteAll()
        {
            WriteArea();
        }
    }

    public class Circle : Shape
    {
        private double r = 0.0;

        public Circle(double radius)
        {
            Debug.Assert(radius > 0.0, "Radius should be greater than zero");
            r = radius;
            surfaceArea = Math.PI * r * r;
        }
        public double GetRadius()
        {
            return r;
        }

        public void WriteRadius()
        {
            Console.WriteLine("Radius is " + GetRadius().ToString());
        }

        public override void WriteAll()
        {
            base.WriteAll();
            WriteRadius();
        } 
    }
    

    public class Polygon : Shape
    {
        protected int sidesAmount = 0;
        protected double[] sides;
        protected double perimeter = 0.0;

        public Polygon(int sidesAmount, double[] args)
        {
            Debug.Assert(sidesAmount > 2, "Polygons contain at least 3 sides");
            Debug.Assert(args.Length >= sidesAmount, "Not each side has length"); // it's fine if more arguments are passed than necessary
            this.sidesAmount = sidesAmount;
            sides = new double[sidesAmount];
            for (int i = 0; i < sidesAmount; i++)
            {
                Debug.Assert(args[i] > 0.0, "Side " + i.ToString() + " has invalid length! Lengths should be greater than 0");
                sides[i] = args[i];
                perimeter += sides[i];
            }
        }

        public double[] GetSides()
        {
            return sides;
        }

        public void WriteSides()
        {
            string temp = "Side lengths are: ";
            foreach(int i in sides)
            {
                temp += sides[i].ToString();
                if (i<sides.Length-1)
                    temp += ", ";
            }
            Console.WriteLine(temp);
        }

        public override void WriteAll()
        {
            base.WriteAll();
            WriteSides();
        }

    }

    public class Triangle : Polygon
    {
        protected bool isOrthogonal = false;
        protected double largestSide = 0.0;
        public Triangle(double[] args) :  base (3, args)
        {
            surfaceArea = perimeter / 2;
            foreach (int i in sides)
            {
                surfaceArea *= (perimeter/2 - sides[i]);
                if (sides[i]>largestSide)
                {
                    largestSide = sides[i];
                }
            }
            surfaceArea = Math.Sqrt(surfaceArea);
            double tempLargestSide = largestSide * largestSide;  //temporary squaring it to check for orthogonality
            foreach (int i in sides)
            {
                if (sides[i] != tempLargestSide) // the check is vulnerable t
                {
                    tempLargestSide -= sides[i] * sides[i]; // reverse way of checking if c^2 = a^2 + b^2
                }
            }
            if (largestSide == 0.0) isOrthogonal = true;
        }

        public bool GetOrthogonal()
        {
            return isOrthogonal;
        }

        public double GetLargestSide()
        {
            return largestSide;
        }

        public void WriteOrthogonal()
        {
            if (isOrthogonal) 
                Console.WriteLine("The triangle is orthogonal");
            else 
                Console.WriteLine("The triangle is not orthogonal");
        }

        public void WriteLargestSide()
        {
            if (isOrthogonal)
                Console.WriteLine("The hypotenuse's length is " + GetLargestSide().ToString());
            else
                Console.WriteLine("The largest side's length is " + GetLargestSide().ToString());
        }

        public override void WriteAll()
        {
            base.WriteAll();
            WriteLargestSide();
            WriteOrthogonal();
        }
    }
}
