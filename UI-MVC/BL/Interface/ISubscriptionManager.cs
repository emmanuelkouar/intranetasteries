using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVC.Domain;

namespace UI_MVC.BL.Interface
{
    interface ISubscriptionManager
    {
        void addSubscription(DateTime datePayed, DateTime expirationDate, int consecutiveYears, SubscriptionTypes subscriptionType, MemberStates memberState, MembershipTypes membershipType, Member memberId);
        IEnumerable<Subscription> getSubscriptionsForMember(Member member);
        IEnumerable<Subscription> getSubscriptionsForYear(int? year);
        void removeSubscription(int id);
    }
}
