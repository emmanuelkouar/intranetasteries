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
    public class ECGController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        IMedicalVisitManager mgrMed = new MedicalVisitManager();
        // GET: ECG
        public ActionResult Index()
        {
            return View();
        }

        // GET: ECG/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ECG/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ECG/Create
        [HttpPost]
        public ActionResult Create(int id,ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                Member member = mgrMbr.GetMember(id);

                //Calculate the age of the member and his expiration date for his ECG
                var age = m.Ecg.Date.Year - member.Birthdate.Year;
                if (m.Ecg.Date < member.Birthdate.AddYears(age)) age--;

                DateTime expDate = new DateTime();

                if(age <= 30)
                {
                    expDate = new DateTime(member.Birthdate.Year + 35, member.Birthdate.Month, member.Birthdate.Day);
                }
                else if (age >= 31 && age <= 40)
                {
                    expDate = new DateTime(m.Ecg.Date.Year + 5, m.Ecg.Date.Month, m.Ecg.Date.Day);
                }
                else if (40 <= age && age <= 43)
                {
                    if( age == 43)
                    {
                        expDate = new DateTime(m.Ecg.Date.Year+2, m.Ecg.Date.Month, m.Ecg.Date.Day);
                    }
                    else if (age == 42)
                    {
                        expDate = new DateTime(member.Birthdate.Year + age+3, member.Birthdate.Month, member.Birthdate.Day);
                    }
                    else if (age == 41)
                    {
                        expDate = new DateTime(member.Birthdate.Year + age+4, member.Birthdate.Month, member.Birthdate.Day);
                    }
                    else if (age == 40)
                    {
                        expDate = new DateTime(member.Birthdate.Year + age+5, member.Birthdate.Month, member.Birthdate.Day);
                    }
                } else if(age == 44)
                {
                    expDate = new DateTime(m.Ecg.Date.Year + 2, m.Ecg.Date.Month, m.Ecg.Date.Day);
                } else if( 45 <= age && age < 55)
                {
                    expDate = new DateTime(m.Ecg.Date.Year + 2, m.Ecg.Date.Month, m.Ecg.Date.Day);
                }
                else if(age >= 55)
                {
                    expDate = new DateTime(m.Ecg.Date.Year + 1, m.Ecg.Date.Month, m.Ecg.Date.Day);
                }

                ECG ecg = new ECG
                {
                    Date = m.Ecg.Date,
                    Notes = m.Ecg.Notes,
                    ExpirationDate = expDate
                };
                member.Ecgs = new List<ECG>();
                member.Ecgs.Add(ecg);
                mgrMbr.ChangeMember(member);



                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: ECG/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ECG/Edit/5
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

        // GET: ECG/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ECG/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                mgrMed.removeECG(id);
                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
