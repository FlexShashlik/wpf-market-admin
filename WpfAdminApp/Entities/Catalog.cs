using RestSharp.Deserializers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAdminApp.Entities
{
    public class Catalog : INotifyPropertyChanged
    {
        private int _id;
        private string _name;

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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
