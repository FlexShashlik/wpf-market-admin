using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfAdminApp.Entities;

namespace WpfAdminApp.ViewModels
{
    public class SubCatalogViewModel : INotifyPropertyChanged
    {
        private SubCatalog _selectedSubCatalog;
        private RelayCommand _addCommand, _removeCommand, _applyCommand;
        
        public ObservableCollection<SubCatalog> SubCatalogs { get; set; }

        public SubCatalog SelectedSubCatalog
        {
            get { return _selectedSubCatalog; }
            set
            {
                _selectedSubCatalog = value;
                OnPropertyChanged("SelectedSubCatalog");
            }
        }

        public SubCatalogViewModel()
        {
            SubCatalogs = new ObservableCollection<SubCatalog>(MarketAPI.GetSubCatalog());
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
                                var values = (object[])obj;
                                string subCatalogName = values[0].ToString();
                                string subCatalogCatalogID = values[1].ToString();

                                if (subCatalogName != string.Empty &&
                                    subCatalogCatalogID != string.Empty)
                                {
                                    SubCatalog subCatalog = new SubCatalog()
                                    {
                                        Name = subCatalogName,
                                        CatalogID = int.Parse(subCatalogCatalogID)
                                    };

                                    ExecuteCommand(MarketAPI.AddSubCatalog, subCatalog);
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
                                // TODO: ExecuteCommand(MarketAPI.UpdateSubCatalog, obj);
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
                                // TODO: ExecuteCommand(MarketAPI.DeleteSubCatalog, obj);
                            },
                            obj => SubCatalogs.Count > 0
                        )
                    );
            }
        }

        #endregion

        private void ExecuteCommand(Func<SubCatalog, bool> apiMethod, object obj)
        {
            if (obj != null)
            {
                SubCatalog subCatalog = obj as SubCatalog;
                bool response = apiMethod(subCatalog);

                SubCatalogs.Clear();
                MarketAPI.GetSubCatalog().ForEach(x => SubCatalogs.Add(x));

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
