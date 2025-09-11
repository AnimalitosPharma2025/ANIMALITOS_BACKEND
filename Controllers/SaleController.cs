using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/Sale/GetSale/{id}")]
        public IActionResult GetSale(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetSale(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetSale));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Sale/GetListSale")]
        public IActionResult GetListSale(SaleFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListSale(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListSale));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Sale/LoadSalesTable")]
        public IActionResult LoadSalesTable(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.LoadSalesTable();
                return ApiHelpers.CreateSuccessResult(obj, nameof(LoadSalesTable));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Sale/CreateSale")]
        public IActionResult CreateSale(Sale sale)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateSale(sale);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateSale));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Sale/UpdateSale")]
        public IActionResult UpdateSale(Sale sale)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateSale(sale);
                return ApiHelpers.CreateSuccessResult(sale, nameof(UpdateSale));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Sale/DeleteRolSale/{hardDelete}")]
        public IActionResult DeleteSale(Sale sale, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteSale(sale, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteSale));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}