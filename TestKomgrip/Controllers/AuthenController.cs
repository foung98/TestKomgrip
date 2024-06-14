using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using TestKomgrip.Models;
using TestKomgrip.Models_DataSQLServer;

namespace TestKomgrip.Controllers
{
    public class AuthenController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly DbkomgripContext _Context;
        public AuthenController(DbkomgripContext Contextb, IConfiguration configuration)
        {
            _Context = Contextb;
            //_logger = logger;
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Name") == null)
            {

                return View();
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var obj = await _Context.TbLogins.Where(a => a.Email.Equals(loginModel.Email) && a.Password.Equals(loginModel.Password)).FirstOrDefaultAsync();
                if (obj != null)
                {
                    HttpContext.Session.SetString("Email", obj.Email.ToString());
                    HttpContext.Session.SetString("Password", obj.Password.ToString());
                    HttpContext.Session.SetString("Name", obj.Name.ToString());
                    HttpContext.Session.SetString("Id", obj.Id.ToString());
                    TempData["piValue"] = obj.Name.ToString();
                    TempData["piValueId"] = obj.Id;

                    var tb = new TbTimeLog()
                    {

                        NameId = obj.Id,
                        LastLogin = DateTime.Now
                    };
                    _Context.TbTimeLogs.Add(tb);
                    _Context.SaveChanges();


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.message = "ไม่พบผู้ใช้งาน";
                    return View();
                }

            }
            ViewBag.message = "is not ModelState";
            return View();

        }

        public ActionResult Logout()
        {


            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Password");
            



            return RedirectToAction("Login");
        }
    }
}
