using Data.DB.CoordinatesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Shape.Square
{
    public class Square
    {
        public List<Point> Points { get; set; }

        public Square(List<Point> points)
        {
            Points = points;
        }

        public override string ToString()
        {
            return string.Join(", ", Points);
        }

        public string CanonicalForm()
        {
            var sortedPoints = Points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            return string.Join(";", sortedPoints.Select(p => $"{p.X},{p.Y}"));
        }

        public override bool Equals(object? obj)
        {
            if (obj is Square other)
            {
                return CanonicalForm() == other.CanonicalForm();
            }
            return false;
        }

        public override int GetHashCode()
        {
            return CanonicalForm().GetHashCode();
        }
    }
}
