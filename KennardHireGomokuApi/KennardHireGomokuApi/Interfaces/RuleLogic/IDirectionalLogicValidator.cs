using System.Collections.Generic;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.Interfaces
{
	public interface IDirectionalLogicValidator
	{
		public ValidatorType Type { get; }

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null);
	}
}
