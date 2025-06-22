using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Status> GetListStatus(StatusFilter filter)
        {
            IQueryable<Models.Status> query = from m in _EntityContext.Statuses select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.Name == filter.Name);
            if (!string.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(m => m.Description == filter.Description);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Status> context = new List<Status>();
            foreach (Models.Status tempitem in tempStuff)
                context.Add(ConvertStatus_ToAccessorContract(tempitem));

            return context;
        }

        public Status GetStatus(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Statuses.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertStatus_ToAccessorContract(objTemp);
        }

        public Status CreateStatus(Status obj)
        {
            var newObj = ConvertStatus_ToAccessorModel(obj);

            _EntityContext.Statuses.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertStatus_ToAccessorContract(newObj);
        }

        public Status UpdateStatus(Status obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Statuses.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Name = obj.Name;
            objTemp.Description = obj.Description;

            _EntityContext.Statuses.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertStatus_ToAccessorContract(objTemp);
        }

        public Status DeleteStatus(Status obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Statuses.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            _EntityContext.Statuses.Remove(objTemp);
            _EntityContext.SaveChanges();
            return ConvertStatus_ToAccessorContract(objTemp);
        }

        private Status ConvertStatus_ToAccessorContract(Models.Status tempitem)
        {
            var newObj = new Status
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                Description = tempitem.Description
            };

            return newObj;
        }

        private Models.Status ConvertStatus_ToAccessorModel(Status tempItem)
        {
            var newObj = new Models.Status
            {
                Id = tempItem.Id,
                Name = tempItem.Name,
                Description = tempItem.Description
            };

            return newObj;
        }
    }
}