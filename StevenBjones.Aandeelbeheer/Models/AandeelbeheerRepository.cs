using StevenBjones.Aandeelbeheer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;

namespace StevenBjones.Aandeelbeheer.Models
{
    //Hier gaan waarden van context opgevraagd worden
    public class AandeelbeheerRepository
    {
        private AandeelbeheerContext _context;
        public AandeelbeheerRepository()
        {
            _context = new AandeelbeheerContext();
        }

        //Requests naar de context die te maken hebben met bedrijf
        #region Bedrijf

        //Haal het bedrijf op met het naam als parameter
        public Bedrijf BedrijfByName(string naam)
        {
            return _context.Bedrijven.FirstOrDefault(bedrijf => bedrijf.BedrijfNaam == naam);
        }

        //Haal alle bedrijven op
        public ObservableCollection<Bedrijf> GetBedrijven()
        {
            return new ObservableCollection<Bedrijf>(_context.Bedrijven);
        }

        // Delete het meegegeven bedrijf
        public void DeleteBedrijven(List<Bedrijf> bedrijven)
        {
            //Selecteer de portefeuille uit de context met hetzelfde ID van de geselecteerde portefeuille
            foreach (Bedrijf bedrijf in bedrijven)
            {
                Bedrijf bedrijfToDelete = _context.Bedrijven.Local.FirstOrDefault(b => b.Id == bedrijf.Id);
                if (bedrijfToDelete != null)
                {
                    _context.Bedrijven.Remove(bedrijfToDelete);
                }
            }
            _context.SaveChanges();
        }

        //Voeg Bedrijf toe aan de dbset
        public Bedrijf AddBedrijf(Bedrijf addBedrijf)
        {
            _context.Bedrijven.Add(addBedrijf);
            _context.SaveChanges();
            return addBedrijf;
        }

        #endregion

        //Requests naar de context die te maken hebben met portefeuille
        #region Portefeuille

        //Haal al de Portefeuilles op
        public ObservableCollection<Portefeuille> GetPortefeuilles()
        {
            //Include dient om objecten mee te geven aan de query.
            //In deze include werken we met een dubbele layer            
            return new ObservableCollection<Portefeuille>(_context.Portefeuilles.Include(p => p.Aandelen.Select(b => b.Bedrijf)));
        }

        //Voeg portefeuille toe aan de dbset
        public Portefeuille Addportefeuille(Portefeuille addportefeuille)
        {
            _context.Portefeuilles.Add(addportefeuille);
            _context.SaveChanges();
            return addportefeuille;
        }

        // Delete de meegegeven portefeuille
        public void DeletePortefeuille(Portefeuille portefeuille)
        {
            //Selecteer de portefeuille uit de context met hetzelfde ID van de geselecteerde portefeuille
            Portefeuille portefeuilleToDelete = _context.Portefeuilles.Local.FirstOrDefault(p => p.Id == portefeuille.Id);

            if (portefeuilleToDelete != null)
            {
                _context.Portefeuilles.Remove(portefeuilleToDelete);
            }
            _context.SaveChanges();
        }

        //Updaten van de portefeuille
        public Portefeuille UpdatePortefeuille(Portefeuille portefeuille)
        {
            if (_context.Portefeuilles.Local.FirstOrDefault(p => p.Id == portefeuille.Id) == null)
            {
                _context.Portefeuilles.Attach(portefeuille);
                _context.Entry(portefeuille).State = System.Data.Entity.EntityState.Modified;
            }
            _context.SaveChanges();
            return portefeuille;
        }

        public Portefeuille GetPortefeuilleMetID(Portefeuille portefeuille)
        {
            return _context.Portefeuilles.FirstOrDefault(p => p.Id == portefeuille.Id);
        }
        #endregion

        //Requests naar de context die te maken hebben met Aandeel
        #region Aandeel

        //Voeg Aandeel toe aan de dbset
        public Aandeel AddAandeel(Aandeel addAandeel)
        {
            _context.Aandelen.Add(addAandeel);
            _context.SaveChanges();
            return addAandeel;
        }

        // Delete het meegegeven aandeel
        public void DeleteAandeel(Aandeel aandeel)
        {
            Aandeel aandeelToDelete = _context.Aandelen.Local.FirstOrDefault(a => a.Id == aandeel.Id);

            if (aandeelToDelete != null)
            {
                _context.Aandelen.Remove(aandeel);
            }
            _context.SaveChanges();
        }

        //Delete aandelen van het gedelete bedrijf
        public void DeleteAandelenVanBedrijf(Bedrijf bedrijf)
        {
            List<Aandeel> aandelenToDelete = new List<Aandeel>(_context.Aandelen.Where(a => a.Bedrijf.BedrijfNaam == bedrijf.BedrijfNaam));
            foreach (Aandeel a in aandelenToDelete)
            {
                _context.Aandelen.Remove(a);
            }

        }

        #endregion
    }
}
