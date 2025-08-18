using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<LoadsContent> GetListLoadContent(LoadsContentFilter filter)
        {
            IQueryable<Models.LoadsContent> query = from m in _EntityContext.LoadsContents select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.LoadId > 0)
                query = query.Where(m => m.LoadId == filter.LoadId);
            if (filter.InventoryId > 0)
                query = query.Where(m => m.InventoryId == filter.InventoryId);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<LoadsContent> context = new List<LoadsContent>();
            foreach (Models.LoadsContent tempitem in tempStuff)
                context.Add(ConvertLoadContent_ToAccessorContract(tempitem));

            return context;
        }

        public LoadsContent GetLoadContent(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.LoadsContents.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertLoadContent_ToAccessorContract(objTemp);
        }

        public LoadsContent CreateLoadContent(LoadsContent obj)
        {
            var newObj = ConvertLoadContent_ToAccessorModel(obj);

            _EntityContext.LoadsContents.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertLoadContent_ToAccessorContract(newObj);
        }

        public LoadsContent UpdateLoadContent(LoadsContent obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.LoadsContents.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.LoadId = obj.LoadId;
            objTemp.InventoryId = obj.InventoryId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.LoadsContents.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertLoadContent_ToAccessorContract(objTemp);
        }

        public LoadsContent DeleteLoadContent(LoadsContent obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.LoadsContents.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.LoadsContents.Remove(objTemp) : _EntityContext.LoadsContents.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertLoadContent_ToAccessorContract(objTemp);
        }

        private LoadsContent ConvertLoadContent_ToAccessorContract(Models.LoadsContent tempitem)
        {
            var newObj = new LoadsContent
            {
                Id = tempitem.Id,
                LoadId = tempitem.LoadId,
                InventoryId = tempitem.InventoryId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.LoadsContent ConvertLoadContent_ToAccessorModel(LoadsContent tempitem)
        {
            var newObj = new Models.LoadsContent
            {
                Id = tempitem.Id,
                LoadId = tempitem.LoadId,
                InventoryId = tempitem.InventoryId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}