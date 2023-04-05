using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscountContracts;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication:ICustomerDiscountApplication
    {
        public OperationResult Define(DefineCustomerDiscount command)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            throw new System.NotImplementedException();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            throw new System.NotImplementedException();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            throw new System.NotImplementedException();
        }
    }
}