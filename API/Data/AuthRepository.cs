using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AuthRepository(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> CanDoAction(int userId,int hakaDocClientAction)
        {
          
            var loggedUser = await _signInManager.UserManager.Users.FirstOrDefaultAsync(a => a.Id==userId);
            if(loggedUser.HaKaDocClientId !=hakaDocClientAction) return false;
            return true;
        }
    }
}