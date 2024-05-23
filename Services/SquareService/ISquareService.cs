using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SquareService
{
    public interface ISquareService
    {
        Task<int> DetectSquares(CancellationToken cancellationToken);
    }
}
