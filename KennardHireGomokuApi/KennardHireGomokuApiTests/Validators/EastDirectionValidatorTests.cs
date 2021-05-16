using System.Collections.Generic;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Implementations.BusinessLogics.Validators;

using NUnit.Framework;

namespace KennardHireGomokuApiTests.Validators
{
	[TestFixture]
	public class EastDirectionValidatorTests
	{

		[Test]
		public void Validate_WhenFourStonesAligned_ShouldReturnTrue() {

			//Arrange
			IEnumerable<StoneModel> stones = new List<StoneModel> {
													new StoneModel{
														Row = 8,
														Column = 9,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 10,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 11,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 12,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 13,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 14,
														Colour = StoneColourType.Black
													}
												};


			var validator = new EastDirectionValidator();
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
		public void Validate_WhenStonesNotAligned_ShouldReturnFalse() {
			//Arrange
			IEnumerable<StoneModel> stones = new List<StoneModel> {
													new StoneModel{
														Row = 8,
														Column = 9,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 1,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 11,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 12,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 13,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 8,
														Column = 14,
														Colour = StoneColourType.Black
													}
												};


			var validator = new EastDirectionValidator();
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
