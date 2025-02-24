using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVC.Domain;

namespace UI_MVC.DAL.Interface
{
    interface ILicenseRepository
    {
        void DeleteDiveLicense(int LicenseId);
        void DeleteSpecialisationLicense(int LicenseId);
        void DeleteApneaLicense(int LicenseId);
        IEnumerable<Member> ReadMembersLicenses();
    }
}
