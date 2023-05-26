using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using EmployeesWebApi.Models;

namespace EmployeesWebApi.DataAccess
{
    public class EmployeeDataAccess
    {
        private SqlConnection myConnection;

        public EmployeeDataAccess()
        {
            myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["EmployeeDBConnectionString"].ToString();
        }

        //Get all Employees
        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> lstEmployee = new List<Employee>();

            SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Employee", myConnection);

            myConnection.Open();

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            myConnection.Close();

            if (ds != null)
            {
                lstEmployee = ds.Tables[0].AsEnumerable().Select(
                          dataRow => new Employee
                          {
                              EmpId = dataRow.Field<int>("EmployeeId"),
                              FirstName = dataRow.Field<string>("FirstName"),
                              LastName = dataRow.Field<string>("LastName"),
                              EmailID = dataRow.Field<string>("EmailID"),
                              Phone = dataRow.Field<string>("Phone")
                          }).ToList();
            }

            return lstEmployee;
        }

        //Get Employee by EmpId
        public Employee GetEmployee(int empId)
        {
            Employee emp = new Employee();

            SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Employee where EmployeeId = @EmployeeId", myConnection);
            sqlCmd.Parameters.AddWithValue("@EmployeeId", empId);

            myConnection.Open();

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            myConnection.Close();

            if (ds != null)
            {
                DataRow dataRow = ds.Tables[0].Rows[0];
                emp.EmpId = dataRow.Field<int>("EmployeeId");
                emp.FirstName = dataRow.Field<string>("FirstName");
                emp.LastName = dataRow.Field<string>("LastName");
                emp.EmailID = dataRow.Field<string>("EmailID");
                emp.Phone = dataRow.Field<string>("Phone");
            }

            return emp;
        }

        // Create new employee
        public void AddEmployee(Employee employee)
        {
            SqlCommand sqlCmd = new SqlCommand("INSERT INTO Employee (FirstName, LastName, EmailID, Phone) Values (@FirstName, @LastName, @EmailID, @Phone)", myConnection);
            sqlCmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", employee.LastName);
            sqlCmd.Parameters.AddWithValue("@EmailID", employee.EmailID);
            sqlCmd.Parameters.AddWithValue("@Phone", employee.Phone);

            myConnection.Open();

            sqlCmd.ExecuteNonQuery();

            myConnection.Close();
        }

        // Update employee
        public void UpdateEmployee(Employee employee)
        {
            SqlCommand sqlCmd = new SqlCommand("UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, EmailID = @EmailID, Phone = @Phone WHERE EmployeeId = @EmployeeId", myConnection);
            sqlCmd.Parameters.AddWithValue("@EmployeeId", employee.EmpId);
            sqlCmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", employee.LastName);
            sqlCmd.Parameters.AddWithValue("@EmailID", employee.EmailID);
            sqlCmd.Parameters.AddWithValue("@Phone", employee.Phone);

            myConnection.Open();

            sqlCmd.ExecuteNonQuery();

            myConnection.Close();
        }

        // Delete employee
        public void DeleteEmployee(int empId)
        {
            SqlCommand sqlCmd = new SqlCommand("DELETE FROM Employee where EmployeeId = @EmployeeId", myConnection);
            sqlCmd.Parameters.AddWithValue("@EmployeeId", empId);
            
            myConnection.Open();

            sqlCmd.ExecuteNonQuery();

            myConnection.Close();
        }
    }
}