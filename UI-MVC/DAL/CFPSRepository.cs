using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.DAL.Interface;
using UI_MVC.Models;

namespace UI_MVC.DAL
{
    public class CFPSRepository : ICFPSRepository
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        public void DeleteCFPS(int cfpsId)
        {
            ctx.CFPSs.RemoveRange(ctx.CFPSs.Where(x => x.CFPSId == cfpsId));
            ctx.SaveChanges();
        }
    }
}