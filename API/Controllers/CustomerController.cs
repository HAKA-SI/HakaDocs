using System.Reflection.Metadata;
using System.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using API.Entities;

namespace API.Controllers
{
    [Authorize]
    public class CustomerController: BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper=mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateCustomer/{haKaDocClientId}")]
         public async Task<ActionResult> CreateCustomer(int haKaDocClientId,CustomerCreationDto model)
         {
            var customerToCreate = _mapper.Map<Customer>(model);
            customerToCreate.HaKaDocClientId=haKaDocClientId;
            _unitOfWork.Add(customerToCreate);
            if(await _unitOfWork.Complete()) return Ok();
            return BadRequest();
         }
    }
}