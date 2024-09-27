using Microsoft.AspNetCore.Mvc;

namespace Exercise_21.Component
{
    public class LogoutViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}
