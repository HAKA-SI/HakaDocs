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
        private readonly IPhotoService _photoService;


        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
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

        [HttpPost("CreateSubProduct/{hakaDocClientId}")]
        public async Task<ActionResult> CreateSubProduct(int hakaDocClientId, [FromForm] SubProductAddingDto model)
        {

            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            bool done = false;
            var context = _unitOfWork.GetDataContext();
            using (var identityContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var prod = _mapper.Map<SubProduct>(model);
                    prod.InsertUserId = loggeduserId;
                    prod.HaKaDocClientId = hakaDocClientId;
                    _unitOfWork.Add(prod);
                    await _unitOfWork.Complete();

                    if (model.MainPhotoFile != null)
                    {
                        var result = _photoService.AddProductPhotoFile(model.MainPhotoFile);
                        //if (result.Error != null) return BadRequest(result.Error.Message);
                        var photo = new Photo
                        {
                            Url = result.SecureUri.ToString(),
                            PublicId = result.PublicId,
                            IsApproved = true,
                            IsMain = true,
                            SubProductId = prod.Id
                        };
                        _unitOfWork.Add(photo);
                    }

                    if (model.OtherPhotoFiles != null)
                    {
                        foreach (var img in model.OtherPhotoFiles)
                        {
                            var res = _photoService.AddProductPhotoFile(img);
                            //  if (res.Error != null) return BadRequest(res.Error.Message);
                            var ph = new Photo
                            {
                                Url = res.SecureUri.ToString(),
                                PublicId = res.PublicId,
                                IsApproved = true,
                                IsMain = false,
                                SubProductId = prod.Id
                            };
                            _unitOfWork.Add(ph);
                        }
                    }
                    if (_unitOfWork.HasChanges())
                        await _unitOfWork.Complete();
                    identityContextTransaction.Commit();
                    done = true;
                }
                catch (System.Exception)
                {

                    identityContextTransaction.Rollback();
                }
            }
            if (done) return Ok();
            return BadRequest("impossible de faire l'enregistrement");
        }

        [HttpGet("SubProductList/{hakaDocClientId}")]
        public async Task<ActionResult> SubProductList(int hakaDocClientId)
        {
            var subProductsFromDb = await _unitOfWork.ProductRepository.GetSubProducts(hakaDocClientId);
            return Ok(_mapper.Map<List<SubProductListDto>>(subProductsFromDb));
        }


        [HttpGet("SubProduct/{subProductId}/{hakaDocClientId}")]
        public async Task<ActionResult> SubProduct(int subProductId, int hakaDocClientId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            SubProduct subProductFromDb = await _unitOfWork.ProductRepository.GetSubProduct(subProductId);
            return Ok(_mapper.Map<SubProductListDto>(subProductFromDb));
        }


 [HttpPost("EditSubProduct/{subProductId}/{photoEdtited}/{hakaDocClientId}")]
        public async Task<ActionResult> EditSubProduct(int subProductId,bool photoEdtited,int hakaDocClientId, [FromForm] SubProductAddingDto model)
        {

            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            bool done = false;
            var context = _unitOfWork.GetDataContext();
            var subproductFromDb = await _unitOfWork.ProductRepository.GetSubProduct(subProductId);
            using (var identityContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var prod = _mapper.Map(model, subproductFromDb);
                    prod.UpdateUserId = loggeduserId;
                    _unitOfWork.Update(prod);
                    await _unitOfWork.Complete();

                    if(photoEdtited)
                    {
                        var photo = subproductFromDb.Photos.FirstOrDefault(a =>a.IsMain);
                        if(photo!=null)
                        {
                        photo.IsMain=false;
                        photo.IsApproved=false;
                        _unitOfWork.Update(photo);
                        }
                    }

                    if (model.MainPhotoFile != null)
                    {
                        var result = _photoService.AddProductPhotoFile(model.MainPhotoFile);
                        //if (result.Error != null) return BadRequest(result.Error.Message);
                        var photo = new Photo
                        {
                            Url = result.SecureUri.ToString(),
                            PublicId = result.PublicId,
                            IsApproved = true,
                            IsMain = true,
                            SubProductId = prod.Id
                        };
                        _unitOfWork.Add(photo);
                    }

                    if (model.OtherPhotoFiles != null)
                    {
                        foreach (var img in model.OtherPhotoFiles)
                        {
                            var res = _photoService.AddProductPhotoFile(img);
                            //  if (res.Error != null) return BadRequest(res.Error.Message);
                            var ph = new Photo
                            {
                                Url = res.SecureUri.ToString(),
                                PublicId = res.PublicId,
                                IsApproved = true,
                                IsMain = false,
                                SubProductId = prod.Id
                            };
                            _unitOfWork.Add(ph);
                        }
                    }
                    if (_unitOfWork.HasChanges())
                        await _unitOfWork.Complete();
                    identityContextTransaction.Commit();
                    done = true;
                }
                catch (System.Exception)
                {

                    identityContextTransaction.Rollback();
                }
            }
            if (done) return Ok();
            return BadRequest("impossible de faire l'enregistrement");
        }

    }
}