using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using KennardHireGomokuApi.Controllers.ReponseModels;
using KennardHireGomokuApi.Controllers.RequestModels;
using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KennardHireGomokuApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GomokuGameController : ControllerBase
	{
		private readonly ILogger<GomokuGameController> _logger;
		private readonly IMapper _mapper;
		private readonly IGomokuService _gomokuService;

		public GomokuGameController(ILogger<GomokuGameController> logger
										, IMapper mapper
										, IGomokuService gomokuService)
		{
			_logger = logger;
			_mapper = mapper;
			_gomokuService = gomokuService;
		}

		[HttpPost("newgame")]
		public async Task<ActionResult> Post([FromBody] NewGameRequest gameRequest)
		{
			_logger.LogInformation($"{typeof(GomokuGameController).Name}:{System.Reflection.MethodBase.GetCurrentMethod().Name}: Received New Game Request => {gameRequest}");
			var convertedGameModel = _mapper.Map<NewGameRequest, GameModel>(gameRequest);
			var result = await Task.FromResult(_gomokuService.CreateGame(convertedGameModel));

			var convertedResult = _mapper.Map<GameModel, GameResponseModel>(result);

			return Ok(convertedResult);
		}

		[HttpPost("blackstone")]
		public async Task<ActionResult> RegisterBlackStone([FromBody] NewStoneLocationRequest stoneRequest)
		{
			_logger.LogInformation($"{typeof(GomokuGameController).Name}:{System.Reflection.MethodBase.GetCurrentMethod().Name}: Received Black Stone => {stoneRequest}");
			var newStone = _mapper.Map<NewStoneLocationRequest, StoneModel>(stoneRequest);
			var result =await Task.FromResult(_gomokuService.PlaceBlackStone(stoneRequest.GameId, stoneRequest.PlayerId, newStone));

			if (result == Enums.EngineResultType.IncorrectPlayer)
				return BadRequest(new StoneAcceptenceResponseModel { Status = result.ToString() });
			return Ok(new StoneAcceptenceResponseModel { Status = result.ToString()} );
		}

		[HttpPost("whitestone")]
		public async Task<ActionResult> RegisterWhiteStone([FromBody] NewStoneLocationRequest stoneRequest)
		{
			_logger.LogInformation($"{typeof(GomokuGameController).Name}:{System.Reflection.MethodBase.GetCurrentMethod().Name}: Received White Stone => {stoneRequest}");
			var newStone = _mapper.Map<NewStoneLocationRequest, StoneModel>(stoneRequest);
			var result = await Task.FromResult(_gomokuService.PlaceWhiteStone(stoneRequest.GameId, stoneRequest.PlayerId, newStone));

			if (result == Enums.EngineResultType.IncorrectPlayer)
				return BadRequest(new StoneAcceptenceResponseModel { Status = result.ToString() });
			return Ok(new StoneAcceptenceResponseModel { Status = result.ToString() });
		}

		[HttpGet("retrievealllstones")]
		public ActionResult GetAllStoneLocations([FromQuery] Guid gameId)
		{
			IEnumerable<StoneModel> stones =_gomokuService.RetrieveAllStonesPlacements(gameId);
			var convertedStones = _mapper.Map<IEnumerable<StoneModel>, IEnumerable<StoneInfoResponseModel>>(stones);

			return Ok(convertedStones);
		}
	}
}
