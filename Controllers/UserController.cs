using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Contracts;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/User/GetUser/{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetUser(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/User/GetListUser")]
        public IActionResult GetListUser(UserFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListUser(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/User/CreateUser")]
        public IActionResult CreateUser(User user)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateUser(user);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/User/UpdateUser")]
        public IActionResult UpdateDelete(User user)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateUser(user);
                return ApiHelpers.CreateSuccessResult(user, nameof(UpdateDelete));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/User/DeleteUser/{hardDelete}")]
        public IActionResult DeleteUser(User user, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteUser(user, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}
