using ANIMALITOS_PHARMA_API.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        private static readonly EncryptHasher _encrypt = new();

        public IEnumerable<User> GetListUser(UserFilter filter)
        {
            IQueryable<Models.User> query = from m in _EntityContext.Users select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Username))
                query = query.Where(m => m.Username == filter.Username);
            if (!string.IsNullOrWhiteSpace(filter.Password))
                query = query.Where(m => m.Password == filter.Username);
            if (filter.EmployeeId > 0)
                query = query.Where(m => m.EmployeeId == filter.EmployeeId);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Contracts.User> context = new List<Contracts.User>();
            foreach (Models.User tempitem in tempStuff)
                context.Add(ConvertUser_ToAccessorContract(tempitem));

            return context;
        }

        public Contracts.User GetUser(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");
            var userTemp = _EntityContext.Users.SingleOrDefault(m => m.Id == id);
            if (userTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertUser_ToAccessorContract(userTemp);
        }

        public Contracts.User CreateUser(Contracts.User obj)
        {
            var newObj = ConvertUser_ToAccessorModel(obj);

            //newObj.Password = _encrypt.Hash(newObj.Password);

            _EntityContext.Users.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertUser_ToAccessorContract(newObj);
        }

        public User UpdateUser(User obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var userTemp = _EntityContext.Users.SingleOrDefault(m => m.Id == obj.Id);
            if (userTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            userTemp.Id = obj.Id;
            userTemp.Username = obj.Username;
            userTemp.Password = obj.Password;
            userTemp.EmployeeId = obj.EmployeeId;
            userTemp.StatusId = obj.StatusId;

            _EntityContext.Users.Update(userTemp);
            _EntityContext.SaveChanges();

            return ConvertUser_ToAccessorContract(userTemp);
        }

        public User DeleteUser(User user, bool hardDelete)
        {
            if (user.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var userTemp = _EntityContext.Users.SingleOrDefault(m => m.Id == user.Id);
            if (userTemp is null)
                throw new Exception($"Object with Id of {user.Id} does not exist.");

            userTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Users.Remove(userTemp) : _EntityContext.Users.Update(userTemp);

            _EntityContext.SaveChanges();
            return ConvertUser_ToAccessorContract(userTemp);
        }

        public bool SignIn(User user)
        {
            var userTemp = _EntityContext.Users.SingleOrDefault(m => m.Username == user.Username);
            if (userTemp is null)
                throw new Exception($"Object with Id of '{user.Username}' does exist.");

            if (!_encrypt.Verify(userTemp.Password, user.Password))
                throw new Exception("Incorrect credentials.");

            return true;
        }
        
        public dynamic SignUp(User user)
        {
            var userTemp = _EntityContext.Users.SingleOrDefault(m => m.Username == user.Username);
            if (userTemp != null)
                throw new Exception($"The user with username '{user.Username}' already exists.");

            var newUser = new Contracts.User
            {
                Username = user.Username,
                Password = _encrypt.Hash(user.Password),
                EmployeeId = user.EmployeeId,
                StatusId = user.StatusId
            };

            var newObj = ConvertUser_ToAccessorModel(newUser);

            _EntityContext.Users.Add(newObj);
            _EntityContext.SaveChanges();

            dynamic userReturn = new {
                Username = newObj.Username
            };
            return userReturn;
        }
        
        private User ConvertUser_ToAccessorContract(Models.User tempitem)
        {
            var newObj = new Contracts.User
            {
                Id = tempitem.Id,
                Username = tempitem.Username,
                Password = tempitem.Password,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.User ConvertUser_ToAccessorModel(Contracts.User tempitem)
        {
            var newObj = new Models.User
            {
                Id = tempitem.Id,
                Username = tempitem.Username,
                Password = tempitem.Password,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }
    }
}
