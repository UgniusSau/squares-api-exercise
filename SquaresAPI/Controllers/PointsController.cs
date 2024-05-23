using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Coordinates;
using Services.PointsService;

namespace SquaresAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _pointsService;
        public PointsController(IPointsService pointsService)
        {
            _pointsService = pointsService;
        }

        [HttpGet("points")]

        public async Task<IActionResult> Get()
        {
            var response = await _pointsService.GetPoints();

            if(response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
