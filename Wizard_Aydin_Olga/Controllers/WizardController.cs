using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wizard_Aydin_Olga.Models;
using Wizard_Aydin_Olga.Controllers;

namespace Wizard_Aydin_Olga.Controllers
{
    public class WizardController : Controller
    {
        public static int kartenImDeck;
        public static int anzahlSpieler;
        public static int aktuelleRunde;

        public static WizardModel wizardModel;

        public List<int> KartenAufDerHand =new List<int>();

        public ActionResult StartView()
        {
            wizardModel = new WizardModel();

            return View();
        }

        [HttpPost]
        public ActionResult StartView(WizardModel model)
        {
            aktuelleRunde = 1;
            anzahlSpieler = 2;

            model.Runde = aktuelleRunde;

            int kartenImDeck = 60;

            for (int i = 0; i < anzahlSpieler; i++)
            {
                KartenAusteilen(anzahlSpieler, kartenImDeck, aktuelleRunde);
            }

            model.Trumpf = TrumpfBestimmen();

            wizardModel = model;

            for (int i = 0; i < anzahlSpieler; i++)
            {
                if (wizardModel.wizardModels == null)
                {
                    wizardModel.wizardModels.Add(model);
                }
            }

            if (wizardModel.wizardModels[0].SpielerName == null)
            {
                wizardModel.wizardModels[0].SpielerName = "Spieler 1";
            }

            if (wizardModel.wizardModels[1].SpielerName == null)
            {
                wizardModel.wizardModels[1].SpielerName = "Spieler 2";
            }


            return View("GameView", wizardModel);
        }

        [HttpPost]
        public ActionResult GameView()
        {
            int kartenImDeck = 60;

            aktuelleRunde++;

            PunkteAuswertung(wizardModel);

            KartenAusteilen(anzahlSpieler, kartenImDeck, aktuelleRunde);

            return View(wizardModel);
        }

        [HttpPost]
        public ActionResult GameAuswertungView(WizardModel wizardModel)
        {
            aktuelleRunde++;

            PunkteAuswertung(wizardModel);

            return View(wizardModel);
        }

        public ActionResult KartenAusteilen(int anzahlSpieler, int kartenImDeck, int aktuelleRunde)
        {
            kartenImDeck = 60;

            WizardModel wizardModel = new WizardModel();

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

            wizardModel.KartenAufDerHand = listNumbers;

            return View(wizardModel);
        }

        public string TrumpfBestimmen()
        {
            string[] farben = { "rot", "blau", "gruen", "gelb" };
            Random rand = new Random();

            int index = rand.Next(farben.Length);

            string trumpf = farben[index];

            return trumpf;
        }


        public ActionResult AuswahlStich()
        {
            int anzahlStiche = aktuelleRunde++;


            return View();
        }

        [HttpPost]
        public ActionResult AuswertungStich(WizardModel wizardModel)
        {
            aktuelleRunde++;

            PunkteAuswertung(wizardModel);

            return View(wizardModel);
        }

        public ActionResult PunkteAuswertung(WizardModel wizardModel)
        {
            aktuelleRunde++;
            if (wizardModel.SticheRichtigAngesagt)
            {

            }

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
            if (rndNumber > 14 && rndNumber <= 26)
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

            KartenAufDerHand.Add(rndNumber);

            wizardModel.KartenAufDerHand = KartenAufDerHand;

            return wizardModel;
        }
    }
}