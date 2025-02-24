using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVC.Domain;

namespace UI_MVC.DAL.Interface
{
    interface ISubscriptionRepository
    {
        void CreateSubscription(Subscription subscription);
        IEnumerable<Subscription> ReadSubscriptionForMember(Member member);
        void DeleteSubscription(int id);
        IEnumerable<Subscription> ReadSubscriptionForYear(int? year);
    }
}
