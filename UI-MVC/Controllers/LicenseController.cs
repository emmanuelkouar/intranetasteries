using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI_MVC.BL;
using UI_MVC.BL.Interface;
using UI_MVC.Domain;

namespace UI_MVC.Controllers
{
    public class LicenseController : Controller
    {
        ILicenseManager mgrLic = new LicenseManager();
        // GET: License
        public ActionResult Index(DiveLevels? searchDiveLicense, SpecialisationLevels? searchSpecLicense, ApneaLevels? searchApneaLicense)
        {
            IEnumerable<Member> membersWithLicense = mgrLic.GetMembersLicenses();
            List<Member> membersWithLicenseThisYear = new List<Member>();
           

            if(searchDiveLicense != null)
            {
                foreach (var member in membersWithLicense)
                {
                    foreach (var sub in member.Subscriptions)
                    {
                        if (sub.ExpirationDate.Year == DateTime.Now.Year)
                        {
                            if(member.DiveLicenses.Count() != 0)
                            {
                                if (member.DiveLicenses.Last().Level.Equals(searchDiveLicense))
                                {
                                    membersWithLicenseThisYear.Add(member);
                                }
                            }
                        }
                    }
                }
            }

            if (searchSpecLicense != null)
            {
                foreach (var member in membersWithLicense)
                {
                    foreach (var sub in member.Subscriptions)
                    {
                        if (sub.ExpirationDate.Year == DateTime.Now.Year)
                        {
                            foreach (var level in member.SpecialisationLicenses)
                            {
                                if (level.Level.Equals(searchSpecLicense))
                                {
                                    membersWithLicenseThisYear.Add(member);
                                }
                            }
                        }
                    }
                }
            }

            if (searchApneaLicense != null)
            {
                foreach (var member in membersWithLicense)
                {
                    foreach (var sub in member.Subscriptions)
                    {
                        if (sub.ExpirationDate.Year == DateTime.Now.Year)
                        {
                            foreach (var level in member.ApneaLicenses)
                            {
                                if (level.Level.Equals(searchApneaLicense))
                                {
                                    membersWithLicenseThisYear.Add(member);
                                }
                            }
                        }
                    }
                }
            }
            if(searchDiveLicense == null && searchSpecLicense == null && searchApneaLicense == null)
            {
                foreach (var item in membersWithLicense)
                {
                    foreach (var sub in item.Subscriptions)
                    {
                        if (sub.ExpirationDate.Year == DateTime.Now.Year)
                        {
                            membersWithLicenseThisYear.Add(item);
                        }
                    }
                }
            }


            ViewBag.DiveLicense = new SelectList(Enum.GetValues(typeof(DiveLevels)));
            ViewBag.SpecLicense = new SelectList(Enum.GetValues(typeof(SpecialisationLevels)));
            ViewBag.ApneaLicense = new SelectList(Enum.GetValues(typeof(ApneaLevels)));

            ViewBag.DiveLicense = new SelectList(Enum.GetValues(typeof(DiveLevels)));

            return View(membersWithLicenseThisYear);
        }

        //Get: License by year get
        public ActionResult LicensesGetByYear(int? year)
        {
            IEnumerable<Member> membersWithLicense = mgrLic.GetMembersLicenses();
            List<Member> membersWithLicenseByYear = new List<Member>();

            foreach (var item in membersWithLicense)
            {
                Member member = new Member
                {
                    MemberId = item.MemberId,
                    LastName = item.LastName,
                    FirstName = item.FirstName, 
                    CMASNumber = item.CMASNumber, 
                    LIFRASNumber = item.LIFRASNumber,
                    ApneaLicenses = new List<ApneaLicense>(),
                    DiveLicenses = new List<DiveLicense>(),
                    SpecialisationLicenses = new List<SpecialisationLicense>()
            };
                foreach (var lic in item.ApneaLicenses)
                {
                    if(lic.DateGet.Year == year) {
                        
                        member.ApneaLicenses.Add(lic);
                    }
                }
                foreach (var lic in item.DiveLicenses)
                {
                    if (lic.DateGet.Year == year) {
                        
                        member.DiveLicenses.Add(lic);
                    }
                }
                foreach (var lic in item.SpecialisationLicenses)
                {
                    if (lic.DateGet.Year == year) {
                        
                        member.SpecialisationLicenses.Add(lic);
                    }
                }
                if(member.ApneaLicenses.Count() != 0 || member.DiveLicenses.Count() != 0 || member.SpecialisationLicenses.Count() != 0)
                {
                    membersWithLicenseByYear.Add(member);
                }
                
            }
         
                ViewBag.year = year;
            return View(membersWithLicenseByYear);
        }

        // GET: License/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: License/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: License/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: License/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: License/Edit/5
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

        // GET: License/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: License/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
