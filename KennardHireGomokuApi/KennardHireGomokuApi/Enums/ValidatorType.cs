namespace KennardHireGomokuApi.Enums
{
	public enum ValidatorType
	{
		//End-to-End
		EastWest,
		NorthSouth,
		NorthEastSouthWest,
		NorthWestSouthEast,
		//
		SameNeighbour,
		SameLocation,
		Range,
		//
		//For Specific Directions
		East,
		North,
		West,
		South,
		NorthEast,
		NorthWest,
		SouthEast,
		SouthWest
	}
}
