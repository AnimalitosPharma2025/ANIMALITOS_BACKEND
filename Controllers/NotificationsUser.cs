using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Contracts;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsUser : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/NotificationsUser/GetNotificationUser/{id}")]
        public IActionResult GetNotificationUser(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetNotificationUser(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetNotificationUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/NotificationsUser/GetListNotificationsUser")]
        public IActionResult GetListNotificationsUser(NotificationsUserFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListNotificationsUser(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListNotificationsUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/NotificationsUser/CreateNotificationUser")]
        public IActionResult CreateNotificationUser(Contracts.NotificationsUser notification)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateNotificationUser(notification);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateNotificationUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/NotificationsUser/UpdateNotificationUser")]
        public IActionResult UpdateNotificationUser(Contracts.NotificationsUser notification)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateNotificationUser(notification);
                return ApiHelpers.CreateSuccessResult(notification, nameof(UpdateNotificationUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/NotificationsUser/DeleteNotificationUser/{hardDelete}")]
        public IActionResult DeleteNotificationUser(Contracts.NotificationsUser notification, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteNotificationUser(notification, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteNotificationUser));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }

}