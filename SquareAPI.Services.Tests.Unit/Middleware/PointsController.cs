using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareAPI.Services.Tests.Unit.Middleware
{
    [ApiController]
    [Route("api/test/v1/[controller]")]
    public class PointsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)  
        {
            await Task.Delay(6000, cancellationToken);
            return Ok();
        }
    }
}
