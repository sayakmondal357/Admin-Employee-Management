using Dapper;
using AdminEmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace AdminEmployeeManagement.Controllers
{
    public class ViewEmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public ViewEmployeeController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public ActionResult Details()
        {
            string connString = this._configuration.GetConnectionString("MyDBConnection");
            List<EmployeeJoinDepartmentView> employeeList = new List<EmployeeJoinDepartmentView>();

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                employeeList = connection.Query<EmployeeJoinDepartmentView>("sp_ViewEmployeeDepartment_Sayak").ToList();
            }
            return View(employeeList);
        }
    }
}