using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Implementations.BusinessLogics.Validators;

using NUnit.Framework;

namespace KennardHireGomokuApiTests.Validators
{
	[TestFixture]
	class SouthEastDirectionValidatorTests
	{

		[Test]
		public void Validate_WhenFourStonesAligned_ShouldReturnTrue()
		{
			//Arrange
			IEnumerable<StoneModel> stones = new List<StoneModel> {
													new StoneModel{
														Row = 7,
														Column = 9,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 6,
														Column = 10,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 5,
														Column = 11,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 14,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 12,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 4,
														Column = 12,
														Colour = StoneColourType.Black
													}
												};


			var validator = new SouthEastDirectionValidator();
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
														Column = 6,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 6,
														Column = 10,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 5,
														Column = 11,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 14,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 12,
														Column = 8,
														Colour = StoneColourType.Black
													},
													new StoneModel{
														Row = 4,
														Column = 12,
														Colour = StoneColourType.Black
													}
												};


			var validator = new SouthEastDirectionValidator();
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
