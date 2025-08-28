using ANIMALITOS_PHARMA_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient
    {
        public IEnumerable<Employee> GetListEmployee(EmployeeFilter filter)
        {
            IQueryable<Models.Employee> query = from m in _EntityContext.Employees select m;
            query = query.AsNoTracking();

            if (filter.Id > 0)
                query = query.Where(m => m.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(m => m.Name == filter.Name);
            if (!string.IsNullOrWhiteSpace(filter.LastName))
                query = query.Where(m => m.LastName == filter.LastName);
            if (!string.IsNullOrWhiteSpace(filter.Rol))
                query = query.Where(m => m.Rol == filter.Rol);
            if (filter.AddressId > 0)
                query = query.Where(m => m.AddressId == filter.AddressId);
            if (filter.DailyAmount > 0)
                query = query.Where(m => m.DailyAmount == filter.DailyAmount);
            if (filter.StatusId != 0)
                query = query.Where(m => m.StatusId == filter.StatusId);

            if (!string.IsNullOrWhiteSpace(filter.SortColumn))
                query = query.OrderBy(query => filter.SortColumn);

            if (filter.PagingBegin > -1)
                query = query.Skip(filter.PagingBegin);
            if (filter.PagingRange > -1)
                query = query.Take(filter.PagingRange);

            var tempStuff = query.ToList();
            List<Employee> context = new List<Employee>();
            foreach (Models.Employee tempitem in tempStuff)
                context.Add(ConvertEmployee_ToAccessorContract(tempitem));

            return context;
        }

        public Employee GetEmployee(int id)
        {
            if (id <= 0)
                throw new Exception("Cannot search object without an Id.");

            var objTemp = _EntityContext.Employees.SingleOrDefault(m => m.Id == id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {id} does exist.");

            return ConvertEmployee_ToAccessorContract(objTemp);
        }

        public IEnumerable<dynamic> LoadEmployeeTable()
        {
            var employeeObjectTable = _EntityContext.Employees
                .Include(e => e.Address)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.LastName,
                    e.Rol,
                    e.Address,
                    e.StatusId,
                })
                .ToList();

            return employeeObjectTable;
        }
        public Employee CreateEmployee(Employee obj)
        {
            var newObj = ConvertEmployee_ToAccessorModel(obj);

            _EntityContext.Employees.Add(newObj);
            _EntityContext.SaveChanges();

            return ConvertEmployee_ToAccessorContract(newObj);
        }

        public Employee UpdateEmployee(Employee obj)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot update object without an Id.");
            var objTemp = _EntityContext.Employees.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.Id = obj.Id;
            objTemp.Name = obj.Name;
            objTemp.LastName = obj.LastName;
            objTemp.Rol = obj.Rol;
            objTemp.AddressId = obj.AddressId;
            objTemp.DailyAmount = obj.DailyAmount;
            objTemp.StatusId = obj.StatusId;

            _EntityContext.Employees.Update(objTemp);
            _EntityContext.SaveChanges();

            return ConvertEmployee_ToAccessorContract(objTemp);
        }

        public Employee DeleteEmployee(Employee obj, bool hardDelete)
        {
            if (obj.Id <= 0)
                throw new Exception("Cannot delete object without an Id.");
            var objTemp = _EntityContext.Employees.SingleOrDefault(m => m.Id == obj.Id);
            if (objTemp is null)
                throw new Exception($"Object with Id of {obj.Id} does not exist.");

            objTemp.StatusId = 0;
            var newUser = (hardDelete == true) ? _EntityContext.Employees.Remove(objTemp) : _EntityContext.Employees.Update(objTemp);

            _EntityContext.SaveChanges();
            return ConvertEmployee_ToAccessorContract(objTemp);
        }

        private Employee ConvertEmployee_ToAccessorContract(Models.Employee tempitem)
        {
            var newObj = new Employee
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                LastName = tempitem.LastName,
                Rol = tempitem.Rol,
                AddressId = tempitem.AddressId,
                DailyAmount = tempitem.DailyAmount,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

        private Models.Employee ConvertEmployee_ToAccessorModel(Employee tempitem)
        {
            var newObj = new Models.Employee
            {
                Id = tempitem.Id,
                Name = tempitem.Name,
                LastName = tempitem.LastName,
                Rol = tempitem.Rol,
                AddressId = tempitem.AddressId,
                DailyAmount= tempitem.DailyAmount,
                StatusId = tempitem.StatusId
            };

            return newObj;
        }

    }
}