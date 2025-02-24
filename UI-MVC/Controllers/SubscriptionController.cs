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
    public class SubscriptionController : Controller
    {
        IMemberManager mgrMbr = new MemberManager();
        ISubscriptionManager mgrSub = new SubscriptionManager();
        // GET: Subscription
        //public ActionResult Index(int? year)
        //{
        //    IEnumerable<Member> members = mgrMbr.GetMembersWithSubscription();
        //    if (year != null)
        //    {
        //        foreach (var item in members)
        //        {
        //            if (item.Subscriptions.Count != 0)
        //            {
        //                List<Subscription> subs = new List<Subscription>();
        //                foreach (var subItem in item.Subscriptions)
        //                {
        //                    if (subItem.ExpirationDate.Year.Equals(year))
        //                    {
        //                        subs.Add(subItem);
        //                    }
        //                }
        //                item.Subscriptions = subs;
        //            }
        //        }
        //    }
        //    return View(members);
        //}

        // GET: Subscription
        //public ActionResult Index(int? year)
        //{
        //    IEnumerable<Member> members;
        //    if (year == null)
        //    {
        //        members = mgrMbr.GetMembersWithSubscription();
        //    }
        //    else
        //    {
        //        members = mgrMbr.GetMembersWithSubscription();
        //    }

        //    return View(members);
        //}

        // GET: Subscription
        public ActionResult Index(int? year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            IEnumerable<Subscription> subs = mgrSub.getSubscriptionsForYear(year);
            subs = subs.OrderBy(s => s.MemberId.LastName);
            ViewBag.Year = year;
            return View(subs);
        }

        // GET: Subscription/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subscription/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscription/Create
        [HttpPost]
        public ActionResult Create(int id, ViewModelMember m, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Member member = mgrMbr.GetMember(id);
                IEnumerable<Subscription> subscriptions =  mgrSub.getSubscriptionsForMember(member);

               

                SubscriptionTypes subType;
                DateTime dateToCompare = new DateTime(m.Subscription.DatePayed.Year, 9, 1);
                int result = DateTime.Compare(m.Subscription.DatePayed, dateToCompare);
                DateTime expDate = new DateTime();

                if(result < 0)
                {
                     expDate = new DateTime(m.Subscription.DatePayed.Year, 12, 31);
                    subType = SubscriptionTypes.SubscriptionFromJanuary;
                } else
                {
                     expDate = new DateTime(m.Subscription.DatePayed.Year + 1, 12, 31);
                    subType = SubscriptionTypes.SubscriptionFromSeptembre;
                }

                //int numberOfSubsriptions = subscriptions.Count() + 1;
                int numberOfSubsriptions = 0;

                if(subscriptions.Count() == 0)
                {
                    if(subType == SubscriptionTypes.SubscriptionFromSeptembre)
                    {
                        numberOfSubsriptions = 2;
                    }
                    else
                    {
                        numberOfSubsriptions = 1;
                    }
                }
                else
                {
                    int lastYear = subscriptions.Last().ExpirationDate.Year;
                    int newYear = expDate.Year;

                    int resultY = newYear - lastYear;

                    if (resultY > 1)
                    {
                        numberOfSubsriptions = 1;
                    }
                    else
                    {
                        numberOfSubsriptions = subscriptions.Last().ConsecutiveYears + 1;
                    }

                }


                MemberStates state;
                if (numberOfSubsriptions >= 3)
                {
                    state = MemberStates.Effectif;
                }
                else
                {
                    state = MemberStates.Adherent;
                }

                Subscription subscription = new Subscription
                {
                    ConsecutiveYears = numberOfSubsriptions,
                    DatePayed = m.Subscription.DatePayed,
                    ExpirationDate = expDate,
                    MembershipType = m.Subscription.MembershipType,
                    MemberState = state,
                    SubscriptionType = subType
                };

                member.Subscriptions = new List<Subscription>();

                member.Subscriptions.Add(subscription);

                mgrMbr.ChangeMember(member);

                return RedirectToAction("Details", "Members", new { id = member.MemberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: Subscription/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subscription/Edit/5
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

        // GET: Subscription/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subscription/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int memberId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                mgrSub.removeSubscription(id);

                return RedirectToAction("Edit", "Members", new { id = memberId });
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
