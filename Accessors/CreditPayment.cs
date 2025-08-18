using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<CreditPayment> GetListCreditPayment(CreditPaymentFilter filter)
        {
            IQueryable<Models.CreditPayment> query = from m in _EntityContext.CreditPayments select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (filter.CreditId > 0)
                query = query.Where(m => m.CreditId == filter.CreditId);
            if (filter.PaymentAmount > 0)
                query = query.Where(m => m.PaymentAmount == filter.PaymentAmount);
            if (filter.PaymentDate != null)
                query = query.Where(m => m.PaymentDate == filter.PaymentDate);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<CreditPayment> context = new List<Contracts.CreditPayment>();
            foreach (Models.CreditPayment tempitem in tempStuff)
                context.Add(ConvertCreditPayment_ToAccessorContract(tempitem));

            return context;
        }

        public CreditPayment GetCreditPayment(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");
            var objTemp = _EntityContext.CreditPayments.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertCreditPayment_ToAccessorContract(objTemp);
        }

        public CreditPayment CreateCreditPayment(CreditPayment obj)
        {
            var newObj = ConvertCreditPayment_ToAccessorModel(obj);

            _EntityContext.CreditPayments.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertCreditPayment_ToAccessorContract(newObj);
        }

        public CreditPayment UpdateCreditPayment(CreditPayment obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.CreditPayments.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.CreditId = obj.CreditId;
            objTemp.PaymentAmount = obj.PaymentAmount;
            objTemp.PaymentDate = obj.PaymentDate;

            _EntityContext.CreditPayments.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertCreditPayment_ToAccessorContract(objTemp);
        }

        public CreditPayment DeleteCreditPayment(CreditPayment obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.CreditPayments.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.CreditPayments.Remove(objTemp) : _EntityContext.CreditPayments.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertCreditPayment_ToAccessorContract(objTemp);
        }

        private CreditPayment ConvertCreditPayment_ToAccessorContract(Models.CreditPayment tempitem)
        {
            var newObj = new Contracts.CreditPayment
            {
                Id = tempitem.Id,
                CreditId = tempitem.CreditId,
                PaymentDate = tempitem.PaymentDate,
                PaymentAmount = tempitem.PaymentAmount,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.CreditPayment ConvertCreditPayment_ToAccessorModel(CreditPayment tempitem)
        {
            var newObj = new Models.CreditPayment
            {
                Id = tempitem.Id,
                CreditId = tempitem.CreditId,
                PaymentDate = tempitem.PaymentDate,
                PaymentAmount = tempitem.PaymentAmount,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }
    }
}
