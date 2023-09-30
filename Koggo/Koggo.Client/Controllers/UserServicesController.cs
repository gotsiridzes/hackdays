using Koggo.Application.Services.Interface;
using Koggo.Client.Models.Home;
using Koggo.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Koggo.Client.Controllers
{
    public class UserServicesController : ControllerBase
    {
        private readonly KoggoDbContext context;

        public UserServicesController(IJwtTokenService jwtTokenService, KoggoDbContext context) : base(jwtTokenService)
        {
            this.context = context;
        }

        // GET: UserServicesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserServicesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserServicesController/Create
        public async Task<ActionResult> CreateAsync(List<UserServicesCreateItem> UserServices)
        {
            await context.UserServices.AddRangeAsync(UserServices.Select(a => new Domain.Models.UserService()
            {
                UserId = a.UserId,
                ServiceId = a.ServiceId,
                Price = a.Price,
                ThumbNailPhoto = a.ThumbNailPhoto,
                AvailableStartHour = a.AvailableStartHour,
                AvailableEndHour = a.AvailableEndHour,
            }));
            await context.SaveChangesAsync();
            return View();
        }

        // POST: UserServicesController/Create
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

        // GET: UserServicesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserServicesController/Edit/5
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

        // GET: UserServicesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserServicesController/Delete/5
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
