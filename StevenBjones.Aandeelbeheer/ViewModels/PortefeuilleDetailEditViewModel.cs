using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    internal class PortefeuilleDetailEditViewModel : BaseViewModel
    {
        //Repository waar requests van dataset staan
        private AandeelbeheerRepository _repository;

        //Private variabelen voor de properties
        private Portefeuille _editPortefeuille;
        private Portefeuille _portefeuille;
        private Aandeel _selectedAandeel;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="repository"> Repository waar requests staan naar de dataset</param>
        public PortefeuilleDetailEditViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
            DeleteCommand = new RelayCommand(DeleteAandeel, CanDeleteAandeel);
            AddCommand = new RelayCommand(AddAandeel);
        }

        //Haal Aandelen op uit dataset
        public void RefreshAandelen()
        {
            Aandelen = new ObservableCollection<Aandeel>(_repository.GetPortefeuilleMetID(Portefeuille).Aandelen);
        }

        #region properties

        //Property die gebruikt wordt voor foutmelding
        public string Error { get; set; }

        public Portefeuille Portefeuille
        {
            get => _portefeuille;
            set
            {
                if (_portefeuille != value)
                {
                    _portefeuille = value;
                    EditPortefeuille = Portefeuille;
                    Titel = $"Wijzig Portefeuille";
                    OnPropertyChanged();
                }
            }
        }

        //Property Observarble lijst van aandelen
        public ObservableCollection<Aandeel> Aandelen { get; set; }

        //Property die opgeslagen wordt als nieuwe portefeuille
        public Portefeuille EditPortefeuille
        {
            get => _editPortefeuille;
            set
            {
                if (value != null)
                {
                    _editPortefeuille = new Portefeuille(value.Eigenaar);
                    _editPortefeuille.Aandelen = value.Aandelen;
                    Aandelen = new ObservableCollection<Aandeel>(value.Aandelen);
                }
                else
                {
                    _editPortefeuille = value;
                }
                OnPropertyChanged();
            }
        }

        #endregion

        #region Save changes

        //RelayCommand voor de saveCommand
        public RelayCommand SaveCommand { get; }

        //Methode waarbij een portefeuille geupdate kan worden. De oude value wordt gelijk gesteld aan de nieuwe van de edit view
        public void SaveChanges()
        {
            if (EditPortefeuille.Eigenaar == "")
            {
                Error = "Gelieve een eigenaar in te geven";
                OnPropertyChanged("Error");
                return;
            }

            Portefeuille.Eigenaar = EditPortefeuille.Eigenaar;

            Portefeuille.Aandelen = new List<Aandeel>(Aandelen);


            _repository.UpdatePortefeuille(Portefeuille);
            Portefeuille = EditPortefeuille = null;
            ReturnToViewRequested?.Invoke(true);
        }

        public event Action<bool> ReturnToViewRequested;

        #endregion

        #region Cancel changes

        public RelayCommand CancelCommand { get; set; }

        public void CancelChanges()
        {
            Portefeuille = EditPortefeuille = null;
            ReturnToViewRequested?.Invoke(false);
        }

        #endregion CancelChanges

        #region Delete Aandeel

        //Haal de geselecteerde aandeel op
        public Aandeel SelectedAandeel
        {
            get => _selectedAandeel;
            set
            {
                if (_selectedAandeel != value)
                {
                    _selectedAandeel = value;
                    OnPropertyChanged();
                }
            }
        }

        //Delete een aandeel
        public RelayCommand DeleteCommand { get; }

        private void DeleteAandeel()
        {
            Aandelen.Remove(_selectedAandeel);
        }

        //Kijkt of de selectedaandeel null is. 
        //Als deze null is zal hij een false retourneren.
        private bool CanDeleteAandeel()
        {
            return _selectedAandeel != null;
        }

        #endregion

        #region Add aandeel

        public RelayCommand AddCommand { get; }

        public event Action AddAandeelRequested;

        //Ga naar addAandeel view
        public void AddAandeel()
        {
            AddAandeelRequested?.Invoke();
        }

        #endregion
    }
}
