using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UI_MVC.BL;
using UI_MVC.BL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        IMedicalVisitManager mgrMed = new MedicalVisitManager();
        ISubscriptionManager mgrSub = new SubscriptionManager();
        // GET: Members
        public ActionResult Index(string searchString, string searchCity)
        {
            IEnumerable<Member> members = mgrMbr.GetMembers();

            if (!string.IsNullOrEmpty(searchString))
            {
                members = members.Where(m => m.LastName.Contains(searchString) || m.FirstName.Contains(searchString));
            }

            if(!string.IsNullOrEmpty(searchCity))
            {
                members = members.Where(m => m.City.Contains(searchCity));
            }  
            members = members.OrderBy(m => m.LastName);
            return View(members);
       
        }

        // GET: MembersWithMed
        public ActionResult MembersMedicalInfo()
        {
            IEnumerable<Member> members = mgrMbr.GetFullMembers();
            List<Member> membersByYear = new List<Member>();

            foreach (var member in members)
            {
                foreach (var sub in member.Subscriptions)
                {
                    if (sub.ExpirationDate.Year == DateTime.Now.Year || (sub.DatePayed.Month >= 9 && sub.DatePayed.Year == DateTime.Now.Year))
                    {
                        membersByYear.Add(member);
                    }
                }
            }

            //ViewBag.VisitSortParm = string.IsNullOrEmpty(sortOrder) ? "visit_desc" : "";

            //switch (sortOrder)
            //{
            //    case "visit_desc":
            //            membersByYear = membersByYear.OrderByDescending(x=>x.MedicalVisits.Select( z=>z.ExpirationDate)).ToList();

            //        break;
            //    default:
            //        membersByYear = membersByYear.OrderBy(m => m.LastName).ToList();
            //        break;
            //}

            return View(membersByYear);
        }
        // GET: MembersByCity
        public ActionResult MembersByCity(int? year, string searchCity)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            IEnumerable<Subscription> subs = mgrSub.getSubscriptionsForYear(year);

            if (!string.IsNullOrEmpty(searchCity))
            {
                subs = subs.Where(m => m.MemberId.City.Contains(searchCity));
            }

            return View(subs);
        }

        // GET: Members/Details/5
        public ActionResult Details(int id)
        {
            Member member = mgrMbr.GetFullMember(id);
            return View(member);
        }

        // GET: Members/Create
        [Authorize(Users = "emmanuel.kouar@outlook.com")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        public ActionResult Create(Member member, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                mgrMbr.addMember(member.LastName, member.FirstName, member.Country, member.Address, member.HouseNumber, member.MailBox, member.Zip, member.City, member.Phone, member.Email, member.Birthdate, member.Gender, member.CMASNumber, member.LIFRASNumber, member.AllergiesOrMedicaments);
                return RedirectToAction("Index" ,"Home");
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: Members/Edit/5
        [Authorize(Users = "emmanuel.kouar@outlook.com")]
        public ActionResult Edit(int id)
        {
            ViewModelMember modelWithoudMed = new ViewModelMember
            {
                Member = mgrMbr.GetFullMember(id)
            };
            return View(modelWithoudMed);
        }

        // POST: Members/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection, ViewModelMember m)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        Member member = mgrMbr.GetMember(id);
        //        member.LastName = m.Member.LastName;
        //        member.FirstName = m.Member.FirstName;
        //        member.Address = m.Member.Address;
        //        member.Birthdate = m.Member.Birthdate;
        //        member.City = m.Member.City;
        //        member.Country = m.Member.Country;
        //        member.Email = m.Member.Email;
        //        member.Gender = m.Member.Gender;
        //        member.HouseNumber = m.Member.HouseNumber;
        //        member.MailBox = m.Member.MailBox;
        //        member.MemberId = m.Member.MemberId;
        //        member.Phone = m.Member.Phone;
        //        member.Zip = m.Member.Zip;



        //        MedicalVisit medicalVisit = new MedicalVisit() { };

        //        medicalVisit.AuthorizeApnea = m.MedicalVisit.AuthorizeApnea;
        //        medicalVisit.Notes = m.MedicalVisit.Notes;
        //        medicalVisit.Date = m.MedicalVisit.Date;
        //        medicalVisit.ExpirationDate = m.MedicalVisit.ExpirationDate;

        //        member.MedicalVisits = new List<MedicalVisit>();

        //        member.MedicalVisits.Add(medicalVisit);

        //        mgrMbr.ChangeMember(member);

        //        return RedirectToAction("Details", new { id = member.MemberId });

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Members/Delete/5


            [HttpPost]
            public ActionResult SaveMember(FormCollection collection, ViewModelMember m)
        {
            try
            {
                // TODO: Add update logic here
                Member member = mgrMbr.GetMember(m.Member.MemberId);
                member.LastName = m.Member.LastName;
                member.FirstName = m.Member.FirstName;
                member.Address = m.Member.Address;
                member.Birthdate = m.Member.Birthdate;
                member.City = m.Member.City;
                member.Country = m.Member.Country;
                member.Email = m.Member.Email;
                member.Gender = m.Member.Gender;
                member.HouseNumber = m.Member.HouseNumber;
                member.MailBox = m.Member.MailBox;
                member.MemberId = m.Member.MemberId;
                member.Phone = m.Member.Phone;
                member.Zip = m.Member.Zip;
                member.AllergiesOrMedicaments = m.Member.AllergiesOrMedicaments;
                member.CMASNumber = m.Member.CMASNumber;
                member.LIFRASNumber = m.Member.LIFRASNumber;

                mgrMbr.ChangeMember(member);

                return RedirectToAction("Details", new { id = member.MemberId });

            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Members/Delete/5
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
