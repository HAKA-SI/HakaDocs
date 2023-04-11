using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExeptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private readonly IEmailSender _emailSender;
        public ExeptionMiddleware(RequestDelegate next, ILogger<ExeptionMiddleware> logger,
        IHostEnvironment env, IEmailSender emailSender)
        {
            _env = env;
            _logger = logger;
            _next = next;
            _emailSender = emailSender;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
                if (_env.IsProduction())
                {
                    var recipientList = new List<string>(){
                    "app_errors@haka-si.com"
                    };
                    foreach (var recipient in recipientList)
                    {
                        var mail = new EmailFormDto();
                        mail.Subject = "HakaDocs Error";
                        mail.Content = "<h1>" + ex.Message + "</h1><br><h1>" + ex.InnerException + "</h1><br>" + ex.ToString();
                        mail.ToEmail = recipient;
                        await SendEmail(mail);

                    }

                }

            }
        }

        private async Task SendEmail(EmailFormDto mail)
        {
            await _emailSender.SendEmailAsync(mail.ToEmail, mail.Subject, mail.Content);
        }
    }
}