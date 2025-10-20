using ANIMALITOS_PHARMA_API.Accessors.Util.StatusEnumerable;
using ANIMALITOS_PHARMA_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace ANIMALITOS_PHARMA_API.Services
{
    public class ProductLotChecker
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductLotChecker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Crea una notificación simple y la asigna a roles específicos
        /// </summary>
        public async Task ExecuteAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AnimalitosPharmaContext>();

            var notification = new Models.Notification
            {
                Title = "Este mensaje debe estar el dia 21 de Octubre",
                Message = "Primer mensaje de prueba para notificaciones automatizadas TEST",
                CreatedDate = DateTime.Now,
                StatusId = (int)ObjectStatus.ACTIVE
            };

            context.Notifications.Add(notification);
            await context.SaveChangesAsync();

            var notificationUser = new Models.NotificationsUser
            {
                IsRead = false,
                UserId = 8,
                NotificationId = notification.Id,
                StatusId = (int)ObjectStatus.UNIT_TEST_CREATE,
            };

            context.NotificationsUsers.Add(notificationUser);
            await context.SaveChangesAsync();
            //// 🔹 Asignar a usuarios con roles específicos
            //var targetUsers = await context.Users
            //    .Where(u => new[] { (int)Roles.Admin, (int)Roles.Pharmacist }.Contains(u.RoleId))
            //    .ToListAsync();

            //foreach (var user in targetUsers)
            //{
            //    context.NotificationUsers.Add(new NotificationUser
            //    {
            //        UserId = user.Id,
            //        NotificationId = notification.Id,
            //        StatusId = (int)ObjectStatus.ACTIVE,
            //        IsRead = false,
            //        ReadDate = null
            //    });
            //}

            //await context.SaveChangesAsync();
        }
    }
}