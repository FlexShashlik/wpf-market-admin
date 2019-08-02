using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfAdminApp.Entities;

namespace WpfAdminApp.ViewModels
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        private User _selectedUser;
        private RelayCommand _removeCommand, _applyCommand;

        public ObservableCollection<User> Users { get; set; }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        public UsersViewModel()
        {
            MarketAPI.GetUsers(out List<User> users, out bool result);

            if (result && users != null)
                Users = new ObservableCollection<User>(users);
        }

        #region Commands

        public RelayCommand ApplyCommand
        {
            get
            {
                return _applyCommand ??
                    (_applyCommand = new RelayCommand
                        (
                            obj =>
                            {
                                ExecuteCommand(MarketAPI.UpdateUser, obj);
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
                                ExecuteCommand(MarketAPI.DeleteUser, obj);
                            },
                            obj => Users?.Count > 0
                        )
                    );
            }
        }

        #endregion

        private void ExecuteCommand(Func<User, bool> apiMethod, object obj)
        {
            if (obj != null)
            {
                User user = obj as User;
                bool response = apiMethod(user);

                Users.Clear();
                MarketAPI.GetUsers(out List<User> users, out bool result);

                if (result && users != null)
                    users.ForEach(x => Users.Add(x));

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
