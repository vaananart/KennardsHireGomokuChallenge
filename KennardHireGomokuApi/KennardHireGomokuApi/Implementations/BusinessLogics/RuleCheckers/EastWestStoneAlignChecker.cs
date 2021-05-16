using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.RuleCheckers
{
	public class EastWestStoneAlignChecker : IRuleChecker
	{
		private readonly IDictionary<ValidatorType, IDirectionalLogicValidator> _validators;

		public EastWestStoneAlignChecker(IDictionary<ValidatorType, IDirectionalLogicValidator> validators)
		{
			_validators = validators;
		}
		
		public EngineResultType? Check(IEnumerable<(int row, int col, DirectionType direction)> matchingNeighbours
										, StoneModel newStone
										, IEnumerable<StoneModel> sameColouredStones)
		{
			if (matchingNeighbours.Any(x => x.direction == DirectionType.East)
				&& (matchingNeighbours.Any(x => x.direction == DirectionType.West)))
			{
				if (_validators[ValidatorType.EastWest].Validate(newStone, sameColouredStones))
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
