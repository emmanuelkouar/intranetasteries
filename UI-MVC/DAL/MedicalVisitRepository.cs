using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.DAL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.DAL
{
    public class MedicalVisitRepository : IMedicalVisitRepository
    {
        private ApplicationDbContext ctx = new ApplicationDbContext();

        public bool checkMedicalExists(int memberId)
        {
            bool medicalExist = ctx.MedicalVisits.Any(m => m.MemberId.MemberId == memberId);
            return medicalExist;
        }

        public void CreateMedicalVisit(MedicalVisit medicalVisit)
        {
            ctx.MedicalVisits.Add(medicalVisit);
            ctx.SaveChanges();
        }

        public void DeleteECG(int ecgId)
        {
            ctx.ECGs.RemoveRange(ctx.ECGs.Where(x => x.ECGId == ecgId));
            ctx.SaveChanges();
        }

        public void DeleteMedicalVisit(int medicalVisitId)
        {
            ctx.MedicalVisits.RemoveRange(ctx.MedicalVisits.Where(x => x.MedicalVisitId == medicalVisitId));
            ctx.SaveChanges();
        }

        public MedicalVisit ReadMedicalVisit(int memberId)
        {
            MedicalVisit medicalVisit = ctx.MedicalVisits.First(m => m.MemberId.MemberId == memberId);
            return medicalVisit;
        }

        public IEnumerable<MedicalVisit> ReadMedicalVisits()
        {
            IEnumerable<MedicalVisit> medicalVisits = ctx.MedicalVisits.Include("MemberId").Distinct().OrderByDescending(m => m.ExpirationDate).ToList();
            return medicalVisits; 
        }
    }
}