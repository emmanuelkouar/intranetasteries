using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_MVC.Domain
{
    public class SpecialisationLicense
    {
        public int SpecialisationLicenseId { get; set; }
        [DisplayName("Niveau")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct level")]
        public SpecialisationLevels Level { get; set; }
        [DisplayName("N° de brevet")]
        public int? LicenseNumber { get; set; }
        [DisplayName("Délivré le")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateGet { get; set; }
        public Member MemberId { get; set; }
    }
}