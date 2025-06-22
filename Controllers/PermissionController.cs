using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();
        [HttpGet]
        [Route("/Permission/GetPermission/{id}")]
        public IActionResult GetPermission(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetPermission(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Permission/GetListPermission")]
        public IActionResult GetListPermission(PermissionFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListPermission(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Permission/CreatePermission")]
        public IActionResult CreatePermission(Permission permission)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreatePermission(permission);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreatePermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Permission/UpdatePermission")]
        public IActionResult UpdatePermission(Permission permission)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdatePermission(permission);
                return ApiHelpers.CreateSuccessResult(permission, nameof(UpdatePermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Permission/DeletePermission/{hardDelete}")]
        public IActionResult DeletePermission(Permission inventoryItem, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeletePermission(inventoryItem, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeletePermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}