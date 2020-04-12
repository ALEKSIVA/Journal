using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Journal
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Appearing += (sender, e) =>
            {
                if(App.Database.GetReg().Count()!=0)
                {
                    Title = "Debt notes" + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " 
                    + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + App.Database.GetReg().Last().Name;
                }
            };
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Subj());
        }

        private async void Button4_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Stat());
        }

        private async void Button5_Clicked(object sender, EventArgs e)
        {
            bool result2 = await DisplayAlert("Confirm action", "Do you really want to exit?", "Yes", "No");
            if (result2)
            {
                Environment.Exit(0);
            }
        }

        private async void Button6_Clicked(object sender, EventArgs e)
        {
            Reg reg = new Reg();
            Registration regPage = new Registration();
            regPage.BindingContext = reg;
            await Navigation.PushAsync(regPage);
        }
    }
}
