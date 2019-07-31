using RestSharp.Deserializers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAdminApp.Entities
{
    public class Product
    {
        private int _id;
        private string _name;
        private int _price;
        private string _imageExtension;
        private int _subCatalogID;

        [DeserializeAs(Name = "id")]
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        [DeserializeAs(Name = "name")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        [DeserializeAs(Name = "price")]
        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        [DeserializeAs(Name = "image_extension")]
        public string ImageExtension
        {
            get { return _imageExtension; }
            set
            {
                _imageExtension = value;
                OnPropertyChanged("ImageExtension");
            }
        }

        [DeserializeAs(Name = "sub_catalog_id")]
        public int SubCatalogID
        {
            get { return _subCatalogID; }
            set
            {
                _subCatalogID = value;
                OnPropertyChanged("SubCatalogID");
            }
        }

        public string ImagePath
        {
            get { return $"{MarketAPI.SERVER}images/{_id}.{_imageExtension}"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
