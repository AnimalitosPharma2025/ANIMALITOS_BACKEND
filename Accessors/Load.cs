using ANIMALITOS_PHARMA_API.Accessors.Util.StatusEnumerable;
using ANIMALITOS_PHARMA_API.Contract.DTO;
using ANIMALITOS_PHARMA_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Reflection;
using XAct;

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
                    EmployeeLastName = l.Employee != null ? l.Employee.LastName : "",
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

        public dynamic GetLoadForEmployee(int employeeId)
        {
            var loads = _EntityContext.Loads
                .Where(l => l.EmployeeId == employeeId) // <-- filtro por empleado
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
                    l.CreatedDate,
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
            using var transaction = _EntityContext.Database.BeginTransaction();
                var productList = new List<Product>();
                var productLotList = new List<ProductLot>();

                foreach (var content in formData.GetProperty("contents").EnumerateArray())
                {
                    int productId = content.GetProperty("productId").GetInt32();
                    int productLotId = content.GetProperty("lotNumber").GetInt32();

                    var product = _EntityContext.Products.SingleOrDefault(m => m.Id == productId);
                    var productLot = _EntityContext.ProductLots.SingleOrDefault(m => m.Id == productLotId);

                    if (product == null || productLot == null)
                        throw new Exception($"Invalid product or lot (ProductId: {productId}, LotId: {productLotId})");

                    productList.Add(ConvertProduct_ToAccessorContract(product));
                    productLotList.Add(ConvertProductLot_ToAccessorContract(productLot));
                }

                var newLoadObject = new Models.Load
                {
                    CreatedDate = DateTime.Now,
                    EmployeeId = formData.GetProperty("employeeId").GetInt32(),
                    StatusId = (int)ObjectStatus.ACTIVE
                };

                decimal totalValue = 0;

                foreach (var content in formData.GetProperty("contents").EnumerateArray())
                {
                    int productId = content.GetProperty("productId").GetInt32();
                    int qty = content.GetProperty("quantity").GetInt32();

                    var product = _EntityContext.Products.SingleOrDefault(m => m.Id == productId);
                    if (product != null)
                    {
                        totalValue += (decimal)product.UnitPrice * qty;
                    }
                }

                newLoadObject.LoadValue = (double?)totalValue;

                _EntityContext.Loads.Add(newLoadObject);
                _EntityContext.SaveChanges();

                int indexProductLot = 0;
                foreach (var lot in productLotList)
                {
                    int requiredQty = formData.GetProperty("contents")[indexProductLot].GetProperty("quantity").GetInt32();

                    var availableItems = _EntityContext.InventoryItems
                        .Where(m => m.ProductLotId == lot.Id && !_EntityContext.LoadsContents.Any(lc => lc.InventoryId == m.Id))
                        .OrderBy(m => m.Id)
                        .Take(requiredQty)
                        .ToList();

                    if (availableItems.Count < requiredQty)
                        throw new Exception($"Not enough free items in lot {lot.Id}. Needed {requiredQty}, found {availableItems.Count}");

                    foreach (var item in availableItems)
                    {
                        var loadContent = new LoadsContent
                        {
                            LoadId = newLoadObject.Id,
                            InventoryId = item.Id,
                            StatusId = (int)ObjectStatus.ACTIVE
                        };

                        _EntityContext.LoadsContents.Add(loadContent);
                    }

                    indexProductLot++;
                }

                _EntityContext.SaveChanges();
                transaction.Commit();

                return new
                {
                    newLoadObject.Id,
                    newLoadObject.CreatedDate,
                    newLoadObject.EmployeeId,
                    newLoadObject.StatusId,
                    Products = productList,
                    Lots = productLotList
                };
            }

        public dynamic UpdateLoadAndContent(dynamic formData)
        {
            using var transaction = _EntityContext.Database.BeginTransaction();

            int loadId = formData.GetProperty("loadId").GetInt32();
            var existingLoad = _EntityContext.Loads
                .SingleOrDefault(l => l.Id == loadId);

            if (existingLoad == null)
                throw new Exception($"Load {loadId} not found");

            existingLoad.EmployeeId = formData.GetProperty("employeeId").GetInt32();

            var productList = new List<Product>();
            var productLotList = new List<ProductLot>();
            var oldContents = _EntityContext.LoadsContents
                .Where(lc => lc.LoadId == loadId)
                .ToList();

            _EntityContext.LoadsContents.RemoveRange(oldContents);
            _EntityContext.SaveChanges();

            decimal totalValue = 0;
            int index = 0;

            foreach (var content in formData.GetProperty("contents").EnumerateArray())
            {
                int productId = content.GetProperty("productId").GetInt32();
                int productLotId = content.GetProperty("lotNumber").GetInt32();
                int qty = content.GetProperty("quantity").GetInt32();

                var product = _EntityContext.Products.SingleOrDefault(m => m.Id == productId);
                var productLot = _EntityContext.ProductLots.SingleOrDefault(m => m.Id == productLotId);

                if (product == null || productLot == null)
                    throw new Exception($"Invalid product or lot (ProductId: {productId}, LotId: {productLotId})");

                productList.Add(ConvertProduct_ToAccessorContract(product));
                productLotList.Add(ConvertProductLot_ToAccessorContract(productLot));
                totalValue += (decimal)product.UnitPrice * qty;

                var availableItems = _EntityContext.InventoryItems
                    .Where(m => m.ProductLotId == productLotId &&
                                !_EntityContext.LoadsContents.Any(lc => lc.InventoryId == m.Id))
                    .OrderBy(m => m.Id)
                    .Take(qty)
                    .ToList();

                if (availableItems.Count < qty)
                    throw new Exception($"Not enough free items in lot {productLotId}. Needed {qty}, found {availableItems.Count}");

                foreach (var item in availableItems)
                {
                    var loadContent = new LoadsContent
                    {
                        LoadId = existingLoad.Id,
                        InventoryId = item.Id,
                        StatusId = (int)ObjectStatus.ACTIVE
                    };
                    _EntityContext.LoadsContents.Add(loadContent);
                }

                index++;
            }

            existingLoad.LoadValue = (double?)totalValue;
            _EntityContext.SaveChanges();

            transaction.Commit();

            return new
            {
                existingLoad.Id,
                existingLoad.CreatedDate,
                existingLoad.EmployeeId,
                existingLoad.StatusId,
                Products = productList,
                Lots = productLotList
            };
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