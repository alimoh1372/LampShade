using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.SlideContracts
{
    public interface ISlideApplication
    {
        OperationResult Create(CreateSlide command);
        OperationResult Edit(EditSlide command);
        OperationResult DeActive(long id);
        OperationResult Active(long id);
        List<SlideViewModel> Search(SlideSearchModel searchModel);
        EditSlide GetDetails(long id);
    }
}