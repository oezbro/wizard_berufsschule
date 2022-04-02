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
        public int Runde { get; set; }
        public string Trumpf { get; set; }
        public List<Karte> KartenDeck { get; set; }
        public List<Spieler> SpielerListe { get; set; }

        public class Karte
        {
            public string KartenFarbe { get; set; }
            public int KartenWert { get; set; }
            public bool IstWizard { get; set; }
            public bool IstNarr { get; set; }
            public string BildPfad { get; set; }
        }
        public class Spieler
        {
            public int PunkteProRunde { get; set; }
            public string SpielerName { get; set; }
            public List<Karte> KartenListe { get; set; }
            public int GemachteStiche { get; set; }
            public int AngesagteStiche { get; set; }
        }
    }
}