using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}