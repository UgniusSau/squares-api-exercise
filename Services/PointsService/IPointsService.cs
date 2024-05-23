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
        Task<IEnumerable<Point>> GetPoints();

        Task AddPoint(PointDTO pointDto);

        Task<bool> DeletePoint(PointDTO pointDto);

        Task ImportPoints(IEnumerable<PointDTO> points);
    }
}
