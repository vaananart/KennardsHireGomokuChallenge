
using System;
using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.Services
{
	public class GomokuService : IGomokuService
	{
		private readonly IList<GameModel> _games;
		private readonly IGomokuLogicEngine _engine;

		public GomokuService(IGomokuLogicEngine engine)
		{
			_games = new List<GameModel>();
			_engine = engine;
		}

		public GameModel CreateGame(GameModel convertedGameModel)
		{
			convertedGameModel.GameBoardGuid = Guid.NewGuid();
			convertedGameModel.WPlayerGuid = Guid.NewGuid();
			convertedGameModel.BPlayerGuid = Guid.NewGuid();
			convertedGameModel.CurrentPlayer = convertedGameModel.BPlayerGuid;
			convertedGameModel.TurnCount = 0;
			_games.Add(convertedGameModel);
			return convertedGameModel;
		}

		public EngineResultType PlaceBlackStone(Guid gameId, Guid playerId, StoneModel newStone)
		{
			var matchedGame = _games.Where(x => x.GameBoardGuid == gameId).FirstOrDefault();

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
			var matchedGame = _games.Where(x => x.GameBoardGuid == gameId).FirstOrDefault();

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
	}
}
