using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVC.Domain;

namespace UI_MVC.BL.Interface
{
    interface IMedicalVisitManager
    {
        void addMedicalVisit(DateTime date, string notes, DateTime expirationDate, bool authorizeApnea, Member memberId);
        MedicalVisit getMedicalVisit(int memberId);
        bool checkMedicalExist(int memberId);
        void removeMedicalVisit(int medicalVisitId);
        void removeECG(int ecgId);
        IEnumerable<MedicalVisit> getMedicalVisits();
    }
}
