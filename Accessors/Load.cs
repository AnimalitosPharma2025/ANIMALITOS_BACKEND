using ANIMALITOS_PHARMA_API.Contract.DTO;
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
                    l.EmployeeId,
                    l.CreatedDate,
                    EmployeeName = l.Employee != null ? l.Employee.Name : "Sin asignar",
                    StatusId = l.Status != null ? l.Status.Id : 0,
                    Contents = l.LoadsContents.Select(lc => new
                    {
                        ProductName = lc.Inventory!.Product.Name,
                        LotNumber = lc.Inventory.ProductLot.Id,
                        ExpirationDate = lc.Inventory.ProductLot.Expiration,
                        UnitPrice = lc.Inventory.Product.UnitPrice,
                        Discount = lc.Inventory.Product.Discount ?? 0
                    }),
                    l.LoadValue,
                    LoadWithDiscount = l.LoadsContents.Sum(lc =>
                        lc.Inventory!.Product.UnitPrice - (lc.Inventory.Product.UnitPrice * (lc.Inventory.Product.Discount ?? 0))),
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

        public dynamic CreateLoadAndContent(dynamic formData)
        {
            //var productList = new List<dynamic>();
            //foreach (var product in formData["contents"])
            //{
            //    int id = product.Id;
            //    productList.Add(_EntityContext.Loads.SingleOrDefault(m => m.Id == id));
            //}
            //var newLoad = new Load
            //{
            //    CreatedDate = DateTime.Now,
            //    EmployeeId = formData.EmployeeId,

            //};

            //_EntityContext.Loads.Add();
            return formData;
        }

        public Load CreateLoad(Load obj)
        {
            var newObj = ConvertLoad_ToAccessorModel(obj);

            _EntityContext.Loads.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertLoad_ToAccessorContract(newObj);
        }
        public IEnumerable<dynamic> LoadDataforModal()
        {
            var productosConLotes = _EntityContext.InventoryItems
                .Include(i => i.Product)
                .Include(i => i.ProductLot)
                .GroupBy(i => i.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    ProductName = g.First().Product.Name,
                    UnitPrice = g.First().Product.UnitPrice,
                    Lots = g.GroupBy(i => i.ProductLotId)
                            .Select(l => new
                            {
                                LotId = l.Key,
                                DateReceipt = l.First().ProductLot.DateReceipt,
                                Expiration = l.First().ProductLot.Expiration,
                                StatusId = l.First().ProductLot.StatusId,
                                Quantity = l.Count()
                            })
                            .ToList()
                })
                .ToList();


            return productosConLotes;
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