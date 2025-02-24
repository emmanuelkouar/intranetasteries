using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVC.Domain;

namespace UI_MVC.BL.Interface
{
    interface ILicenseManager
    {
        void RemoveDiveLicense(int LicenseId);
        void RemoveSpecialisationLicense(int LicenseId);
        void RemoveApneaLicense(int LicenseId);
        IEnumerable<Member> GetMembersLicenses();
    }
}
