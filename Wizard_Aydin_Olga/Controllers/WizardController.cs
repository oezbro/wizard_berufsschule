using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wizard_Aydin_Olga.Controllers
{
    public class WizardController : Controller
    {
        public static int kartenImDeck;
        public static int anzahlSpieler;
        public static int aktuelleRunde;


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KartenVerteilen()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}