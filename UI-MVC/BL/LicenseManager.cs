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
    public class LicenseManager : ILicenseManager
    {
        private ILicenseRepository repo = new LicenseRepository();

        public IEnumerable<Member> GetMembersLicenses()
        {
            return repo.ReadMembersLicenses();
        }

        public void RemoveApneaLicense(int LicenseId)
        {
            repo.DeleteApneaLicense(LicenseId);
        }

        public void RemoveDiveLicense(int LicenseId)
        {
            repo.DeleteDiveLicense(LicenseId);
        }

        public void RemoveSpecialisationLicense(int LicenseId)
        {
            repo.DeleteSpecialisationLicense(LicenseId);
        }
    }
}