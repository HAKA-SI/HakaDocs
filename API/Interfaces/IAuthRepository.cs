using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IAuthRepository
    {
         Task<bool>CanDoAction(int userId,int hakaDocClientAction);
    }
}