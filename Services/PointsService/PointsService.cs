using Data.DB.CoordinatesDB;
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
    }
}
