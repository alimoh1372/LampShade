using System.Collections.Generic;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleagueDiscountContracts
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Define(DefineColleagueDiscount command);
        OperationResult Edit(EditColleagueDiscount command);
        OperationResult Active(long id);
        OperationResult Remove(long id);

        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}