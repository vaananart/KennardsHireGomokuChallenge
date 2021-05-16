using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

namespace KennardHireGomokuApi.Implementations.BusinessLogics
{
	public class GomokuLogicEngine : IGomokuLogicEngine
	{
		private readonly IDictionary<ValidatorType, IDirectionalLogicValidator> _validators;
		private readonly IDictionary<ValidatorType, IGeneralRuleValidator> _generalRuleValidators;
		private readonly IEnumerable<IRuleChecker> _ruleCheckers;

		public GomokuLogicEngine(IDictionary<ValidatorType, IGeneralRuleValidator> generalRulevValidators, IDictionary<ValidatorType, IDirectionalLogicValidator> validators, IEnumerable<IRuleChecker> ruleCheckers)
		{
			_validators = validators;
			_generalRuleValidators = generalRulevValidators;
			_ruleCheckers = ruleCheckers;
		}

		public EngineResultType? AddStoneToBoard(GameModel matchedGame, StoneModel newStone)
		{
			if (!_generalRuleValidators[ValidatorType.Range].Validate(newStone))
				return EngineResultType.StoneRejected;
			
			if ((!_generalRuleValidators[ValidatorType.SameLocation].Validate(newStone, matchedGame.BlackStones)) 
				|| (!_generalRuleValidators[ValidatorType.SameLocation].Validate(newStone, matchedGame.WhiteStones)))
				return EngineResultType.ProposedStoneLocationOccupied;

			newStone.index = ++matchedGame.TurnCount;
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
			
			//NOTE: check whether there are other stones occupied.


			if (matchedList.Count() >= 4)
			{
				//need to start tracking scores now.
				//1st: find all the neighbouring stones
				var matchedNeighbourStoneLocations = from c in newStone.GetNeighbouringLocations()
													 where matchedList
															 .Where(x => x.Row == c.row
																	 && x.Column == c.col).Count() > 0
													 select c;

				foreach (var ruleChecker in _ruleCheckers)
				{
					var result = ruleChecker.Check(matchedNeighbourStoneLocations, newStone, matchedList);
					if (result != null)
						return result;
				}
				//If c have N and S
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.North) && (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.South)))
				//{

				//	if (_validators[ValidatorType.NorthSouth].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have E and W
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.East) 
				//&& (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.West)))
				//{
				//	if (_validators[ValidatorType.EastWest].Validate(newStone, matchedList))
				//	{

				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have NE and SW
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.NorthEast)
				//&& (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.SouthWest)))
				//{
				//	if (_validators[ValidatorType.NorthEastSouthWest].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}

				//}
				//if c have NW and SE
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.NorthWest)
				//&& (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.SouthEast)))
				//{
				//	if (_validators[ValidatorType.NorthWestSouthEast].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}

				//}
				//
				//if c have North
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.North))
				//{
				//	if (_validators[ValidatorType.North].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have South
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.South))
				//{
				//	if (_validators[ValidatorType.South].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have East
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.East))
				//{
				//	if (_validators[ValidatorType.East].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have West
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.West))
				//{
				//	if (_validators[ValidatorType.West].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have NE
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.NorthEast))
				//{
				//	if (_validators[ValidatorType.NorthEast].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have NW
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.NorthWest))
				//{
				//	if (_validators[ValidatorType.NorthWest].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have SE
				//if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.SouthEast))
				//{
				//	if (_validators[ValidatorType.SouthEast].Validate(newStone, matchedList))
				//	{
				//		if (newStone.Colour == StoneColourType.White)
				//			return EngineResultType.WhiteWon;
				//		else if (newStone.Colour == StoneColourType.Black)
				//			return EngineResultType.BlackWon;
				//	}
				//}
				//if c have SW
				if (matchedNeighbourStoneLocations.Any(x => x.direction == DirectionType.SouthWest)) 
				{
					if (_validators[ValidatorType.SouthWest].Validate(newStone, matchedList))
					{
						if (newStone.Colour == StoneColourType.White)
							return EngineResultType.WhiteWon;
						else if (newStone.Colour == StoneColourType.Black)
							return EngineResultType.BlackWon;
					}
				}

			}

			matchedList.Add(newStone);
			return EngineResultType.StoneAccepted;
		}
	}
}
