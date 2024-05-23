using Microsoft.EntityFrameworkCore;
using Data.DB.CoordinatesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Coordinates
{
    public class CoordinatesRepository : ICoordinatesRepository
    {
        private readonly CoordinatesDBContext _context;

        public CoordinatesRepository(CoordinatesDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Point>> GetPoints()
        {
            return await _context.Points.ToListAsync();
        }
    }
}
