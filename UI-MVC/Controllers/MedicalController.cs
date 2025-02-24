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
    public class MedicalController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        IMedicalVisitManager mgrMed = new MedicalVisitManager();

        // GET: Medical
        public ActionResult Index()
        {
            IEnumerable<MedicalVisit> medicalVisits = mgrMed.getMedicalVisits();
            return View(medicalVisits);
        }

        // GET: Medical/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Medical/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medical/Create
        [HttpPost]
        public ActionResult Create(int id,ViewModelMember m, FormCollection collection)
        {
            DateTime expDate = new DateTime();
            DateTime dateToCompare = new DateTime(m.MedicalVisit.Date.Year, 9, 1);
            int result = DateTime.Compare(m.MedicalVisit.Date, dateToCompare);

            if (result < 0 )
            {
                expDate = new DateTime(m.MedicalVisit.Date.Year + 1, 1, 31);
            }
            else if (result >= 0){
                expDate = new DateTime(m.MedicalVisit.Date.Year + 2, 1, 31);
            }
            try
            {
                Member member = mgrMbr.GetMember(id);
                MedicalVisit medVisit = new MedicalVisit
                {
                    Date = m.MedicalVisit.Date,
                    Notes = m.MedicalVisit.Notes,
                    AuthorizeApnea = m.MedicalVisit.AuthorizeApnea,
                    ExpirationDate = expDate
                };
                member.MedicalVisits = new List<MedicalVisit>();

                member.MedicalVisits.Add(medVisit);
                mgrMbr.ChangeMember(member);


                //medVMgr.addMedicalVisit(visit.Date, visit.Notes, visit.ExpirationDate, visit.AuthorizeApnea, member);
                // TODO: Add insert logic here

                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return RedirectToAction("Index","Members");
            }
        }

        // GET: Medical/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Medical/Edit/5
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
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: Medical/Delete/5
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Members");
        }

        // POST: Medical/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                mgrMed.removeMedicalVisit(id);
               // return RedirectToAction("Index", "Members");
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
