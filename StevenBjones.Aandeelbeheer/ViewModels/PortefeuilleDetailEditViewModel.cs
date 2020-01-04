using StevenBjones.Common;
using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    class PortefeuilleDetailEditViewModel : BaseViewModel
    {
        private AandeelbeheerRepository _repository;
        private Portefeuille _portefeuille;


        private Aandeel _selectedAandeel;
        //Deze wordt gebruikt om visueel te laten zien als er een aandeel verwijderd wordt
        private ObservableCollection<Aandeel> _aandelen;

        public PortefeuilleDetailEditViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;          

            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);
            DeleteCommand = new RelayCommand(DeleteAandeel,CanDeleteAandeel);
            AddCommand = new RelayCommand(AddAandeel);        
        }

        public string Error { get; set; }
        public Portefeuille Portefeuille
        {
            get { return _portefeuille; }
            set
            {
                if (_portefeuille != value)
                {                   
                    _portefeuille = value;
                    OnPropertyChanged();
                    Titel = $"Wijzig {Portefeuille}";
                    EditPortefeuille = Portefeuille;                    
                }
            }
        }

        public ObservableCollection<Aandeel> Aandelen
        {
            get
            {
                return _aandelen;
            }
            set
            {
                _aandelen = value;          
            }
        }

        private Portefeuille editPortefeuille;

        public Portefeuille EditPortefeuille
        {
            get { return editPortefeuille; }
            set
            {
                if (value != null)
                {                   
                    editPortefeuille = new Portefeuille(value.Eigenaar);
                    editPortefeuille.Aandelen = value.Aandelen;
                    Aandelen = new ObservableCollection<Aandeel>(value.Aandelen);
                }                   
                else
                {
                    editPortefeuille = value;                    
                }                
                  
                OnPropertyChanged();
            }
        }

        #region Save changes

        //RelayCommand voor de saveCommand
        public RelayCommand SaveCommand { get; private set; }

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

            Portefeuille.Aandelen = new List<Aandeel>( Aandelen );


            _repository.UpdatePortefeuille(Portefeuille);
            Portefeuille = EditPortefeuille = null;
            ReturnToViewRequested?.Invoke(true);
        }

        public event Action<Boolean> ReturnToViewRequested;
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
            get { return _selectedAandeel; }
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
        public RelayCommand DeleteCommand { get; private set; }
        private void DeleteAandeel()
        {
            //repository.DeleteAandeel(selectedAandeel);
            _aandelen.Remove(_selectedAandeel);            
        }

        //Kijkt of de selectedaandeel null is. 
        //Als deze null is zal hij een false retourneren.

        private Boolean CanDeleteAandeel()
        {
            return _selectedAandeel != null;
        }




        #endregion

        #region Add aandeel

        public RelayCommand AddCommand { get; private set; }

        public event Action AddAandeelRequested;

        public void AddAandeel()
        {
            AddAandeelRequested?.Invoke();
        }

        #endregion

        public void RefreshAandelen()
        {
           Aandelen = new ObservableCollection<Aandeel>(_repository.GetPortefeuilleMetID(Portefeuille).Aandelen);
        }


    }
}


// list.contains

    //Obsvervable list maken --> deze biden
    //ittems uit observable list wegdoen
    //Hierna 2 lijsten vergelijken
    // LijstPushen