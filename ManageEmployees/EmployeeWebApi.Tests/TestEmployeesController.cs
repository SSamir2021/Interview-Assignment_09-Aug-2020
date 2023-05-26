using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeesWebApi.Controllers;
using EmployeesWebApi.Models;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;

namespace EmployeeWebApi.Tests
{
    [TestClass]
    public class TestEmployeesController
    {
        [TestMethod]
        public void GetEmployees_ShouldReturnAllEmployees()
        {
            var controller = new EmployeesController();

            var actionResult = controller.GetEmployees() as List<Employee>;

            Assert.AreEqual(12, actionResult.Count);
        }

        [TestMethod]
        public void GetEmployee_ShouldReturnEmployeeById()
        {
            var controller = new EmployeesController();

            IHttpActionResult actionResult = controller.GetEmployee(2);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;
             
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.EmpId);
        }

        [TestMethod]
        public void PostEmployee_ShouldAddNewEmployee()
        {
            var controller = new EmployeesController();

            Employee testEmployee = new Employee();
            testEmployee.FirstName = "Rakesh";
            testEmployee.LastName = "Arora";
            testEmployee.EmailID = "rakesh@gmail.com";
            testEmployee.Phone = "+919700000000";

            IHttpActionResult actionResult = controller.PostEmployee(testEmployee);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Employee>;
             
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsNotNull(createdResult.RouteValues["id"]);            
        }

        [TestMethod]
        public void PutEmployee_ShouldUpdateExistingEmployee()
        {
            var controller = new EmployeesController();

            Employee testEmployee = new Employee();
            testEmployee.EmpId = 4;
            testEmployee.FirstName = "Deepak";
            testEmployee.LastName = "Chandra";
            testEmployee.EmailID = "deepak1241@gmail.com";
            testEmployee.Phone = "+919700000112";

            IHttpActionResult actionResult = controller.PutEmployee(4, testEmployee);
            var contentResult = actionResult as NegotiatedContentResult<Employee>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void DeleteEmployee_ShouldDeleteExistingEmployee()
        {
            var controller = new EmployeesController();

            IHttpActionResult actionResult = controller.DeleteEmployee(20);
            var contentResult = actionResult as OkNegotiatedContentResult<Employee>;
            
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }        
    }

}
