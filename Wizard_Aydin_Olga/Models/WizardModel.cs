using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wizard_Aydin_Olga.Models;

namespace Wizard_Aydin_Olga.Models
{
    public class WizardModel : Controller
    {
        public int SpielerAnzahl { get; set; }
        public int AnzahlStiche { get; set; }
        public int Runde { get; set; }
        public bool SticheRichtigAngesagt { get; set; }
        public string Trumpf { get; set; }

        public class Karten
        {
            public string KartenFarbe { get; set; }
            public int KartenWert { get; set; }
            public bool IstWizard { get; set; }
            public bool IstNarr { get; set; }
            public string BildPfad { get; set; }
        }

        public class Spieler
        {
            public int Punkte { get; set; }
            public string SpielerName { get; set; }
            public List<Karten> KartenListe { get; set; }
        }

        public List<Spieler> SpielerListe { get; set; }
    }
}