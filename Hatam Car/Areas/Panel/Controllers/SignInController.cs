using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hatam_Car.Areas.Panel.Models;

namespace Hatam_Car.Areas.Panel.Controllers
{
    public class SignInController : Controller
    {
        WispoCarEntities2 DB = new WispoCarEntities2();
        // GET: Panel/SignIn
        public ActionResult Index()
        {
            return View();
        }
        // save to database function
        public JsonResult Save(User classUsers)
        {
            DB.Users.Add(classUsers);
            DB.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);

        }
    }
}