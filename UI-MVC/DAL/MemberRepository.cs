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
    public class MemberRepository : IMemberRepository
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();
        public void CreateMember(Member member)
        {
            ctx.Members.Add(member);
            ctx.SaveChanges();
        }

        public void UpdateMember(Member member)
        {
            ctx.Entry(member).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public IEnumerable<Member> ReadMembers()
        {
            IEnumerable<Member> Members = ctx.Members.ToList();
            return Members;
        }

        public Member ReadMember(int memberId)
        {
            Member member = ctx.Members.First(m => m.MemberId == memberId);
            return member;
        }

        public Member ReadFullMember(int memberId)
        {
           // Member member = ctx.Members.Include(m => m.Ecgs).Include(m => m.CFPSs).Include(m => m.Subscriptions).Include(m => m.MedicalVisits).Include(m => m.Ecgs).First(m => m.MemberId == memberId);

            Member member = ctx.Members.First(m => m.MemberId == memberId);

            member.Ecgs = ctx.ECGs.Where(e => e.MemberId.MemberId == memberId).ToList();
            member.CFPSs = ctx.CFPSs.Where(c => c.MemberId.MemberId == memberId).ToList();
            member.Subscriptions = ctx.Subscriptions.Where(s => s.MemberId.MemberId == memberId).ToList();
            member.MedicalVisits = ctx.MedicalVisits.Where(m => m.MemberId.MemberId == memberId).ToList();
            member.Ecgs = ctx.ECGs.Where(e => e.MemberId.MemberId == memberId).ToList();
            member.DiveLicenses = ctx.DiveLicenses.Where(d => d.MemberId.MemberId == memberId).ToList();
            member.SpecialisationLicenses = ctx.SpecialisationLicenses.Where(s => s.MemberId.MemberId == memberId).ToList();
            member.ApneaLicenses = ctx.ApneaLicenses.Where(a => a.MemberId.MemberId == memberId).ToList();
            member.ICEs = ctx.ICEs.Where(i => i.MemberId.MemberId == memberId).ToList();
            member.MemberFunctions = ctx.MemberFunctions.Where(f => f.MemberId.MemberId == memberId).ToList();

            return member;
        }

        public IEnumerable<Member> ReadFullMembers()
        {
            //IEnumerable<Member> membersWithMed = ctx.Members.Include(s => s.Subscriptions).Include(mv => mv.MedicalVisits).Include(e => e.Ecgs).Include(c => c.CFPSs).ToList();
            IEnumerable<Member> membersWithMed = ctx.Members.ToList();

            foreach (var item in membersWithMed)
            {
                item.Subscriptions = ctx.Subscriptions.Where(s => s.MemberId.MemberId == item.MemberId).ToList();
                item.MedicalVisits = ctx.MedicalVisits.Where(m => m.MemberId.MemberId == item.MemberId).ToList();
                item.Ecgs = ctx.ECGs.Where(e => e.MemberId.MemberId == item.MemberId).ToList();
                item.CFPSs = ctx.CFPSs.Where(c => c.MemberId.MemberId == item.MemberId).ToList();
            }

            foreach (var item in membersWithMed)
            {
                if (item.MedicalVisits.Count != 0)
                {
                    List<MedicalVisit> recentvisit = new List<MedicalVisit>();
                    MedicalVisit visit = ctx.MedicalVisits.OrderByDescending(x => x.ExpirationDate).First(m => m.MemberId.MemberId == item.MemberId);
                    recentvisit.Add(visit);
                    item.MedicalVisits = recentvisit;
                }

                if (item.Ecgs.Count != 0)
                {
                    List<ECG> recentecg = new List<ECG>();
                    ECG ecg = ctx.ECGs.OrderByDescending(x => x.ExpirationDate).First(m => m.MemberId.MemberId == item.MemberId);
                    recentecg.Add(ecg);
                    item.Ecgs = recentecg;
                }

                if(item.Subscriptions.Count != 0)
                {
                    List<Subscription> recentSubscription = new List<Subscription>();
                    Subscription sub = ctx.Subscriptions.OrderByDescending(x => x.ExpirationDate).First(m => m.MemberId.MemberId == item.MemberId);
                    recentSubscription.Add(sub);
                    item.Subscriptions = recentSubscription;
                }

                if(item.CFPSs.Count != 0)
                {
                    List<CFPS> recentCFPS = new List<CFPS>();
                    CFPS cfps = ctx.CFPSs.OrderByDescending(x => x.ExpirationDate).First(m => m.MemberId.MemberId == item.MemberId);
                    recentCFPS.Add(cfps);
                    item.CFPSs = recentCFPS;
                }
            }
            return membersWithMed;
        }

        public IEnumerable<Member> ReadMembersWithSubscription()
        {
            IEnumerable<Member> membersWithSubs = ctx.Members.Include(m => m.Subscriptions).ToList();

            foreach (var item in membersWithSubs)
            {
                if(item.Subscriptions.Count != 0)
                {
                    List<Subscription> lastSubs = new List<Subscription>();
                    Subscription subs = ctx.Subscriptions.OrderByDescending(x => x.ExpirationDate).First(m => m.MemberId.MemberId == item.MemberId);
                    lastSubs.Add(subs);
                    item.Subscriptions = lastSubs;
                }

            }
            return membersWithSubs;
        }

        public IEnumerable<Member> ReadMembersWithECG()
        {
            IEnumerable<Member> membersWithEcg = ctx.Members.Include(m => m.Ecgs).ToList();
            return membersWithEcg;
        }

        public List<Member> ReadExportToExcel(int year)
        {
            List<Member> members = new List<Member>();

            List<Subscription> subscriptions = ctx.Subscriptions.Include(s => s.MemberId).Where(s => s.ExpirationDate.Year == (int)year || (s.DatePayed.Month >= 9 && s.DatePayed.Year == (int)year)).ToList();

            foreach (var subs in subscriptions)
            {
                Member member = ReadFullMember(subs.MemberId.MemberId);

                //member.ApneaLicenses = ctx.ApneaLicenses.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                //member.CFPSs = ctx.CFPSs.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                //member.MemberFunctions = ctx.MemberFunctions.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                //member.SpecialisationLicenses = ctx.SpecialisationLicenses.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                //member.Subscriptions = ctx.Subscriptions.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                //member.MedicalVisits = ctx.MedicalVisits.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                member.ICEs = ctx.ICEs.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                //member.Ecgs = ctx.ECGs.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                //member.DiveLicenses = ctx.DiveLicenses.Where(m => m.MemberId.MemberId == subs.MemberId.MemberId).ToList();
                


                members.Add(member);
            }
            
            return members;
        }


        public IEnumerable<Member> ReadMembersWithCFPS()
        {
            IEnumerable<Member> membersWithCFPS = ctx.Members.Include(m => m.Subscriptions).Include(m => m.CFPSs).ToList();
            foreach (var item in membersWithCFPS)
            {
                if (item.CFPSs.Count != 0)
                {
                    List<CFPS> lastCFPS = new List<CFPS>();
                    CFPS cfps = ctx.CFPSs.OrderByDescending(x => x.ExpirationDate).First(m => m.MemberId.MemberId == item.MemberId);
                    lastCFPS.Add(cfps);
                    item.CFPSs = lastCFPS;
                }

            }
            return membersWithCFPS;
        }

        public void DeleteFunction(int FunctionId)
        {
            ctx.MemberFunctions.RemoveRange(ctx.MemberFunctions.Where(f => f.MemberFunctionId == FunctionId));
            ctx.SaveChanges();
        }
    }

}

