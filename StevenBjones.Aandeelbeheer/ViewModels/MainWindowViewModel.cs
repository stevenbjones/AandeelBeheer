using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    public class MainWindowVIewModel : BaseViewModel
    {
        //De repository die gebruikt wordt voor het aanspreken van de dataset
        private AandeelbeheerRepository _repository;

        //De viewModels
        private PortefeuilleListVieuwModel _portefeuilleListVieuwModel;
        private PortefeuilleDetailViewModel _portefeuilleDetailViewModel;
        private PortefeuilleDetailAddViewModel _portefeuilleDetailAddViewModel;
        private PortefeuilleDetailEditViewModel _portefeuilleDetailEditViewModel;
        private PortefeuilleAddAandeelViewModel _portefeuilleAddAandeelViewModel;
        private BedrijfListViewModel _bedrijfListViewModel;
        private BedrijfAddViewModel _bedrijfAddViewModel;

        //Dit zijn de 2 views van het mainwindow. Deze worden steeds veranderd
        private BaseViewModel _currentListViewModel;
        private BaseViewModel _currentDetailViewModel;


        public MainWindowVIewModel()
        {
            //Instanties aanmaken
            _repository = new AandeelbeheerRepository();

            _portefeuilleListVieuwModel = new PortefeuilleListVieuwModel(_repository);
            _portefeuilleDetailViewModel = new PortefeuilleDetailViewModel(_repository);
            _portefeuilleDetailAddViewModel = new PortefeuilleDetailAddViewModel(_repository);
            _portefeuilleDetailEditViewModel = new PortefeuilleDetailEditViewModel(_repository);
            _portefeuilleAddAandeelViewModel = new PortefeuilleAddAandeelViewModel(_repository);
            _bedrijfListViewModel = new BedrijfListViewModel(_repository);
            _bedrijfAddViewModel = new BedrijfAddViewModel(_repository);


            //Deze functie veranderd de listviewmodel. Dit is de linker kant van de mainWindow
            SetListViewModel(_portefeuilleListVieuwModel);

            //Deze functie veranderd de detailViewModel. Dit is de rechter kant van de mainwindow
            SetDetailViewModel(_portefeuilleDetailViewModel);
        }

        #region properties

        //Property huidige listViewModel
        public BaseViewModel CurrentListViewModel
        {
            get { return _currentListViewModel; }
            set
            {
                if (_currentListViewModel != value)
                {
                    _currentListViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        //Property huidige listViewDetailModel
        public BaseViewModel CurrentDetailViewModel
        {
            get { return _currentDetailViewModel; }
            set
            {
                if (_currentDetailViewModel != value)
                {
                    _currentDetailViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region SetListViewModel
        /// <summary>
        /// Methode waar de linker kant van het scherm wordt gezet
        /// </summary>
        /// <param name="viewmodel"> viewmodel waar naar veranderd moet worden</param>
        /// <param name="connect"> Bool die eerst op true staat. hierna wordt hij op false gezet</param>
        public void SetListViewModel(BaseViewModel viewmodel, bool connect = true)
        {
            if (connect && _currentListViewModel != null)
                SetListViewModel(CurrentListViewModel, false);
            switch(viewmodel.GetType().Name)
            {
                //Hier steek je alle functies in, belangrijk voor veredere aanpassingen
                case "PortefeuilleListVieuwModel":
                    if(connect)
                    {
                        _portefeuilleListVieuwModel.PropertyChanged += portefeuilleListVieuwModel_PropertyChanged;
                        _portefeuilleListVieuwModel.AddPortefeuilleRequested += portefeuilleListVieuwModel_AddPortefeuilleRequested;
                        //Anders werkt edit niet

                        _portefeuilleListVieuwModel.EditPortefeuilleRequested += portefeuilleListVieuwModel_EditPortefeuilleRequested;
                        CurrentListViewModel = _portefeuilleListVieuwModel;
                    }
                    else
                    {
                        _portefeuilleListVieuwModel.PropertyChanged -= portefeuilleListVieuwModel_PropertyChanged;
                        _portefeuilleListVieuwModel.AddPortefeuilleRequested -= portefeuilleListVieuwModel_AddPortefeuilleRequested;
                        _portefeuilleListVieuwModel.EditPortefeuilleRequested -= portefeuilleListVieuwModel_EditPortefeuilleRequested;
                    }
                    break;
            }
        }

        private void portefeuilleListVieuwModel_EditPortefeuilleRequested(Data.Portefeuille obj)
        {
            SetDetailViewModel(_portefeuilleDetailEditViewModel);
            _portefeuilleDetailEditViewModel.Portefeuille = obj;
        }

        #endregion

        #region SetDetailViewModel

        /// <summary>
        /// Verander de detail views
        /// </summary>
        /// <param name="viewModel"></param> 
        /// <param name="connect"></param> 
        public void SetDetailViewModel(BaseViewModel viewModel, bool connect = true)
        {
            if (connect && CurrentDetailViewModel != null)
                SetDetailViewModel(CurrentDetailViewModel, false);
            switch (viewModel.GetType().Name)
            {
                case "PortefeuilleDetailViewModel":
                    if(connect)
                    {
                        //Wanneer de Property changed roept hij de functie portefeuilleListVieuwModel_PropertyChanged op 
                        _portefeuilleListVieuwModel.PropertyChanged += portefeuilleListVieuwModel_PropertyChanged;
                        CurrentDetailViewModel = _portefeuilleDetailViewModel;
                    }
                    else
                    {
                        _portefeuilleListVieuwModel.PropertyChanged -= portefeuilleListVieuwModel_PropertyChanged;
                    }
                    
                    break;

                case "PortefeuilleDetailAddViewModel":
                    if(connect)
                    {
                        //Voeg aan de action van portefeuilleDetailAddViewModel de locale functie portefeuilleDetailAddViewModel_ReturnViewRequested
                        _portefeuilleDetailAddViewModel.ReturnToViewRequested += portefeuilleDetailAddViewModel_ReturnViewRequested;
                        CurrentDetailViewModel = _portefeuilleDetailAddViewModel;
                    }
                    else
                    {
                        _portefeuilleDetailAddViewModel.ReturnToViewRequested -= portefeuilleDetailAddViewModel_ReturnViewRequested;
                    }
                    
                    break;

                case "PortefeuilleDetailEditViewModel":
                    if(connect)
                    {
                        _portefeuilleDetailEditViewModel.ReturnToViewRequested += portefeuilleDetailEditViewModel_ReturnToViewRequested;
                        _portefeuilleDetailEditViewModel.PropertyChanged += portefeuilleDetailEditViewModel_PropertyChanged;
                        _portefeuilleDetailEditViewModel.AddAandeelRequested += portefeuilleDetailEditViewModel_AddAandeelRequested;
                        CurrentDetailViewModel = _portefeuilleDetailEditViewModel;
                    }
                    else
                    {
                        _portefeuilleDetailEditViewModel.ReturnToViewRequested -= portefeuilleDetailEditViewModel_ReturnToViewRequested;
                        _portefeuilleDetailEditViewModel.PropertyChanged -= portefeuilleDetailEditViewModel_PropertyChanged;
                    }
                    break;

                case "PortefeuilleAddAandeelViewModel":
                    if (connect)
                    {
                        //Voeg aan de action van portefeuilleDetailAddViewModel de locale functie portefeuilleDetailAddViewModel_ReturnViewRequested
                        _portefeuilleAddAandeelViewModel.ReturnToViewRequested += portefeuilleAddAandeelViewModel_ReturnViewRequested;

                        CurrentDetailViewModel = _portefeuilleAddAandeelViewModel;
                    }
                    else
                    {
                        _portefeuilleAddAandeelViewModel.ReturnToViewRequested -= portefeuilleAddAandeelViewModel_ReturnViewRequested;
                    }

                    break;

                case "BedrijfListViewModel":
                    if (connect)
                    {
                        //Voeg aan de action van portefeuilleDetailAddViewModel de locale functie portefeuilleDetailAddViewModel_ReturnViewRequested
                        _bedrijfListViewModel.ReturnToViewRequested += _bedrijfListViewModel_ReturnViewRequested;
                        _bedrijfListViewModel.AddBedrijfRequested += _bedrijfListViewModel_AddBedrijfRequested;
                        _bedrijfListViewModel.ObservableBedrijven = _repository.GetBedrijven();
                        CurrentDetailViewModel = _bedrijfListViewModel;
                    }
                    else
                    {
                        _bedrijfListViewModel.ReturnToViewRequested -= _bedrijfListViewModel_ReturnViewRequested;
                        _bedrijfListViewModel.AddBedrijfRequested -= _bedrijfListViewModel_AddBedrijfRequested;
                    }
                    break;

                case "BedrijfAddViewModel":
                    if (connect)
                    {
                        //Voeg aan de action van portefeuilleDetailAddViewModel de locale functie portefeuilleDetailAddViewModel_ReturnViewRequested
                        _bedrijfAddViewModel.ReturnToViewRequested += _bedrijfAddViewModel_ReturnViewRequested;
                        CurrentDetailViewModel = _bedrijfAddViewModel;
                    }
                    else
                    {
                        _bedrijfListViewModel.ReturnToViewRequested -= _bedrijfListViewModel_ReturnViewRequested;
                        _bedrijfListViewModel.AddBedrijfRequested -= _bedrijfListViewModel_AddBedrijfRequested;
                    }
                    break;
            }
        }

        private void _bedrijfAddViewModel_ReturnViewRequested(bool refresh)
        {
            SetDetailViewModel(_bedrijfListViewModel);
            if (refresh)
                _bedrijfListViewModel.RefreshBedrijven();
        }

        private void portefeuilleDetailAddViewModel_ReturnViewRequested(bool refreshPortefeuille)
        {
            SetDetailViewModel(_portefeuilleDetailViewModel);
            if (refreshPortefeuille)
                _portefeuilleListVieuwModel.RefreshPortefeuilles();
        }
        #endregion

        #region portefeuilleListVieuwModel functies
        
        private void portefeuilleListVieuwModel_AddPortefeuilleRequested()
        {
            SetDetailViewModel(_portefeuilleDetailAddViewModel);
        }

        private void portefeuilleListVieuwModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedPortefeuille":
                    _portefeuilleDetailViewModel.Portefeuille = _portefeuilleListVieuwModel.SelectedPortefeuille;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region portefeuilleDetailEditViewModel functies

        private void portefeuilleDetailEditViewModel_ReturnToViewRequested(bool refreshPortefeuille)
        {
            SetDetailViewModel(_portefeuilleDetailViewModel);
            if (refreshPortefeuille)
                _portefeuilleListVieuwModel.RefreshPortefeuilles();
        }

        //On property changed van detail edit listview
        private void portefeuilleDetailEditViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                //Deze code gaan we gebruiken voor het weizigen van het aandeel.
                case "SelectedAandeel":
                    _portefeuilleDetailEditViewModel.SelectedAandeel = _portefeuilleDetailEditViewModel.SelectedAandeel;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region add aandeel

        private void portefeuilleAddAandeelViewModel_ReturnViewRequested(bool refreshPortefeuille,BaseViewModel view)
        {
            SetDetailViewModel(view);
            if (refreshPortefeuille)
            {
                _portefeuilleListVieuwModel.RefreshPortefeuilles();
                _portefeuilleDetailEditViewModel.RefreshAandelen();
            }
               
        }

        private void portefeuilleDetailEditViewModel_AddAandeelRequested()
        {
            _portefeuilleAddAandeelViewModel.Selectedportefeuille = _portefeuilleDetailViewModel.Portefeuille;
            SetDetailViewModel(_portefeuilleAddAandeelViewModel);
        }

        #endregion

        #region bedrijfListViewModel functies

        private void _bedrijfListViewModel_ReturnViewRequested(bool refreshBedrijven)
        {
            SetDetailViewModel(_portefeuilleDetailViewModel);
            _portefeuilleAddAandeelViewModel.RefreshBedrijven();
            _portefeuilleListVieuwModel.RefreshPortefeuilles();
        }

        private void _bedrijfListViewModel_AddBedrijfRequested()
        {
            SetDetailViewModel(_bedrijfAddViewModel);
        }

        #endregion
    }
}
