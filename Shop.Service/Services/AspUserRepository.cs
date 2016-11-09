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
    public class AspUserRepository : GenericRepository<AspNetUser>
    {
        public AspUserRepository(DbContext context) : base(context) { }
    }
}
