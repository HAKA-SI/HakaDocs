using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class TemplatesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public TemplatesController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetInvoiceTemplates/{hakadocClientId}")]
        public async Task<ActionResult> InvoiceTemplate(int hakadocClientId)
        {
          var context = _unitOfWork.GetDataContext();
          var templates =await context.InvoiceTemplates.Where(a => a.HaKaDocClientId == hakadocClientId).ToListAsync();
          return Ok(templates);
          
        }
    }
}