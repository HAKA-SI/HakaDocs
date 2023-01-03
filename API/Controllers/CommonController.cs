using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;

namespace API.Controllers
{
    public class CommonController: BaseApiController
    {
        private readonly ICacheRepository _cache;
        public CommonController(ICacheRepository cache)
        {
            _cache = cache;
        }

         [HttpGet("GetCities")]
        public async Task<ActionResult> GetCities()
        {
            var cities = await _cache.GetCities();
            return Ok(cities);
        }
    }
}