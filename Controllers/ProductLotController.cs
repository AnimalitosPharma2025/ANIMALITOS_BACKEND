using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductLotController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/ProductLot/GetProductLot/{id}")]
        public IActionResult GetProductLot(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetProductLot(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetProductLot));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/ProductLot/GetListProductLot")]
        public IActionResult GetListProductLot(ProductLotFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListProductLot(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListProductLot));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/ProductLot/CreateProductLot")]
        public IActionResult CreateProductLot(ProductLot productLot)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateProductLot(productLot);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateProductLot));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/ProductLot/UpdateProductLot")]
        public IActionResult UpdateProductLot(ProductLot productLot)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateProductLot(productLot);
                return ApiHelpers.CreateSuccessResult(productLot, nameof(UpdateProductLot));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/ProductLot/DeleteProductLot/{hardDelete}")]
        public IActionResult DeleteProductLot(ProductLot productLot, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteProductLot(productLot, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteProductLot));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}