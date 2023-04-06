using System;
using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscountContracts;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication:ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            OperationResult result = new OperationResult();
            CustomerDiscount discount;
            DateTime startDate = command.StartDate.ToGeorgianDateTime();
            DateTime endDate = command.EndDate.ToGeorgianDateTime();
            if (_customerDiscountRepository.IsExists(x=>x.Reason==command.Reason && x.StartDate ==startDate && x.EndDate == endDate ))
            {
                result.Failed(ApplicationMessage.Duplication);
            }


            discount = new CustomerDiscount(command.FkProductId, command.Reason, startDate, endDate);
            _customerDiscountRepository.Create(discount);
         _customerDiscountRepository.SaveChanges();
          return  result.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {

            OperationResult result = new OperationResult();
            CustomerDiscount discount;
            DateTime startDate = command.StartDate.ToGeorgianDateTime();
            DateTime endDate = command.EndDate.ToGeorgianDateTime();
            if (_customerDiscountRepository.IsExists(x => x.Reason == command.Reason &&
                                                          x.StartDate == startDate && x.EndDate == endDate && x.Id != command.Id))
            {
              return  result.Failed(ApplicationMessage.Duplication);
            }

            discount = _customerDiscountRepository.Get(command.Id);
            if (discount ==null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            discount.Edit(command.FkProductId, command.Reason, startDate, endDate);
            _customerDiscountRepository.SaveChanges();
            return result.Succedded();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel=null)
        {
          return  _customerDiscountRepository.Search(searchModel);
        }
    }
}