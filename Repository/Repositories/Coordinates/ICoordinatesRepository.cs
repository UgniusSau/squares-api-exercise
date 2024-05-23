using Data.DB.CoordinatesDB;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Coordinates
{
    public interface ICoordinatesRepository
    {
        Task<IEnumerable<Point>> GetPoints();

        Task AddPoint(Point point);

        Task<bool> DeletePoint(int X, int Y);

        Task ImportPoints(IEnumerable<Point> points);
    }
}
