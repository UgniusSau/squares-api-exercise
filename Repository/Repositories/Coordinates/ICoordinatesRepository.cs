using Data.DB.CoordinatesDB;
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
    }
}
