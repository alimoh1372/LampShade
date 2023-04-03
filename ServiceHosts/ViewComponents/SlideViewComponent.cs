using _01_LampshadeQuery.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponents
{
    public class SlideViewComponent:ViewComponent
    {
        private readonly ISlideQuery _slideQuery;

        public SlideViewComponent(ISlideQuery slideQuery)
        {
            _slideQuery = slideQuery;
        }

        public IViewComponentResult Invoke()
        {
            var slideQueryViewmodel = _slideQuery.GetSlides();
            return View(slideQueryViewmodel);
        }
    }
}