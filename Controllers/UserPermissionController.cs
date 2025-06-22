using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/UserPermission/GetUserPermission/{id}")]
        public IActionResult GetUserPermission(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetUserPermission(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetUserPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/UserPermission/GetListUserPermission")]
        public IActionResult GetListUserPermission(UserPermissionFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListUserPermission(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListUserPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/UserPermission/CreateUserPermission")]
        public IActionResult CreateUserPermission(UserPermission userPermission)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateUserPermission(userPermission);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateUserPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/UserPermission/UpdateUserPermission")]
        public IActionResult UpdateUserPermission(UserPermission userPermission)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateUserPermission(userPermission);
                return ApiHelpers.CreateSuccessResult(userPermission, nameof(UpdateUserPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/UserPermission/DeleteUserPermission/{hardDelete}")]
        public IActionResult DeleteUserPermission(UserPermission userPermission, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteUserPermission(userPermission, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteUserPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}