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
        //public int kartenImDeck;
        //public int anzahlSpieler;
        //public int aktuelleRunde;

        //public WizardModel wizardModel;

        public ActionResult StartView()
        {
            WizardModel wizardModel = new WizardModel();

            return View();
        }

        [HttpPost]
        public ActionResult StartView(WizardModel model)
        {
            model.Runde = 1;
            model.SpielerAnzahl = 2;

            WizardModel.Karten karten = new WizardModel.Karten();

            WizardModel.Spieler spieler = new WizardModel.Spieler();

            List<WizardModel.Karten> kartenAufDerHand = new List<WizardModel.Karten>();

            //model.Runde = aktuelleRunde;

            int kartenImDeck = 60;

            model.Trumpf = TrumpfBestimmen();

            for (int i = 0; i < model.SpielerAnzahl; i++)
            {
                model.SpielerListe.Add(spieler);

                if (model.SpielerListe[0].SpielerName == null)
                {
                    model.SpielerListe[0].SpielerName = "Spieler 1";
                }
                if (model.SpielerListe[1].SpielerName == null)
                {
                    model.SpielerListe[1].SpielerName = "Spieler 2";
                }
                kartenAufDerHand.Add(KartenAusteilen(kartenImDeck, model.Runde));

                model.SpielerListe[i].KartenListe = kartenAufDerHand;
            }

            //for (int i = 0; i < model.SpielerListe.Count; i++)
            //{
            //    kartenAufDerHand.Add(KartenAusteilen(kartenImDeck, aktuelleRunde));

            //    model.SpielerListe[i].KartenListe = kartenAufDerHand;
            //}

            return View("GameView", model);
        }

        [HttpPost]
        public ActionResult GameView(WizardModel model)
        {
            int kartenImDeck = 60;

            model.Runde++;

            //PunkteAuswertung(wizardModel);

            //WizardModel.Karten karten = new WizardModel.Karten();

            foreach (WizardModel.Spieler spieler in model.SpielerListe)
            {
                KartenAusteilen(kartenImDeck, model.Runde);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult GameAuswertungView(WizardModel model)
        {
            model.Runde++;

            PunkteAuswertung(model);

            return View(model);
        }

        public WizardModel.Karten KartenAusteilen(int kartenImDeck, int aktuelleRunde)
        {
            kartenImDeck = 60;

            var rand = new Random();
            List<int> listNumbers = new List<int>();
            int number;

            WizardModel.Karten karten = new WizardModel.Karten();

            //List<Tuple<int, string>> kartenListe = new List<Tuple<int, string>>();

            for (int i = 0; i < aktuelleRunde; i++)
            {
                //do
                //{
                number = rand.Next(1, 15);
                //} while (listNumbers.Contains(number));
                //listNumbers.Add(number);

                karten.KartenWert = number;

                string[] farben = { "rote", "blaue", "gruene", "gelbe" };

                int index = rand.Next(farben.Length);

                karten.KartenFarbe = farben[index];

                //kartenListe.Add(new Tuple<int, string>(karten.KartenWert, karten.KartenFarbe));

                kartenImDeck--;

                if (number == 14)
                {
                    karten.IstNarr = true;
                }
                if (number == 15)
                {
                    karten.IstWizard = true;
                }
                //wizardModel.KartenListe.Add(karten);

                if (karten.IstNarr == true)
                {
                    karten.BildPfad = "narr1.png";
                }
                else if (karten.IstWizard == true)
                {
                    karten.BildPfad = "zauberer1.png";
                }
                else
                {
                    karten.BildPfad = karten.KartenFarbe + karten.KartenWert.ToString() + ".png";
                }
            }

            return karten;
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
            //int anzahlStiche = aktuelleRunde++;


            return View();
        }

        [HttpPost]
        public ActionResult AuswertungStich(WizardModel wizardModel)
        {
            //aktuelleRunde++;

            //PunkteAuswertung(wizardModel);

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