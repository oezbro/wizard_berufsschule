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

            int kartenImDeck = 60;

            Random rand = new Random();

            model.Trumpf = TrumpfBestimmen();

            for (int i = 0; i < model.SpielerAnzahl; i++)
            {
                if (model.SpielerListe[i].SpielerName == null)
                {
                    int spielerZahl = i + 1;

                    model.SpielerListe[i].SpielerName = "Spieler " + spielerZahl;
                }
                List<WizardModel.Karten> kartenAufDerHand = new List<WizardModel.Karten>();

                kartenAufDerHand.Add(KartenAusteilen(kartenImDeck, model, rand));

                model.SpielerListe[i].KartenListe = kartenAufDerHand;
            }

            wizardModel = model;

            return View("GameView", wizardModel);
        }


        [HttpPost]
        public ActionResult GameView(WizardModel model)
        {
            model = wizardModel;

            int kartenImDeck = 60;

            Random rand = new Random();

            model.Runde++;

            List<WizardModel.Karten> kartenListe = new List<WizardModel.Karten>();

            foreach (WizardModel.Spieler spieler in model.SpielerListe)
            {
                spieler.KartenListe.Add(KartenAusteilen(kartenImDeck, model, rand));
            }

            return View(wizardModel);
        }

        [HttpPost]
        public ActionResult GameAuswertungView(WizardModel model)
        {
            model.Runde++;

            PunkteAuswertung(model);

            return View(model);
        }

        public WizardModel.Karten KartenAusteilen(int kartenImDeck, WizardModel model, Random rand)
        {
            kartenImDeck = 60;

            List<int> listNumbers = new List<int>();
            int number;

            WizardModel.Karten karten = new WizardModel.Karten();

            for (int i = 0; i < model.Runde; i++)
            {
                number = rand.Next(1, 15);

                karten.KartenWert = number;

                string[] farben = { "rote", "blaue", "gruene", "gelbe" };

                int index = rand.Next(farben.Length);

                karten.KartenFarbe = farben[index];

                kartenImDeck--;

                if (number == 14)
                {
                    karten.IstNarr = true;
                }
                if (number == 15)
                {
                    karten.IstWizard = true;
                }

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