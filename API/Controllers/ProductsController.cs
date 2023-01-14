using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using API.Dtos;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]

    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost("CreateCategory/{hakaClientId}/{categoryName}")]
        public async Task<ActionResult> CreateCategory(int hakaClientId, string categoryName)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaClientId))) return Unauthorized();

            Category category = new Category
            {
                Name = categoryName,
                InsertUserId = loggeduserId,
                HaKaDocClientId = hakaClientId
            };

            _unitOfWork.Add(category);

            if (!(await _unitOfWork.Complete())) return BadRequest("impossible d'ajouter la categorie");
            return Ok(_mapper.Map<CategoryWithDetailsDto>(category));

        }


        [HttpGet("CategoryList/{hakaDocClientId}")]
        public async Task<ActionResult> CategoryList(int hakaDocClientId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            var categories = await _unitOfWork.ProductRepository.GetCategories(hakaDocClientId);
            return Ok(_mapper.Map<List<CategoryForListDto>>(categories));
        }


        [HttpGet("CategoryListWithDetails/{hakaDocClientId}")]
        public async Task<ActionResult> CategoryListWithDetails(int hakaDocClientId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            var categoriesFromDb = await _unitOfWork.ProductRepository.GetCategoriesWithProducts(hakaDocClientId);
            var categoriesToReturn = _mapper.Map<List<CategoryWithDetailsDto>>(categoriesFromDb);
            return Ok(categoriesToReturn);
        }

        [HttpPut("EditCategory/{hakaDocClientId}/{categoryId}/{categoryName}")]
        public async Task<ActionResult> EditCategory(int hakaDocClientId, int categoryId, string categoryName)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            Category categoryFromDb = await _unitOfWork.ProductRepository.GetCategory(categoryId);
            categoryFromDb.Name = categoryName;
            categoryFromDb.UpdateUserId = loggeduserId;
            _unitOfWork.Update(categoryFromDb);
            if (!(await _unitOfWork.Complete())) return BadRequest("impossible de modifier la categorie");
            return Ok();
        }

        [HttpDelete("DeleteCategory/{hakaDocClientId}/{categoryId}")]
        public async Task<ActionResult> DeleteCategory(int hakaDocClientId, int categoryId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            Category categoryFromDb = await _unitOfWork.ProductRepository.GetCategory(categoryId);
            categoryFromDb.Active = false;
            categoryFromDb.UpdateUserId = loggeduserId;
            _unitOfWork.Update(categoryFromDb);
            if (!(await _unitOfWork.Complete())) return BadRequest("impossible de modifier la categorie");
            return Ok();
        }

        [HttpPost("CreateProduct/{hakaDocClientId}/{categoryId}/{productName}")]
        public async Task<ActionResult> CreateCategory(int hakaDocClientId, int categoryId, string productName)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            var product = new Product
            {
                CategoryId = categoryId,
                Name = productName,
                InsertUserId = loggeduserId,
                HaKaDocClientId = hakaDocClientId
            };

            _unitOfWork.Add(product);
            if (!(await _unitOfWork.Complete())) return BadRequest("impossible d'ajouter ce produit");

              Product productFromDb = await _unitOfWork.ProductRepository.GetProductWithDetails(product.Id);
            return Ok(_mapper.Map<ProductWithDetailDto>(productFromDb));
        }


        [HttpPut("EditProduct/{productId}/{hakaDocClientId}/{categoryId}/{productName}")]
        public async Task<ActionResult> EditProduct(int productId, int hakaDocClientId, int categoryId, string productName)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            Product productFromDb = await _unitOfWork.ProductRepository.GetProductWithDetails(productId);
            productFromDb.Name = productName;
            productFromDb.UpdateUserId = loggeduserId;
            _unitOfWork.Update(productFromDb);
            if (!(await _unitOfWork.Complete())) return BadRequest("impossible d'ajouter ce produit");
            return Ok(_mapper.Map<ProductWithDetailDto>(productFromDb));
        }


        [HttpDelete("DeleteProduct/{productId}/{hakaDocClientId}")]
        public async Task<ActionResult> DeleteProduct(int productId, int hakaDocClientId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            Product productFromDb = await _unitOfWork.ProductRepository.GetProduct(productId);
            productFromDb.Active = false;
            _unitOfWork.Update(productFromDb);
            if (!(await _unitOfWork.Complete())) return BadRequest("impossible d'ajouter ce produit");
            return Ok();
        }

        [HttpGet("ProductsWithDetails/{hakaDocClientId}")]
        public async Task<ActionResult> ProductsWithDetails(int hakaDocClientId)
        {
            var prodsFromDb = await _unitOfWork.ProductRepository.GetProductsWithDetails(hakaDocClientId);
            return Ok(_mapper.Map<List<ProductWithDetailDto>>(prodsFromDb));
        }

        [HttpGet("ProductList/{hakaDocClientId}")]
        public async Task<ActionResult> ProductList(int hakaDocClientId)
        {
            var prodsFromDb = await _unitOfWork.ProductRepository.GetProducts(hakaDocClientId);
            return Ok(_mapper.Map<List<ProductForListDto>>(prodsFromDb));
            // return Ok(prodsFromDb);
        }

    }
}