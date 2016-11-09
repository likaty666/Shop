using Shop.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.UI.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        IEnumerable<TravelMode> list = new List<TravelMode>(){
        new TravelMode{id = 1 , name = "Автомобиль"},
        new TravelMode{id = 2 , name = "велосипед"},
        new TravelMode{id = 3 , name = "общественный транспорт"},
        new TravelMode{id = 4 , name = "пешком"}
        };

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.travelmode = new SelectList(list, "id", "name");
            return View();
        }
    }
}