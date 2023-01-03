using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {


    //   [AllowAnonymous]
    //     [HttpGet("TestHtmlToPDF")]
    //     public async Task<IActionResult> TestHtmlToPDF()
    //     {
    //         var client = new HttpClient();
    //         string url = "http://localhost:4800/api/docs/CreatePdfFile";
    //         var doc = new PdfToCreateDto()
    //         {
    //             Html = "<h1>   test ce confection de fichier pdf 2</h1>",
    //             FileName ="referfr"
    //         };

    //         client.DefaultRequestHeaders.Accept.Clear();
    //         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
    //         var response = await client.PostAsJsonAsync(url, doc);
    //         var responseString = await response.Content.ReadAsStringAsync();

    //         return Ok(responseString);

          
    //     }   
    }
}