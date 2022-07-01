
namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("username is taken");

            var user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                //      Gender = user.Gender
            };
        }

    [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.Include(p => p.Photos).SingleOrDefaultAsync(user => user.UserName == loginDto.Username.ToLower());
            if (user == null) return BadRequest("invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized();


            // return user;
            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(ph => ph.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
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
            return await _userManager.Users.AnyAsync(user => user.UserName == userName.ToLower());
        }
    }
}