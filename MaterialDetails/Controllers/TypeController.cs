using MaterialDetails.Interfaces;
using MaterialDetails.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaterialDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TypeController : ControllerBase
    {
        private readonly ITypeService _typeService;
        public TypeController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var types = await _typeService.GetTypes(); ;
                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
