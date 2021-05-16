
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Implementations.BusinessLogics;
using KennardHireGomokuApi.Interfaces;
using KennardHireGomokuApi.Interfaces.RuleLogic;

using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

namespace KennardHireGomokuApiTests.BusinessLogic
{
	[TestFixture]
	public class SimpleLogicEngineTests
	{
		private GameModel _game;
		private  IGomokuLogicEngine _logicEngine;

		[SetUp]
		public void Setup()
		{
			_game = new GameModel();
			var list = from t in Assembly
									.GetAssembly(typeof(IDirectionalLogicValidator))
									.GetTypes()
					   where t.GetInterfaces().Contains(typeof(IDirectionalLogicValidator))
					   select t;
			IDictionary<ValidatorType, IDirectionalLogicValidator> validatorLookup = new Dictionary<ValidatorType, IDirectionalLogicValidator>();

			foreach (Type validator in list)
			{
				var instance = Activator.CreateInstance(validator) as IDirectionalLogicValidator;
				validatorLookup[instance.Type] = instance;
			}

			IDictionary<ValidatorType, IGeneralRuleValidator> generalRulesValidatorLookup = new Dictionary<ValidatorType, IGeneralRuleValidator>();
			var generalRuleList = from t in Assembly
									 .GetAssembly(typeof(IGeneralRuleValidator))
									 .GetTypes()
								  where t.GetInterfaces()
										.Contains(typeof(IGeneralRuleValidator))
								  select t;
			foreach (Type validator in generalRuleList)
			{
				var instance = Activator.CreateInstance(validator) as IGeneralRuleValidator;
				generalRulesValidatorLookup[instance.Type] = instance;
			}

			// Rule Checks
			var rules = from t in Assembly
							.GetAssembly(typeof(IRuleChecker))
							.GetTypes()
						where t.GetInterfaces()
							.Contains(typeof(IRuleChecker))
						select t;

			IList<IRuleChecker> rulesLookup = new List<IRuleChecker>();

			foreach (Type ruleChecker in rules)
			{
				var instance = Activator.CreateInstance(ruleChecker, validatorLookup) as IRuleChecker;
				rulesLookup.Add(instance);
			}
			var mockLogger = new Mock<ILogger<GomokuLogicEngine>>();
			_logicEngine = new GomokuLogicEngine(mockLogger.Object,generalRulesValidatorLookup, validatorLookup, rulesLookup);
		}

		[Test]
		public void AddStoneToBoard_WhenFirstBlackStoneAdded_ShouldReturnStoneAccepted()
		{
			//Arrange
			var stone = new StoneModel
							{
								Colour = StoneColourType.Black,
								Row = 8,
								Column = 8
							};

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.StoneAccepted, result);
		}

		[Test]
		public void AddStoneToBoard_WhenSecondBlackStoneAdded_ShouldReturnRejected()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};
			_game.BlackStones.Add(stone);

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.ProposedStoneLocationOccupied, result);
		}

		[Test]
		public void AddStoneToBoard_WhenSecondWhiteStoneAdded_ShouldReturnAccepted()
		{
			//Arrange
			var whitestone = new StoneModel
			{
				Colour = StoneColourType.White,
				Row = 8,
				Column = 8
			};

			_game.WhiteStones.Add(whitestone);

			var stone = new StoneModel
			{
				Colour = StoneColourType.White,
				Row = 8,
				Column = 8
			};

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.ProposedStoneLocationOccupied, result);
		}


		[Test]
		public void AddStoneToBoad_WhenFiveBlackStoneAlignedAlongNorthdirection_ShouldReturnBlackWon()
		{
			//Arrange
			_game.BlackStones = new List<StoneModel>{
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 9,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 10,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 11,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 13,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 15,
															Column = 8
														}
												};

			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 12,
				Column = 8
			};

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.BlackWon, result);
		}

		[Test]
		public void AddStoneToBoad_WhenNewBlackStoneCompletesAlignmentAlongNorthSouthdirection_ShouldReturnBlackWon()
		{
			//Arrange
			_game.BlackStones = new List<StoneModel>{
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 9,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 11,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 12,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 16,
															Column = 8
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 17,
															Column = 8
														}
												};

			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 10,
				Column = 8
			};

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.BlackWon, result);
		}

		[Test]
		public void AddStoneToBoad_WhenNewBlackStoneCompletesAlignmentAlongEastWestdirection_ShouldReturnBlackWon()
		{
			//Arrange
			_game.BlackStones = new List<StoneModel>{
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 9
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 10
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 7
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 6
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 1
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 13
														}
												};

			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.BlackWon, result);
		}

		[Test]
		public void AddStoneToBoad_WhenNewBlackStoneCompletesAlignmentAlongNorthEastSouthWestdirection_ShouldReturnBlackWon()
		{
			//Arrange
			_game.BlackStones = new List<StoneModel>{
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 9,
															Column = 9
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 10,
															Column = 10
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 7,
															Column = 7
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 6,
															Column = 6
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 1
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 13
														}
												};

			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.BlackWon, result);
		}

		[Test]
		public void AddStoneToBoad_WhenNewBlackStoneCompletesAlignmentAlongNorthWestSouthEastdirection_ShouldReturnBlackWon()
		{
			//Arrange
			_game.BlackStones = new List<StoneModel>{
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 9,
															Column = 7
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 10,
															Column = 6
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 7,
															Column = 9
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 6,
															Column = 10
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 5,
															Column = 11
														},
														new StoneModel
														{
															Colour = StoneColourType.Black,
															Row = 8,
															Column = 13
														}
												};

			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var result = _logicEngine.AddStoneToBoard(_game, stone);

			//Assert
			Assert.AreEqual(EngineResultType.BlackWon, result);
		}

	}
}
