using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Sale> GetListSale(SaleFilter filter)
        {
            IQueryable<Models.Sale> query = from m in _EntityContext.Sales select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.PurchaseDate != null)
                query = query.Where(m => m.PurchaseDate == filter.PurchaseDate);
            if (filter.ClientId > 0)
                query = query.Where(m => m.ClientId == filter.ClientId);
            if (filter.EmployeeId > 0)
                query = query.Where(m => m.EmployeeId == filter.EmployeeId);
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
            List<Sale> context = new List<Sale>();
            foreach (Models.Sale tempitem in tempStuff)
                context.Add(ConvertSale_ToAccessorContract(tempitem));

            return context;
        }

        public Sale GetSale(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Sales.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertSale_ToAccessorContract(objTemp);
        }

        public Sale CreateSale(Sale obj)
        {
            var newObj = ConvertSale_ToAccessorModel(obj);

            _EntityContext.Sales.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertSale_ToAccessorContract(newObj);
        }

        public Sale UpdateSale(Sale obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Sales.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.PurchaseDate = obj.PurchaseDate;
            objTemp.ClientId = obj.ClientId;
            objTemp.EmployeeId = obj.EmployeeId;
            objTemp.InventoryId = obj.InventoryId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Sales.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertSale_ToAccessorContract(objTemp);
        }

        public Sale DeleteSale(Sale obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Sales.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Sales.Remove(objTemp) : _EntityContext.Sales.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertSale_ToAccessorContract(objTemp);
        }

        private Sale ConvertSale_ToAccessorContract(Models.Sale tempitem)
        {
            var newObj = new Sale
            {
                Id = tempitem.Id,
                PurchaseDate = tempitem.PurchaseDate,
                ClientId = tempitem.ClientId,
                EmployeeId = tempitem.EmployeeId,
                InventoryId = tempitem.InventoryId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Sale ConvertSale_ToAccessorModel(Sale tempItem)
        {
            var newObj = new Models.Sale
            {
                Id = tempItem.Id,
                PurchaseDate = tempItem.PurchaseDate,
                ClientId = tempItem.ClientId,
                EmployeeId = tempItem.EmployeeId,
                InventoryId = tempItem.InventoryId,
                StatusId = tempItem.StatusId
            };

            return newObj;
        }
    }
}