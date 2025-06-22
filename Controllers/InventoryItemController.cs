using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryItemController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();
        [HttpGet]
        [Route("/InventoryItem/GetInventoryItem/{id}")]
        public IActionResult GetInventoryItem(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetInventoryItem(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetInventoryItem));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/InventoryItem/GetListInventoryItem")]
        public IActionResult GetListInventoryItem(InventoryItemFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListInventoryItem(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListInventoryItem));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/InventoryItem/CreateInventoryItem")]
        public IActionResult CreateInventoryItem(InventoryItem inventoryItem)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateInventoryItem(inventoryItem);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateInventoryItem));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/InventoryItem/UpdateInventoryItem")]
        public IActionResult UpdateInventoryItem(InventoryItem inventoryItem)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateInventoryItem(inventoryItem);
                return ApiHelpers.CreateSuccessResult(inventoryItem, nameof(UpdateInventoryItem));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/InventoryItem/DeleteInventoryItem/{hardDelete}")]
        public IActionResult DeleteInventoryItem(InventoryItem inventoryItem, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteInventoryItem(inventoryItem, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteInventoryItem));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}