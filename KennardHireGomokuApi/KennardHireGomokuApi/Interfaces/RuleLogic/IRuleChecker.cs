using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.Interfaces.RuleLogic
{
	public interface IRuleChecker
	{
		public EngineResultType? Check(IEnumerable<(int row, int col, DirectionType direction)> matchingNeighbours, StoneModel newStone, IEnumerable<StoneModel> sameColouredStones);
	}
}
