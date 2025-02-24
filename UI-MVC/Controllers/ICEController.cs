using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_MVC.BL;
using UI_MVC.BL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.Controllers
{
    public class ICEController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        IICEManager mgrICE = new ICEManager();
        // GET: ICE
        public ActionResult Index()
        {
            return View();
        }

        // GET: ICE/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ICE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ICE/Create
        [HttpPost]
        public ActionResult Create(int id, ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Member member = mgrMbr.GetMember(id);
                ICE ice = new ICE
                {
                    FirstName = m.Ice.FirstName,
                    LastName = m.Ice.LastName,
                    Description = m.Ice.Description,
                    Phone = m.Ice.Phone
                };
                member.ICEs = new List<ICE>();
                member.ICEs.Add(ice);
                mgrMbr.ChangeMember(member);
                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: ICE/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ICE/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ICE/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ICE/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                mgrICE.RemoveICE(id);
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
