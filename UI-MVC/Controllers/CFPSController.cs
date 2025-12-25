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
    public class CFPSController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        ICFPSManager mgrCFPS = new CFPSManager();
        // GET: CFPS
        public ActionResult Index()
        {
            IEnumerable<Member> membersWithCFPS = mgrMbr.GetMembersWithCFPS();
            //membersWithCFPS = membersWithCFPS.Where(m => m.Subscriptions.Last().ExpirationDate.Year == DateTime.Now.Year || (m.Subscriptions.Last().DatePayed.Month >= 9 && m.Subscriptions.Last().DatePayed.Year == DateTime.Now.Year));
            membersWithCFPS = membersWithCFPS.Where(m => m.Subscriptions.Last().ExpirationDate.Year == DateTime.Now.Year);
            return View(membersWithCFPS);
        }

        // GET: CFPS/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CFPS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CFPS/Create
        [HttpPost]
        public ActionResult Create(int id, ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                DateTime expDate = new DateTime(m.CFPS.DateGet.Year + 5, m.CFPS.DateGet.Month, m.CFPS.DateGet.Day);

                Member member = mgrMbr.GetMember(id);
                CFPS cfps = new CFPS
                {
                    DateGet = m.CFPS.DateGet,
                    ExpirationDate = expDate,
                    IsRecycling = m.CFPS.IsRecycling
                };

                member.CFPSs = new List<CFPS>();

                member.CFPSs.Add(cfps);

                mgrMbr.ChangeMember(member);

                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return RedirectToAction("Index", "Members");
            }
        }

        // GET: CFPS/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CFPS/Edit/5
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

        // GET: CFPS/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CFPS/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                mgrCFPS.removeCFPS(id);
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml"); 
            }
        }
    }
}


