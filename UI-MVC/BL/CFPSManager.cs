using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.BL.Interface;
using UI_MVC.DAL;
using UI_MVC.DAL.Interface;

namespace UI_MVC.BL
{
    public class CFPSManager : ICFPSManager
    {
        private ICFPSRepository repo = new CFPSRepository();
        public void removeCFPS(int cfpsId)
        {
            repo.DeleteCFPS(cfpsId);
        }
    }
}