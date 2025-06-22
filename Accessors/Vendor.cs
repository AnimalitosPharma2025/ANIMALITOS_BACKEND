using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Vendor> GetListUserVendor(VendorFilter filter)
        {
            IQueryable<Models.Vendor> query = from m in _EntityContext.Vendors select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.Name == filter.Name);
            if (filter.AddressId > 0)
                query = query.Where(m => m.AddressId == filter.AddressId);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Vendor> context = new List<Vendor>();
            foreach (Models.Vendor tempitem in tempStuff)
                context.Add(ConvertVendor_ToAccessorContract(tempitem));

            return context;
        }

        public Vendor GetVendor(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Vendors.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertVendor_ToAccessorContract(objTemp);
        }

        public Vendor CreateVendor(Vendor obj)
        {
            var newObj = ConvertVendor_ToAccessorModel(obj);

            _EntityContext.Vendors.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertVendor_ToAccessorContract(newObj);
        }

        public Vendor UpdateVendor(Vendor obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Vendors.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Name = obj.Name;
            objTemp.AddressId = obj.AddressId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Vendors.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertVendor_ToAccessorContract(objTemp);
        }

        public Vendor DeleteVendor(Vendor obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Vendors.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            _EntityContext.Vendors.Remove(objTemp);
            _EntityContext.SaveChanges();
            return ConvertVendor_ToAccessorContract(objTemp);
        }

        private Vendor ConvertVendor_ToAccessorContract(Models.Vendor tempitem)
        {
            var newObj = new Vendor
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                AddressId = tempitem.AddressId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Vendor ConvertVendor_ToAccessorModel(Vendor tempItem)
        {
            var newObj = new Models.Vendor
            {
                Id = tempItem.Id,
                Name = tempItem.Name,
                AddressId = tempItem.AddressId,
                StatusId = tempItem.StatusId
            };

            return newObj;
        }
    }
}