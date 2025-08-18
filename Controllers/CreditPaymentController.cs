using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Contracts;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreditPaymentController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/CreditPayment/GetCreditPayment/{id}")]
        public IActionResult GetCreditPayment(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetCreditPayment(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetCreditPayment));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/CreditPayment/GetListCreditPayment")]
        public IActionResult GetListCreditPayment(CreditPaymentFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListCreditPayment(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListCreditPayment));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/CreditPayment/CreateCreditPayment")]
        public IActionResult CreateCreditPayment(CreditPayment creditPayment)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateCreditPayment(creditPayment);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateCreditPayment));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/CreditPayment/UpdateCreditPayment")]
        public IActionResult UpdateCreditPayment(CreditPayment creditPayment)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateCreditPayment(creditPayment);
                return ApiHelpers.CreateSuccessResult(creditPayment, nameof(CreateCreditPayment));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/CreditPayment/DeleteCreditPayment/{hardDelete}")]
        public IActionResult DeleteCreditPayment(CreditPayment creditPayment, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteCreditPayment(creditPayment, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteCreditPayment));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }

}