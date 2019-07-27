using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfAdminApp.Entities;

namespace WpfAdminApp.ViewModels
{
    class CatalogViewModel
    {
        private Catalog _selectedCatalog;
        private RelayCommand _addCommand, _removeCommand, _applyCommand;

        public ObservableCollection<Catalog> Catalogs { get; set; }

        public Catalog SelectedCatalog
        {
            get { return _selectedCatalog; }
            set
            {
                _selectedCatalog = value;
                OnPropertyChanged("SelectedCatalog");
            }
        }

        public CatalogViewModel()
        {
            Catalogs = new ObservableCollection<Catalog>(MarketAPI.GetCatalog());
        }

        #region Commands

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand
                        (
                            obj =>
                                {
                                    Catalogs.Add(new Catalog());
                                }
                        )
                    );
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                    (_removeCommand = new RelayCommand
                        (
                            obj =>
                                {
                                    Catalog catalog = obj as Catalog;
                                    Catalogs?.Remove(catalog);
                                },
                            obj => Catalogs.Count > 0
                        )
                    );
            }
        }

        public RelayCommand ApplyCommand
        {
            get
            {
                return _applyCommand ??
                    (_applyCommand = new RelayCommand
                        (
                            obj =>
                            {
                                Catalog catalog = obj as Catalog;
                                //MarketAPI(catalog?.ID);
                                MessageBox.Show("Not Implemented");
                            }
                        )
                    );
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
