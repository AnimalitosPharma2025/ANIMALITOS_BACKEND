using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using ANIMALITOS_PHARMA_API.Custom;
using ANIMALITOS_PHARMA_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Common;

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
                    Username = userTemp.Username,
                    //Permissions = CheckUserPermissions(userTemp.Id),
                    Token = token
                };
                return ApiHelpers.CreateSuccessResult(objectReturn, nameof(SignIn));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        //public List<Permission> CheckUserPermissions(int userId)
        //{
        //    var permissions = _entityContext.Permissions.ToList();
        //    var rols = _entityContext.Rols.ToList();


        //    //IQueryable<Models.RolPermission> query = from rp in _entityContext.RolPermissions 
        //    //                                         where rp.

        //    var rolPermission = _entityContext.RolPermissions.
        //}

        [HttpPost]
        [Route("/Authorization/SignUp")]
        public IActionResult SignUp(Contracts.User user)
        {
            try
            {
                if (user.Id > 0)
                    throw new Exception($"Object with Id of {user.Id} exist.");

                var newObj = new Models.User
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = _encrypt.Hash(user.Password),
                    EmployeeId = user.EmployeeId,
                    StatusId = user.StatusId
                };

                _entityContext.Users.Add(newObj);
                _entityContext.SaveChanges();

                dynamic objectReturn = new
                {
                    newObj.Id,
                    newObj.Username,
                    newObj.EmployeeId,
                    newObj.StatusId
                };

                return ApiHelpers.CreateSuccessResult(objectReturn, nameof(SignUp));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }

}