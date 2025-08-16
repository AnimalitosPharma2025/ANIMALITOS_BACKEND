using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<InventoryItem> GetListInventoryItem(InventoryItemFilter filter)
        {
            IQueryable<Models.InventoryItem> query = from m in _EntityContext.InventoryItems select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.ProductId > 0)
                query = query.Where(m => m.ProductId == filter.ProductId);
            if (filter.ProductLotId > 0)
                query = query.Where(m => m.ProductLotId == filter.ProductLotId);
            if (filter.EmployeeId > 0)
                query = query.Where(m => m.EmployeeId == filter.EmployeeId);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<InventoryItem> context = new List<InventoryItem>();
            foreach (Models.InventoryItem tempitem in tempStuff)
                context.Add(ConvertInventoryItem_ToAccessorContract(tempitem));

            return context;
        }

        public InventoryItem GetInventoryItem(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.InventoryItems.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertInventoryItem_ToAccessorContract(objTemp);
        }

        public InventoryItem CreateInventoryItem(InventoryItem obj)
        {
            var newObj = ConvertInventoryItem_ToAccessorModel(obj);

            _EntityContext.InventoryItems.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertInventoryItem_ToAccessorContract(newObj);
        }

        public InventoryItem UpdateInventoryItem(InventoryItem obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.InventoryItems.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.ProductId = obj.ProductId;
            objTemp.ProductLotId = obj.ProductLotId;
            objTemp.EmployeeId = obj.EmployeeId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.InventoryItems.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertInventoryItem_ToAccessorContract(objTemp);
        }

        public InventoryItem DeleteInventoryItem(InventoryItem obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.InventoryItems.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.InventoryItems.Remove(objTemp) : _EntityContext.InventoryItems.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertInventoryItem_ToAccessorContract(objTemp);
        }

        private InventoryItem ConvertInventoryItem_ToAccessorContract(Models.InventoryItem tempitem)
        {
            var newObj = new InventoryItem
            {
                Id = tempitem.Id,
                ProductId = tempitem.ProductId,
                ProductLotId = tempitem.ProductLotId,
                EmployeeId = tempitem.EmployeeId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.InventoryItem ConvertInventoryItem_ToAccessorModel(InventoryItem tempitem)
        {
            var newObj = new Models.InventoryItem
            {
                Id = tempitem.Id,
                ProductId = tempitem.ProductId,
                ProductLotId = tempitem.ProductLotId,
                EmployeeId = tempitem.EmployeeId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}