using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koggo.Application.Models;

public class SearchUserServices
{
	public decimal? MinPrice { get; set; }
	public decimal? MaxPrice { get; set; }
	public string[]? Categories { get; set; }
}