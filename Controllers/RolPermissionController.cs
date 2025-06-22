using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolPermissionController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/RolPermission/GetRolPermission/{id}")]
        public IActionResult GetRolPermission(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetRolPermission(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetRolPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/RolPermission/GetListRolPermission")]
        public IActionResult GetListRolPermission(RolPermissionFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListRolPermission(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListRolPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/RolPermission/CreateRolPermission")]
        public IActionResult CreateRolPermission(RolPermission rolPermission)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateRolPermission(rolPermission);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateRolPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/RolPermission/UpdateRolPermission")]
        public IActionResult UpdateRolPermission(RolPermission rolPermission)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateRolPermission(rolPermission);
                return ApiHelpers.CreateSuccessResult(rolPermission, nameof(UpdateRolPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/RolPermission/DeleteRolPermission/{hardDelete}")]
        public IActionResult DeleteRolPermission(RolPermission rolPermission, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteRolPermission(rolPermission, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteRolPermission));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}