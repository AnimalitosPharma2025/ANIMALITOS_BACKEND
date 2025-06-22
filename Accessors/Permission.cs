using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Permission> GetListPermission(PermissionFilter filter)
        {
            IQueryable<Models.Permission> query = from m in _EntityContext.Permissions select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.Name == filter.Name);
            if (!string.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(m => m.Description == filter.Description);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Permission> context = new List<Permission>();
            foreach (Models.Permission tempitem in tempStuff)
                context.Add(ConvertPermission_ToAccessorContract(tempitem));

            return context;
        }

        public Permission GetPermission(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Permissions.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertPermission_ToAccessorContract(objTemp);
        }

        public Permission CreatePermission(Permission obj)
        {
            var newObj = ConvertPermission_ToAccessorModel(obj);

            _EntityContext.Permissions.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertPermission_ToAccessorContract(newObj);
        }

        public Permission UpdatePermission(Permission obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Permissions.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Name = obj.Name;
            objTemp.Description = obj.Description;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Permissions.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertPermission_ToAccessorContract(objTemp);
        }

        public Permission DeletePermission(Permission obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Permissions.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Permissions.Remove(objTemp) : _EntityContext.Permissions.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertPermission_ToAccessorContract(objTemp);
        }

        private Permission ConvertPermission_ToAccessorContract(Models.Permission tempitem)
        {
            var newObj = new Permission
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                Description = tempitem.Description,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Permission ConvertPermission_ToAccessorModel(Permission tempitem)
        {
            var newObj = new Models.Permission
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                Description = tempitem.Description,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}