using System;
using System.Collections.Generic;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.Interfaces
{
	public interface IGomokuService
	{
		GameModel CreateGame(GameModel convertedGameModel);
		EngineResultType PlaceBlackStone(Guid gameId, Guid playerId, StoneModel newStone);
		EngineResultType PlaceWhiteStone(Guid gameId, Guid playerId, StoneModel newStone);
		IEnumerable<StoneModel> RetrieveAllStonesPlacements(Guid gameId);
	}
}
