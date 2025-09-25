//using ANIMALITOS_PHARMA_API.Accessors.Util.StatusEnumerable;
//using ANIMALITOS_PHARMA_API.Models;
//using Microsoft.EntityFrameworkCore;
//using System.Data;

//public class ProductLotExpirationService : BackgroundService
//{
//    private readonly IServiceProvider _serviceProvider;

//    public ProductLotExpirationService(IServiceProvider serviceProvider)
//    {
//        _serviceProvider = serviceProvider;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        while (!stoppingToken.IsCancellationRequested)
//        {
//            var now = DateTime.Now;
//            var nextRun = DateTime.Today.AddHours(6); // todos los días a las 6:00 AM

//            if (now > nextRun)
//                nextRun = nextRun.AddDays(1);

//            var delay = nextRun - now;
//            await Task.Delay(delay, stoppingToken);

//            await CheckLots();
//            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
//        }
//    }

//    private async Task CheckLots()
//    {
//        using var scope = _serviceProvider.CreateScope();
//        var context = scope.ServiceProvider.GetRequiredService<AnimalitosPharmaContext>();

//        var today = DateTime.Today;
//        var twoMonthsAhead = today.AddMonths(2);

//        // 🔹 1. Lotes por caducar
//        var aboutToExpireLots = context.ProductLots
//            .Where(lot => lot.Expiration <= twoMonthsAhead &&
//                          lot.Expiration > today &&
//                          lot.StatusId == (int)ObjectStatus.ACTIVE)
//            .ToList();

//        foreach (var lot in aboutToExpireLots)
//        {
//            await CreateNotificationForRoles(context,
//                "Lote por caducar",
//                $"El lote {lot.Id} del producto {lot.ProductId} caduca el {lot.Expiration:d}.",
//                new[] { (int)Roles.Admin, (int)Roles.Pharmacist });
//        }

//        // 🔹 2. Lotes caducados
//        var expiredLots = context.ProductLots
//            .Where(lot => lot.Expiration < today &&
//                          lot.StatusId != (int)ObjectStatus.PRODUCT_LOT_EXPIRED)
//            .ToList();

//        foreach (var lot in expiredLots)
//        {
//            lot.StatusId = (int)ObjectStatus.PRODUCT_LOT_EXPIRED;

//            await CreateNotificationForRoles(context,
//                "Lote caducado",
//                $"El lote {lot.Id} del producto {lot.ProductId} ha caducado.",
//                new[] { (int)Roles.Admin, (int)Roles.Pharmacist });
//        }

//        await context.SaveChangesAsync();
//    }

//    /// <summary>
//    /// Crea una notificación y la asigna a los usuarios con roles específicos.
//    /// </summary>
//    private async Task CreateNotificationForRoles(
//        AnimalitosPharmaContext context,
//        string title,
//        string message,
//        int[] roleIds)
//    {
//        var notification = new Notification
//        {
//            Title = title,
//            Message = message,
//            CreatedDate = DateTime.Now,
//            StatusId = (int)ObjectStatus.ACTIVE
//        };

//        context.Notifications.Add(notification);
//        await context.SaveChangesAsync();

//        var targetUsers = context.Users
//            .Where(u => roleIds.Contains(u.RoleId))
//            .ToList();

//        foreach (var user in targetUsers)
//        {
//            context.NotificationUsers.Add(new NotificationUser
//            {
//                UserId = user.Id,
//                NotificationId = notification.Id,
//                StatusId = (int)ObjectStatus.ACTIVE,
//                IsRead = false,
//                ReadDate = null
//            });
//        }

//        await context.SaveChangesAsync();
//    }
//}
