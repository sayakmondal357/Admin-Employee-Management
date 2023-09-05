using AdminEmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace AdminEmployeeManagement.Controllers
{
    public class InsertMultipleEmployeesController : Controller
    {
        private readonly IConfiguration _configuration;

        public InsertMultipleEmployeesController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [HttpGet]
        public ActionResult InsertMultipleEmployees()
        {
            string connString = this._configuration.GetConnectionString("MyDBConnection");

            using (var connection = new SqlConnection(connString))
            {
                var departments = connection.Query<Department>("sp_SelectDepartment_Sayak").ToList();
                ViewBag.Departments = new SelectList(departments, "Department_Id", "Department_Name");
            }
            return View();
        }


        [HttpPost]
        public JsonResult InsertMultipleEmployees([FromBody] List<Employee> employees)
        {
            string connString = this._configuration.GetConnectionString("MyDBConnection");

            if (ModelState.IsValid)
            {
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    int len = employees.Count;
                    for (var i = 0; i < len; i++)
                    {
                        var parameters = new
                        {
                            Name = employees[i].Name,
                            Email = employees[i].Email,
                            Mobile = employees[i].Mobile,
                            Department_Id = employees[i].Department_Id,
                            Status = employees[i].Status
                        };
                        connection.Execute("sp_InsertEmployee_Sayak", parameters, commandType: CommandType.StoredProcedure);
                    }
                }
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}