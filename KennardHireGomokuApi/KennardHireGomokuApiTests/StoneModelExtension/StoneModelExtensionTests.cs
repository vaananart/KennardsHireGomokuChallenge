
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;

using NUnit.Framework;

namespace KennardHireGomokuApiTests.StoneModelExtension
{
	[TestFixture]
	public class StoneModelExtensionTests
	{
		[Test]
		public void GetNeighbouringLocations_ShouldReturnListOfNeighbouringCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.GetNeighbouringLocations();

			//Assert
			Assert.AreEqual((row: 9, col: 8, direction: DirectionType.North)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.North)
									.FirstOrDefault());
			Assert.AreEqual((row: 7, col: 8, direction: DirectionType.South)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.South)
									.FirstOrDefault());
			Assert.AreEqual((row: 8, col: 9, direction: DirectionType.East)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.East)
									.FirstOrDefault());
			Assert.AreEqual((row: 8, col: 7, direction: DirectionType.West)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.West)
									.FirstOrDefault());
			Assert.AreEqual((row: 9, col: 9, direction: DirectionType.NorthEast)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.NorthEast)
									.FirstOrDefault());
			Assert.AreEqual((row: 9, col: 7, direction: DirectionType.NorthWest)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.NorthWest)
									.FirstOrDefault());
			Assert.AreEqual((row: 7, col: 9, direction: DirectionType.SouthEast)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.SouthEast)
									.FirstOrDefault());
			Assert.AreEqual((row: 7, col: 7, direction: DirectionType.SouthWest)
								, neighbouringCordinates
									.Where(x => x.direction == DirectionType.SouthWest)
									.FirstOrDefault());
		}

		[Test]
		public void NorthFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.NorthFiveSteps();

			//Assert
			for (int i = 1; i <= 4; i++)
			{
				Assert.AreEqual((row: stone.Row + i, col: stone.Column), neighbouringCordinates.Where(x=>x.row	 == stone.Row + i && x.col == stone.Column).FirstOrDefault());
			}
		}

		[Test]
		public void SouthFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.SouthFiveSteps();

			//Assert
			for (int i = 1; i <= 4; i++)
			{
				Assert.AreEqual((row: stone.Row - i, col: stone.Column), neighbouringCordinates.Where(x => x.row == stone.Row - i && x.col == stone.Column).FirstOrDefault());
			}
		}

		[Test]
		public void EastFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.EastFiveSteps();

			//Assert
			for (int i = 1; i <= 5; i++)
			{
				Assert.AreEqual((row: stone.Row, col: stone.Column + 1), neighbouringCordinates.Where(x => x.row == stone.Row && x.col == stone.Column + 1).FirstOrDefault());
			}
		}

		[Test]
		public void WestFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.WestFiveSteps();

			//Assert
			for (int i = 1; i <= 5; i++)
			{
				Assert.AreEqual((row: stone.Row, col: stone.Column - 1), neighbouringCordinates.Where(x => x.row == stone.Row && x.col == stone.Column - 1).FirstOrDefault());
			}
		}

		[Test]
		public void NorthEastFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.NorthEastFiveSteps();

			//Assert
			for (int i = 1; i <= 4; i++)
			{
				Assert.AreEqual((row: stone.Row + i, col: stone.Column + i), neighbouringCordinates.Where(x => x.row == stone.Row + i && x.col == stone.Column + i).FirstOrDefault());
			}
		}

		[Test]
		public void NorthWestFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.NorthWestFiveSteps();

			//Assert
			for (int i = 1; i <= 4; i++)
			{
				Assert.AreEqual((row: stone.Row + i, col: stone.Column - i), neighbouringCordinates.Where(x => x.row == stone.Row + i && x.col == stone.Column - i).FirstOrDefault());
			}
		}

		[Test]
		public void SouthEastFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.SouthEastFiveSteps();

			//Assert
			for (int i = 1; i <= 4; i++)
			{
				Assert.AreEqual((row: stone.Row - i, col: stone.Column + i), neighbouringCordinates.Where(x => x.row == stone.Row - i && x.col == stone.Column + i).FirstOrDefault());
			}
		}

		[Test]
		public void SouthWestFiveSteps_ShouldReturnFiveCoordinates()
		{
			//Arrange
			var stone = new StoneModel
			{
				Colour = StoneColourType.Black,
				Row = 8,
				Column = 8
			};

			//Action
			var neighbouringCordinates = stone.SouthWestFiveSteps();

			//Assert
			for (int i = 1; i <= 4; i++)
			{
				Assert.AreEqual((row: stone.Row - i, col: stone.Column - i), neighbouringCordinates.Where(x => x.row == stone.Row - i && x.col == stone.Column - i).FirstOrDefault());
			}
		}
	}
}
