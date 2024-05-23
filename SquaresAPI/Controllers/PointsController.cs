using Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Coordinates;
using Services.PointsService;
using System.Threading;

namespace SquaresAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _pointsService;

        public PointsController(IPointsService pointsService)
        {
            _pointsService = pointsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoints(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _pointsService.GetPoints(cancellationToken);

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex) when (ex is TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Try again later {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoint([FromBody] PointDTO point, CancellationToken cancellationToken)
        {
            try
            {
                await _pointsService.AddPoint(point, cancellationToken);
               
                return Ok("Added succesfully");
            }
            catch (Exception ex)
            {
                return ex.Message switch
                {
                    "Point already exists" => Conflict("Point already exists"),
                    _ => StatusCode(500, "Try again later")
                };
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePoint([FromBody] PointDTO point, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _pointsService.DeletePoint(point, cancellationToken);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Try again later");
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportPointsList([FromBody] IEnumerable<PointDTO> points, CancellationToken cancellationToken)
        {
            try
            {
                await _pointsService.ImportPoints(points, cancellationToken);
                return Ok("Imported succesfully");
            }
            catch(Exception ex)
            {
                return ex.Message switch
                {
                    "Duplicate found" => BadRequest("Provided list has duplicate points"),
                    "Incorrect parameter" => BadRequest("Ensure correct list format"),
                    _ => StatusCode(500, "Try again later")
                };
            }
        }
    }
}
