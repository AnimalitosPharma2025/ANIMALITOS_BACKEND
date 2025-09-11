using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoadController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/Load/GetLoad/{id}")]
        public IActionResult GetLoad(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetLoad(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetLoad));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Load/GetListLoad")]
        public IActionResult GetListLoad(LoadFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListLoad(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListLoad));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Load/LoadDataforModal")]
        public IActionResult LoadDataforModal()
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.LoadDataforModal();
                return ApiHelpers.CreateSuccessResult(obj, nameof(LoadDataforModal));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Load/ConstructTableLoads")]
        public IActionResult ConstructTableLoads()
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.ConstructTableLoads();
                return ApiHelpers.CreateSuccessResult(obj, nameof(ConstructTableLoads));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Load/GetLoadForEmployee/{id}")]
        public IActionResult GetLoadForEmployee(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetLoadForEmployee(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetLoadForEmployee));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Load/CreateLoadAndContent")]
        public IActionResult CreateLoadAndContent(dynamic load)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateLoadAndContent(load);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateLoadAndContent));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Load/UpdateLoadAndContent")]
        public IActionResult UpdateLoadAndContent(dynamic load)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateLoadAndContent(load);
                return ApiHelpers.CreateSuccessResult(obj, nameof(UpdateLoadAndContent));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Load/CreateLoad")]
        public IActionResult CreateLoad(Load load)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateLoad(load);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateLoad));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Load/UpdateLoad")]
        public IActionResult UpdateLoad(Load load)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateLoad(load);
                return ApiHelpers.CreateSuccessResult(load, nameof(CreateLoad));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Load/DeleteLoad/{hardDelete}")]
        public IActionResult DeleteLoad(Load load, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteLoad(load, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteLoad));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }

}