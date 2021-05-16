using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class NorthDirectionValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.North;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var matchedstones = from t in stones
								from u in newStone
										.NorthFiveSteps()
								where t.Row == u.row 
										&& t.Column == u.col
								select t;

			if (matchedstones.Count() == 4)
				return true;

			return false;
		}
	}
}
