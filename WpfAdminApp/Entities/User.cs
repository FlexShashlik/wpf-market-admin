using RestSharp.Deserializers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAdminApp.Entities
{
    public class User : INotifyPropertyChanged
    {
        private int _id;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _role;

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

        [DeserializeAs(Name = "email")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        [DeserializeAs(Name = "first_name")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        [DeserializeAs(Name = "last_name")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        [DeserializeAs(Name = "role")]
        public string Role
        {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged("Role");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
