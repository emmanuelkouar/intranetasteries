using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_MVC.Domain
{
    public enum MemberFunctions: byte
    {
        Secretaire = 1, 
        Tresorier,
        ChefDEcole, 
        President, 
        ResponsableMateriel,
        MembreCE
    }
}