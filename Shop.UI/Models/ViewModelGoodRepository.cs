using Shop.DataLayer.DbLayer;
using Shop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.UI.Models
{
    public class ViewModelGoodRepository
    {
        IGenericRepository<Good> repogood;
        IGenericRepository<Photo> repophoto;

        public ViewModelGoodRepository(IGenericRepository<Good> repogood, IGenericRepository<Photo> repophoto)
        {
            this.repogood = repogood;
            this.repophoto = repophoto;
        }
        //public ViewModelGood Get(int id)
        //{
           
        //    return from g in repogood.Get
        //           join p in repophoto.GetAll().AsQueryable() on g.GoodId equals p.GoodId into grp
        //           from gr in grp.DefaultIfEmpty()

        //           select new ViewModelGood
        //           {
        //               GoodId = g.GoodId,
        //               GoodName = g.GoodName,
        //               ManufacturerName = g.Manufacturer.ManufacturerName,
        //               PriceUsd = g.PriceUsd,
        //               Rest = g.Rest,
        //               PhotoId = gr.PhotoId,
        //               PathPhoto = gr.PathPhoto
        //           };
        //}
        public IEnumerable<ViewModelGood> GetAll()
        {
            return from g in repogood.GetAll().AsQueryable()
                   join p in repophoto.GetAll().AsQueryable() on g.GoodId equals p.GoodId into grp
                   from gr in grp.DefaultIfEmpty()

                   select new ViewModelGood
                   {
                       GoodId = g.GoodId,
                       GoodName = g.GoodName,
                       ManufacturerName = g.Manufacturer.ManufacturerName,
                       PriceUsd = g.PriceUsd,
                       Rest = g.Rest,
                       PhotoId = gr.PhotoId,
                       PathPhoto = gr.PathPhoto,
                      IsMain=gr.IsMain
                   };

        }
    }
}