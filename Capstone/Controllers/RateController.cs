using Capstone.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateRepository _rateRepository;

        public RateController(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_rateRepository.GetAll());
        }
    }
}
