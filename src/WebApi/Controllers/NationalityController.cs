using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransferMarketPlatform.Application.DTOs;
using TransferMarketPlatform.Application.Interfaces;

namespace TransferMarketPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalityController : ControllerBase
    {
        private readonly INationalityService _nationalityService;

        public NationalityController(INationalityService nationalityService)
        {
            _nationalityService = nationalityService;
        }

        [HttpGet]
        public ActionResult<List<NationalityDto>> GetNationalities()
        {
            var nationalities = _nationalityService.GetNationalities();
            return Ok(nationalities);
        }
    }
}
