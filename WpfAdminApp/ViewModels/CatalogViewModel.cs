using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfAdminApp.Entities;

namespace WpfAdminApp.ViewModels
{
    public class CatalogViewModel : INotifyPropertyChanged
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
            MarketAPI.GetCatalog(out List<Catalog> catalogs, out bool result);

            if (result && catalogs != null)
                Catalogs = new ObservableCollection<Catalog>(catalogs);
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
                                string catalogName = obj as string;

                                if (catalogName != string.Empty)
                                {
                                    Catalog catalog = new Catalog() { Name = catalogName };
                                    ExecuteCommand(MarketAPI.AddCatalog, catalog);
                                }
                            }
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
                                ExecuteCommand(MarketAPI.UpdateCatalog, obj);
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
                                    ExecuteCommand(MarketAPI.DeleteCatalog, obj);
                                },
                            obj => Catalogs?.Count > 0
                        )
                    );
            }
        }

        #endregion

        private void ExecuteCommand(Func<Catalog, bool> apiMethod, object obj)
        {
            if (obj != null)
            {
                Catalog catalog = obj as Catalog;
                bool response = apiMethod(catalog);

                Catalogs?.Clear();
                MarketAPI.GetCatalog(out List<Catalog> catalogs, out bool result);

                if (result && catalogs != null)
                    catalogs.ForEach(x => Catalogs.Add(x));

                MessageBox.Show
                (
                    response ?
                    MarketAPI.SuccessMessage : MarketAPI.FailMessage
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
