using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Rol> GetListRol(RolFilter filter)
        {
            IQueryable<Models.Rol> query = from m in _EntityContext.Rols select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.Name == filter.Name);
            if (!string.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(m => m.Description == filter.Description);
            if(filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Rol> context = new List<Rol>();
            foreach (Models.Rol tempitem in tempStuff)
                context.Add(ConvertRol_ToAccessorContract(tempitem));

            return context;
        }

        public Rol GetRol(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Rols.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertRol_ToAccessorContract(objTemp);
        }

        public Rol CreateRol(Rol obj)
        {
            var newObj = ConvertRol_ToAccessorModel(obj);

            _EntityContext.Rols.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertRol_ToAccessorContract(newObj);
        }

        public Rol UpdateRol(Rol obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Rols.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Name = obj.Name;
            objTemp.Description = obj.Description;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Rols.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertRol_ToAccessorContract(objTemp);
        }

        public Rol DeleteRol(Rol obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Rols.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Rols.Remove(objTemp) : _EntityContext.Rols.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertRol_ToAccessorContract(objTemp);
        }

        private Rol ConvertRol_ToAccessorContract(Models.Rol tempitem)
        {
            var newObj = new Rol
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                Description = tempitem.Description,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Rol ConvertRol_ToAccessorModel(Rol tempItem)
        {
            var newObj = new Models.Rol
            {
                Id = tempItem.Id,
                Name = tempItem.Name,
                Description = tempItem.Description,
                StatusId = tempItem.StatusId
            };

            return newObj;
        }

    }
}