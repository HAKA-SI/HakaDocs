using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    public class OrdersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("SaveClientOrder/{hakaDocClientId}")]
        public async Task<ActionResult> SaveClientOrder(int hakaDocClientId,int insertUserId, PhysicalBasketDto model)
        {
             var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            return Ok();
        }

    }
}