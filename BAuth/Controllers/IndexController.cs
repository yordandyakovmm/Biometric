using System.Web.Mvc;

namespace BAuth.Controllers
{


    public class IndexController : BaseController
    {

        //[Route("{item}/{category}")]
        public ActionResult Index(string category, string item)
        {
            return View("Index");
        }

    }
}