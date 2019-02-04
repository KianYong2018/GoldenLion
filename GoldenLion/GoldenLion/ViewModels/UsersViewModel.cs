using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace GoldenLion.ViewModels
{
    class UsersViewModel : INotifyPropertyChanged
    {
        UserAccountManager UserAccountManager;
        public UsersViewModel(IEnumerable<UserAccount> userAccounts)
        {
            UserAccountManager = UserAccountManager.DefaultUserAccount;
            
            var pokemons = new List<UserAccount>(userAccounts);
            Users = new ObservableCollection<SelectableItemWrapper<UserAccount>>(pokemons.Select(pk => new SelectableItemWrapper<UserAccount> { Item = pk }));
        }

        private ObservableCollection<SelectableItemWrapper<UserAccount>> _Users;

        public ObservableCollection<SelectableItemWrapper<UserAccount>> Users
        {
            get { return _Users; }
            set { _Users = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<UserAccount> _selectedUsers;
        public ObservableCollection<UserAccount> SelectedUsers
        {
            get { return _selectedUsers; }
            private set { _selectedUsers = value; RaisePropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<UserAccount> GetSelectedUsers()
        {
            var selected = Users.Where(p => p.IsSelected).Select(p => p.Item).ToList();
            return new ObservableCollection<UserAccount>(selected);
        }

        void SelectAll(bool select)
        {
            foreach(var p in Users)
            {
                p.IsSelected = select;
            }
        }

        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
