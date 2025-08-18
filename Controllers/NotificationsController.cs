using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Contracts;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Notifications : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();

        [HttpGet]
        [Route("/Notifications/GetNotification/{id}")]
        public IActionResult GetNotification(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetNotification(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetNotification));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Notifications/GetListNotifications")]
        public IActionResult GetListNotifications(NotificationFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListNotifications(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListNotifications));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Notifications/CreateNotification")]
        public IActionResult CreateNotification(Notification notification)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateNotification(notification);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateNotification));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Notifications/UpdateNotification")]
        public IActionResult UpdateNotification(Notification notification)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateNotification(notification);
                return ApiHelpers.CreateSuccessResult(notification, nameof(UpdateNotification));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Notifications/DeleteNotification/{hardDelete}")]
        public IActionResult DeleteNotification(Notification notification, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteNotification(notification, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteNotification));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }

}