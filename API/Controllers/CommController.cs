using System.Security.Cryptography.X509Certificates;

using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;

namespace API.Controllers
{
    public class CommController : BaseApiController

    {
        private readonly IEmailSender _sender;
        public CommController(IEmailSender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("SendEmail")]
        public async Task<ActionResult> SendEmail(EmailDto emaildDto)
        {

            await _sender.SendEmailAsync(emaildDto.To, emaildDto.Subject, emaildDto.Content);
            return Ok("la requète c'est bien terminé....");
        }

        [HttpGet("WheatherList")]
        public ActionResult WheatherList()
        {
            List<Titre> response = new List<Titre> {
               new Titre{Id=1,Name="titre 1"},
               new Titre{Id=2,Name="titre 2"},
               new Titre{Id=3,Name="titre 3"},
               new Titre{Id=4,Name="titre 4"},
               new Titre{Id=5,Name="titre 5"}
           };
           return Ok(response);
        }

       

        private class Titre
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}