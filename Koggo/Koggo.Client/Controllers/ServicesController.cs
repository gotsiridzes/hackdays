using Koggo.Application.Services.Interface;
using Koggo.Client.Models;
using Koggo.Client.Models.Home;
using Koggo.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Koggo.Client.Controllers
{
    public class ServicesController : ControllerBase
    {
        private readonly KoggoDbContext context;

        public ServicesController(IJwtTokenService jwtTokenService, KoggoDbContext context) : base(jwtTokenService)
        {
            this.context = context;
        }

        // GET: ServicesController
        public ActionResult Index()
        {
            var data = context.Services.ToList();
            var model = new ServicesModel()
            {
                TokenIsValid = base.ValidateToken(),
                Services = data
            };
            return View("ServicesListView", model);
        }

        // GET: ServicesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ServicesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ServicesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ServicesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ServicesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
