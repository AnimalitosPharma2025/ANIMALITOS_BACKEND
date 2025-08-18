using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<NotificationsUser> GetListNotificationsUser(NotificationsUserFilter filter)
        {
            IQueryable<Models.NotificationsUser> query = from m in _EntityContext.NotificationsUsers select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.IsRead != null)
                query = query.Where(m => m.IsRead == filter.IsRead);
            if (filter.UserId > 0)
                query = query.Where(m => m.UserId == filter.UserId);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<NotificationsUser> context = new List<NotificationsUser>();
            foreach (Models.NotificationsUser tempitem in tempStuff)
                context.Add(ConvertNotificationsUser_ToAccessorContract(tempitem));

            return context;
        }

        public Notification GetNotification(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Notifications.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertNotification_ToAccessorContract(objTemp);
        }

        public Notification CreateNotification(Notification obj)
        {
            var newObj = ConvertNotification_ToAccessorModel(obj);

            _EntityContext.Notifications.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertNotification_ToAccessorContract(newObj);
        }

        public Notification UpdateNotification(Notification obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Notifications.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Title = obj.Title;
            objTemp.Message = obj.Message;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Notifications.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertNotification_ToAccessorContract(objTemp);
        }

        public Notification DeleteNotification(Notification obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Notifications.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Notifications.Remove(objTemp) : _EntityContext.Notifications.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertNotification_ToAccessorContract(objTemp);
        }

        private NotificationsUser ConvertNotificationsUser_ToAccessorContract(Models.NotificationsUser tempitem)
        {
            var newObj = new NotificationsUser
            {
                Id = tempitem.Id,
                IsRead = tempitem.IsRead,
                UserId = tempitem.UserId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.NotificationsUser ConvertNotificationsUser_ToAccessorModel(NotificationsUser tempitem)
        {
            var newObj = new Models.NotificationsUser
            {
                Id = tempitem.Id,
                IsRead = tempitem.IsRead,
                UserId = tempitem.UserId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}