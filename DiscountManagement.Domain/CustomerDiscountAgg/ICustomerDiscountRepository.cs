using System.Collections.Generic;
using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.CustomerDiscountContracts;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository:IBaseRepository<long,CustomerDiscount>
    {
        EditCustomerDiscount GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel=null);
    }
}