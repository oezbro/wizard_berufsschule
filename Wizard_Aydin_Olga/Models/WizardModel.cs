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
        public int Punkte { get; set; }
        public int KartenWert { get; set; }
        public string SpielerName1 { get; set; }
        public string SpielerName2 { get; set; }
        public string KartenFarbe { get; set; }
        public bool IstWizard { get; set; }
        public bool IstNarr { get; set; }
        public int AnzahlStiche { get; set; }
        public int Runde { get; set; }
        public List<int> KartenAufDerHand { get; set; }
        public bool SticheRichtigAngesagt { get; set; }
        public string Trumpf { get; set; }
    }
}