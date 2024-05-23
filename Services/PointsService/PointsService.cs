using Data.DB.CoordinatesDB;
using Data.DTOs;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PointsService
{
    public class PointsService : IPointsService
    {
        private readonly ICoordinatesRepository _coordinatesRepository;

        public PointsService(ICoordinatesRepository coordinatesRepository)
        {
            _coordinatesRepository = coordinatesRepository;
        }

        public async Task<IEnumerable<Point>> GetPoints()
        {
            return await _coordinatesRepository.GetPoints();
        }

        public async Task AddPoint(PointDTO pointDto)
        {
            var existingPoints = await _coordinatesRepository.GetPoints();
            if (existingPoints.Any(p => p.X == pointDto.X && p.Y == pointDto.Y))
            {
                throw new Exception("Point already exists");
            }

            var newPoint = new Point { X = pointDto.X, Y = pointDto.Y };
            await _coordinatesRepository.AddPoint(newPoint);
        }

        public async Task<bool> DeletePoint(PointDTO pointDto)
        {
            return await _coordinatesRepository.DeletePoint(pointDto.X, pointDto.Y);
        }

        public async Task ImportPoints(IEnumerable<PointDTO> pointsDto)
        {
            if (pointsDto == null || !pointsDto.Any())
            {
                throw new Exception("Incorrect parameter");
            }

            var duplicates = pointsDto.GroupBy(p => new { p.X, p.Y })
                                     .Where(g => g.Count() > 1)
                                     .Select(g => g.Key)
                                     .ToList();

            if (duplicates.Count != 0)
            {
                throw new Exception("Duplicate found");
            }

            var points = pointsDto.Select(p => new Point { X = p.X, Y = p.Y });
            await _coordinatesRepository.ImportPoints(points);
        }
    }
}
