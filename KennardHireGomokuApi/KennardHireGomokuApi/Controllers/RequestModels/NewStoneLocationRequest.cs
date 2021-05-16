using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KennardHireGomokuApi.Controllers.RequestModels
{
	public class NewStoneLocationRequest
	{
		public Guid GameId{ get; set; }
		public Guid PlayerId { get; set; }
		public int Column{ get; set; }
		public int Row { get; set; }
	}
}
