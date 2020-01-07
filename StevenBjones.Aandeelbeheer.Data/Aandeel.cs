using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.Data
{
    [Table("tblAandelen")]
    public class Aandeel
    {
        //Properties
        public int Id { get; set; }
        public Bedrijf Bedrijf { get; set; }
        public int BeginWaarde { get; set; }
        public int ActueleWaarde { get; set; }
        public int Hoeveelheid { get; set; }

        //Internal gemaakt zodat bij het gebruiken van de classe er geen ID moet meegeven worden
        internal Aandeel(int id,int beginWaarde, int actueleWaarde, int hoeveelheid)
        {
            Id = id;            
            BeginWaarde = beginWaarde;
            ActueleWaarde = actueleWaarde;
            Hoeveelheid = hoeveelheid;
                  
        }
                
        public Aandeel(int beginWaarde, int actueleWaarde, int hoeveelheid) : this(0, beginWaarde, actueleWaarde, hoeveelheid)
        { }

        public Aandeel()
        { }
       

        //De string van het object Aandeel
        public override string ToString()
        {
            return $"{Id} -Begin waarde: {BeginWaarde} - Actuele waarde: {ActueleWaarde} - hoeveelheid:{Hoeveelheid} van {Bedrijf}   ";
        }
    }
}
