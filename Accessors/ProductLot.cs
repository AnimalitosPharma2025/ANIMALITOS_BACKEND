using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<ProductLot> GetListProductLot(ProductLotFilter filter)
        {
            IQueryable<Models.ProductLot> query = from m in _EntityContext.ProductLots select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.Expiration != null)
                query = query.Where(m => m.Expiration == filter.Expiration);
            if (filter.DateReceipt != null)
                query = query.Where(m => m.DateReceipt == filter.DateReceipt);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<ProductLot> context = new List<ProductLot>();
            foreach (Models.ProductLot tempitem in tempStuff)
                context.Add(ConvertProductLot_ToAccessorContract(tempitem));

            return context;
        }

        public ProductLot GetProductLot(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.ProductLots.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertProductLot_ToAccessorContract(objTemp);
        }

        public ProductLot CreateProductLot(ProductLot obj)
        {
            var newObj = ConvertProductLot_ToAccessorModel(obj);

            _EntityContext.ProductLots.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertProductLot_ToAccessorContract(newObj);
        }

        public ProductLot UpdateProductLot(ProductLot obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.ProductLots.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Expiration = obj.Expiration;
            objTemp.DateReceipt = obj.DateReceipt;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.ProductLots.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertProductLot_ToAccessorContract(objTemp);
        }

        public ProductLot DeleteProductLot(ProductLot obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.ProductLots.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.ProductLots.Remove(objTemp) : _EntityContext.ProductLots.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertProductLot_ToAccessorContract(objTemp);
        }

        private ProductLot ConvertProductLot_ToAccessorContract(Models.ProductLot tempitem)
        {
            var newObj = new ProductLot
            {
                Id = tempitem.Id,
                Expiration = tempitem.Expiration,
                DateReceipt = tempitem.DateReceipt,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.ProductLot ConvertProductLot_ToAccessorModel(ProductLot tempItem)
        {
            var newObj = new Models.ProductLot
            {
                Id = tempItem.Id,
                Expiration = tempItem.Expiration,
                DateReceipt = tempItem.DateReceipt,
                StatusId = tempItem.StatusId
            };

            return newObj;
        }

    }
}