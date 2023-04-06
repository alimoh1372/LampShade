using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscountContracts;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Application.Contracts.ProductContracts;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication:IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;
       

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {

            OperationResult result = new OperationResult();
            ColleagueDiscount entity;
            if (_colleagueDiscountRepository.IsExists(x => x.FkProductId == command.FkProductId && x.DiscountRate==command.DiscountRate))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            entity = new ColleagueDiscount(command.FkProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(entity);
            _colleagueDiscountRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            OperationResult result = new OperationResult();
            ColleagueDiscount entity;
            if (_colleagueDiscountRepository.IsExists(x => x.FkProductId == command.FkProductId && x.DiscountRate == command.DiscountRate && x.Id!=command.Id))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            entity = _colleagueDiscountRepository.Get(command.Id);
            if (entity==null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            entity.Edit(command.FkProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Active(long id)
        {
            OperationResult result = new OperationResult();
            ColleagueDiscount entity;
           

            entity = _colleagueDiscountRepository.Get(id);
            if (entity == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            entity.Active();
            _colleagueDiscountRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Remove(long id)
        {
            OperationResult result = new OperationResult();
            ColleagueDiscount entity;


            entity = _colleagueDiscountRepository.Get(id);
            if (entity == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            entity.Remove();
            _colleagueDiscountRepository.SaveChanges();
            return result.Succedded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel=null)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}