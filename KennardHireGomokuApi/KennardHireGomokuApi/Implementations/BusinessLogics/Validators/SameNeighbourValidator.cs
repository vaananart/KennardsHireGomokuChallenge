
using System.Collections.Generic;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class SameNeighbourValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.SameNeighbour;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			throw new System.NotImplementedException();
		}
	}
}
