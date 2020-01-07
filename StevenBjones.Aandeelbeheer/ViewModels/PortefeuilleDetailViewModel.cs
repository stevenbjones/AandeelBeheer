using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    class PortefeuilleDetailViewModel : BaseViewModel
    {
        //De repository die gebruikt wordt voor het aanspreken van de dataset
        private AandeelbeheerRepository _repository;

        //Variabele die gebruikt wordt voor de property portefeuille
        private Portefeuille _portefeuille;

        /// <summary>
        /// Constructor van PortefeuilleDetailViewModel 
        /// </summary>
        /// <param name="repository"> Repository waar request naar de dataset staan</param>
        public PortefeuilleDetailViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;
        }

        #region properties

        //Property portefeuille
        public Portefeuille Portefeuille
        {
            get { return _portefeuille; }
            set
            {
                if(_portefeuille!= value)
                {
                    _portefeuille = value;
                    Titel = $"Portefeuille detail";
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}
