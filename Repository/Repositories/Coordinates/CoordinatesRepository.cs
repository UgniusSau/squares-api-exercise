using Microsoft.EntityFrameworkCore;
using Data.DB.CoordinatesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Repository.Repositories.Coordinates
{
    public class CoordinatesRepository : ICoordinatesRepository
    {
        private readonly CoordinatesDBContext _context;

        public CoordinatesRepository(CoordinatesDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Point>> GetPoints(CancellationToken cancellationToken)
        {
            return await _context.Points.ToListAsync(cancellationToken);
        }

        public async Task AddPoint(Point point, CancellationToken cancellationToken)
        {
            _context.Points.Add(point);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeletePoint(int X, int Y, CancellationToken cancellationToken)
        {
            var point = await _context.Points.SingleOrDefaultAsync(p => p.X == X && p.Y == Y, cancellationToken);

            if (point == null)
            {
                return false;
            }

            _context.Points.Remove(point);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task ImportPoints(IEnumerable<Point> points, CancellationToken cancellationToken)
        {
            _context.Points.RemoveRange(_context.Points);

            await _context.Points.AddRangeAsync(points, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
