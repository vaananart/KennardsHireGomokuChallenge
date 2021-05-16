using System.Collections.Generic;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Implementations.BusinessLogics.Validators;

using NUnit.Framework;

namespace KennardHireGomokuApiTests.Validators
{
	[TestFixture]
	class SouthDirectionValidatorTests
	{

		[Test]
		public void Validate_WhenFourStonesAligned_ShouldReturnTrue()
		{
			//Arrange
			IEnumerable<StoneModel> stones = new List<StoneModel> {
													new StoneModel{
														Row = 7,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 6,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 5,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 4,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 12,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 14,
														Column = 8,
														Colour = StoneColourType.Black
													}
												};


			var validator = new SouthDirectionValidator();
			var newStone = new StoneModel
			{
				Row = 8,
				Column = 8,
				Colour = StoneColourType.Black
			};
			//Action
			var result = validator.Validate(newStone, stones);

			//Assert
			Assert.AreEqual(true, result);
		}

		[Test]
		public void Validate_WhenStonesNotAligned_ShouldReturnFalse() 
		{
			//Arrange
			IEnumerable<StoneModel> stones = new List<StoneModel> {
													new StoneModel{
														Row = 7,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 1,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 5,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 4,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 12,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 14,
														Column = 8,
														Colour = StoneColourType.Black
													}
												};


			var validator = new SouthDirectionValidator();
			var newStone = new StoneModel
			{
				Row = 8,
				Column = 8,
				Colour = StoneColourType.Black
			};
			//Action
			var result = validator.Validate(newStone, stones);

			//Assert
			Assert.AreEqual(false, result);
		}
	}
}
