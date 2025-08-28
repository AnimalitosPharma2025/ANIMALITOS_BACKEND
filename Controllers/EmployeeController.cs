using ANIMALITOS_PHARMA_API.Accessors;
using ANIMALITOS_PHARMA_API.Controllers.Helpers;
using ANIMALITOS_PHARMA_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ANIMALITOS_PHARMA_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AnimalitosClient accessor = new();
        [HttpGet]
        [Route("/Employee/GetEmployee/{id}")]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetEmployee(id);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetEmployee));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Employee/GetListEmployee")]
        public IActionResult GetListEmployee(EmployeeFilter filter)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.GetListEmployee(filter);
                return ApiHelpers.CreateSuccessResult(obj, nameof(GetListEmployee));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpGet]
        [Route("/Employee/LoadEmployeeTable")]
        public IActionResult LoadEmployeeTable()
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.LoadEmployeeTable();
                return ApiHelpers.CreateSuccessResult(obj, nameof(LoadEmployeeTable));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPost]
        [Route("/Employee/CreateEmployee")]
        public IActionResult CreateEmployee(Employee employee)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.CreateEmployee(employee);
                return ApiHelpers.CreateSuccessResult(obj, nameof(CreateEmployee));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpPut]
        [Route("/Employee/UpdateEmployee")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.UpdateEmployee(employee);
                return ApiHelpers.CreateSuccessResult(employee, nameof(UpdateEmployee));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }

        [HttpDelete]
        [Route("/Employee/DeleteEmployee/{hardDelete}")]
        public IActionResult DeleteEmployee(Employee employee, bool hardDelete = false)
        {
            try
            {
                accessor.Login((string)ApiHelpers.AuthorizationUser(Request));
                var obj = accessor.DeleteEmployee(employee, hardDelete);
                return ApiHelpers.CreateSuccessResult(obj, nameof(DeleteEmployee));
            }
            catch (Exception ex)
            {
                return ApiHelpers.CreateBadResult(ex);
            }
        }
    }
}