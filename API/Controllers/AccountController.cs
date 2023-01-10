

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
using System.Collections.Generic;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using System.Linq;

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
            var actionAllowed =await _unitOfWork.AuthRepository.CanDoAction(User.GetUserId(),haKaDocClientId);
            if(!actionAllowed) return Unauthorized();
            var role = new AppRole
            {
                Name = roleName,
                HaKaDocClientId = haKaDocClientId
            };
            var result =await _roleManager.CreateAsync(role);
            if(result.Succeeded) return Ok();
            return BadRequest("impossible d'ajouter le role");

        }


        [HttpGet("GetRoleList/{haKaDocClientId}")]
        public async Task<ActionResult> GetRoleList(int haKaDocClientId)
        {
            var actionAllowed =await _unitOfWork.AuthRepository.CanDoAction(User.GetUserId(),haKaDocClientId);
            if(!actionAllowed) return Unauthorized();
            var roles =await _roleManager.Roles.Include(a => a.UserRoles).Where(a => a.HaKaDocClientId == haKaDocClientId).ToListAsync();
            var rolesToReturn = new List<RoleForListDto>();
            foreach (var role in roles)
            {
                rolesToReturn.Add(new RoleForListDto{Name = role.Name,Id=role.Id,TotalUsers=role.UserRoles.Count()});
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

    }
}
