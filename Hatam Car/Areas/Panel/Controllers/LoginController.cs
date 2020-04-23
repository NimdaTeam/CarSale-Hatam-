using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hatam_Car.Areas.Panel.Models;

namespace Hatam_Car.Areas.Panel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Panel/Login
        WispoCarEntities2 DB = new WispoCarEntities2();
        public ActionResult Index()
        {
            return View();
        }
        //Login Function
        public ActionResult CheckLogin(string txtUsername, string txtPassword)
        {
            var Result = DB.Users.Where(item => item.Email == txtUsername && item.Password == txtPassword).ToList();
            if (Result.Count > 0)
            {
                Session["Sess_UserID"] = Result[0].ID;
                Session["Sess_UserName"] = Result[0].Email;
                Session["Sess_UserRole"] = Result[0].Role;
                Session["Sess_Name"] = Result[0].Name;

                return Redirect("/Panel/PanelHome");



            }
            else
            {
                return Redirect("/Panel/Login");
              
            }
        }
    }
}