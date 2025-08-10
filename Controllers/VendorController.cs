using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/Vendor/GetUserVendor/{id}")]
        public IActionResult GetVendor(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetVendor(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetVendor));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Vendor/GetListVendor")]
        public IActionResult GetListVendor(VendorFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListUserVendor(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListVendor));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Vendor/LoadVendorTable")]
        public IActionResult LoadVendorTable()
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.LoadVendorTable();
                return ApiHelpers.CreateSuccessResult(obj, nameof(LoadVendorTable));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Vendor/CreateVendor")]
        public IActionResult CreateVendor(Vendor vendor)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateVendor(vendor);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateVendor));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Vendor/UpdateVendor")]
        public IActionResult UpdateVendor(Vendor vendor)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateVendor(vendor);
                return ApiHelpers.CreateSuccessResult(vendor, nameof(UpdateVendor));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Vendor/DeleteVendor/{hardDelete}")]
        public IActionResult DeleteVendor(Vendor vendor, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteVendor(vendor, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteVendor));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}