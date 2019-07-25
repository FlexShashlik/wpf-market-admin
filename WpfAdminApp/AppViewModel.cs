using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAdminApp
{
    class AppViewModel
    {
        private Catalog _selectedCatalog;

        public ObservableCollection<Catalog> Catalogs { get; set; }

        public Catalog SelectedCatalog
        {
            get { return _selectedCatalog; }
            set
            {
                _selectedCatalog = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        public AppViewModel()
        {
            Catalogs = new ObservableCollection<Catalog>
            {
                new Catalog { ID = 1, Name="Доборные элементы" },
                new Catalog { ID = 2, Name="Кровельные элементы"},
                new Catalog { ID = 3, Name="Еще какие-то" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
