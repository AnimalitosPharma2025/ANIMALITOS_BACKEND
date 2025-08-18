using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Product> GetListProduct(ProductFilter filter)
        {
            IQueryable<Models.Product> query = from m in _EntityContext.Products select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.Name == filter.Name);
            if (!string.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(m => m.Description == filter.Description);
            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(m => m.Category == filter.Category);
            if (filter.PurchasePrice > 0)
                query = query.Where(m => m.PurchasePrice == filter.PurchasePrice);
            if (filter.UnitPrice > 0)
                query = query.Where(m => m.UnitPrice == filter.UnitPrice);
            if (filter.VendorId > 0)
                query = query.Where(m => m.VendorId == filter.VendorId);
            if (!string.IsNullOrEmpty(filter.Code))
                query = query.Where(m => m.Code == filter.Code);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Product> context = new List<Product>();
            foreach (Models.Product tempitem in tempStuff)
                context.Add(ConvertProduct_ToAccessorContract(tempitem));

            return context;
        }

        public Product GetProduct(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Products.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertProduct_ToAccessorContract(objTemp);
        }

        public Product CreateProduct(Product obj)
        {
            var newObj = ConvertProduct_ToAccessorModel(obj);

            _EntityContext.Products.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertProduct_ToAccessorContract(newObj);
        }

        public IEnumerable<dynamic> LoadProductTable()
        {
            var productsWithStock = (from product in _EntityContext.Products
                                     let stock = _EntityContext.InventoryItems.Count(i => i.ProductId == product.Id)
                                     select new
                                     {
                                         product.Id,
                                         product.Code,
                                         product.Name,
                                         product.UnitPrice,
                                         product.PurchasePrice,
                                         product.Category,
                                         Status = product.StatusId,
                                         Stock = stock
                                     }).ToList();

            return productsWithStock;
        }

        public Product UpdateProduct(Product obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Products.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Name = obj.Name;
            objTemp.Description = obj.Description;
            objTemp.Category = obj.Category;
            objTemp.PurchasePrice = obj.PurchasePrice;
            objTemp.UnitPrice = obj.UnitPrice;
            objTemp.Code = obj.Code;
            objTemp.VendorId = obj.VendorId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Products.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertProduct_ToAccessorContract(objTemp);
        }

        public Product DeleteProduct(Product obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Products.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Products.Remove(objTemp) : _EntityContext.Products.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertProduct_ToAccessorContract(objTemp);
        }

        private Product ConvertProduct_ToAccessorContract(Models.Product tempitem)
        {
            var newObj = new Product
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                Description = tempitem.Description,
                Category = tempitem.Category,
                PurchasePrice = tempitem.PurchasePrice,
                UnitPrice = tempitem.UnitPrice,
                Code = tempitem.Code,
                VendorId = tempitem.VendorId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Product ConvertProduct_ToAccessorModel(Product tempitem)
        {
            var newObj = new Models.Product
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                Description = tempitem.Description,
                Category = tempitem.Category,
                PurchasePrice = tempitem.PurchasePrice,
                UnitPrice = tempitem.UnitPrice,
                Code = tempitem.Code,
                VendorId = tempitem.VendorId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}