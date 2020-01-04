using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;
using StevenBjones.Aandeelbeheer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    class PortefeuilleDetailAddViewModel : BaseViewModel
    {
        private AandeelbeheerRepository _repository;
        private Portefeuille _addPortefeuille = new Portefeuille();

        public PortefeuilleDetailAddViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
        }

        private string Error { get; set; }

        public Portefeuille Addportefeuille
        {
            get{ return _addPortefeuille; }
            set
            {
                if(_addPortefeuille != value)
                {
                    _addPortefeuille = value;
                    OnPropertyChanged();
                }
            }
        }

        public event Action<Boolean> ReturnToViewRequested;

        public RelayCommand SaveCommand { get; private set; }

        public void SaveChanges()
        {
            if (Addportefeuille.Eigenaar == null)
            {
                Error = "Gelieve een waarde in te geven";
                OnPropertyChanged("Error");
                return;
            }
          
            _repository.Addportefeuille(Addportefeuille);
            Addportefeuille = new Portefeuille();
            ReturnToViewRequested?.Invoke(true);
        }

        public RelayCommand CancelCommand { get; set; }

        public void CancelChanges()
        {
            Addportefeuille = new Portefeuille();
            ReturnToViewRequested?.Invoke(false);
        }
    }
}
