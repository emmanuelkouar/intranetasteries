using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UI_MVC.Domain
{
    public class ICE
    {
        public int ICEId { get; set; }
        [DisplayName("Prénom")]
        public string FirstName { get; set; }
        [DisplayName("Nom")]
        public string LastName { get; set; }
        public string Description { get; set; }
        [DisplayName("Téléphone")]
        public string Phone { get; set; }
        public Member MemberId { get; set; }
    }
}