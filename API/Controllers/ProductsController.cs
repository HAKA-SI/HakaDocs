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
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]

    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IConfiguration _config;


        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _photoService = photoService;
        }


        [HttpPost("CreateCategory/{hakaClientId}/{categoryName}/{productGroupId}")]
        public async Task<ActionResult> CreateCategory(int hakaClientId, string categoryName, int productGroupId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaClientId))) return Unauthorized();

            Category category = new Category
            {
                Name = categoryName,
                InsertUserId = loggeduserId,
                HaKaDocClientId = hakaClientId,
                ProductGroupId = productGroupId
            };

            _unitOfWork.Add(category);

            if (!(await _unitOfWork.Complete())) return BadRequest("impossible d'ajouter la categorie");
            return Ok(_mapper.Map<CategoryWithDetailsDto>(category));

        }


        [HttpGet("CategoryList/{productGroupId}/{hakaDocClientId}")]
        public async Task<ActionResult> CategoryList(int productGroupId, int hakaDocClientId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            var categories = await _unitOfWork.ProductRepository.GetCategories(hakaDocClientId, productGroupId);
            return Ok(_mapper.Map<List<CategoryForListDto>>(categories));
        }


        [HttpGet("CategoryListWithDetails/{productGroupId}/{hakaDocClientId}")]
        public async Task<ActionResult> CategoryListWithDetails(int productGroupId, int hakaDocClientId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            var categoriesFromDb = await _unitOfWork.ProductRepository.GetCategoriesWithProducts(hakaDocClientId, productGroupId);
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

        [HttpGet("ProductsWithDetails/{productGroupId}/{hakaDocClientId}")]
        public async Task<ActionResult> ProductsWithDetails(int productGroupId, int hakaDocClientId)
        {
            var prodsFromDb = await _unitOfWork.ProductRepository.GetProductsWithDetails(hakaDocClientId, productGroupId);
            return Ok(_mapper.Map<List<ProductWithDetailDto>>(prodsFromDb));
        }

        [HttpGet("ProductList/{productGroupId}/{hakaDocClientId}")]
        public async Task<ActionResult> ProductList(int productGroupId, int hakaDocClientId)
        {
            var prodsFromDb = await _unitOfWork.ProductRepository.GetProductsWithDetails(hakaDocClientId, productGroupId);
            return Ok(_mapper.Map<List<ProductForListDto>>(prodsFromDb));
            // return Ok(prodsFromDb);
        }

        [HttpPost("CreateSubProduct/{productGroupId}/{hakaDocClientId}")]
        public async Task<ActionResult> CreateSubProduct(int productGroupId, int hakaDocClientId, [FromForm] SubProductAddingDto model)
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

        [HttpGet("SubProductList/{productGroupId}/{hakaDocClientId}")]
        public async Task<ActionResult> SubProductList(int productGroupId, int hakaDocClientId)
        {
            var subProductsFromDb = await _unitOfWork.ProductRepository.GetSubProducts(hakaDocClientId, productGroupId);
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
        public async Task<ActionResult> EditSubProduct(int subProductId, bool photoEdtited, int hakaDocClientId, [FromForm] SubProductAddingDto model)
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

                    if (photoEdtited)
                    {
                        var photo = subproductFromDb.Photos.FirstOrDefault(a => a.IsMain);
                        if (photo != null)
                        {
                            photo.IsMain = false;
                            photo.IsApproved = false;
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

        [HttpGet("SubProductWithSNsList/{hakaDocClientId}/{productGroupId}")]
        public async Task<ActionResult> SubProductWithSNsList(int hakaDocClientId, int productGroupId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            List<SubProduct> subProductsFromDb = await _unitOfWork.ProductRepository.GetSubProductWithSNs(hakaDocClientId, productGroupId);
            return Ok(_mapper.Map<List<SubProductListDto>>(subProductsFromDb));
        }

        [HttpPost("CreateSubProductsSN/{hakaDocClientId}")]
        public async Task<ActionResult> CreateSubProductsSN(int hakaDocClientId, ProductSNAddingDto model)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            int stockEntryTypeId = _config.GetValue<int>("AppSettings:stockEntryTypeId");
            int in_stockEntryActionId = _config.GetValue<int>("AppSettings:inStockEntryHistoryId");
            var dataContext = _unitOfWork.GetDataContext();
            bool done = false;

            //var createdProduct = await _uow.ProductRepository.CreateSubProductsNoSN(insertUserId, prod, stockmvt, prod.Quantity);

            using (var identityContextTransaction = dataContext.Database.BeginTransaction())
            {
                try
                {
                    // insertion dans SubProductSN(avec le storeId=> ce a qui permetra de retrouver le Produit)
                    // insertion StockMvts
                    // insertion InventOps
                    // insertion StockMvtInventOps
                    // insertion InventOpSubProductSNs

                    var stockmvt = new StockMvt
                    {
                        InventOpTypeId = stockEntryTypeId,
                        ToStoreId = model.StoreId,
                        RefNum = model.RefNum,
                        Note = model.Note,
                        MvtDate = model.MvtDate,
                        InsertUserId = loggeduserId
                    };

                    //insertion dans stock mvt
                    _unitOfWork.Add(stockmvt);
                    await _unitOfWork.Complete();

                    InventOp inv = new InventOp
                    {
                        InsertUserId = stockmvt.InsertUserId,
                        InventOpTypeId = stockmvt.InventOpTypeId,
                        OpDate = stockmvt.MvtDate,
                        ToStoreId = stockmvt.ToStoreId,
                        FromStoreId = stockmvt.FromStoreId,
                        FormNum = stockmvt.RefNum,
                        SubProductId = model.SubProductId,
                        Quantity = model.sns.Count()
                    };
                    _unitOfWork.Add(inv);
                    await _unitOfWork.Complete();

                    var stockMvtInventOp = new StockMvtInventOp
                    {
                        InventOpId = inv.Id,
                        StockMvtId = stockmvt.Id
                    };
                    _unitOfWork.Add(stockMvtInventOp);

                    StoreProduct storeProduct = await _unitOfWork.ProductRepository.StoreProduct(model.StoreId, model.SubProductId);
                    if (storeProduct == null)
                    {
                        storeProduct = new StoreProduct
                        {
                            StoreId = model.StoreId,
                            SubProductId = model.SubProductId,
                            Quantity = model.sns.Count()
                        };
                        _unitOfWork.Add(storeProduct);
                    }
                    else
                        storeProduct.Quantity += model.sns.Count();

                    var subproduct = await _unitOfWork.ProductRepository.GetSubProduct(model.SubProductId);
                    subproduct.Quantity += model.sns.Count();
                    _unitOfWork.Update(subproduct);

                    StockHistory h = new StockHistory
                    {
                        OpDate = stockmvt.MvtDate,
                        UserId = loggeduserId,
                        InventOpId = inv.Id,
                        StockHistoryActionId = in_stockEntryActionId,
                        StoreId = model.StoreId,
                        SubProductId = model.SubProductId
                    };
                    StockHistory history = await _unitOfWork.ProductRepository.StoreSubProductHistory(model.StoreId, model.SubProductId);
                    if (history == null)
                    {
                        h.OldQty = 0;
                        h.NewQty = model.sns.Count();
                        h.Delta = model.sns.Count();
                    }
                    else
                    {
                        h.OldQty = history.NewQty;
                        h.NewQty = history.NewQty + model.sns.Count();
                        h.Delta = (h.NewQty - h.OldQty);
                    }
                    _unitOfWork.Add(h);

                    foreach (var item in model.sns)
                    {
                        //
                        var prod = new SubProductSN
                        {
                            StoreId = model.StoreId,
                            SubProductId = model.SubProductId,
                            SN = item,
                            InsertUserId = loggeduserId,
                            HaKaDocClientId = hakaDocClientId
                        };
                        _unitOfWork.Add(prod);
                        await _unitOfWork.Complete();

                        InventOpSubProductSN inventProd = new InventOpSubProductSN
                        {
                            InventOpId = inv.Id,
                            SubProductSNId = prod.Id
                        };
                        _unitOfWork.Add(inventProd);
                    }

                    await _unitOfWork.Complete();
                    identityContextTransaction.Commit();
                    done = true;
                }
                catch (System.Exception)
                {
                    identityContextTransaction.Rollback();
                }
            }

            if (done == true) return Ok();
            return BadRequest("impossible d'enregistrer l'entrée en stock");
        }

        [HttpPost("CreateSubProductsNoSN/{hakaDocClientId}")]
        public async Task<ActionResult> CreateSubProductsNoSN(int hakaDocClientId, [FromForm] ProductSNAddingDto model)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            int stockEntryTypeId = _config.GetValue<int>("AppSettings:stockEntryTypeId");
            int in_stockEntryActionId = _config.GetValue<int>("AppSettings:inStockEntryHistoryId");
            var dataContext = _unitOfWork.GetDataContext();
            // var createdIds = await _uow.ProductRepository.CreateSubProductsSN(insertUserId, model, stockEntryTypeId, in_stockEntryActionId);

            bool done = false;


            using (var identityContextTransaction = dataContext.Database.BeginTransaction())

                try
                {
                    //insertion dans stockMvt (une ligne)
                    // insertion dans inventOP (nbre de subproducts)
                    // mise à jour de la quantité de subproduct dans le magasin et dans SubProduct(pour le nombre total)
                    //  insertion dans StockMvtInventOP (nbre de subproducts)
                    // insertion dans StockHistory (nbre de subproducts)


                    //stockMvt
                    var stockmvt = new StockMvt
                    {
                        InventOpTypeId = stockEntryTypeId,
                        ToStoreId = model.StoreId,
                        RefNum = model.RefNum,
                        Note = model.Note,
                        MvtDate = model.MvtDate,
                        InsertUserId = loggeduserId
                    };

                    _unitOfWork.Add(stockmvt);
                    await _unitOfWork.Complete();


                    //InventOp
                    var inventOp = new InventOp
                    {
                        InsertUserId = loggeduserId,
                        InventOpTypeId = stockmvt.InventOpTypeId,
                        OpDate = stockmvt.MvtDate,
                        ToStoreId = stockmvt.ToStoreId,
                        SubProductId = model.SubProductId,
                        FormNum = stockmvt.RefNum,
                        Quantity = model.Quantity
                    };
                    _unitOfWork.Add(inventOp);
                    await _unitOfWork.Complete();

                    //Mise a jour des quantité
                    var subproduct = await _unitOfWork.ProductRepository.GetSubProduct(model.SubProductId);
                    subproduct.Quantity += Convert.ToInt32(model.Quantity);
                    _unitOfWork.Update(subproduct);

                    StoreProduct storeProduct = await _unitOfWork.ProductRepository.StoreProduct(model.StoreId, model.SubProductId);
                    if (storeProduct == null)
                    {
                        storeProduct = new StoreProduct
                        {
                            StoreId = model.StoreId,
                            SubProductId = model.SubProductId,
                            Quantity = Convert.ToInt32(model.Quantity)
                        };
                        _unitOfWork.Add(storeProduct);
                    }
                    else
                        storeProduct.Quantity += Convert.ToInt32(model.Quantity);

                    await _unitOfWork.Complete();


                    //stockMvtInventOp
                    var stockMvtInventOp = new StockMvtInventOp
                    {
                        InventOpId = inventOp.Id,
                        StockMvtId = stockmvt.Id
                    };
                    _unitOfWork.Add(stockMvtInventOp);
                    await _unitOfWork.Complete();

                    //stockHistory
                    StockHistory h = new StockHistory
                    {
                        OpDate = stockmvt.MvtDate,
                        UserId = loggeduserId,
                        InventOpId = inventOp.Id,
                        StockHistoryActionId = in_stockEntryActionId,
                        StoreId = model.StoreId,
                        SubProductId = model.SubProductId
                    };
                    StockHistory history = await _unitOfWork.ProductRepository.StoreSubProductHistory(model.StoreId, model.SubProductId);
                    if (history == null)
                    {
                        h.OldQty = 0;
                        h.NewQty = Convert.ToInt32(model.Quantity);
                        h.Delta = Convert.ToInt32(model.Quantity);
                    }
                    else
                    {
                        h.OldQty = history.NewQty;
                        h.NewQty = (history.NewQty + Convert.ToInt32(model.Quantity));
                        h.Delta = (h.NewQty - h.OldQty);
                    }
                    _unitOfWork.Add(h);
                    await _unitOfWork.Complete();

                    // foreach (var item in model.sns)
                    // {
                    //     //
                    //     var prod = new SubProductSN
                    //     {
                    //         StoreId = model.StoreId,
                    //         SubProductId = model.SubProductId,
                    //         SN = item
                    //     };
                    //     _unitOfWork.Add(prod);
                    //     await _unitOfWork.Complete();

                    //     InventOpSubProductSN inventProd = new InventOpSubProductSN
                    //     {
                    //         InventOpId = inventOp.Id,
                    //         SubProductSNId = prod.Id
                    //     };
                    //     _unitOfWork.Add(inventProd);
                    //     await _unitOfWork.Complete();
                    // }
                    identityContextTransaction.Commit();
                    done = true;
                }
                catch (System.Exception ex)
                {
                    identityContextTransaction.Rollback();
                }

            if (done == true) return Ok();
            return BadRequest("impossible d'enregistrer l'entrée en stock");

        }

        [HttpGet("SubProductSnBySubProductId/{hakaDocClientId}/{subProductId}")]
        public async Task<ActionResult> SubProductSnBySubProductId(int hakaDocClientId, int subProductId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            List<SubProductSN> subProductSNsFromDb = await _unitOfWork.ProductRepository.GetSubProductSnBySubProductId(subProductId);

            return Ok(_mapper.Map<List<SubProductSnListDto>>(subProductSNsFromDb));
        }

        [HttpGet("InventOpSubProdutSNs/{hakaDocClientId}/{inventOpId}/{subProductId}")]
        public async Task<ActionResult> InventOpSubProdutSNs(int hakaDocClientId, int inventOpId, int subProductId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            List<SubProductSN> prodsFromDb = await _unitOfWork.ProductRepository.getInventOpSubProductSNs(inventOpId, subProductId);
            return Ok(_mapper.Map<List<SubProductSnListDto>>(prodsFromDb));
        }

        [HttpDelete("DeleteInventOpSubProductSN/{hakaDocClientId}/{inventOpId}/{subPorductSNId}")]
        public async Task<ActionResult> DeleteInventOpSubProductSN(int hakaDocClientId, int inventOpId, int subPorductSNId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            bool done = false;
            var context = _unitOfWork.GetDataContext();
            using (var identityContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    SubProductSN subProductSn = await _unitOfWork.ProductRepository.GetSubProductSN(subPorductSNId);
                    subProductSn.Active = false;

                    _unitOfWork.Update(subProductSn);

                    SubProduct subProduct = await _unitOfWork.ProductRepository.GetSubProduct(subProductSn.SubProductId);
                    subProduct.Quantity--;
                    _unitOfWork.Update(subProduct);

                    StoreProduct storeProd = await _unitOfWork.ProductRepository.StoreProduct(subProductSn.StoreId, subProduct.Id);
                    storeProd.Quantity--;
                    _unitOfWork.Update(storeProd);

                    var inv = await _unitOfWork.ProductRepository.GetInventOpById(inventOpId);
                    if (inv.InventOpSubProductSNs.Count() == 1)
                    {
                        //une seule ligne de SubProductSn Concernêe...
                        inv.Active = false;
                    }
                    else
                    {
                        inv.Quantity--;
                        _unitOfWork.Update(inv);
                    }

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


        [HttpDelete("DeleteInventOpSubProduct/{hakaDocClientId}/{inventOpId}/{subProductId}")]
        public async Task<ActionResult> DeleteInventOpSubProduct(int hakaDocClientId, int inventOpId, int subProductId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            bool done = false;
            var context = _unitOfWork.GetDataContext();
            using (var identityContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var inv = await _unitOfWork.ProductRepository.GetInventOpById(inventOpId);
                    inv.Active = false;
                    _unitOfWork.Update(inv);

                    SubProduct subProduct = await _unitOfWork.ProductRepository.GetSubProduct(subProductId);
                    subProduct.Quantity = subProduct.Quantity - Convert.ToInt32(inv.Quantity);
                    _unitOfWork.Update(subProduct);

                    StoreProduct storeProd = await _unitOfWork.ProductRepository.StoreProduct(Convert.ToInt32(inv.ToStoreId),subProduct.Id);
                    storeProd.Quantity=storeProd.Quantity-Convert.ToInt32(inv.Quantity);
                    _unitOfWork.Update(storeProd);

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


        [HttpGet("CanDeleteSubProductInventOp/{hakaDocClientId}/{subProductId}/{storeId}/{quantity}")]
        public async Task<ActionResult> CanDeleteSubProductInventOp(int hakaDocClientId, int quantity, int subProductId, int storeId)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();
            StoreProduct storeProd = await _unitOfWork.StoreRepository.GetStoreProduct(storeId, subProductId);
            if (storeProd.Quantity >= quantity) return Ok(true);
            return Ok(false);
        }


    }
}