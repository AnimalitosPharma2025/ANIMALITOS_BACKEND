using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Load> GetListLoad(LoadFilter filter)
        {
            IQueryable<Models.Load> query = from m in _EntityContext.Loads select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.CreatedDate != null)
                query = query.Where(m => m.CreatedDate == filter.CreatedDate);
            if (filter.EmployeeId > 0)
                query = query.Where(m => m.EmployeeId == filter.EmployeeId);
            if (filter.LoadValue > 0)
                query = query.Where(m => m.LoadValue == filter.LoadValue);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Load> context = new List<Load>();
            foreach (Models.Load tempitem in tempStuff)
                context.Add(ConvertLoad_ToAccessorContract(tempitem));

            return context;
        }

        public IEnumerable<dynamic> ConstructTableLoads()
        {
            var loads = _EntityContext.Loads
                .Include(l => l.Employee)
                .Include(l => l.Status)
                .Include(l => l.LoadsContents)
                    .ThenInclude(lc => lc.Inventory)
                        .ThenInclude(inv => inv.Product)
                .Include(l => l.LoadsContents)
                    .ThenInclude(lc => lc.Inventory)
                        .ThenInclude(inv => inv.ProductLot)
                .Select(l => new
                {
                    LoadId = l.Id,
                    Employee = l.Employee != null ? l.Employee.Name : "Sin asignar",
                    Status = l.Status != null ? l.Status.Name : "N/A",
                    Contents = l.LoadsContents.Select(lc => new
                    {
                        ProductName = lc.Inventory!.Product.Name,
                        LotNumber = lc.Inventory.ProductLot.Id,
                        ExpirationDate = lc.Inventory.ProductLot.Expiration
                    }),
                    Quantity = l.LoadsContents.Count()
                })
                .ToList();

            return loads;
        }

        public Load GetLoad(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Loads.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertLoad_ToAccessorContract(objTemp);
        }

        public Load CreateLoad(Load obj)
        {
            var newObj = ConvertLoad_ToAccessorModel(obj);

            _EntityContext.Loads.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertLoad_ToAccessorContract(newObj);
        }

        public Load UpdateLoad(Load obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Loads.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.CreatedDate = obj.CreatedDate;
            objTemp.EmployeeId = obj.EmployeeId;
            objTemp.LoadValue = obj.LoadValue;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Loads.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertLoad_ToAccessorContract(objTemp);
        }

        public Load DeleteLoad(Load obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Loads.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Loads.Remove(objTemp) : _EntityContext.Loads.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertLoad_ToAccessorContract(objTemp);
        }

        private Load ConvertLoad_ToAccessorContract(Models.Load tempitem)
        {
            var newObj = new Load
            {
                Id = tempitem.Id,
                CreatedDate = tempitem.CreatedDate,
                EmployeeId = tempitem.EmployeeId,
                LoadValue = tempitem.LoadValue,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Load ConvertLoad_ToAccessorModel(Load tempitem)
        {
            var newObj = new Models.Load
            {
                Id = tempitem.Id,
                CreatedDate = tempitem.CreatedDate,
                EmployeeId = tempitem.EmployeeId,
                LoadValue = tempitem.LoadValue,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}