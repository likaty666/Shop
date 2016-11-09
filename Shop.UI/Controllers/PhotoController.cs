using MvcFileUploader;
using MvcFileUploader.Models;
using Shop.DataLayer.DbLayer;
using Shop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.UI.Controllers
{
    public class PhotoController : Controller
    {
        IGenericRepository<Good> repogood;
        IGenericRepository<Photo> repophoto;
        // GET: Photo
        public PhotoController(IGenericRepository<Good> repogood, IGenericRepository<Photo> repophoto)
        {
            this.repogood = repogood;
            this.repophoto = repophoto;
        }
        public ActionResult AddPhoto(int id)
        {
           
            var model = repogood.Get(id);
            return View(model);
        }

       
    }
}