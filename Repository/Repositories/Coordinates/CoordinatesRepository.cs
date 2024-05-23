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

        public async Task AddPoint(Point point)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePoint(int X, int Y)
        {
            var point = await _context.Points.SingleOrDefaultAsync(p => p.X == X && p.Y == Y);

            if (point == null)
            {
                return false;
            }

            _context.Points.Remove(point);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task ImportPoints(IEnumerable<Point> points)
        {
            _context.Points.RemoveRange(_context.Points);

            await _context.Points.AddRangeAsync(points);
            await _context.SaveChangesAsync();
        }
    }
}
