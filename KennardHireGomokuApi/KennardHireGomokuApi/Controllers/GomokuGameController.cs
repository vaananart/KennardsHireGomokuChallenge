using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KennardHireGomokuApi.Controllers.RequestModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KennardHireGomokuApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GomokuGameController : ControllerBase
	{
		private readonly ILogger<GomokuGameController> _logger;

		public GomokuGameController(ILogger<GomokuGameController> logger)
		{
			_logger = logger;
		}

		[HttpPost("newgame")]
		public ActionResult Post([FromBody] NewGameRequest gameRequest)
		{
			return Ok(new GameResultModel
			{
				GameId = new Guid()
			});
		}

		[HttpPost("blackstone")]
		public ActionResult RegisterBlackStone([FromBody] NewStoneLocationRequest stoneRequest)
		{
			return Ok("BlackStoneRecived");
		}

		[HttpPost("whitestone")]
		public ActionResult RegisterWhiteStone([FromBody] NewStoneLocationRequest stoneRequest)
		{
			return Ok("WhiteStoneReceived");
		}

		[HttpGet("retrievealllstones")]
		public ActionResult GetAllStoneLocations()
		{
			return null;
		}
	}
}
