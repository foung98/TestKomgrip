using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using TestKomgrip.Models;
using TestKomgrip.Models_DataSQLServer;



namespace TestKomgrip.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly DbkomgripContext _Context;
        public HomeController(DbkomgripContext Contextb, IConfiguration configuration)
        {
            _Context = Contextb;
            //_logger = logger;
        }

        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Name") == null)
            {
                //ViewBag.returnUrl = Request.Headers["Referer"].ToString();
                return RedirectToAction("Login", "Authen");
                //return View();
            }
            ViewBag.Name = HttpContext.Session.GetString("Name");

            return View();
        }

        
        public IActionResult ManageUsers()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name"); 
            var DataUser = (from i in _Context.TbLogins
                            join t in _Context.TbTimeLogs on i.Id equals t.NameId
                            select new UserModel
                            {
                                Id = t.Id,
                                NameId = t.NameId,
                                Email = i.Email,
                                Name = i.Name,
                                Position = i.Position,
                                Lastlogin = t.LastLogin
                            }).ToList();

            return View(DataUser);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            //var user = _Context.TbTimeLogs.Single(x => x.Id == Id);

            var user = await _Context.TbTimeLogs.FindAsync(Id);
            if (user != null)
            {
                _Context.TbTimeLogs.Remove(user);
            }

            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageUsers));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbLogin = await _Context.TbLogins.FindAsync(id);
            if (tbLogin == null)
            {
                return NotFound();
            }
            return View(tbLogin);
        }

        // POST: TbLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,Position")] TbLogin tbLogin)
        {
            if (id != tbLogin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(tbLogin);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbLoginExists(tbLogin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbLogin);
        }

        private bool TbLoginExists(int id)
        {
            throw new NotImplementedException();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
