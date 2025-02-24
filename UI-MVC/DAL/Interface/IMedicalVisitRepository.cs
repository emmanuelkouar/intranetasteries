using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVC.Domain;

namespace UI_MVC.DAL.Interface
{
    interface IMedicalVisitRepository
    {
        void CreateMedicalVisit(MedicalVisit medicalVisit);
        MedicalVisit ReadMedicalVisit(int memberId);
        bool checkMedicalExists(int memberId);
        void DeleteMedicalVisit(int medicalVisitId);
        void DeleteECG(int ecgId);
        IEnumerable<MedicalVisit> ReadMedicalVisits();
    }
}
