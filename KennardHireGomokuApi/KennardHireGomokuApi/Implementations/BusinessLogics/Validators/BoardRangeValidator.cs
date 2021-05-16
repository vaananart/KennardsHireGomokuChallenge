using System.Collections.Generic;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces.RuleLogic;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class BoardRangeValidator : IGeneralRuleValidator
	{
		public ValidatorType Type => ValidatorType.Range;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			if (newStone.Column > 15 || newStone.Column < 1)
				return false;

			if (newStone.Row > 15 || newStone.Row < 1)
				return false;

			return true;
		}
	}
}
