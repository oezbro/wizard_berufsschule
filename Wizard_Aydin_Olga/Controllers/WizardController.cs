using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wizard_Aydin_Olga.Models;

namespace Wizard_Aydin_Olga.Controllers
{
    public class WizardController : Controller
    {
        public static int kartenImDeck;
        public static int anzahlSpieler;
        public static int aktuelleRunde;


        public ActionResult StartView()
        {
            return View();
        }

        public ActionResult KartenAusteilen(int anzahlSpieler, int kartenImDeck, int aktuelleRunde)
        {
            kartenImDeck = 60;
            aktuelleRunde++;

            var rand = new Random();
            List<int> listNumbers = new List<int>();
            int number;
            for (int i = 0; i < aktuelleRunde; i++)
            {
                do
                {
                    number = rand.Next(1, kartenImDeck);
                } while (listNumbers.Contains(number));
                listNumbers.Add(number);
            }

            return View();
        }

        public ActionResult AuswahlStich()
        {
            int anzahlStiche = aktuelleRunde++;


            return View();
        }

        public ActionResult PunkteAuswertung()
        {
            aktuelleRunde++;


            return View();
        }
    }
}