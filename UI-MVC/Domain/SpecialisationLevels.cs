using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_MVC.Domain
{
    public enum SpecialisationLevels: byte
    {
        PPA = 1,
        NitroxBasique, 
        NitroxConfirme,
        InstructeuNitroxBasique,
	    InstructeurNitroxConfirme, 
	    FormateurNitrox,
	    PlongeurTrimixNormoxique,
	    InstructeurTrimixNormoxique,
	    TechnicienMelangeNitroxNiv1,
	    TechnicienMelangeTrimixNiv2,
	    VetementEtanche,
	    PlongeeAdapteeEncadrant,
	    PlongeeAdapteeMoniteur,
	    PlongeeAdapteeMoniteurFormateur,
	    PlongeurSouterrainNiv1,
	    PlongeurSouterrainNiv2,
	    PlongeurSouterrainNiv3,
	    AMPlongéeSouterraine,
	    MoniteurPlongéeSouterraine,
	    PlongeeEnfantEncadrementStandard,
	    PlongeeEnfantInstructeurBasique,
	    PlongeeEnfantInstructeur,
	    PlongeeEnfantInstructeurCertificateur,
	    PlongeurPhotographe,
	    PlongeurCineaste,
        Oceanologue,
		RecyclageMoniteurMADECO 

    }

}
