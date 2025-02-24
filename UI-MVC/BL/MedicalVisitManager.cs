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
    public class MedicalVisitManager : IMedicalVisitManager
    {
        private IMedicalVisitRepository repo = new MedicalVisitRepository();

        public void addMedicalVisit(DateTime date, string notes, DateTime expirationDate, bool authorizeApnea, Member memberId)
        {
            MedicalVisit medicalVisit = new MedicalVisit()
            {
                Date = date,
                Notes = notes,
                ExpirationDate = expirationDate,
                AuthorizeApnea = authorizeApnea,
                MemberId = memberId
            };
            repo.CreateMedicalVisit(medicalVisit);
        }

        public bool checkMedicalExist(int memberId)
        {
            return repo.checkMedicalExists(memberId);
        }

        public MedicalVisit getMedicalVisit(int memberId)
        {
            return repo.ReadMedicalVisit(memberId);
        }

        public IEnumerable<MedicalVisit> getMedicalVisits()
        {
            return repo.ReadMedicalVisits();
        }

        public void removeECG(int ecgId)
        {
            repo.DeleteECG(ecgId);
        }

        public void removeMedicalVisit(int medicalVisitId)
        {
            repo.DeleteMedicalVisit(medicalVisitId);
        }
    }
}