using StevenBjones.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;
using System.Collections.ObjectModel;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    class BedrijfListViewModel : BaseViewModel
    {
        private AandeelbeheerRepository _repository;


        private Bedrijf _selectedBedrijf;
        //Deze wordt gebruikt om visueel te laten zien als er een Bedrijf verwijderd wordt
        private ObservableCollection<Bedrijf> _observableBedrijven;

        public BedrijfListViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
            DeleteCommand = new RelayCommand(DeleteBedrijf, CanDeleteBedrijf);

            AddCommand = new RelayCommand(AddBedrijf);
        }

        public ObservableCollection<Bedrijf> ObservableBedrijven
        {
            get
            {
                return _observableBedrijven;
            }
            set
            {
                _observableBedrijven = value;
            }
        }

        public List<Bedrijf> Bedrijven
        {
            get;
            set;
        }

        #region Save changes

        //RelayCommand voor de saveCommand
        public RelayCommand SaveCommand { get; private set; }

        //Methode waarbij een bedrijf geupdate kan worden. De oude value wordt gelijk gesteld aan de nieuwe van de edit view
        public void SaveChanges()
        {
            Bedrijven = new List<Bedrijf>(_repository.GetBedrijven());

            List<Bedrijf> bedrijvenToDelete = Bedrijven.Except(ObservableBedrijven).ToList();
            Bedrijven = new List<Bedrijf>(ObservableBedrijven);

            //Als een bedrijf gedelete wordt zullen ook de aandelen van het bedrijf verwijderd worden
            foreach (Bedrijf b in bedrijvenToDelete)
            {
                _repository.DeleteAandelenVanBedrijf(b);
            }

            _repository.DeleteBedrijven(bedrijvenToDelete);

            ReturnToViewRequested?.Invoke(true);
        }

        public event Action<Boolean> ReturnToViewRequested;
        #endregion

        #region Cancel changes

        public RelayCommand CancelCommand { get; set; }

        public void CancelChanges()
        {
            ReturnToViewRequested?.Invoke(false);
        }

        #endregion CancelChanges

        #region Delete Bedrijf

        //Haal de geselecteerde Bedrijf op
        public Bedrijf SelectedBedrijf
        {
            get { return _selectedBedrijf; }
            set
            {
                if (_selectedBedrijf != value)
                {
                    _selectedBedrijf = value;
                    OnPropertyChanged();
                }
            }
        }

        //Delete een Bedrijf
        public RelayCommand DeleteCommand { get; private set; }
        private void DeleteBedrijf()
        {
            _observableBedrijven.Remove(_selectedBedrijf);
        }

        //Kijkt of de selectedBedrijf null is. 
        //Als deze null is zal hij een false retourneren.

        private Boolean CanDeleteBedrijf()
        {
            return _selectedBedrijf != null;
        }

        #endregion


        #region Add Bedrijf

        public RelayCommand AddCommand { get; private set; }

        public event Action AddBedrijfRequested;

        public void AddBedrijf()
        {
            AddBedrijfRequested?.Invoke();
        }

        #endregion

        public void RefreshBedrijven()
        {
            Bedrijven = new List<Bedrijf>(_repository.GetBedrijven());
        }
    }
}
