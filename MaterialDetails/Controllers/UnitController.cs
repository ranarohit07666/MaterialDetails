using MaterialDetails.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaterialDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var units = await _unitService.GetUnits(); ;
                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
