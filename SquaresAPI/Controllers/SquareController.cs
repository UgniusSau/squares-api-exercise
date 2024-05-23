using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.SquareService;

namespace SquaresAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SquareController : ControllerBase
    {
        private readonly ISquareService _squareService;

        public SquareController(ISquareService squareService)
        {
            _squareService = squareService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var response = await _squareService.DetectSquares(cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return ex.Message switch
                {
                    "Not enough points" => BadRequest("There are not enough points to start detection"),
                    _ => StatusCode(500, "Try again later")
                };
            }
        }
    }
}
