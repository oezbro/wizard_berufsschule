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
        //public int kartenImDeck;
        //public int anzahlSpieler;
        //public int aktuelleRunde;

        public static WizardModel wizardModel;

        public ActionResult StartView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StartView(WizardModel model)
        {
            model.Runde = 1;
            model.SpielerAnzahl = 2;

            Random rand = new Random();

            model.Trumpf = TrumpfBestimmen();

            //List<WizardModel.Karte> kartenAufDerHand = new List<WizardModel.Karte>();

            WizardModel.Spieler spieler = new WizardModel.Spieler();

            for (int i = 0; i < model.SpielerAnzahl; i++)
            {
                if (model.SpielerListe[i].SpielerName == null)
                {
                    int spielerZahl = i + 1;

                    model.SpielerListe[i].SpielerName = "Spieler " + spielerZahl;
                }

                KartenAusteilen(model, rand, model.SpielerListe[i]);
            }

            wizardModel = model;

            return View("GameView", model);
        }


        [HttpPost]
        public ActionResult GameView(WizardModel model)
        {
            model = wizardModel;

            int kartenImDeck = 60;

            Random rand = new Random();

            model.Runde++;

            List<WizardModel.Karte> kartenListe = new List<WizardModel.Karte>();

            for (int i = 0; i < model.SpielerAnzahl; i++)
            {
                KartenAusteilen(model, rand, model.SpielerListe[i]);
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

        public void KartenAusteilen(WizardModel model, Random rand, WizardModel.Spieler spieler)
        {
            List<WizardModel.Karte> kartenAufDerHand = new List<WizardModel.Karte>();

            for (int i = 0; i < model.Runde; i++)
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
                }
                if (number == 15)
                {
                    karte.IstWizard = true;
                }

                if (karte.IstNarr == true)
                {
                    karte.BildPfad = "narr1.png";
                }
                else if (karte.IstWizard == true)
                {
                    karte.BildPfad = "zauberer1.png";
                }
                else
                {
                    karte.BildPfad = karte.KartenFarbe + karte.KartenWert.ToString() + ".png";
                }

                kartenAufDerHand.Add(karte);
            }
            spieler.KartenListe = kartenAufDerHand;
            //return spieler.KartenListe;
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