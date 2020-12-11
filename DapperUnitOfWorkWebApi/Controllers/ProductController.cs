using System.Threading.Tasks;
using DapperUnitOfWork.Repo;
using DapperUnitOfWork.Repo.Entities;
using DapperUnitOfWorkLib.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DapperUnitOfWorkWebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController: ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IProductRepository _productRepo;

        public ProductController(
            IUnitOfWork uow,
            IProductRepository productRepo
        )
        {
            this._uow = uow;
            this._productRepo = productRepo;
        }



        [HttpGet("getPaginated")]
        public ActionResult GetPaginated(int current,int itemsPerPage) {
            _uow.BeginTrans();
            int total = 0;
            var result = _productRepo.GetPaginated(ref total,current,itemsPerPage);
            _uow.Commit();

            return Ok(new {
               Total = total,
               List= result
            });
        }

        [HttpGet("getProductById")]
        public ActionResult GetProductById(int id){
            _uow.BeginTrans();
            var result =_productRepo.Get(id);
            _uow.Commit();

            return Ok(result);
        }


        [HttpGet("getProductAll")]
        public ActionResult GetProductAll(){
            _uow.BeginTrans();
            var result = _productRepo.GetAll();
            _uow.Commit();
            return Ok(result);
        }

        [HttpPost("addProduct")]
        public ActionResult AddProduct(Product product){
            _uow.BeginTrans();
            _productRepo.Insert(product);
            var result = _productRepo.Get(product.Id);
            _uow.Commit();
            return Ok(result);
        }


        [HttpPut("updateProduct")]
        public ActionResult UpdateProduct(Product product){
            _uow.BeginTrans();
            _productRepo.Update(product);
            var result = _productRepo.Get(product.Id);
            _uow.Commit();
            return Ok(result);
        }

        [HttpDelete("deleteProduct")]
        public ActionResult DeleteProduct(int id){
            var product = _productRepo.Get(id);
            _productRepo.Delete(product);
            return Ok();
        }




        
        [HttpGet("getPaginatedAsync")]
        public async Task<ActionResult> GetPaginatedAsync(int current,int itemsPerPage) {
            _uow.BeginTrans();
            var result = await _productRepo.GetPaginatedAsync(current,itemsPerPage);
            _uow.Commit();

            return Ok(new {
               Total = result.total,
               List= result.list
            });
        }

        
        [HttpGet("getProductByIdAsync")]
        public async Task<ActionResult> GetProductByIdAsync(int id){
            _uow.BeginTrans();
            var result = await _productRepo.GetTaskAsync(id);
            _uow.Commit();

            return Ok(result);
        }


        [HttpPost("addProductAsync")]
        public async Task<ActionResult> AddProductAsync(Product product){
            _uow.BeginTrans();
            await _productRepo.InsertAsync(product);
            var result = await _productRepo.GetTaskAsync(product.Id);
            _uow.Commit();
            return Ok(result);
        }


        [HttpPut("updateProductAsync")]
        public async Task<ActionResult> UpdateProductAsync(Product product){
            _uow.BeginTrans();
            await _productRepo.UpdateAsync(product);
            var result = await _productRepo.GetTaskAsync(product.Id);
            _uow.Commit();
            return Ok(result);
        }

        [HttpDelete("deleteProductAsync")]
        public async Task<ActionResult> DeleteProductAsync(int id){
            var product = await _productRepo.GetTaskAsync(id);
            await _productRepo.DeleteAsync(product);
            return Ok();
        }


        
    }
}