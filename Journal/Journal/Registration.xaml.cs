using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Journal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        public Registration()
        {
            InitializeComponent();
            Appearing += (sender, e) =>
             {
                 if(App.Database.GetReg().Count()!=0)
                 {
                     entryEmail.Text = App.Database.GetReg().Last().Email;
                     entryName.Text = App.Database.GetReg().Last().Name;
                     entryPhone.Text = App.Database.GetReg().Last().Phone;
                 }
             };
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if(entryEmail.Text.Length!=0 && entryName.Text.Length!=0 && entryPhone.Text.Length!=0 &&entryEmail.Text.Contains("@"))
            {
                var reg = (Reg)BindingContext;
                App.Database.SaveReg(reg);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please fill the fields correctly", "Ok");
            }
        }
    }
}