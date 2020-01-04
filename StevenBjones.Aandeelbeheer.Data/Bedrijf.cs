using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.Data
{
    [Table("tblBedrijven")]
    public class Bedrijf
    {
        //Properties van Bedrijf
        public int Id { get; set; }
        public string BedrijfSymbool { get; set; }
        public string BedrijfNaam { get; set; }

        //Internal gemaakt zodat bij het gebruiken van de classe er geen ID moet meegeven worden
        internal Bedrijf(int id,string bedrijfSymbool,string bedrijfnaam)
        {
            Id = id;
            BedrijfSymbool = bedrijfSymbool;
            BedrijfNaam = bedrijfnaam;
        }

        public Bedrijf() { }

        public Bedrijf(string bedrijfSymbool,string bedrijfnaam):this(0,bedrijfSymbool,bedrijfnaam)
        { }

        //String van het object
        public override string ToString()
        {
            return $"bedrijf {BedrijfNaam} ({BedrijfSymbool})";
        }
    }
}
