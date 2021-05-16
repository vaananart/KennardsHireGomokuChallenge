using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

using Microsoft.Extensions.Logging;

namespace KennardHireGomokuApi.Implementations.BusinessLogics
{
	public class GomokuLogicEngine : IGomokuLogicEngine
	{
		private readonly IDictionary<ValidatorType, IDirectionalLogicValidator> _validators;
		private readonly ILogger _logger;
		private readonly IDictionary<ValidatorType, IGeneralRuleValidator> _generalRuleValidators;
		private readonly IEnumerable<IRuleChecker> _ruleCheckers;

		public GomokuLogicEngine(ILogger<GomokuLogicEngine> logger
									, IDictionary<ValidatorType, IGeneralRuleValidator> generalRulevValidators
									,IDictionary<ValidatorType, IDirectionalLogicValidator> validators
									, IEnumerable<IRuleChecker> ruleCheckers)
		{
			_logger = logger;
			_generalRuleValidators = generalRulevValidators;
			_validators = validators;
			_ruleCheckers = ruleCheckers;
		}

		public EngineResultType? AddStoneToBoard(GameModel matchedGame, StoneModel newStone)
		{
			//NOTE: Check for occupied stones
			if (!_generalRuleValidators[ValidatorType.Range].Validate(newStone))
				return EngineResultType.StoneRejected;

			//NOTE: check whether there are other stones occupied.
			if ((!_generalRuleValidators[ValidatorType.SameLocation].Validate(newStone, matchedGame.BlackStones)) 
				|| (!_generalRuleValidators[ValidatorType.SameLocation].Validate(newStone, matchedGame.WhiteStones)))
				return EngineResultType.ProposedStoneLocationOccupied;

			newStone.Index = ++matchedGame.TurnCount;
			IList<StoneModel> matchedList = null;
			if (newStone.Colour == StoneColourType.Black)
			{
				matchedList = matchedGame.BlackStones;
			}
			else
			{
				matchedList = matchedGame.WhiteStones;
			}

			//NOTE: Add stone if this is the first stone in the collection.
			if (matchedList.Count() == 0)
			{
				
				matchedList.Add(newStone);
				return EngineResultType.StoneAccepted;
			}
			
			if (matchedList.Count() >= 4)
			{
				//NOTE: find all the neighbouring stones
				var matchedNeighbourStoneLocations = from c in newStone.GetNeighbouringLocations()
													 where matchedList
															 .Where(x => x.Row == c.row
																	 && x.Column == c.col).Count() > 0
													 select c;

				//NOTE: Run pass all the rules to determine winners
				foreach (var ruleChecker in _ruleCheckers)
				{
					var result = ruleChecker.Check(matchedNeighbourStoneLocations, newStone, matchedList);
					if (result != null)
					{
						matchedList.Add(newStone);
						return result;
					}
				}
			}

			matchedList.Add(newStone);
			return EngineResultType.StoneAccepted;
		}
	}
}
