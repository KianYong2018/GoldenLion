using GoldenLion.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GoldenLion
{
    class MainPageViewModel : BaseViewModel
    {
        //This is just as an example. Do not create a static property in your ViewModel, for other ViewModels to communicate with, in a production app.
        public static List<SelectableData<UserAccount>> SelectedData { get; set; } //Need to change this, once I understand

        public List<SelectableData<UserAccount>> DataSource { get; set; }

        public MainPageViewModel()
        {
            //Load Data (For now I put static data)
            SelectedData = new List<SelectableData<UserAccount>>()
            {
                new SelectableData<UserAccount>() {Data = new UserAccount() {IdUserAccount = "szdxfcgvhbjknlm", AndroidIOS = false, Deleted = false, Name = "Test", Password="2sedtryvubh", Role="CIC", Username="Test", UWP = false } },
                new SelectableData<UserAccount>() {Data = new UserAccount() {IdUserAccount = "aesrxtfcgh", AndroidIOS = false, Deleted = false, Name = "Test2", Password="dtyuvyhbjnlkm", Role="CIC", Username="Test2", UWP = false} },
                new SelectableData<UserAccount>() {Data = new UserAccount() {IdUserAccount = "dfcvgybhunjm", AndroidIOS = false, Deleted = false, Name = "Test3", Password="123456789", Role="CIC", Username="Test3", UWP = false} }
            };
        }

        public void OnAppearing()
        {
            DataSource = SelectedData.Where(x => x.Selected).ToList();
            OnPropertyChanged(nameof(DataSource));
        }
    }
}
