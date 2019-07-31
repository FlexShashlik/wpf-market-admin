using Microsoft.Win32;
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
        private RelayCommand _addCommand, _removeCommand, _applyCommand, _chooseImageCommand;
        private string _localImageLocation;

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
                                string productName = values[0].ToString();
                                string productPrice = values[1].ToString();
                                string productImgExt = values[2].ToString();
                                string productSubCatalogID = values[3].ToString();

                                if (productName != string.Empty &&
                                    productPrice != string.Empty &&
                                    productImgExt != string.Empty &&
                                    productSubCatalogID != string.Empty &&
                                    _localImageLocation != string.Empty)
                                {
                                    Product product = new Product()
                                    {
                                        Name = productName,
                                        Price = int.Parse(productPrice),
                                        ImageExtension = productImgExt,
                                        SubCatalogID = int.Parse(productSubCatalogID),
                                        LocalImagePath = _localImageLocation,
                                    };

                                    ExecuteCommand(MarketAPI.AddProduct, product);
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
                                ExecuteCommand(MarketAPI.UpdateProduct, obj);
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

        public RelayCommand ChooseImageCommand
        {
            get
            {
                return _chooseImageCommand ??
                    (_chooseImageCommand = new RelayCommand
                        (
                            obj =>
                            {
                                OpenFileDialog openFileDialog = new OpenFileDialog();
                                openFileDialog.Filter =
                                "Картинки (*.png;*.jpeg;*.jpg;*.gif)" +
                                "|*.png;*.jpeg;*.jpg;*.gif" +
                                "|Все файлы (*.*)|*.*";

                                if (openFileDialog.ShowDialog() == true)
                                    _localImageLocation = openFileDialog.FileName;
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
