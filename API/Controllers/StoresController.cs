using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    public class StoresController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public StoresController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("StoreList/{hakaDocClientId}")]
        public async Task<ActionResult> AllStores(int hakaDocClientId)
        {
            var stores = await _unitOfWork.StoreRepository.StoreList(hakaDocClientId);
            var response = _mapper.Map<List<StoreListDto>>(stores);
            return Ok(response);
        }

         [HttpGet("StoreStock/{storeId}")]
         public async Task<ActionResult> StoreStock(int storeId)
         {
          var subProductsFromDb = await _unitOfWork.StoreRepository.GetStoreStock(storeId);
          
         return Ok(_mapper.Map<List<SubProductListDto>>(subProductsFromDb));
         }

    }
}