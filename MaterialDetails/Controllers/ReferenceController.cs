using MaterialDetails.Interfaces;
using MaterialDetails.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaterialDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceService _referenceService;
        public ReferenceController(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var references = await _referenceService.GetReferenceDetails();
                return Ok(references);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var references = await _referenceService.GetReferenceDetailById(id);
                return Ok(references);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
