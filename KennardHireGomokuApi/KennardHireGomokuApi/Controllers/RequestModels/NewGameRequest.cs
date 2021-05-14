using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KennardHireGomokuApi.Controllers.RequestModels
{
	public class NewGameRequest
	{
		public string WPlayerName {get;set;}
		public string BPlayerName { get; set; }
	}
}
