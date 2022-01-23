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

        [HttpPost]
        public ActionResult StartView(WizardModel wizardModel)
        {
            aktuelleRunde = 1;
            anzahlSpieler = 2;

            List<string> TeilnehmerListe = new List<string>();
            TeilnehmerListe.Add(wizardModel.SpielerName1);
            TeilnehmerListe.Add(wizardModel.SpielerName2);

            return RedirectToAction("GameView", wizardModel);
        }

        public ActionResult GameView(WizardModel wizardModel)
        {
            return View();
        }

        public ActionResult KartenAusteilen(int anzahlSpieler, int kartenImDeck, int aktuelleRunde)
        {
            kartenImDeck = 60;

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

                KartenWert(number);

                kartenImDeck--;
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

        public WizardModel KartenWert(int rndNumber)
        {
            WizardModel wizardModel = new WizardModel();

            if (rndNumber <= 13)
            {
                wizardModel.KartenWert = rndNumber;
                wizardModel.KartenFarbe = "rot";
            }
            if (rndNumber > 14 && rndNumber <= 26 )
            {
                wizardModel.KartenWert = rndNumber / 2;
                wizardModel.KartenFarbe = "grün";
            }
            if (rndNumber > 27 && rndNumber <= 39)
            {
                wizardModel.KartenWert = rndNumber / 3;
                wizardModel.KartenFarbe = "gelb";
            }
            if (rndNumber > 40 && rndNumber <= 52)
            {
                wizardModel.KartenWert = rndNumber / 4;
                wizardModel.KartenFarbe = "blau";
            }
            if (rndNumber > 53 && rndNumber <= 56)
            {
                wizardModel.KartenWert = rndNumber / 5;
                wizardModel.IstNarr = true;
            }
            if (rndNumber > 57 && rndNumber <= 60)
            {
                wizardModel.KartenWert = rndNumber / 6;
                wizardModel.IstWizard = true;
            }

            return wizardModel;
        }
    }
}