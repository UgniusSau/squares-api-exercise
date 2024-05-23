using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.DB.CoordinatesDB
{
    public class CoordinatesDBContext : DbContext
    {
        public CoordinatesDBContext(DbContextOptions<CoordinatesDBContext> options) : base(options)
        {
        }

        public DbSet<Point> Points { get; set; }
    }
}
