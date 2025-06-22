using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Credit> GetListCredit(CreditFilter filter)
        {
            IQueryable<Models.Credit> query = from m in _EntityContext.Credits select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.PurchaseDate is not null)
                query = query.Where(m => m.PurchaseDate == filter.PurchaseDate);
            if (filter.ExpirationDate is not null)
                query = query.Where(m => m.ExpirationDate == filter.ExpirationDate);
            if (filter.SaleId > 0)
                query = query.Where(m => m.SaleId == filter.SaleId);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Credit> context = new List<Credit>();
            foreach (Models.Credit tempitem in tempStuff)
                context.Add(ConvertCredit_ToAccessorContract(tempitem));

            return context;
        }

        public Credit GetCredit(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Credits.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertCredit_ToAccessorContract(objTemp);
        }

        public Credit CreateCredit(Credit obj)
        {
            var newObj = ConvertCredit_ToAccessorModel(obj);

            _EntityContext.Credits.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertCredit_ToAccessorContract(newObj);
        }

        public Credit UpdateCredit(Credit obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Credits.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.PurchaseDate = obj.PurchaseDate;
            objTemp.ExpirationDate = obj.ExpirationDate;
            objTemp.SaleId = obj.SaleId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Credits.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertCredit_ToAccessorContract(objTemp);
        }

        public Credit DeleteCredit(Credit obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Credits.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Credits.Remove(objTemp) : _EntityContext.Credits.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertCredit_ToAccessorContract(objTemp);
        }

        private Credit ConvertCredit_ToAccessorContract(Models.Credit tempitem)
        {
            var newObj = new Credit
            {
                Id = tempitem.Id,
                PurchaseDate = tempitem.PurchaseDate,
                ExpirationDate = tempitem.ExpirationDate,
                SaleId = tempitem.SaleId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Credit ConvertCredit_ToAccessorModel(Credit tempitem)
        {
            var newObj = new Models.Credit
            {
                Id = tempitem.Id,
                PurchaseDate = tempitem.PurchaseDate,
                ExpirationDate = tempitem.ExpirationDate,
                SaleId = tempitem.SaleId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}
