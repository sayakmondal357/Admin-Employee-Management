using AdminEmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace AdminEmployeeManagement.Controllers
{
    public class EditEmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EditEmployeeController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string connString = _configuration.GetConnectionString("MyDBConnection");

            using (var connection = new SqlConnection(connString))
            {
                var departments = connection.Query<Department>("sp_SelectDepartment_Sayak").ToList();
                ViewBag.Departments = new SelectList(departments, "Department_Id", "Department_Name");

                var employee = connection.QueryFirstOrDefault<Employee>("sp_SelectEmployeeById_Sayak", new { Id = id }, commandType: CommandType.StoredProcedure);

                if (employee == null)
                {
                    connection.Close();
                    return NotFound();
                }

                return View(employee);
            }
        }

        [HttpPost]
        public ActionResult Edit(Employee editedEmployee)
        {
            if (ModelState.IsValid)
            {
                string connString = this._configuration.GetConnectionString("MyDBConnection");
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    var departments = connection.Query<Department>("sp_SelectDepartment_Sayak").ToList();
                    ViewBag.Departments = new SelectList(departments, "Department_Id", "Department_Name");

                    var parameter = new
                    {
                        Id = editedEmployee.Id,
                        Name = editedEmployee.Name,
                        Email = editedEmployee.Email,
                        Mobile = editedEmployee.Mobile,
                        Department_Id = editedEmployee.Department_Id,
                        Status = editedEmployee.Status
                    };

                    connection.Execute("sp_UpdateEmployee_Sayak", parameter, commandType: CommandType.StoredProcedure);
                    connection.Close();

                    return RedirectToAction("Details", "ViewEmployee");
                }
            }

            return View(editedEmployee);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string connString = this._configuration.GetConnectionString("MyDBConnection");
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute("sp_DeleteEmployee_Sayak", new { Id = id }, commandType: CommandType.StoredProcedure);
                connection.Close();
            }

            return RedirectToAction("Details", "ViewEmployee");
        }
    }
}