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
        private AandeelbeheerRepository _repository;
        private Portefeuille _portefeuille;

        public PortefeuilleDetailViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;
        }

        public Portefeuille Portefeuille
        {
            get { return _portefeuille; }
            set
            {
                if(_portefeuille!= value)
                {
                    _portefeuille = value;
                    OnPropertyChanged();
                    Titel =  $"{Portefeuille}";
                }
            }
        }
    }
}
