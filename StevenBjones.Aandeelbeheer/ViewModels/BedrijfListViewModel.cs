using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    internal class BedrijfListViewModel : BaseViewModel
    {
        //Repository waar requests van dataset staan
        private readonly AandeelbeheerRepository _repository;

        //Properties variabelen
        private Bedrijf _selectedBedrijf;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="repository"> Repository waar requests staan naar de dataset</param>
        public BedrijfListViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
            DeleteCommand = new RelayCommand(DeleteBedrijf, CanDeleteBedrijf);

            AddCommand = new RelayCommand(AddBedrijf);
        }

        public void RefreshBedrijven()
        {
            Bedrijven = new List<Bedrijf>(_repository.GetBedrijven());
        }

        #region properties

        public ObservableCollection<Bedrijf> ObservableBedrijven { get; set; }

        public List<Bedrijf> Bedrijven { get; set; }

        #endregion


        #region Save changes

        //RelayCommand voor de saveCommand
        public RelayCommand SaveCommand { get; }

        //Methode waarbij een bedrijf geupdate kan worden. De oude value wordt gelijk gesteld aan de nieuwe van de edit view
        public void SaveChanges()
        {
            Bedrijven = new List<Bedrijf>(_repository.GetBedrijven());

            var bedrijvenToDelete = Bedrijven.Except(ObservableBedrijven).ToList();
            Bedrijven = new List<Bedrijf>(ObservableBedrijven);

            //Als een bedrijf gedelete wordt zullen ook de aandelen van het bedrijf verwijderd worden
            foreach (var b in bedrijvenToDelete)
                _repository.DeleteAandelenVanBedrijf(b);

            _repository.DeleteBedrijven(bedrijvenToDelete);

            ReturnToViewRequested?.Invoke(true);
        }

        public event Action<bool> ReturnToViewRequested;

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
            get => _selectedBedrijf;
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
        public RelayCommand DeleteCommand { get; }

        private void DeleteBedrijf()
        {
            ObservableBedrijven.Remove(_selectedBedrijf);
        }

        //Kijkt of de selectedBedrijf null is. 
        //Als deze null is zal hij een false retourneren.

        private bool CanDeleteBedrijf()
        {
            return _selectedBedrijf != null;
        }

        #endregion


        #region Add Bedrijf

        public RelayCommand AddCommand { get; }

        public event Action AddBedrijfRequested;

        public void AddBedrijf()
        {
            AddBedrijfRequested?.Invoke();
        }

        #endregion
    }
}