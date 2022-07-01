
namespace API.Controllers
{
    public class DashboardController : BaseApiController
    {
        private readonly IHubContext<DashboardHub> _dashboardHub;
        public DashboardController(IHubContext<DashboardHub> dashboardHub)
        {
            _dashboardHub = dashboardHub;
        }
        // public async Task<IActionResult> Get()
    //     public IActionResult Get()
    //     {
    //         // var responseJson = new List<ClassDto>();
    //         // string url = "https://test1.educnotes.com/api/auth/getclasses";

    //         // //var client = new HttpClient();
    //         // //         string url = "http://localhost:4800/api/docs/CreatePdfFile";
    //         // //         var doc = new PdfToCreateDto()
    //         // //         {
    //         // //             Html = "<h1>   test ce confection de fichier pdf 2</h1>",
    //         // //             FileName ="referfr"
    //         // //         };

    //         // //         client.DefaultRequestHeaders.Accept.Clear();
    //         // //         client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
    //         // //         var response = await client.PostAsJsonAsync(url, doc);
    //         // using (HttpClient client = new HttpClient())
    //         // {
    //         //     var requestGet = new HttpRequestMessage
    //         //     {
    //         //         Method = HttpMethod.Get,
    //         //         RequestUri = new Uri(url)
    //         //     };
    //         //     var resp = await client.SendAsync(requestGet).ConfigureAwait(false);
    //         //     var responseString = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
    //         //     responseJson = JsonConvert.DeserializeObject<List<ClassDto>>(responseString);
    //         //     // var r = new Random();
    //         //     // if(responseJson.Count>0)
    //         //     // responseJson.FirstOrDefault(a => a.Id ==13).MaxStudent = r.Next(1, 3000);


    //         // }
    //  //       var timerManager = new TimerManager(() => _dashboardHub.Clients.All.SendAsync("transferdashboarddata", GetRandomData()));

    //         return Ok(new { Message = "Request Completed" });
    //     }


        // [HttpGet("getcities")]
        // public async Task<IActionResult> getCities()
        // {
        //     var responseJson = new List<ClassDto>();
        //     string url = "https://test1.educnotes.com/api/auth/getclasses";

        //     using (HttpClient client = new HttpClient())
        //     {
        //         var requestGet = new HttpRequestMessage
        //         {
        //             Method = HttpMethod.Get,
        //             RequestUri = new Uri(url)
        //         };
        //         var resp = await client.SendAsync(requestGet).ConfigureAwait(false);
        //         var responseString = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        //         responseJson = JsonConvert.DeserializeObject<List<ClassDto>>(responseString);
        //     }
        //     return Ok(responseJson);

        // }


        // [HttpPost("getEmailCategories")]
        // public async Task<ActionResult> GetEmailCategories(List<ClientForListDto> clients)
        // {
        //     var emailCatsToReturn = new List<EmailCategoriesDto>();
        //     var ss = new List<String>();
        //     foreach (var cClient in clients)
        //     {
        //         var responseJson = new List<EmailCategoriesDto>();
        //         string url = cClient.BaseUrl + "comm/EmailCategories";
        //         using (HttpClient client = new HttpClient())
        //         {
        //             var requestGet = new HttpRequestMessage
        //             {
        //                 Method = HttpMethod.Get,
        //                 RequestUri = new Uri(url)
        //             };
        //             client.DefaultRequestHeaders.Add("Authorization","Bearer " + cClient.Token);
        //             var resp = await client.SendAsync(requestGet).ConfigureAwait(false);
        //             var responseString = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        //             responseJson = JsonConvert.DeserializeObject<List<EmailCategoriesDto>>(responseString);
        //             emailCatsToReturn.AddRange(responseJson);
        //         }
        //     }
        //     return Ok(emailCatsToReturn);
        // }

        //  [HttpPost("GetSmsRecap")]
        // public async Task<ActionResult> GetSmsRecap(List<ClientForListDto> clients)
        // {
        //     var emailCatsToReturn = new List<ClientWithDetail>();
        //     var ss = new List<String>();
        //     foreach (var cClient in clients)
        //     {
        //         var responseJson = new SmsRecapDto();
        //         string url = cClient.BaseUrl + "monitoring/smsrecap";
        //         using (HttpClient client = new HttpClient())
        //         {
        //             var requestGet = new HttpRequestMessage
        //             {
        //                 Method = HttpMethod.Get,
        //                 RequestUri = new Uri(url)
        //             };
        //             client.DefaultRequestHeaders.Add("Authorization","Bearer " + cClient.Token);
        //             var resp = await client.SendAsync(requestGet).ConfigureAwait(false);
        //             var responseString = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        //             var responseConverted = JsonConvert.DeserializeObject<SmsRecapDto>(responseString);
        //             emailCatsToReturn.Add( new ClientWithDetail{
        //                 Name = cClient.Name,
        //                 Id= cClient.Id,
        //                 SmsRecap = responseConverted

        //             });
        //         }
        //     }
        //     return Ok(emailCatsToReturn);
        // }

        // private List<ChartModel> GetRandomData()
        // {
        //     var r = new Random();
        //     return new List<ChartModel>()
        // {
        //    new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data1" },
        //    new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data2" },
        //    new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data3" },
        //    new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data4" }
        // };
        // }
    }
}