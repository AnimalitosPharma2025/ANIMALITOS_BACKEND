using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();
        [HttpGet]
        [Route("/Client/GetClient/{id}")]
        public IActionResult GetClient(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetClient(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetClient));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Client/GetListClient")]
        public IActionResult GetListClient(ClientFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListClient(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListClient));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Client/LoadClientTable")]
        public IActionResult LoadClientTable()
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.LoadClientTable();
                return ApiHelpers.CreateSuccessResult(obj, nameof(LoadClientTable));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Client/CreateClient")]
        public IActionResult CreateClient(Client client)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateClient(client);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateClient));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Client/UpdateClient")]
        public IActionResult UpdateClient(Client client)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateClient(client);
                return ApiHelpers.CreateSuccessResult(client, nameof(UpdateClient));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Client/DeleteClient/{hardDelete}")]
        public IActionResult DeleteClient(Client client, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteClient(client, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteClient));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}