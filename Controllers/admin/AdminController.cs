using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Contracts;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/Administrator/GetUser")]
        public IActionResult GetAddressBook(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetAddressBook(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetAddressBook));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}
