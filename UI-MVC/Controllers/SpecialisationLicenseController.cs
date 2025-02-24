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
    public class SpecialisationLicenseController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        ILicenseManager mgrLic = new LicenseManager();
        // GET: SpecialisationLicense
        public ActionResult Index()
        {
            return View();
        }

        // GET: SpecialisationLicense/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SpecialisationLicense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecialisationLicense/Create
        [HttpPost]
        public ActionResult Create(int id, ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Member member = mgrMbr.GetMember(id);
                SpecialisationLicense license = new SpecialisationLicense
                {
                    DateGet = m.SpecialisationLicense.DateGet,
                    Level = m.SpecialisationLicense.Level,
                    LicenseNumber = m.SpecialisationLicense.LicenseNumber
                };

                member.SpecialisationLicenses = new List<SpecialisationLicense>();

                member.SpecialisationLicenses.Add(license);
                mgrMbr.ChangeMember(member);

                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: SpecialisationLicense/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SpecialisationLicense/Edit/5
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

        // GET: SpecialisationLicense/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SpecialisationLicense/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                mgrLic.RemoveSpecialisationLicense(id);
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
