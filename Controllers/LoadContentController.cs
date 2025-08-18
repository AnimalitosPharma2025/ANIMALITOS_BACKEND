using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Contracts;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoadsContentController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/LoadContent/GetLoadContent/{id}")]
        public IActionResult GetLoadContent(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetLoadContent(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetLoadContent));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/LoadContent/GetListLoadContent")]
        public IActionResult GetListLoadContent(LoadsContentFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListLoadContent(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListLoadContent));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/LoadContent/CreateLoadContent")]
        public IActionResult CreateLoadContent(LoadsContent loadContent)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateLoadContent(loadContent);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateLoadContent));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/LoadContent/UpdateLoadContent")]
        public IActionResult UpdateLoadContent(LoadsContent loadContent)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateLoadContent(loadContent);
                return ApiHelpers.CreateSuccessResult(loadContent, nameof(CreateLoadContent));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/LoadContent/DeleteLoadContent/{hardDelete}")]
        public IActionResult DeleteLoad(LoadsContent loadContent, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteLoadContent(loadContent, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteLoad));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }

}