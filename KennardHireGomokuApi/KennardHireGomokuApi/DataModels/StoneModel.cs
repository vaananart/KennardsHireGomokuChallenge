using System;
using System.Collections.Generic;

using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.DataModels
{
	public class StoneModel
	{
		public StoneColourType Colour { get; set; }
		public int Column { get; set; }
		public int Row { get; set; }
		public int index{ get; set; }

	}

	public static class StoneModelExtension {
		public static IEnumerable<(int row, int col, DirectionType direction)> 
			GetNeighbouringLocations(this StoneModel stone) => new List<(int
																		, int
																		, DirectionType)> {
																			stone.NorthLocation()
																			,stone.SouthLocation()
																			,stone.EastLocation()
																			,stone.WestLocation()
																			,stone.NorthEastLocation()
																			,stone.NorthWestLocation()
																			,stone.SouthEastLocation()
																			,stone.SouthWestLocation()
																		};

		public static (int row, int col, DirectionType direction) 
			NorthLocation(this StoneModel stone) => (row: stone.Row + 1
														, col: stone.Column
														, direction: DirectionType.North);

		public static (int row, int col, DirectionType direction) 
			SouthLocation(this StoneModel stone) => (row: stone.Row - 1
														, col: stone.Column
														, direction: DirectionType.South);
															  
		public static (int row, int col, DirectionType direction) 
			EastLocation(this StoneModel stone) => (row: stone.Row
														, col: stone.Column + 1
														, direction: DirectionType.East);

		public static (int row, int col, DirectionType direction) 
			WestLocation(this StoneModel stone) => (row: stone.Row
														, col: stone.Column - 1
														, direction: DirectionType.West);

		public static (int row, int col, DirectionType direction)
			NorthEastLocation(this StoneModel stone) => (row: stone.Row + 1
															, col: stone.Column + 1
															, direction: DirectionType.NorthEast);

		public static (int row, int col, DirectionType direction) 
			NorthWestLocation(this StoneModel stone) => (row: stone.Row + 1
															, col: stone.Column - 1
															, direction: DirectionType.NorthWest);

		public static (int row, int col, DirectionType direction) 
			SouthEastLocation(this StoneModel stone) => (row: stone.Row - 1
															, col: stone.Column + 1
															, direction: DirectionType.SouthEast);
		public static (int row, int col, DirectionType direction) 									   
			SouthWestLocation(this StoneModel stone) => (row: stone.Row - 1
															, col: stone.Column - 1
															, direction: DirectionType.SouthWest);

		public static IEnumerable<(int row, int col)> 
			NorthFiveSteps(this StoneModel stone)=> new List<(int row, int col)> {
																			(stone.Row + 1, stone.Column),
																			(stone.Row + 2, stone.Column),
																			(stone.Row + 3, stone.Column),
																			(stone.Row + 4, stone.Column)
																		};
		public static IEnumerable<(int row, int col)> 
			SouthFiveSteps(this StoneModel stone) => new List<(int, int)> {
																			(stone.Row - 1, stone.Column),
																			(stone.Row - 2, stone.Column),
																			(stone.Row - 3, stone.Column),
																			(stone.Row - 4, stone.Column)
																		};
		public static IEnumerable<(int row, int col)> 
			EastFiveSteps(this StoneModel stone) => new List<(int, int)> {
																			(stone.Row, stone.Column + 1),
																			(stone.Row, stone.Column + 2),
																			(stone.Row, stone.Column + 3),
																			(stone.Row, stone.Column + 4)
																		};
		public static IEnumerable<(int row, int col)> 
			WestFiveSteps(this StoneModel stone) => new List<(int, int)> {
																			(stone.Row, stone.Column - 1),
																			(stone.Row, stone.Column - 2),
																			(stone.Row, stone.Column - 3),
																			(stone.Row, stone.Column - 4)
																		};
		public static IEnumerable<(int row, int col)>
			NorthEastFiveSteps(this StoneModel stone) => new List<(int, int)> {
																			(stone.Row + 1, stone.Column + 1),
																			(stone.Row + 2, stone.Column + 2),
																			(stone.Row + 3, stone.Column + 3),
																			(stone.Row + 4, stone.Column + 4)
																		};
		public static IEnumerable<(int row, int col)> 
			NorthWestFiveSteps(this StoneModel stone) => new List<(int, int)> {
																			(stone.Row + 1, stone.Column - 1),
																			(stone.Row + 2, stone.Column - 2),
																			(stone.Row + 3, stone.Column - 3),
																			(stone.Row + 4, stone.Column - 4)
																		};
		public static IEnumerable<(int row, int col)> 
			SouthEastFiveSteps(this StoneModel stone) => new List<(int, int)> {
																			(stone.Row - 1, stone.Column + 1),
																			(stone.Row - 2, stone.Column + 2),
																			(stone.Row - 3, stone.Column + 3),
																			(stone.Row - 4, stone.Column + 4)
																		};
		public static IEnumerable<(int row, int col)> 
			SouthWestFiveSteps(this StoneModel stone) => new List<(int, int)> {
																			(stone.Row - 1, stone.Column - 1),
																			(stone.Row - 2, stone.Column - 2),
																			(stone.Row - 3, stone.Column - 3),
																			(stone.Row - 4, stone.Column - 4)
																		};

	}
	
}
