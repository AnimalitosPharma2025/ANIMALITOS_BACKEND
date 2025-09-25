using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();
        [HttpGet]
        [Route("/Product/GetProduct/{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetProduct(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetProduct));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Product/GetListProduct")]
        public IActionResult GetListProduct(ProductFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListProduct(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListProduct));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Product/GetAllProducts")]
        public IActionResult LoadProductTable()
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.LoadProductTable();
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListProduct));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Product/CreateProduct")]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateProduct(product);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateProduct));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        //[HttpPost]
        //[Route("/Product/ImportCSVProduct")]
        //public IActionResult ImportCSVProduct(IFormFile fileProduct)
        //{
        //    try
        //    {
        //        accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
        //        var obj = accessor.ImportCSVProduct(fileProduct);
        //        return ApiHelpers.CreateSuccessResult(obj, nameof(ImportCSVProduct));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiHelpers.CreateBadResult(ex);
        //    }
        //}

        [HttpPut]
        [Route("/Product/UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateProduct(product);
                return ApiHelpers.CreateSuccessResult(product, nameof(UpdateProduct));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Product/DeleteProduct/{hardDelete}")]
        public IActionResult DeleteProduct(Product product, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteProduct(product, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteProduct));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}