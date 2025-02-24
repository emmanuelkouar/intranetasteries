using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.BL.Interface;
using UI_MVC.DAL;
using UI_MVC.DAL.Interface;
using UI_MVC.Domain;

namespace UI_MVC.BL
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private ISubscriptionRepository repo = new SubscriptionRepository();

        public void addSubscription(DateTime datePayed, DateTime expirationDate, int consecutiveYears, SubscriptionTypes subscriptionType, MemberStates memberState, MembershipTypes membershipType, Member memberId)
        {
            Subscription subscription = new Subscription()
            {
                ConsecutiveYears = consecutiveYears,
                DatePayed = datePayed,
                ExpirationDate = expirationDate,
                MemberId = memberId,
                MembershipType = membershipType,
                MemberState = memberState,
                SubscriptionType = subscriptionType
            };

            repo.CreateSubscription(subscription);
        }

        public IEnumerable<Subscription> getSubscriptionsForMember(Member member)
        {
            return repo.ReadSubscriptionForMember(member);
        }

        public void removeSubscription(int id)
        {
            repo.DeleteSubscription(id);
        }

        public IEnumerable<Subscription> getSubscriptionsForYear(int? year)
        {
            return repo.ReadSubscriptionForYear(year);
        }
    }
}