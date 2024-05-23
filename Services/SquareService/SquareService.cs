using Data.DB.CoordinatesDB;
using Data.Models.Shape.Square;
using Repository.Repositories.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.SquareService
{
    public class SquareService : ISquareService
    {
        private readonly ICoordinatesRepository _coordinatesRepository;

        public SquareService(ICoordinatesRepository coordinatesRepository)
        {
            _coordinatesRepository = coordinatesRepository;
        }

        public async Task<int> DetectSquares()
        {
            var points = await _coordinatesRepository.GetPoints();

            if (!points.Any() || points.Count() < 4)
            {
                throw new Exception("Not enough points");
            }

            var squares = new HashSet<Square>();
            var pointSet = new HashSet<(int, int)>(points.Select(p => (p.X, p.Y)));

            for (int i = 0; i < points.Count(); i++)
            {
                for (int j = i + 1; j < points.Count(); j++)
                {
                    var p1 = points.ElementAt(i);
                    var p2 = points.ElementAt(j);

                    int dx = p2.X - p1.X;
                    int dy = p2.Y - p1.Y;

                    // Clockwise rotation
                    var p3a = (p1.X - dy, p1.Y + dx);
                    var p4a = (p2.X - dy, p2.Y + dx);

                    // Counter-Clockwise rotation
                    var p3b = (p1.X + dy, p1.Y - dx);
                    var p4b = (p2.X + dy, p2.Y - dx);

                    if (pointSet.Contains(p3a) && pointSet.Contains(p4a))
                    {
                        var square = new Square(new List<Point> { p1, p2, new Point { X = p3a.Item1, Y = p3a.Item2 }, new Point { X = p4a.Item1, Y = p4a.Item2 } });
                        squares.Add(square);
                    }

                    if (pointSet.Contains(p3b) && pointSet.Contains(p4b))
                    {
                        var square = new Square(new List<Point> { p1, p2, new Point{ X = p3b.Item1, Y = p3b.Item2 }, new Point{ X = p4b.Item1, Y = p4b.Item2 } });
                        squares.Add(square);
                    }
                }
            }

            return squares.Count;
        }
    }
}
