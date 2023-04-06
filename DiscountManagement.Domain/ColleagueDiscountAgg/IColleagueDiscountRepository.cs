using System.Collections.Generic;
using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.ColleagueDiscountContracts;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository:IBaseRepository<long,ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel = null);
    }
}