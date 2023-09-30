using Koggo.Application.Models;
using Koggo.Application.Services.Interface;
using Koggo.Client.Models.Home;
using Koggo.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace Koggo.Client.Controllers;

public class UserServicesController : ControllerBase
{
	private readonly IUserServiceRepository _userServiceRepository;
	public UserServicesController(IJwtTokenService jwtTokenService, IUserServiceRepository userServiceRepository) : base(jwtTokenService)
	{
		_userServiceRepository = userServiceRepository;
	}

	public async Task<IActionResult> Index(int page, SearchUserServices req, CancellationToken cancellationToken)
	{
		var isValid = ValidateToken();
		var model = new UserServicesModel
		{
			TokenIsValid = isValid
		};

		if (!isValid)
			return View(model);

		model.UserServices = await _userServiceRepository
			.GetUserServicesByPageFilteredAsync(
				page,
				10,
				req.MinPrice,
				req.MaxPrice,
				req.Categories);

		model.ServiceTypes = await _userServiceRepository
			.ListServiceTypesAsync();

		return View(model);
	}
}