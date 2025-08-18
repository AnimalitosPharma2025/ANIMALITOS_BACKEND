using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<AddressBook> GetListAddressBook(AddressBookFilter filter)
        {
            IQueryable<Models.AddressBook> query = from m in _EntityContext.AddressBooks select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Direction))
                query = query.Where(m => m.Direction == filter.Direction);
            if (!string.IsNullOrWhiteSpace(filter.Phone))
                query = query.Where(m => m.Phone == filter.Phone);
            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(m => m.Email == filter.Email);
            if (!string.IsNullOrWhiteSpace(filter.Rfc))
                query = query.Where(m => m.Rfc == filter.Rfc);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<AddressBook> context = new List<Contracts.AddressBook>();
            foreach (Models.AddressBook tempitem in tempStuff)
                context.Add(ConvertAddressBook_ToAccessorContract(tempitem));

            return context;
        }

        public AddressBook GetAddressBook(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");
            var objTemp = _EntityContext.AddressBooks.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertAddressBook_ToAccessorContract(objTemp);
        }

        public AddressBook CreateAddressBook(AddressBook obj)
        {
            var newObj = ConvertAddressBook_ToAccessorModel(obj);

            _EntityContext.AddressBooks.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertAddressBook_ToAccessorContract(newObj);
        }

        public AddressBook UpdateAddressBook(AddressBook obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.AddressBooks.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Direction = obj.Direction;
            objTemp.Phone = obj.Phone;
            objTemp.Email = obj.Email;
            objTemp.Rfc = obj.Rfc;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.AddressBooks.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertAddressBook_ToAccessorContract(objTemp);
        }

        public AddressBook DeleteAddressBook(AddressBook obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.AddressBooks.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.AddressBooks.Remove(objTemp) : _EntityContext.AddressBooks.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertAddressBook_ToAccessorContract(objTemp);
        }

        private AddressBook ConvertAddressBook_ToAccessorContract(Models.AddressBook tempitem)
        {
            var newObj = new Contracts.AddressBook
            {
                Id = tempitem.Id,
                Direction = tempitem.Direction,
                Phone = tempitem.Phone,
                Email = tempitem.Email,
                Rfc = tempitem.Rfc,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.AddressBook ConvertAddressBook_ToAccessorModel(AddressBook tempitem)
        {
            var newObj = new Models.AddressBook
            {
                Id = tempitem.Id,
                Direction = tempitem.Direction,
                Phone = tempitem.Phone,
                Email = tempitem.Email,
                Rfc = tempitem.Rfc,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }
    }
}
