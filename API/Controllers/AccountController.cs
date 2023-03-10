using System.Runtime.InteropServices;


using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Dtos;
using API.Extensions;
using System.Linq;
using System.Text.Encodings.Web;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;

        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleInManager,
            DataContext context,
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IConfiguration config,
            IEmailSender emailSender
        )
        {
            _signInManager = signInManager;
            _roleManager = roleInManager;
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _config = config;
            _tokenService = tokenService;
            _context = context;
            _emailSender = emailSender;
        }



        [HttpPost("CreateRole/{haKaDocClientId}/{roleName}")]
        public async Task<ActionResult> CreateRole(int haKaDocClientId, string roleName)
        {
            var actionAllowed = await _unitOfWork.AuthRepository.CanDoAction(User.GetUserId(), haKaDocClientId);
            if (!actionAllowed) return Unauthorized();
            var role = new AppRole
            {
                Name = roleName,
                HaKaDocClientId = haKaDocClientId
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded) return Ok();
            return BadRequest("impossible d'ajouter le role");

        }


        [HttpGet("GetRoleList/{haKaDocClientId}")]
        public async Task<ActionResult> GetRoleList(int haKaDocClientId)
        {
            var actionAllowed = await _unitOfWork.AuthRepository.CanDoAction(User.GetUserId(), haKaDocClientId);
            if (!actionAllowed) return Unauthorized();
            var roles = await _roleManager.Roles.Include(a => a.UserRoles).Where(a => a.HaKaDocClientId == haKaDocClientId).ToListAsync();
            var rolesToReturn = new List<RoleForListDto>();
            foreach (var role in roles)
            {
                rolesToReturn.Add(new RoleForListDto { Name = role.Name, Id = role.Id, TotalUsers = role.UserRoles.Count() });
            }
            return Ok(rolesToReturn);
        }

        // private async  Task<bool> verifyHakaDocClientAccount(int hakadocClientId)
        // {
        //     var userName = User.GetUsername();
        //     var loggedUser = await _signInManager.UserManager.FindByNameAsync(userName);
        //     if(loggedUser.HaKaDocClientId !=hakadocClientId) return false;
        //     return true;
        // }

        [AllowAnonymous]
        [HttpGet("{email}/emailexists/{hakaDocClientId}")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email, int hakaDocClientId)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("veuillez saisir une email");
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Email == email && a.HaKaDocClientId == hakaDocClientId);
            if (user != null) return true;
            return false;

        }


        [HttpPost("CreateAccount/{haKaDocClientId}")]
        public async Task<IActionResult> SavePreInscription(int haKaDocClientId, AccountCreationDto model)
        {
            var actionAllowed = await _unitOfWork.AuthRepository.CanDoAction(User.GetUserId(), haKaDocClientId);
            if (!actionAllowed) return Unauthorized();
            var userName = Guid.NewGuid();
            var userToCreate = _mapper.Map<AppUser>(model);
            userToCreate.HaKaDocClientId = haKaDocClientId;
            userToCreate.UserName = userName.ToString();
            userToCreate.ValidationCode = userName.ToString();
            string password = _config.GetValue<String>("AppSettings:defaultPassword");
            var result = await _userManager.CreateAsync(userToCreate, password);

            // var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            if (result.Succeeded)
            {
                // envoi du lien de creation de compte dans la boite mail
                var callbackUrl = _config.GetValue<String>("AppSettings:DefaultEmailValidationLink") + userToCreate.ValidationCode;
                string subject = "creation de compte HaKaDocs";
                string content = $"veuillez confirmez votre code au lien suivant : <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicker ici</a>.";
                await _emailSender.SendEmailAsync(model.Email, subject, content);
                return Ok();
            }

            return BadRequest(result.Errors);

        }


        [AllowAnonymous]
        [HttpGet("{userName}/VerifyUserName")]
        public async Task<IActionResult> VerifyUserName(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if (result != null) return Ok(true);
            return Ok(false);

        }


        [AllowAnonymous]
        [HttpGet("emailValidation/{code}")]
        public async Task<IActionResult> emailValidation(string code)
        {

            // int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userManager.Users.FirstOrDefaultAsync(a => a.ValidationCode == code);

            if (user != null)
            {
                if (user.EmailConfirmed == true)
                    return BadRequest("cet compte a déja été confirmé...");
                return Ok(_mapper.Map<UserDto>(user));
            }
            return BadRequest("ce lien n'existe pas");

        }


        [AllowAnonymous]
        [HttpPost("{id}/setLoginPassword")] // edition du mot de passe apres validation du code
        public async Task<IActionResult> setLoginPassword(int id, LoginDto model)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return BadRequest("cet utilisateur n'existe pas");
            var newPassword = _userManager.PasswordHasher.HashPassword(user, model.Password);
            user.UserName = model.Username.ToLower();
            user.NormalizedUserName = model.Username.ToUpper();
            user.PasswordHash = newPassword;
            user.CodeValidated = true;
            user.EmailConfirmed = true;
            user.ValidationDate = DateTime.Now;
            user.Active = true;

            var res = await _userManager.UpdateAsync(user);
            // var roleName = "";
            // if (user.TypeEmpId == 1)
            //     roleName = "admin";
            // else if (user.TypeEmpId == 11)
            //     roleName = "AgentHotline";
            // else
            // {
            //     // var role = await _context.Roles.FirstOrDefaultAsync(a => a.Id == user.TypeEmpId);
            //     var role = await _roleManager.FindByIdAsync(user.TypeEmpId.ToString());
            //     roleName = role.Name;
            // }
            // // var appUser = await _userManager.Users
            // //     .FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName);
            // var roleResult = await _userManager.AddToRoleAsync(user, roleName);

            //ajout de la photo
            // var savedPhoto = new Photo();

            // if (res.Succeeded && roleResult.Succeeded)
            if (res.Succeeded)
            {
                // var photoUrl = "";
                // if (loginForDto.PhotoFile != null)
                // {
                //     var uploadResult = new ImageUploadResult();

                //     using (var stream = loginForDto.PhotoFile.OpenReadStream())
                //     {
                //         var uploadParams = new ImageUploadParams()
                //         {
                //             File = new FileDescription(loginForDto.PhotoFile.Name, stream)
                //         };
                //         uploadParams.Folder = "RLE2022/";
                //         uploadResult = _cloudinary.Upload(uploadParams);
                //         if (uploadParams != null)
                //         {
                //             photoUrl = uploadResult.Uri.ToString();
                //             _context.Add(new Photo
                //             {
                //                 Url = uploadResult.Uri.ToString(),
                //                 PublicId = uploadResult.PublicId,
                //                 Description = loginForDto.PhotoFile.FileName,
                //                 UserId = id,
                //                 IsMain = true
                //             });
                //             await _context.SaveChangesAsync();
                //         }
                //     }
                // }
                // // await _cache.SetUsers();
                // var mail = new EmailFormDto();
                // mail.Subject = "Compte confirmé";
                // mail.Content = "<b> " + user.LastName + " " + user.FirstName + "</b>, votre compte a bien été enregistré";
                // mail.ToEmail = user.Email;
                // await _uow.RleRepository.SendEmail(mail);


                var userToReturn = _mapper.Map<UserDto>(user);
                userToReturn.Token = await _tokenService.CreateToken(user);
                // userToReturn.PhotoUrl = photoUrl;
                return Ok(userToReturn);
            }
            return BadRequest("impossible de terminer cette opération");

        }

    }
}
