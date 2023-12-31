﻿using System.Diagnostics;
using Koggo.Domain.Interfaces;

namespace Koggo.Domain.Models;

public class Service : IBaseModel
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public ServiceType ServiceType { get; set; }
	public string IconImage { get; set; } = null!;
	public ICollection<UserService> UserServices { get; set; }
}