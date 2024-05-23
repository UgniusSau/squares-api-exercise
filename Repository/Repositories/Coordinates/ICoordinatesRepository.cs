using Data.DB.CoordinatesDB;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Repositories.Coordinates
{
    public interface ICoordinatesRepository
    {
        Task<IEnumerable<Point>> GetPoints(CancellationToken cancellationToken);

        Task AddPoint(Point point, CancellationToken cancellationToken);

        Task<bool> DeletePoint(int X, int Y, CancellationToken cancellationToken);

        Task ImportPoints(IEnumerable<Point> points, CancellationToken cancellationToken);
    }
}
