using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.Data
{
    public class AandeelbeheerContextDropCreateDatabaseAlways : DropCreateDatabaseAlways<AandeelbeheerContext>
    {
        protected override void Seed(AandeelbeheerContext context)
        {
            Bedrijf bedrijf;
            Aandeel aandeel;
            Portefeuille portefeuille;

            #region testWaarden1

            //Nieuw bedrijf
            bedrijf = new Bedrijf("FE", "Federa Eng");

            //Nieuwe portefeuille
            portefeuille = new Portefeuille("bob");            

            //Maak aandeel aan en steek deze in de prop van portefeuille
            aandeel = new Aandeel(30, 20, 3);
            aandeel.Bedrijf = bedrijf;

            //Voeg het gemaakte aandeel toe aan het object portefeuille
            portefeuille.Aandelen.Add(aandeel);

            //Voeg portefeuille toe aan de context
            context.Portefeuilles.Add(portefeuille);

            #endregion

            #region TestWaarden2
            //Test Waarde 2

            //Nieuwe portefeuille
            portefeuille = new Portefeuille("bobyke");

            //Nieuw bedrijf
            bedrijf = new Bedrijf("SB", "Steven Bjones");

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(5, 50, 80);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(50, 500, 100);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(5, 30, 20);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(5, 20, 300);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Stop de portefeuille in de database set
            context.Portefeuilles.Add(portefeuille);

            #endregion

            #region TestWaarden3
            //Test Waarde 3

            //Nieuwe portefeuille
            portefeuille = new Portefeuille("Steven");

            //Nieuw bedrijf
            bedrijf = new Bedrijf("RA", "Radio Afio");

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(5, 50, 80);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Nieuw bedrijf
            bedrijf = new Bedrijf("RB", "Radio Bjones");

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(50, 500, 100);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(5, 30, 20);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Maak aandelen aan  en stop ze in portefeuille
            aandeel = new Aandeel(5, 20, 300);
            aandeel.Bedrijf = bedrijf;
            portefeuille.Aandelen.Add(aandeel);

            //Stop de portefeuille in de database set
            context.Portefeuilles.Add(portefeuille);

            #endregion

            //save to context
            context.SaveChanges();
        }
    }
}
