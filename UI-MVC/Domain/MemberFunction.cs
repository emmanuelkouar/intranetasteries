using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI_MVC.Domain
{
    public class MemberFunction
    {
        public int MemberFunctionId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct function")]
        [DisplayName("Fonction")]
        public MemberFunctions Function { get; set; }
        public Member MemberId { get; set; }
    }
}