using StevenBjones.Aandeelbeheer.Data;
using StevenBjones.Aandeelbeheer.Models;
using StevenBjones.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StevenBjones.Aandeelbeheer.ViewModels
{
    class BedrijfAddViewModel : BaseViewModel
    {
        private AandeelbeheerRepository _repository;
        private Bedrijf _bedrijf;

        public BedrijfAddViewModel(AandeelbeheerRepository repository)
        {
            _repository = repository;

            _bedrijf = new Bedrijf();
            SaveCommand = new RelayCommand(SaveChanges);
            CancelCommand = new RelayCommand(CancelChanges);           
        }

        public string Error { get; set; }

        public Bedrijf Bedrijf
        {
            get { return _bedrijf; }
            set
            {
                if (_bedrijf != value)
                {
                    _bedrijf = value;
                }
            }
        }

        public event Action<Boolean> ReturnToViewRequested;

        public RelayCommand SaveCommand { get; private set; }

        public void SaveChanges()
        {

            if (Bedrijf.BedrijfNaam == null || Bedrijf.BedrijfSymbool == null)
            {
                Error = "Gelieve een waarde in te voegen";
                OnPropertyChanged("Error");
                return;
            }
            
            //Aandeel wordt ook in de dataset toegevoegd zodat de ID correct aangepast wordt
            _repository.AddBedrijf(Bedrijf);

            ReturnToViewRequested?.Invoke(true);
        }

        public RelayCommand CancelCommand { get; set; }

        public void CancelChanges()
        {
            ReturnToViewRequested?.Invoke(false);
        }
    }
}
