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
    public class ApneaLicenseController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        ILicenseManager mgrLic = new LicenseManager();
        // GET: ApneaLicense
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApneaLicense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApneaLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApneaLicense/Create
        [HttpPost]
        public ActionResult Create(int id, ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Member member = mgrMbr.GetMember(id);
                ApneaLicense license = new ApneaLicense
                {
                    DateGet = m.ApneaLicense.DateGet,
                    Level = m.ApneaLicense.Level,
                    LicenseNumber = m.ApneaLicense.LicenseNumber
                };
                member.ApneaLicenses = new List<ApneaLicense>();
                member.ApneaLicenses.Add(license);
                mgrMbr.ChangeMember(member);
                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: ApneaLicense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApneaLicense/Edit/5
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

        // GET: ApneaLicense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApneaLicense/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                mgrLic.RemoveApneaLicense(id);
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
