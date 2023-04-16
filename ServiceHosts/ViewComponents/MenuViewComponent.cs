using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponents
{
    public class MenuViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}