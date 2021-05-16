using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.Interfaces
{
	public interface IGomokuLogicEngine
	{
		EngineResultType? AddStoneToBoard(GameModel matchedGame, StoneModel newStone);
	}
}