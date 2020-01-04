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
        private AandeelbeheerRepository _repository;
        private Aandeel _addAandeel = new Aandeel();
        private List<Bedrijf> _bedrijven = new List<Bedrijf>();
        private PortefeuilleDetailEditViewModel _portefeuilleDetailEditViewModel;
        private BedrijfListViewModel _bedrijfListViewModel;

        public PortefeuilleAddAandeelViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;
            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);

            BedrijfCommand = new RelayCommand(BedrijfView);
            Bedrijven = _repository.GetBedrijven();

            _portefeuilleDetailEditViewModel = new PortefeuilleDetailEditViewModel(_repository);
            _bedrijfListViewModel = new BedrijfListViewModel(_repository);

        }

        public string Error { get; set; }

        public Portefeuille Selectedportefeuille { get; set; }

        public ObservableCollection<Bedrijf> Bedrijven { get; set; }

        public Aandeel AddAandeel
        {
            get { return _addAandeel; }
            set
            {
                if (_addAandeel != value)
                {
                    _addAandeel = value;
                }
            }
        }

        public event Action<Boolean, BaseViewModel> ReturnToViewRequested;

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


            ReturnToViewRequested?.Invoke(true, _portefeuilleDetailEditViewModel);
        }

        public RelayCommand CancelCommand { get; set; }

        public void CancelChanges()
        {

            ReturnToViewRequested?.Invoke(false, _portefeuilleDetailEditViewModel);
        }

        public RelayCommand BedrijfCommand { get; set; }

        public void BedrijfView()
        {
            ReturnToViewRequested?.Invoke(false, _bedrijfListViewModel);
        }

        internal void RefreshBedrijven()
        {
            Bedrijven = _repository.GetBedrijven();
        }


    }
}
