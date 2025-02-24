using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.BL.Interface;
using UI_MVC.DAL;
using UI_MVC.DAL.Interface;

namespace UI_MVC.BL
{
    public class ICEManager : IICEManager
    {
        private IICERepository repo = new ICERepository();

        public void RemoveICE(int IceId)
        {
            repo.DeleteICE(IceId);
        }
    }
}