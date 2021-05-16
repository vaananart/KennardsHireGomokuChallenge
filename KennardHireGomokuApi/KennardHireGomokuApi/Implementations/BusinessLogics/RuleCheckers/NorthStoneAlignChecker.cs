﻿using System.Collections.Generic;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.RuleCheckers
{
	public class NorthStoneAlignChecker : IRuleChecker
	{
		public NorthStoneAlignChecker(IDictionary<ValidatorType, IDirectionalLogicValidator> validators)
		{ }
		public EngineResultType? Check(IEnumerable<(int row, int col, DirectionType direction)> matchingNeighbours
										, StoneModel newStone
										, IEnumerable<StoneModel> sameColouredStones)
		{
			throw new System.NotImplementedException();
		}
	}
}
