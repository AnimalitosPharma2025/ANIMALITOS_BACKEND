using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreditController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();
        [HttpGet]
        [Route("/Credit/GetCredit/{id}")]
        public IActionResult GetCredit(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetCredit(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetCredit));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Credit/GetListCredit")]
        public IActionResult GetListCredit(CreditFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListCredit(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListCredit));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Credit/CreateCredit")]
        public IActionResult CreateCredit(Credit credit)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateCredit(credit);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateCredit));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Credit/UpdateCredit")]
        public IActionResult UpdateCredit(Credit credit)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateCredit(credit);
                return ApiHelpers.CreateSuccessResult(credit, nameof(UpdateCredit));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Credit/DeleteCredit/{hardDelete}")]
        public IActionResult DeleteCredit(Credit credit, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteCredit(credit, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteCredit));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}