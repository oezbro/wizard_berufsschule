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

                model.SpielerListe[1].AngesagteStiche = KIGegnerStiche(model);

                List<WizardModel.Karte> kartenAufDerHand = new List<WizardModel.Karte>();

                for (int r = 0; r < model.Runde; r++)
                {
                    kartenAufDerHand.Add(model.KartenDeck.FirstOrDefault());

                    model.SpielerListe[i].KartenListe = kartenAufDerHand;

                    model.KartenDeck.RemoveAt(0);
                }
            }

            wizardModel = model;

            return View("GameView", wizardModel);
        }

        private int KIGegnerStiche(WizardModel model)
        {
            int randomStiche;

            Random random = new Random();

            int maxStiche = model.Runde + 1;

            randomStiche = random.Next(0, maxStiche);

            return randomStiche;
        }

        [HttpPost]
        public ActionResult GameView(WizardModel model)
        {
            for (int i = 0; i < 2; i++)
            {
                wizardModel.SpielerListe[i].AngesagteStiche = model.SpielerListe[i].AngesagteStiche;
                wizardModel.SpielerListe[i].GemachteStiche = model.SpielerListe[i].GemachteStiche;
            }
            model = wizardModel;

            DeckMischen(model);

            Random rand = new Random();

            model.Runde++;

            for (int i = 0; i < model.SpielerAnzahl; i++)
            {
                model.SpielerListe[i].PunkteProRunde = SticheAuswerten(model.SpielerListe[i]);

                model.SpielerListe[1].AngesagteStiche = KIGegnerStiche(model);

                List<WizardModel.Karte> kartenAufDerHand = new List<WizardModel.Karte>();

                for (int r = 0; r < model.Runde; r++)
                {
                    kartenAufDerHand.Add(model.KartenDeck.FirstOrDefault());

                    model.SpielerListe[i].KartenListe = kartenAufDerHand;

                    model.KartenDeck.RemoveAt(0);
                }
                wizardModel = model;
            }
            return View(wizardModel);
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

            int s = 60;

            foreach (WizardModel.Spieler spieler in model.SpielerListe)
            {
                if (spieler.KartenListe != null)
                {
                    s = 60 - spieler.KartenListe.Count();
                }
            }

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

            number = rand.Next(1, 16);

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

        public int SticheAuswerten(WizardModel.Spieler spieler)
        {
            if (spieler.GemachteStiche == spieler.AngesagteStiche)
            {
                for (int s = 1; s <= spieler.GemachteStiche; s++)
                {
                    spieler.PunkteProRunde += 10;
                }
                spieler.PunkteProRunde += 20;
            }
            else if (spieler.GemachteStiche != spieler.AngesagteStiche)
            {
                if (spieler.GemachteStiche > spieler.AngesagteStiche)
                {
                    int differenz;
                    differenz = spieler.GemachteStiche - spieler.AngesagteStiche;

                    for (int f = 1; f <= differenz; f++)
                    {
                        spieler.PunkteProRunde -= 10;
                    }
                }
                else if (spieler.GemachteStiche < spieler.AngesagteStiche)
                {
                    int unterschied;
                    unterschied = spieler.AngesagteStiche - spieler.GemachteStiche;

                    for (int s = 1; s <= unterschied; s++)
                    {
                        spieler.PunkteProRunde -= 10;
                    }

                }
            }
            return spieler.PunkteProRunde;
        }
    }
}