using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.Domain;

namespace UI_MVC.Models
{
    public class ViewModelMember
    {
        public Member Member { get; set; }
        public  MedicalVisit MedicalVisit { get; set; }
        public ECG Ecg { get; set; }
        public Subscription Subscription { get; set; }
        public CFPS CFPS { get; set; }
        public DiveLicense DiveLicense { get; set; }
        public SpecialisationLicense SpecialisationLicense { get; set; }
        public ApneaLicense ApneaLicense { get; set; }
        public ICE Ice { get; set; }
        public MemberFunction MemberFunction { get; set; }
    }
}