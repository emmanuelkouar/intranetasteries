using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.DAL.Interface;
using UI_MVC.Models;

namespace UI_MVC.DAL
{
    public class ICERepository : IICERepository
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        public void DeleteICE(int IceId)
        {
            ctx.ICEs.RemoveRange(ctx.ICEs.Where(x => x.ICEId == IceId));
            ctx.SaveChanges();
        }
    }
}