using ANIMALITOS_PHARMA_API.Contract.DTO;
using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.EntityFrameworkCore;
using XAct;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Client> GetListClient(ClientFilter filter)
        {
            IQueryable<Models.Client> query = from m in _EntityContext.Clients select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.Name == filter.Name);
            if (filter.CreditLimit > 0)
                query = query.Where(m => m.CreditLimit == filter.CreditLimit);
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
            List<Client> context = new List<Client>();
            foreach (Models.Client tempitem in tempStuff)
                context.Add(ConvertClient_ToAccessorContract(tempitem));

            return context;
        }

        public IEnumerable<dynamic> LoadClientTable()
        {
            var clientsData = _EntityContext.Clients
                .Select(c => new ClientDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreditLimit = (decimal)c.CreditLimit,
                    StatusId = c.StatusId,
                    Address = c.Address != null ? new AddressDTO
                    {
                        Id = c.Address.Id,
                        Direction = c.Address.Direction,
                        Phone = c.Address.Phone,
                        Email = c.Address.Email,
                        Rfc = c.Address.Rfc
                    } : null,
                    Credits = _EntityContext.Credits
                        .Where(cr => cr.ClientId == c.Id)
                        .Select(cr => new CreditDTO
                        {
                            Id = cr.Id,
                            PurchaseDate = cr.PurchaseDate,
                            ExpirationDate = cr.ExpirationDate,
                            TotalDebt = (decimal)(cr.TotalDebt ?? 0),
                            StatusId = cr.StatusId,
                            Payments = _EntityContext.CreditPayments
                                .Where(cp => cp.CreditId == cr.Id)
                                .Select(cp => new PaymentDTO
                                {
                                    Id = cp.Id,
                                    PaymentAmount = (decimal)cp.PaymentAmount,
                                    PaymentDate = (DateTime)cp.PaymentDate
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .OrderBy(c => c.Id)
                .ToList();

            return clientsData;
        }

        public Client GetClient(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Clients.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertClient_ToAccessorContract(objTemp);
        }

        public Client CreateClient(Client obj)
        {
            var newObj = ConvertClient_ToAccessorModel(obj);

            _EntityContext.Clients.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertClient_ToAccessorContract(newObj);
        }

        public Client UpdateClient(Client obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Clients.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Name = obj.Name;
            objTemp.CreditLimit = (double)obj.CreditLimit;
            objTemp.AddressId = obj.AddressId;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Clients.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertClient_ToAccessorContract(objTemp);
        }

        public Client DeleteClient(Client obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Clients.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Clients.Remove(objTemp) : _EntityContext.Clients.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertClient_ToAccessorContract(objTemp);
        }

        private Client ConvertClient_ToAccessorContract(Models.Client tempitem)
        {
            var newObj = new Client
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                CreditLimit = tempitem.CreditLimit,
                AddressId = tempitem.AddressId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Client ConvertClient_ToAccessorModel(Client tempitem)
        {
            var newObj = new Models.Client
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                CreditLimit = (double)tempitem.CreditLimit,
                AddressId = tempitem.AddressId,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }
    }
}