using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Contracts;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressBookController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/AddressBook/GetAddressBook/{id}")]
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

        [HttpPost]
        [Route("/AddressBook/GetListAddressBook")]
        public IActionResult GetListAddressBook(AddressBookFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListAddressBook(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListAddressBook));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/AddressBook/CreateAddressBook")]
        public IActionResult CreateAddressBook(AddressBook addressBook)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateAddressBook(addressBook);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateAddressBook));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/AddressBook/UpdateAddressBook")]
        public IActionResult UpdateAddressBook(AddressBook addressBook)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateAddressBook(addressBook);
                return ApiHelpers.CreateSuccessResult(addressBook, nameof(UpdateAddressBook));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/AddressBook/DeleteAddressBook/{hardDelete}")]
        public IActionResult DeleteAddressBook(AddressBook addressBook, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteAddressBook(addressBook, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteAddressBook));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }

}
