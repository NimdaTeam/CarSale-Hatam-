using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hatam_Car.Areas.Panel.Models;

namespace Hatam_Car.Areas.Panel.Controllers
{
    public class Ticket_AddingController : Controller
    {
        WispoCarEntities2 DB = new WispoCarEntities2();
        // GET: Panel/Ticket_Adding
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Save(Ticket ClassAtt)
        {
            DB.Tickets.Add(ClassAtt);
            DB.SaveChanges();
            return Json(ClassAtt.ID, JsonRequestBehavior.AllowGet);
        }
        public void GetPersImageFile(int EmpID)
        {

            string NewFilename = "";

            string SaveLocation = Server.MapPath("~/FileArchive/");
            String CurrentDate = DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "-");
            foreach (string item in Request.Files)
            {

                var Filename = Request.Files[item].FileName;
                var DotPos = Filename.LastIndexOf(".");
                var FileameWithoutExtension = Filename.Substring(0, DotPos);
                var FileExtension = Filename.Substring(DotPos);
                var CurrentTime = DateTime.Now.Hour + "_" + DateTime.Now.Minute;

                NewFilename = FileameWithoutExtension + "_" + CurrentDate + "_" + CurrentTime + FileExtension;
                var FinalPath = SaveLocation + NewFilename;
                Request.Files[item].SaveAs(FinalPath);


            }
            //2-Save Attach Files:
            if (NewFilename.Length > 0)
            {
                var Result = DB.Tickets.Where(item => item.ID == EmpID).ToList();
                Result[0].ImgUrl = NewFilename;
                DB.SaveChanges();
            }


        }
        public JsonResult Show()
        {
            var Result = DB.TicketViews.ToList();
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }
}