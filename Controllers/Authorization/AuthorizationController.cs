using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using ANIMALITOS_PHARMA_API.Custom;
using ANIMALITOS_PHARMA_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers.Authorization
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ANIMALITOS_CLIENT _animalitos_pharma;
        private static readonly EncryptHasher _encrypt = new();
        private readonly AnimalitosPharmaContext _entityContext;

        public AuthorizationController(AnimalitosPharmaContext entityContext, ANIMALITOS_CLIENT animalitos_pharma)
        {
            _entityContext = entityContext;
            _animalitos_pharma = animalitos_pharma;
        }

        [HttpPost]
        [Route("/Authorization/SignIn")]
        public IActionResult SignIn(Contracts.User user)
        {
            try
            {
                var userTemp = _entityContext.Users.SingleOrDefault(m => m.Username == user.Username);
                if (userTemp is null)
                    throw new Exception($"Object with Id of '{user.Username}' does exist.");

                if (!_encrypt.Verify(userTemp.Password, user.Password))
                    throw new Exception("Incorrect credentials.");

                var token = _animalitos_pharma.GenerateToken(user);
                dynamic objectReturn = new
                {
                    Username = user.Username,
                    Token = token
                };
                return ApiHelpers.CreateSuccessResult(objectReturn, nameof(SignIn));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
        //[HttpPost]
        //[Route("/Authorization/SignUp")]
        //public IActionResult SignUp(Contracts.User user)
        //{
        //    try
        //    {
        //        //var obj = accessor.SignUp(user);
        //        return ApiHelpers.CreateSuccessResult(obj, nameof(SignUp));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiHelpers.CreateBadResult(ex);
        //    }
        //}
    }

}