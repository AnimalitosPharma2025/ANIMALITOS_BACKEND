using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<UserPermission> GetListUserPermission(UserPermissionFilter filter)
        {
            IQueryable<Models.UserPermission> query = from m in _EntityContext.UserPermissions select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.UserId > 0)
                query = query.Where(m => m.UserId == filter.UserId);
            if (filter.PermissionId > 0)
                query = query.Where(m => m.PermissionId == filter.PermissionId);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<UserPermission> context = new List<UserPermission>();
            foreach (Models.UserPermission tempitem in tempStuff)
                context.Add(ConvertUserPermission_ToAccessorContract(tempitem));

            return context;
        }

        public UserPermission GetUserPermission(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.UserPermissions.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertUserPermission_ToAccessorContract(objTemp);
        }

        public UserPermission CreateUserPermission(UserPermission obj)
        {
            var newObj = ConvertUserPermission_ToAccessorModel(obj);

            _EntityContext.UserPermissions.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertUserPermission_ToAccessorContract(newObj);
        }

        public UserPermission UpdateUserPermission(UserPermission obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.UserPermissions.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.UserId = obj.UserId;
            objTemp.PermissionId = obj.PermissionId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.UserPermissions.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertUserPermission_ToAccessorContract(objTemp);
        }

        public UserPermission DeleteUserPermission(UserPermission obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.UserPermissions.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            _EntityContext.UserPermissions.Remove(objTemp);
            _EntityContext.SaveChanges();
            return ConvertUserPermission_ToAccessorContract(objTemp);
        }

        private UserPermission ConvertUserPermission_ToAccessorContract(Models.UserPermission tempitem)
        {
            var newObj = new UserPermission
            {
                Id = tempitem.Id,
                UserId = tempitem.UserId,
                PermissionId = tempitem.PermissionId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.UserPermission ConvertUserPermission_ToAccessorModel(UserPermission tempItem)
        {
            var newObj = new Models.UserPermission
            {
                Id = tempItem.Id,
                UserId = tempItem.UserId,
                PermissionId = tempItem.PermissionId,
                StatusId = tempItem.StatusId
            };

            return newObj;
        }
    }
}