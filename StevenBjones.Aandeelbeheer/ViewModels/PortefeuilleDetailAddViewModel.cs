using System;
using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    internal class PortefeuilleDetailAddViewModel : BaseViewModel
    {
        private Portefeuille _addPortefeuille = new Portefeuille();
        private AandeelbeheerRepository _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Repository waar requests staan naar de dataset</param>
        public PortefeuilleDetailAddViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
        }

        #region  properties

        //String waar de error boodschap wordt bewaard
        public string Error { get; set; }

        //Nieuw aangemaakte portefeuille
        public Portefeuille Addportefeuille
        {
            get => _addPortefeuille;
            set
            {
                if (_addPortefeuille != value)
                {
                    _addPortefeuille = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public event Action<bool> ReturnToViewRequested;

        #region  save

        public RelayCommand SaveCommand { get; }

        //Opslaan van de nieuwe portefeuille
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

        #endregion

        #region Cancel

        public RelayCommand CancelCommand { get; set; }

        //Ga terug naar vorige view
        public void CancelChanges()
        {
            Addportefeuille = new Portefeuille();
            ReturnToViewRequested?.Invoke(false);
        }

        #endregion
    }
}