using MvcFileUploader;
using MvcFileUploader.Models;
using Shop.DataLayer.DbLayer;
using Shop.Repository.Repositories;
using Shop.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.UI.Controllers
{
    public class GoodController : Controller
    {
        // GET: Good
        List<ShoppingCart> shopcart;
        IGenericRepository<Good> repogood;
        IGenericRepository<Photo> repophoto;
        IGenericRepository<Sale> repsal;
        IGenericRepository<Salepos> repsalpos;
        IGenericRepository<UserAcc> repacc;
        IGenericRepository<Manufacturer> repman;
        int idForPhoto;
        public GoodController(IGenericRepository<Manufacturer> repman, IGenericRepository<UserAcc> repacc, IGenericRepository<Sale> repsal, IGenericRepository<Salepos> repsalpos, IGenericRepository<Good> repogood, IGenericRepository<Photo> repophoto)
        {
            this.repacc = repacc;
            this.repogood = repogood;
            this.repophoto = repophoto;
            this.repsal = repsal;
            this.repsalpos = repsalpos;
            this.repman = repman;
            idForPhoto = 0;
        }

        public ActionResult Index()
        {
            ViewModelGoodRepository repo = new ViewModelGoodRepository(repogood, repophoto);

            var model = repo.GetAll();
            return View(model);
        }
       // [Authorize(Roles = "User")]
        public ActionResult Detail(int id)
        {
            ViewBag.ManufacturerId = new SelectList(repman.GetAll(), "ManufacturerId", "ManufacturerName");
            var model = repogood.Get(id);
            idForPhoto = id;
            return View(model);
        }

        public ActionResult EditDetail(int id)
        {
            ViewBag.ManufacturerId = new SelectList(repman.GetAll(), "ManufacturerId", "ManufacturerName");
            var model = repogood.Get(id);
            idForPhoto = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult EditDetail(Good g)
        {
            if (ModelState.IsValid)
            {
                repogood.AddOrUpdate(g);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Show(int id)
        {
            var model = repophoto.FindBy(x => x.GoodId == id);
            return View(model);
        }

        //=================================================PHOTOES==============

        public ActionResult UploadFile(int? entityId)
        {

            var statuses = new List<ViewDataUploadFileResult>();
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var st = FileSaver.StoreFile(x =>
                {
                    x.File = Request.Files[i];
                    x.DeleteUrl = Url.Action("DeleteFile", new { entityId = entityId });
                    x.StorageDirectory = Server.MapPath("~/Content/uploads");
                    x.UrlPrefix = "/Content/uploads";
                    x.FileName = Request.Files[i].FileName;
                    x.ThrowExceptions = true;
                });
                Photo p = new Photo() { PathPhoto = "/Content/uploads/" + Request.Files[i].FileName, GoodId = Convert.ToInt32(entityId) };
                repophoto.AddOrUpdate(p);
                statuses.Add(st);
            }

            statuses.ForEach(x => x.thumbnailUrl = x.url + "?width=80&height=80");
            statuses.ForEach(x => x.url = Url.Action("DownloadFile", new { fileUrl = x.url, mimetype = x.type }));

            var viewresult = Json(new { files = statuses });
            if (Request.Headers["Accept"] != null && !Request.Headers["Accept"].Contains("application/json"))
                viewresult.ContentType = "text/plain";

            return viewresult;
        }
        public ActionResult Delete(int id)
        {
            Photo p = repophoto.Get(id);
            repophoto.Delete(p);
            return RedirectToPrevious("Index", "Good");
        }
        [HttpPost] // should accept only post
        public ActionResult DeleteFile(int? entityId, string fileUrl)
        {
            var filePath = Server.MapPath("~" + fileUrl);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var mm = repophoto.GetAll().Where(x => x.GoodId == (int)entityId);
            foreach (var item in mm)
            {
                if (item.PathPhoto == fileUrl.Substring(fileUrl.LastIndexOf('/') + 1))
                {
                    Photo p = repophoto.Get(item.PhotoId);
                    repophoto.Delete(p);
                }
            }




            var viewresult = Json(new { error = String.Empty });
            //for IE8 which does not accept application/json
            if (Request.Headers["Accept"] != null && !Request.Headers["Accept"].Contains("application/json"))
                viewresult.ContentType = "text/plain";

            return viewresult; // trigger success
        }


        public ActionResult DownloadFile(string fileUrl, string mimetype)
        {
            var filePath = Server.MapPath("~" + fileUrl);

            if (System.IO.File.Exists(filePath))
                return File(filePath, mimetype);
            else
            {
                return new HttpNotFoundResult("File not found");
            }
        }

        public ActionResult RedirectToPrevious(String defaultAction, String defaultController)
        {
            if (Session == null || Session["PrevUrl"] == null)
            {
                return RedirectToAction(defaultAction, defaultController);
            }

            String url = ((Uri)Session["PrevUrl"]).PathAndQuery;

            if (Request.Url != null && Request.Url.PathAndQuery != url)
            {
                return Redirect(url);
            }

            return RedirectToAction(defaultAction, defaultController);
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            if (httpContext.Request.RequestType == "GET"
                && !httpContext.Request.IsAjaxRequest()
                && filterContext.IsChildAction == false)    // do no overwrite if we do child action.
            {
                // stop overwriting previous page if we just reload the current page.
                if (Session["CurUrl"] != null
                    && ((Uri)Session["CurUrl"]).Equals(httpContext.Request.Url))
                    return;

                Session["PrevUrl"] = Session["CurUrl"] ?? httpContext.Request.Url;
                Session["CurUrl"] = httpContext.Request.Url;
            }
        }
    }
}