
using API.Data;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MonitoringController : ControllerBase
  {
    private readonly DataContext _context;
    public MonitoringController(DataContext context, IConnectionMultiplexer redis)
    {
      _context = context;
    }

  }
}