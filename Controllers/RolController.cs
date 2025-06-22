using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/Rol/GetRol/{id}")]
        public IActionResult GetRol(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetRol(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetRol));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Rol/GetListRol")]
        public IActionResult GetListRol(RolFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListRol(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListRol));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Rol/CreateRol")]
        public IActionResult CreateRol(Rol rol)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateRol(rol);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateRol));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Rol/UpdateRol")]
        public IActionResult UpdateRol(Rol rol)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateRol(rol);
                return ApiHelpers.CreateSuccessResult(rol, nameof(UpdateRol));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Rol/DeleteRol/{hardDelete}")]
        public IActionResult DeleteRol(Rol rol, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteRol(rol, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteRol));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}