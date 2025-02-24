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
    public class DiveLicenseController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        ILicenseManager mgrLic = new LicenseManager();
        // GET: DiveLicense
        public ActionResult Index()
        {
            return View();
        }

        // GET: DiveLicense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DiveLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiveLicense/Create
        [HttpPost]
        public ActionResult Create(int id, ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Member member = mgrMbr.GetMember(id);
                DiveLicense license = new DiveLicense
                {
                    DateGet = m.DiveLicense.DateGet,
                    Level = m.DiveLicense.Level,
                    LicenseNumber = m.DiveLicense.LicenseNumber
                };

                member.DiveLicenses = new List<DiveLicense>();

                member.DiveLicenses.Add(license);
                mgrMbr.ChangeMember(member);

                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: DiveLicense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DiveLicense/Edit/5
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

        // GET: DiveLicense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DiveLicense/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                mgrLic.RemoveDiveLicense(id);
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
