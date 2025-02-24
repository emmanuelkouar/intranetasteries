using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UI_MVC.DAL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.DAL
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        public void CreateSubscription(Subscription subscription)
        {
            ctx.Subscriptions.Add(subscription);
            ctx.SaveChanges();
        }

        public void DeleteSubscription(int id)
        {
            ctx.Subscriptions.RemoveRange(ctx.Subscriptions.Where(x => x.SubscriptionId == id));
            ctx.SaveChanges();
        }

        public IEnumerable<Subscription> ReadSubscriptionForMember(Member member)
        {
            IEnumerable<Subscription> subscriptions = ctx.Subscriptions.Where(s => s.MemberId.MemberId == member.MemberId).ToList();
            return subscriptions;
        }

        public IEnumerable<Subscription> ReadSubscriptionForYear(int? year)
        {
            IEnumerable<Subscription> subscriptions = ctx.Subscriptions.Include(s => s.MemberId.MemberFunctions).Include(s => s.MemberId).Where(s => s.ExpirationDate.Year == (int)year || (s.DatePayed.Month >= 9 && s.DatePayed.Year == (int)year)).ToList();
            return subscriptions;
        }
    }
}