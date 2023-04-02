using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.SlideContracts;

namespace SM.Domain.SlideAgg
{
    public interface ISlideRepository:IBaseRepository<long,Slide>
    {
        List<SlideViewModel> Search(SlideSearchModel searchModel=null);
        EditSlide GetDetails(long id);
    }
}