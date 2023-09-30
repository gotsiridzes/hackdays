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

        public ActionResult Index(bool tf = false)
        {
            var data = context.Services.ToList();
            var model = new ServicesModel()
            {
                TokenIsValid = base.ValidateToken(),
                Services = data
            };
            return View("ServicesListView", model);
        }
    }
}
