using StevenBjones.Common;
using StevenBjones.Aandeelbeheer.Data;
using System;
using System.Collections.Generic;
using StevenBjones.Aandeelbeheer.Models;
using System.Collections.ObjectModel;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    public class PortefeuilleAddAandeelViewModel : BaseViewModel
    {
        // declaratie van repository waar requests naar context staan
        private AandeelbeheerRepository _repository;

        //Variabelen voor properties
        private Aandeel _addAandeel = new Aandeel();
        private List<Bedrijf> _bedrijven = new List<Bedrijf>();

        //Views waar naar genavigeerd kan worden
        private PortefeuilleDetailEditViewModel _portefeuilleDetailEditViewModel;
        private BedrijfListViewModel _bedrijfListViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository">Repository waar requests staan naar de datase</param>
        public PortefeuilleAddAandeelViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;
            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);

            BedrijfCommand = new RelayCommand(BedrijfView);
            Bedrijven = _repository.GetBedrijven();

            _portefeuilleDetailEditViewModel = new PortefeuilleDetailEditViewModel(_repository);
            _bedrijfListViewModel = new BedrijfListViewModel(_repository);

            AddAandeel = new Aandeel();

        }

        #region properties

        public string Error { get; set; }

        public Portefeuille Selectedportefeuille { get; set; }

        public ObservableCollection<Bedrijf> Bedrijven { get; set; }

        public Aandeel AddAandeel { get; set; }

        #endregion

        public event Action<Boolean, BaseViewModel> ReturnToViewRequested;

        #region Save

        public RelayCommand SaveCommand { get; private set; }

        public void SaveChanges()
        {
            if (AddAandeel.Bedrijf == null)
            {
                Error = "Gelieve een bedrijf te kiezen";
                OnPropertyChanged("Error");
                return;
            }
            if (AddAandeel.ActueleWaarde <= 0 || AddAandeel.BeginWaarde <= 0 || AddAandeel.Hoeveelheid <= 0)
            {
                Error = "Gelieve een waarde in te vullen die groter is als 0";
                OnPropertyChanged("Error");
                return;
            }
            //Aandeel wordt ook in de dataset toegevoegd zodat de ID correct aangepast wordt
            _repository.AddAandeel(AddAandeel);
            _repository.UpdatePortefeuille(Selectedportefeuille).Aandelen.Add(AddAandeel);

            AddAandeel = new Aandeel();

            ReturnToViewRequested?.Invoke(true, _portefeuilleDetailEditViewModel);
        }

        #endregion

        #region Cancel

        public RelayCommand CancelCommand { get; set; }

        public void CancelChanges()
        {

            ReturnToViewRequested?.Invoke(false, _portefeuilleDetailEditViewModel);
        }

        #endregion

        #region BedrijfKnop

        public RelayCommand BedrijfCommand { get; set; }

        public void BedrijfView()
        {
            ReturnToViewRequested?.Invoke(false, _bedrijfListViewModel);
        }

        #endregion

        internal void RefreshBedrijven()
        {
            Bedrijven = _repository.GetBedrijven();
        }
    }
}
