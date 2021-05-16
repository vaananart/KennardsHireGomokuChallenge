using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.RuleCheckers
{
	public class NorthSouthStoneAlignChecker : IRuleChecker
	{
		private readonly IDictionary<ValidatorType, IDirectionalLogicValidator> _validators;

		public NorthSouthStoneAlignChecker(IDictionary<ValidatorType, IDirectionalLogicValidator> validators)
		{
			_validators = validators;
		}
		public EngineResultType? Check(IEnumerable<(int row, int col, DirectionType direction)> matchingNeighbours
										, StoneModel newStone
										, IEnumerable<StoneModel> sameColouredStones)
		{
			if (matchingNeighbours.Any(x => x.direction == DirectionType.North) 
				&& (matchingNeighbours.Any(x => x.direction == DirectionType.South)))
			{

				if (_validators[ValidatorType.NorthSouth].Validate(newStone, sameColouredStones))
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
