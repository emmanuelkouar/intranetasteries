using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.DAL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.DAL
{
    public class LicenseRepository : ILicenseRepository
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        public void DeleteApneaLicense(int LicenseId)
        {
            ctx.ApneaLicenses.RemoveRange(ctx.ApneaLicenses.Where(x => x.ApneaLicenseId == LicenseId));
            ctx.SaveChanges();
        }

        public void DeleteDiveLicense(int LicenseId)
        {
            ctx.DiveLicenses.RemoveRange(ctx.DiveLicenses.Where(x => x.DiveLicenseId == LicenseId));
            ctx.SaveChanges();
        }

        public void DeleteSpecialisationLicense(int LicenseId)
        {
            ctx.SpecialisationLicenses.RemoveRange(ctx.SpecialisationLicenses.Where(x => x.SpecialisationLicenseId == LicenseId));
            ctx.SaveChanges();
        }

        public IEnumerable<Member> ReadMembersLicenses()
        {
            IEnumerable<Member> membersLicenses = ctx.Members.ToList();

            foreach (var item in membersLicenses)
            {
                item.Subscriptions = ctx.Subscriptions.Where(s => s.MemberId.MemberId == item.MemberId).ToList();
                item.ApneaLicenses = ctx.ApneaLicenses.Where(a => a.MemberId.MemberId == item.MemberId).ToList();
                item.SpecialisationLicenses = ctx.SpecialisationLicenses.Where(s => s.MemberId.MemberId == item.MemberId).ToList();
                item.DiveLicenses = ctx.DiveLicenses.Where(d => d.MemberId.MemberId == item.MemberId).ToList();
            }

            return membersLicenses;
        }
    }
}