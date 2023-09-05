using Dapper;
using AdminEmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace AdminEmployeeManagement.Controllers
{
    public class AddEmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public AddEmployeeController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        [HttpGet]
        public IActionResult Add()
        {
            string connString = this._configuration.GetConnectionString("MyDBConnection");

            using (var connection = new SqlConnection(connString))
            {
                var departments = connection.Query<Department>("sp_SelectDepartment_Sayak").ToList();
                ViewBag.Departments = new SelectList(departments, "Department_Id", "Department_Name");
            }
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult Add(Employee employee)
        {
            string connString = this._configuration.GetConnectionString("MyDBConnection");

            if (ModelState.IsValid)
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    var parameters = new
                    {
                        Name = employee.Name,
                        Email = employee.Email,
                        Mobile = employee.Mobile,
                        Department_Id = employee.Department_Id,
                        Status = employee.Status
                    };
                    connection.Execute("sp_InsertEmployee_Sayak", parameters, commandType: CommandType.StoredProcedure);
                }
                return RedirectToAction("Index", "Home");
            }

            // If validation fails, re-populate the Departments list
            using (var connection = new SqlConnection(connString))
            {
                var departments = connection.Query<Department>("sp_SelectDepartment_Sayak").ToList();
                ViewBag.Departments = new SelectList(departments, "Department_Id", "Department_Name");
            }
            return View(employee);
        }
    }
}