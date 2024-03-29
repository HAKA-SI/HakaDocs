using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using API.Errors;
using Microsoft.EntityFrameworkCore;
using WhatsappBusiness.CloudApi.Interfaces;
using WhatsappBusiness.CloudApi.Messages.Requests;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {
        private readonly DataContext _context;

        private readonly ITokenService _tokenService;

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWhatsAppBusinessClient _whatsAppBusinessClient;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            DataContext context,
            ITokenService tokenService,
            IMapper mapper,
            IConfiguration config,
            IEmailSender emailSender
            , IWhatsAppBusinessClient whatsAppBusinessClient
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _tokenService = tokenService;
            _context = context;
            _emailSender = emailSender;
            _whatsAppBusinessClient = whatsAppBusinessClient;
        }
        [Authorize]
        [HttpPost("webhook")]
        public ActionResult WhatsappWebHook(string token)
        {
            string challenge = HttpContext.Request.Query["challenge"];

            // Renvoyez le défi en tant que réponse pour la validation
            return Ok();
        }




        //    [HttpPost("register")]
        //     public async Task<ActionResult> Register(RegisterDto registerDto)
        //     {
        //         string password =  _config["Password"] ;
        //         if (await CheckEmailExistAsync(registerDto.Email).Result.Value)
        //         {
        //             return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []{"email dejà utilisé..."}});
        //         }

        //         var user = new AppUser
        //         {
        //             Email = registerDto.Email,
        //             UserName = registerDto.Email,
        //             PhoneNumber=registerDto.PhoneNumber,

        //         };

        //         var result = await _userManager.CreateAsync(user, password);

        //         if (!result.Succeeded) return BadRequest(new ApiResponse(400));
        //         //envoi du lien de confirmation
        //         var email_to_send=new EmailFormDto{
        //                 Subject="Confirmation de compte",
        //                 ToEmail=registerDto.Email,
        //                 Content="<h3> veuillez confirmez vontre compte en cliquznt sur le lien suivant"
        //             };

        //         SendEmail(email_to_send);
        //         return Ok();

        //      }




        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =
                await _userManager
                    .Users
                    .Include(p => p.Photos)
                    .SingleOrDefaultAsync(user =>
                        user.UserName == loginDto.Username.ToLower());
            if (user == null) return BadRequest("invalid username");

            var result =
                await _signInManager
                    .CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();

            // return user;
            return new UserDto
            {
                Username = user.UserName,
                Id = user.Id,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(ph => ph.IsMain)?.Url,
                FullName = user.FirstName + " " + user.LastName,
                HaKaDocClientId = user.HaKaDocClientId,
                Gender = user.Gender
            };
        }


        [AllowAnonymous]
        [HttpGet("WhatsappTest")]
        public async Task<ActionResult> WhatsappTest()
        {
            // TextMessageRequest textMessageRequest = new TextMessageRequest();
            // textMessageRequest.To = "2250707390636";
            // textMessageRequest.Text = new WhatsAppText();
            // textMessageRequest.Text.Body = "Message Body";
            // textMessageRequest.Text.PreviewUrl = false;
            // var results = await _whatsAppBusinessClient.SendTextMessageAsync(textMessageRequest);

            TextTemplateMessageRequest textTemplateMessage = new TextTemplateMessageRequest();
            textTemplateMessage.To = "2250711620318";
            textTemplateMessage.Template = new TextMessageTemplate();
            textTemplateMessage.Template.Name = "hello_world";
            textTemplateMessage.Template.Language = new TextMessageLanguage();
            textTemplateMessage.Template.Language.Code = "en_US";

            var results = await _whatsAppBusinessClient.SendTextMessageTemplateAsync(textTemplateMessage);
            return Ok();
        }





        // private async Task<List<ClientForListDto>> GetClientsTokens()
        // {
        //     var clients = await _context.Clients.ToListAsync();
        //     var clientsToReturn = new List<ClientForListDto>();
        //     foreach (var client in clients)
        //     {
        //         //making http post request
        //         var httpClient = new HttpClient();
        //         string url = client.BaseUrl + "auth/login";
        //         var doc = new UserForLoginDto()
        //         {
        //             Username = "admin",
        //             Password = "password"
        //         };
        //         httpClient.DefaultRequestHeaders.Accept.Clear();
        //         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //         var response = await httpClient.PostAsJsonAsync(url, doc);
        //         var responseString = await response.Content.ReadAsStringAsync();
        //         var responseData = JsonConvert.DeserializeObject<AuthDataReturnedDto>(responseString);
        //         if (responseData != null)
        //         {
        //             clientsToReturn.Add(
        //                 new ClientForListDto {

        //                     Id = client.Id,

        //                     BaseUrl = client.BaseUrl,

        //                     Name = client.Name,

        //                     Token = responseData.Token,
        //                     SubDomain = client.SubDomain }
        //             );
        //         }
        //     }
        //     return clientsToReturn;
        // }
        private async Task<bool> UserExists(string userName)
        {
            return await _userManager
                .Users
                .AnyAsync(user => user.UserName == userName.ToLower());
        }
        private async void SendEmail(EmailFormDto mail)
        {
            await _emailSender.SendEmailAsync(mail.ToEmail, mail.Subject, mail.Content);

        }


    }
}