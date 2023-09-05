using Microsoft.AspNetCore.Mvc;

namespace AdminEmployeeManagement.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if (UserName.ToLower().Equals("admin") && Password.ToLower().Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid username or password";
            return View();
        }
    }
}