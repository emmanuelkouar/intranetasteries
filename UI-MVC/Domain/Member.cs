using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_MVC.Domain
{
    public class Member
    {
        public int MemberId { get; set; }
       [DisplayName("Nom")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Prénom")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Pays")]
        [Required]
        public string Country { get; set; }
        [DisplayName("Rue")]
        [Required]
        public string Address { get; set; }
        [DisplayName("n°")]
        [Required]
        public string HouseNumber { get; set; }
        [DisplayName("Boite")]
        public int? MailBox { get; set; }
        [DisplayName("Code postal")]
        [Required]
        public int Zip { get; set; }
        [DisplayName("Ville")]
        [Required]
        public string City { get; set; }
        [DisplayName("Téléphone")]
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [DisplayName("Date de naissance")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        [DisplayName("Genre")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct gender")]
        public Gender Gender { get; set; }
        [DisplayName("CMAS")]
        public int? CMASNumber { get; set; }
        [DisplayName("LIFRAS")]
        public int? LIFRASNumber { get; set; }
        [DisplayName("Allergies")]
        public string AllergiesOrMedicaments { get; set; }
        public ICollection <MedicalVisit> MedicalVisits { get; set; }
        public ICollection <ECG> Ecgs { get; set; }
        public ICollection <Subscription> Subscriptions { get; set; }
        public ICollection <CFPS> CFPSs { get; set; }
        [DisplayName("Brevets de plongée")]
        public ICollection <DiveLicense> DiveLicenses { get; set; }
        [DisplayName("Brevets de spécialisation")]
        public ICollection <SpecialisationLicense> SpecialisationLicenses { get; set; }
        [DisplayName("Brevets de d'apnée")]
        public ICollection <ApneaLicense> ApneaLicenses { get; set; }
        public ICollection <ICE> ICEs { get; set; }
        public ICollection <MemberFunction> MemberFunctions { get; set; }

    }
}