using System;
using Xamarin.Forms;
using Plugin.Messaging;
using Xamarin.Essentials;
using System.Linq;

namespace Journal
{

    public partial class Subj : ContentPage
    {
        public Subj()
        {
            InitializeComponent();
            Appearing += (sender, e) =>
            {
                if (App.Database.GetReg().Count() != 0)
                {
                    Title = "Debts" + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " "
                    + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + App.Database.GetReg().Last().Name;
                }
            };
        }

        protected override void OnAppearing()
        {
            DebtList.ItemsSource = App.Database.GetItems();
            base.OnAppearing();
        }

        private async void DebtList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Subjects selectedSubjects = (Subjects)e.SelectedItem;
            SubjectPage subjectPage = new SubjectPage();
            subjectPage.BindingContext = selectedSubjects;
            await Navigation.PushAsync(subjectPage);
           
        }

        private async void Bt1_Clicked(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            SubjectPage subjectPage = new SubjectPage();
            subjectPage.BindingContext = subjects;
            await Navigation.PushAsync(subjectPage);
        }
    }
}