using Shop.DataLayer.DbLayer;
using Shop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.Services
{
    public class ConverterRepository:GenericRepository<Converter>
    {
        public ConverterRepository(DbContext context) : base(context) { }
    }
}
