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
                    UserId = userTemp.Id,
                    userTemp.Username,
                    Permissions = CheckUserPermissions(userTemp.Id),
                    Token = token
                };
                return ApiHelpers.CreateSuccessResult(objectReturn, nameof(SignIn));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
        private List<String> CheckUserPermissions(int userId)
        {
            var userRol = _entityContext.UserRols
                .Where(x => x.UserId == userId)
                .SingleOrDefault();

            if (userRol is null)
                throw new Exception($"The user with the ID {userRol.Id} has no role");

            var rolPermission = _entityContext.RolPermissions
                .Where(x => x.RolId == userRol.Id)
                .ToList();

            var listUserPermission = new List<string>();
            foreach (var permission in rolPermission)
            {
                listUserPermission.Add(
                    _entityContext.Permissions.Where(x => x.Id == permission.PermissionId).SingleOrDefault().Name.ToString()
                );
            }

            return listUserPermission;
        }

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