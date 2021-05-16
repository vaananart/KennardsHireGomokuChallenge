using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.RuleCheckers
{
	public class SouthWestStoneAlignChecker : IRuleChecker
	{
		private readonly IDictionary<ValidatorType, IDirectionalLogicValidator> _validators;

		public SouthWestStoneAlignChecker(IDictionary<ValidatorType, IDirectionalLogicValidator> validators)
		{
			_validators = validators;
		}
		public EngineResultType? Check(IEnumerable<(int row, int col, DirectionType direction)> matchingNeighbours
										, StoneModel newStone
										, IEnumerable<StoneModel> sameColouredStones)
		{
			if (matchingNeighbours.Any(x => x.direction == DirectionType.SouthWest))
			{
				if (_validators[ValidatorType.SouthWest].Validate(newStone, sameColouredStones))
				{
					if (newStone.Colour == StoneColourType.White)
						return EngineResultType.WhiteWon;
					else if (newStone.Colour == StoneColourType.Black)
						return EngineResultType.BlackWon;
				}
			}

			return null;
		}
	}
}
