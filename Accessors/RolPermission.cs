using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<RolPermission> GetListRolPermission(RolPermissionFilter filter)
        {
            IQueryable<Models.RolPermission> query = from m in _EntityContext.RolPermissions select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.RolId > 0)
                query = query.Where(m => m.RolId == filter.RolId);
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
            List<RolPermission> context = new List<RolPermission>();
            foreach (Models.RolPermission tempitem in tempStuff)
                context.Add(ConvertRolPermission_ToAccessorContract(tempitem));

            return context;
        }

        public RolPermission GetRolPermission(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.RolPermissions.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertRolPermission_ToAccessorContract(objTemp);
        }

        public RolPermission CreateRolPermission(RolPermission obj)
        {
            var newObj = ConvertRolPermission_ToAccessorModel(obj);

            _EntityContext.RolPermissions.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertRolPermission_ToAccessorContract(newObj);
        }

        public RolPermission UpdateRolPermission(RolPermission obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.RolPermissions.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.RolId = obj.RolId;
            objTemp.PermissionId = obj.PermissionId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.RolPermissions.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertRolPermission_ToAccessorContract(objTemp);
        }

        public RolPermission DeleteRolPermission(RolPermission obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.RolPermissions.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.RolPermissions.Remove(objTemp) : _EntityContext.RolPermissions.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertRolPermission_ToAccessorContract(objTemp);
        }

        private RolPermission ConvertRolPermission_ToAccessorContract(Models.RolPermission tempitem)
        {
            var newObj = new RolPermission
            {
                Id = tempitem.Id,
                RolId = tempitem.RolId,
                PermissionId = tempitem.PermissionId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.RolPermission ConvertRolPermission_ToAccessorModel(RolPermission tempItem)
        {
            var newObj = new Models.RolPermission
            {
                Id = tempItem.Id,
                RolId = tempItem.RolId,
                PermissionId = tempItem.PermissionId,
                StatusId = tempItem.StatusId
            };

            return newObj;
        }

    }
}