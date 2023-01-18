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
using API.Extensions;
using API.Helpers;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    public class CustomerController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("CreateCustomer/{haKaDocClientId}")]
        public async Task<ActionResult> CreateCustomer(int haKaDocClientId, CustomerCreationDto model)
        {
            var insertUserId = User.GetUserId();
            if ((await _unitOfWork.AuthRepository.CanDoAction(insertUserId, haKaDocClientId)) == false) return Unauthorized();

            var customerToCreate = _mapper.Map<Customer>(model);
            customerToCreate.HaKaDocClientId = haKaDocClientId;
            customerToCreate.InsertUserId = insertUserId;
            if (string.IsNullOrEmpty(model.CustomerCode))
            {
                CustomerCode customerCode = await _unitOfWork.CustomerRepository.GetCustomerCodeLevel(haKaDocClientId);
                if (customerCode.CodeLevel < 10)
                    customerToCreate.CustomerCode = "C0000" + customerCode.CodeLevel;
                else if (customerCode.CodeLevel < 100 && customerCode.CodeLevel >= 10)
                    customerToCreate.CustomerCode = "C000" + customerCode.CodeLevel;
                else if (customerCode.CodeLevel < 1000 && customerCode.CodeLevel >= 100)
                    customerToCreate.CustomerCode = "C00" + customerCode.CodeLevel;
                else if (customerCode.CodeLevel < 10000 && customerCode.CodeLevel >= 1000)
                    customerToCreate.CustomerCode = "C0" + customerCode.CodeLevel;
                else
                    customerToCreate.CustomerCode = "C" + customerCode.CodeLevel;

                customerCode.CodeLevel++;
                _unitOfWork.Update(customerCode);
            }
            _unitOfWork.Add(customerToCreate);

            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest();
        }

        [HttpGet("GetCustomerList/{haKaDocClientId}")]
        public async Task<IActionResult> GetCustomerList(int haKaDocClientId)
        {
            var customers = await _unitOfWork.CustomerRepository.GetCustomerList(haKaDocClientId);
            var customersToReturn = _mapper.Map<List<CustomerForListDto>>(customers);
            return Ok(customersToReturn);
        }

        [HttpGet("GetCustomerById/{customerId}/{haKaDocClientId}")]
        public async Task<IActionResult> GetCustomerById(int customerId, int haKaDocClientId)
        {
            var insertUserId = User.GetUserId();
            if ((await _unitOfWork.AuthRepository.CanDoAction(insertUserId, haKaDocClientId)) == false) return Unauthorized();
            Customer customerFromDb = await _unitOfWork.CustomerRepository.GetCustomerById(customerId);
            if (customerFromDb == null) return NotFound();
            return Ok(_mapper.Map<CustomerForListDto>(customerFromDb));
        }

        [HttpPut("UpdateCustomerData/{customerId}/{hakaDocClientId}")]
        public async Task<ActionResult> UpdateCustomerData(int customerId, int hakaDocClientId, CustomerCreationDto model)
        {
            if ((await _unitOfWork.AuthRepository.CanDoAction(User.GetUserId(), hakaDocClientId)) == false) return Unauthorized();
            Customer customerFromDb = await _unitOfWork.CustomerRepository.GetCustomerById(customerId);
            if (customerFromDb == null) return NotFound();
            _mapper.Map(model, customerFromDb);
            _unitOfWork.Update(customerFromDb);
            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest();
        }
    }
}