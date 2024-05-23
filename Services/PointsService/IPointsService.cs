using Data.DB.CoordinatesDB;
using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PointsService
{
    public interface IPointsService
    {
        Task<IEnumerable<Point>> GetPoints(CancellationToken cancellationToken);

        Task AddPoint(PointDTO pointDto, CancellationToken cancellationToken);

        Task<bool> DeletePoint(PointDTO pointDto, CancellationToken cancellationToken);

        Task ImportPoints(IEnumerable<PointDTO> points, CancellationToken cancellationToken);
    }
}
