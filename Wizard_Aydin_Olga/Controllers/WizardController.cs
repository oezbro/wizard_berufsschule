using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wizard_Aydin_Olga.Models;
using Wizard_Aydin_Olga.Controllers;
using System.Threading.Tasks;

namespace Wizard_Aydin_Olga.Controllers
{
    public class WizardController : Controller
    {
        public static WizardModel wizardModel;

        public ActionResult StartView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SticheSetzen(WizardModel model)
        {
            wizardModel.AnzahlStiche = model.AnzahlStiche;

            model = wizardModel;

            return RedirectToAction("StartView", "Wizard", model);
        }

        [HttpPost]
        public ActionResult StartView(WizardModel model)
        {
            model.Runde = 1;
            model.SpielerAnzahl = 2;

            Random rand = new Random();

            model.Trumpf = TrumpfBestimmen();

            DeckMischen(model);

            for (int i = 0; i < model.SpielerAnzahl; i++)
            {
                if (model.SpielerListe[i].SpielerName == null)
                {
                    int spielerZahl = i + 1;

                    model.SpielerListe[i].SpielerName = "Spieler " + spielerZahl;
                }

                List<WizardModel.Karte> kartenAufDerHand = new List<WizardModel.Karte>();

                for (int r = 0; r < model.Runde; r++)
                {
                    kartenAufDerHand.Add(model.KartenDeck.FirstOrDefault());

                    model.SpielerListe[i].KartenListe = kartenAufDerHand;

                    model.KartenDeck.RemoveAt(0);
                }
            }

            wizardModel = model;

            return View("GameView", model);
        }

        [HttpPost]
        public ActionResult GameView(WizardModel model)
        {
            model = wizardModel;

            Random rand = new Random();

            model.Runde++;

            DeckMischen(model);

            for (int i = 0; i < model.SpielerAnzahl; i++)
            {
                List<WizardModel.Karte> kartenAufDerHand = new List<WizardModel.Karte>();

                for (int r = 0; r < model.Runde; r++)
                {
                    kartenAufDerHand.Add(model.KartenDeck.FirstOrDefault());

                    model.SpielerListe[i].KartenListe = kartenAufDerHand;

                    model.KartenDeck.RemoveAt(0);
                }
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

        public void DeckMischen(WizardModel model)
        {
            Random rand = new Random();

            List<WizardModel.Karte> kartenImDeck = new List<WizardModel.Karte>();

            if (model.KartenDeck == null)
            {
                kartenImDeck.Add(RandomKarte(model, rand));
                model.KartenDeck = kartenImDeck;
            }

            int s = 60 - (model.Runde * 4);

            for (int i = 0; model.KartenDeck.Count() < s; i++)
            {
                WizardModel.Karte karte = new WizardModel.Karte();

                karte = RandomKarte(model, rand);

                if (kartenImDeck.Any(x => x.KartenFarbe == karte.KartenFarbe && x.KartenWert == karte.KartenWert))
                {
                    kartenImDeck.Remove(karte);
                }
                else
                {
                    kartenImDeck.Add(karte);
                }
                model.KartenDeck = kartenImDeck;
            }
            wizardModel = model;
        }

        public WizardModel.Karte RandomKarte(WizardModel model, Random rand)
        {
            int number;

            WizardModel.Karte karte = new WizardModel.Karte();

            number = rand.Next(1, 15);

            karte.KartenWert = number;

            string[] farben = { "rote", "blaue", "gruene", "gelbe" };

            int index = rand.Next(farben.Length);

            karte.KartenFarbe = farben[index];

            if (number == 14)
            {
                karte.IstNarr = true;
                karte.BildPfad = "narr1.png";
            }
            else if (number == 15)
            {
                karte.IstWizard = true;
                karte.BildPfad = "zauberer1.png";
            }
            else
            {
                karte.BildPfad = karte.KartenFarbe + karte.KartenWert.ToString() + ".png";
            }

            return karte;
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