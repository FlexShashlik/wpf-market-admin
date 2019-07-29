using System;
using System.Collections.Generic;
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
                                ExecuteCommand(MarketAPI.UpdateCatalog, obj);
                            }
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

                Catalogs.Clear();
                MarketAPI.GetCatalog().ForEach(x => Catalogs.Add(x));

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
