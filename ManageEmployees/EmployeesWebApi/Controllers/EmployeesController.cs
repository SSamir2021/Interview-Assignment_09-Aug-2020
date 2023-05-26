using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeesWebApi.Models;
using EmployeesWebApi.DataAccess;

namespace EmployeesWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        EmployeeDataAccess empdb = new EmployeeDataAccess();
        
        // GET: api/Employees
        public IEnumerable<Employee> GetEmployees()
        {
            return empdb.GetAllEmployees();
        }        

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = empdb.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }        

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            empdb.AddEmployee(employee);

            return CreatedAtRoute("DefaultApi", new { id = employee.EmpId }, employee);
        }         

        // PUT: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmpId)
            {
                return BadRequest();
            }

            empdb.UpdateEmployee(employee);

            return Content(HttpStatusCode.Accepted, employee); 
        }        

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = empdb.GetEmployee(id);

            if (employee == null)
            {
                return NotFound();
            }

            empdb.DeleteEmployee(id);           

            return Ok(employee);
        }        
    }
}