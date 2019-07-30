using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfAdminApp.Entities;

namespace WpfAdminApp.ViewModels
{
    public class ProductsViewModel
    {
        private Product _selectedProduct;
        private RelayCommand _addCommand, _removeCommand, _applyCommand;

        public ObservableCollection<Product> Products { get; set; }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>(MarketAPI.GetProducts());
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
                                MessageBox.Show(values[0].ToString());
                                /*string catalogName = obj as string;

                                if (catalogName != string.Empty)
                                {
                                    Catalog catalog = new Catalog() { Name = catalogName };
                                    ExecuteCommand(MarketAPI.AddCatalog, catalog);
                                }*/
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
                                //ExecuteCommand(MarketAPI.DeleteProduct, obj);
                            },
                            obj => Products.Count > 0
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
                                //ExecuteCommand(MarketAPI.UpdateProduct, obj);
                            }
                        )
                    );
            }
        }

        #endregion

        private void ExecuteCommand(Func<Product, bool> apiMethod, object obj)
        {
            if (obj != null)
            {
                Product product = obj as Product;
                bool response = apiMethod(product);

                Products.Clear();
                MarketAPI.GetProducts().ForEach(x => Products.Add(x));

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
