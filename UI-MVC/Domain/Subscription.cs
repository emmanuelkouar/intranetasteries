using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_MVC.Domain
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        [DisplayName("Date de paiement")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePayed { get; set; }
        [DisplayName("Date d'échéance")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
        public int ConsecutiveYears { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct subscription type.")]
        public SubscriptionTypes SubscriptionType { get; set; } //SubscriptionFromSeptembre / SubscriptionFromJanuary
        [DisplayName("Statut")]
        public MemberStates MemberState { get; set; } //Effectif (consecutive years > 3) / adhérent
        [DisplayName("Type de membre")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct subscription type.")]
        public MembershipTypes MembershipType { get; set; } //Première et seconde appartenance
        public Member MemberId { get; set; }

    }
}