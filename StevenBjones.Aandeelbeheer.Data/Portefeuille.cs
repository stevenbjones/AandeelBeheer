using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.Data
{
    [Table("tblPortefeuilles")]
    public class Portefeuille
    {
        //Properties
        public int Id { get; set; }
        public bool IsPositief
        {
            get
            {
                return this.IsPortefeuillePositief();
            }
        } 
        public string Eigenaar { get; set; }
        public List<Aandeel> Aandelen { get; set; }


        //Internal gemaakt zodat bij het gebruiken van de classe er geen ID moet meegeven worden
        internal Portefeuille(int id,string eigenaar)
        {
            Id = id;          
            Eigenaar = eigenaar;
            Aandelen = new List<Aandeel>();

        }

        public Portefeuille() { }

        public Portefeuille(string eigenaar) : this(0, eigenaar)
        {
        }

        public bool IsPortefeuillePositief()
        {
            int resultaat = 0;
            foreach (Aandeel a in this.Aandelen)
            {
                resultaat += (a.ActueleWaarde * a.Hoeveelheid) - (a.BeginWaarde * a.Hoeveelheid);
            }
            if (resultaat > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //String van het object
        public override string ToString()
        {
            return $"Portefeuille {Id} met {Eigenaar} heeft {Aandelen?.Count()} aandelen";
        }
    }
}
