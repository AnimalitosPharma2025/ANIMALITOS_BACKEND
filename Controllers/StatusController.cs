using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/Status/GetStatus/{id}")]
        public IActionResult GetStatus(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetStatus(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetStatus));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Status/GetListStatus")]
        public IActionResult GetListStatus(StatusFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListStatus(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListStatus));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Status/CreateStatus")]
        public IActionResult CreateStatus(Status status)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateStatus(status);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateStatus));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Status/UpdateStatus")]
        public IActionResult UpdateStatus(Status status)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateStatus(status);
                return ApiHelpers.CreateSuccessResult(status, nameof(UpdateStatus));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Status/DeleteStatus/{hardDelete}")]
        public IActionResult DeleteStatus(Status status, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteStatus(status, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteStatus));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}