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

            model.Trumpf = TrumpfBestimmen();

            wizardModel = model;

            for (int i = 0; i < anzahlSpieler; i++)
            {
                if (wizardModel.wizardModels == null)
                {
                    wizardModel.wizardModels.Add(model);
                }
            }

            for (int i = 0; i < wizardModel.wizardModels.Count(); i++)
            {
                KartenAusteilen(kartenImDeck, aktuelleRunde);

                wizardModel.wizardModels[i] = wizardModel;
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

            KartenAusteilen(kartenImDeck, aktuelleRunde);

            return View(wizardModel);
        }

        [HttpPost]
        public ActionResult GameAuswertungView(WizardModel wizardModel)
        {
            aktuelleRunde++;

            PunkteAuswertung(wizardModel);

            return View(wizardModel);
        }

        public void KartenAusteilen(int kartenImDeck, int aktuelleRunde)
        {
            kartenImDeck = 60;

            var rand = new Random();
            List<int> listNumbers = new List<int>();
            int number;

            List<Tuple<int, string>> kartenListe = new List<Tuple<int, string>>();

            for (int i = 0; i < aktuelleRunde; i++)
            {
                do
                {
                    number = rand.Next(1, 15);
                } while (listNumbers.Contains(number));
                listNumbers.Add(number);

                wizardModel.KartenWert = number;

                string[] farben = { "rot", "blau", "gruen", "gelb" };

                int index = rand.Next(farben.Length);

                wizardModel.KartenFarbe = farben[index];

                kartenListe.Add(new Tuple<int, string>(wizardModel.KartenWert, wizardModel.KartenFarbe));

                wizardModel.KartenAufDerHand = kartenListe;

                kartenImDeck--;

                if (number == 14)
                {
                    wizardModel.IstNarr = true;
                }
                if (number == 15)
                {
                    wizardModel.IstWizard = true;
                }

            }
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
            if (wizardModel.SticheRichtigAngesagt)
            {

            }

            return View();
        }
    }
}