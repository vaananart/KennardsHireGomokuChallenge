
using System;
using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

using Microsoft.Extensions.Logging;

namespace KennardHireGomokuApi.Implementations.Services
{
	public class GomokuService : IGomokuService
	{
		private readonly IDictionary<string, GameModel> _games;
		private readonly ILogger _logger;
		private readonly IGomokuLogicEngine _engine;

		public GomokuService(ILogger<GomokuService> logger, IGomokuLogicEngine engine)
		{
			_logger = logger;
			_engine = engine;
			_games = new Dictionary<string, GameModel>();
			
		}

		public GameModel CreateGame(GameModel convertedGameModel)
		{
			convertedGameModel.GameBoardGuid = Guid.NewGuid();
			convertedGameModel.WPlayerGuid = Guid.NewGuid();
			convertedGameModel.BPlayerGuid = Guid.NewGuid();
			convertedGameModel.CurrentPlayer = convertedGameModel.BPlayerGuid;
			convertedGameModel.TurnCount = 0;
			_games[convertedGameModel.GameBoardGuid.ToString()] = convertedGameModel;
			return convertedGameModel;
		}

		public EngineResultType PlaceBlackStone(Guid gameId, Guid playerId, StoneModel newStone)
		{
			GameModel matchedGame = null;
			if (!_games.TryGetValue(gameId.ToString(), out matchedGame))
				return EngineResultType.GameNotFound;

			newStone.Colour = StoneColourType.Black;

			if (matchedGame.CurrentPlayer == playerId)
			{
				var result =_engine.AddStoneToBoard(matchedGame, newStone);
				if (result == EngineResultType.StoneAccepted)
				{
					matchedGame.CurrentPlayer = matchedGame.WPlayerGuid;
				}

				return result.Value;
			}

			return EngineResultType.IncorrectPlayer;
		}

		public EngineResultType PlaceWhiteStone(Guid gameId, Guid playerId, StoneModel newStone)
		{
			GameModel matchedGame = null;
			if (!_games.TryGetValue(gameId.ToString(), out matchedGame))
				return EngineResultType.GameNotFound;

			newStone.Colour = StoneColourType.White;

			if (matchedGame.CurrentPlayer == playerId)
			{
				var result = _engine.AddStoneToBoard(matchedGame, newStone);
				if (result == EngineResultType.StoneAccepted)
				{
					matchedGame.CurrentPlayer = matchedGame.BPlayerGuid;
				}

				return result.Value;
			}

			return EngineResultType.IncorrectPlayer;
		}

		public IEnumerable<StoneModel> RetrieveAllStonesPlacements(Guid gameId)
		{
			GameModel matchedGame = null;
			if (!_games.TryGetValue(gameId.ToString(), out matchedGame))
				throw new ArgumentException($"{typeof(GomokuService).Name}:{System.Reflection.MethodBase.GetCurrentMethod().Name}:gameId => {gameId} does not exists.");

			var consolidatedStones = matchedGame.BlackStones.ToList();

			consolidatedStones.AddRange(matchedGame.WhiteStones);

			return consolidatedStones.OrderByDescending(x => x.Index);
		}
	}
}
