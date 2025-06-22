using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ANIMALITOS_PHARMA_API.Controllers.Helpers
{
    public static class ApiHelpers
    {
        public static IActionResult CreateSuccessResult(object data, string message)
        {
            var dataCollection = new[] { data };
            return CreateSuccessResult(dataCollection, message);
        }

        public static IActionResult CreateSuccessResult(IEnumerable<object> data, string message)
        {
            return new ObjectResult(
                new List<ResultHelper> {
                    new() {
                        IsSuccess = true,
                        Message = message,
                        ResultObject = data.ToList()
                    }
                });
        }

        public static IActionResult CreateBadResult(Exception e)
        {
            return new BadRequestObjectResult(
                new List<ResultHelper> {
                    ErrorHandler.GetErrorResult(e)
                });
        }
        public static object AuthorizationUser(HttpRequest request)
        {
            var requestAuthorization = request.Headers.Authorization.ToString();
            if (requestAuthorization.IsNullOrEmpty() != false)
            {
                throw new Exception("User not authorized or not registered");
            }

            return request.Headers["Authorization"].ToString();
        }
    }
}