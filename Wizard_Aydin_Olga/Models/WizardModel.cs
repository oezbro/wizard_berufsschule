using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wizard_Aydin_Olga.Models
{
    public class WizardModel : Controller
    {
        public int SpielerAnzahl { get; set; }
        public int Punkte { get; set; }
        public int KartenWert { get; set; }
        public string SpielerName1 { get; set; }
        public string SpielerName2 { get; set; }
    }
}