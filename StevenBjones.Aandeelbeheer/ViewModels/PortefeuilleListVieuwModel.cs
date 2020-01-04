using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    public class PortefeuilleListVieuwModel : BaseViewModel
    {
        //De repository die gebruikt wordt voor het aanspreken van de dataset
        private AandeelbeheerRepository _repository;

        //Variabelen die gebruikt worden voor de property
        private ObservableCollection<Portefeuille> _portefeuilles;
        private Portefeuille _selectedPortefeuille;

        public PortefeuilleListVieuwModel(AandeelbeheerRepository repository)
        {
            _repository = repository;
            _portefeuilles = repository.GetPortefeuilles();         
             
            Titel = "Portefeuilles";

            DeleteCommand = new RelayCommand(DeletePortefeuille, CanDeletePortefeuille);
            AddCommand = new RelayCommand(AddPortefeuille);
            EditCommand = new RelayCommand<Portefeuille>(EditPortefeuille, CanEditPortefeuille);
        }

        public ObservableCollection<Portefeuille> Portefeuilles
        {
            get { return _portefeuilles; }
            set
            {
                if (_portefeuilles != value)
                {
                    _portefeuilles = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Delete Portefeuille

        //Haal de geselecteerde portefeuille op
        public Portefeuille SelectedPortefeuille
        {
            get { return _selectedPortefeuille; }
            set
            {
                if (_selectedPortefeuille != value)
                {
                    _selectedPortefeuille = value;
                    OnPropertyChanged();
                }
            }
        }


        //Delete een portefeuille
        public RelayCommand DeleteCommand { get; private set; }
        private void DeletePortefeuille()
        {
            _repository.DeletePortefeuille(_selectedPortefeuille);
            RefreshPortefeuilles();
        }

        //Kijkt of de selectedPortefeuille null is. 
        //Als deze null is zal hij een false retourneren.

        private Boolean CanDeletePortefeuille()
        {
            return _selectedPortefeuille != null;
        }


        #endregion

        #region Add Portefeuille

        public RelayCommand AddCommand { get; private set; }

        public event Action AddPortefeuilleRequested;

        public void AddPortefeuille()
        {
            AddPortefeuilleRequested?.Invoke();
        }



        #endregion



        #region Edit Portefeuille
        //RelayCommand voor het editen van een portefeuille
        public RelayCommand<Portefeuille> EditCommand { get; private set; }

        //Invoke als portefeuille niet null is
        public void EditPortefeuille(Portefeuille portefeuille)
        {
            EditPortefeuilleRequested?.Invoke(portefeuille);
        }

        //Kijk na of portefeuille null is niet, als deze null is returned hij false. Dan zal de knop niet gebruikt kunnen worden
        public Boolean CanEditPortefeuille(Portefeuille portefeuille)
        {
            return portefeuille != null;
        }

        public event Action<Portefeuille> EditPortefeuilleRequested;
        #endregion

        //Refresh --> haal de portefeuiles opnieuw op als er een wijzeging was.
        //Deze steek je in property Portefeuilles --> Deze is databind op de wpf
        public void RefreshPortefeuilles()
        {
            Portefeuilles = _repository.GetPortefeuilles();
        }

  
        
    }
}
