using Plugin.Messaging;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Journal
{

    public partial class SubjectPage : ContentPage
    {
        public SubjectPage()
        {
            InitializeComponent();
            Appearing += (sender, e) =>
            {
                if (App.Database.GetReg().Count() != 0)
                {
                    Title = "Your debt info" + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + 
                    " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " +
                    App.Database.GetReg().Last().Name;
                }
            };
        }

        private async void Button_Clicked2(object sender, EventArgs e)
        {
            var subjects = (Subjects)BindingContext;
            App.Database.DeleteItem(subjects.Id);
            await Navigation.PopAsync(true);
        }

        private async void Button_Clicked3(object sender, EventArgs e)
        {
            if (subjectEntry.Text.Length != 0 && gradeEntry.Text.All(ch => char.IsNumber(ch))
            && gradeEntry.Text.Length <= 3 && gradeEntry.Text.Length != 0 &&
            lessonsEntry.Text.All(ch => char.IsNumber(ch)) && lessonsEntry.Text.Length != 0)
            {
                var subjects = (Subjects)BindingContext;
                App.Database.SaveItem(subjects);
                await Navigation.PopAsync(true);
            }
            else
            {
                await DisplayAlert("Error", "Please fill the fields correctly", "Ok");
            }
        }
            async void Button_Clicked(object sender, EventArgs e)
            {
                var subjects = (Subjects)BindingContext;
                            var share = await DisplayActionSheet("Which way do you want to share?",
                   "Cancel", "", "By SMS",
                 "by Email", "By another application");
                switch (share)
                {
                    case "By SMS":
                        var smsMessanger = CrossMessaging.Current.SmsMessenger;
                        if (smsMessanger.CanSendSms)
                        {
                            smsMessanger.SendSms(App.Database.GetReg().Last().Phone, "Info about my debt in debt notes app.\n" +
                                "Subject " + subjects.Subjectt + "\n" +
                                "Lessons amount " + subjects.Lessons + "\n" +
                                "Grade " + subjects.Grade + "\n" +
                                "Debt received " + subjects.dateGet + "\n" +
                                "Debt must be fixed " + subjects.dateToDo);
                        }
                        break;
                    case "By Email":
                        var emailMessenger = CrossMessaging.Current.EmailMessenger;
                        if (emailMessenger.CanSendEmail)
                        {
                            emailMessenger.SendEmail(App.Database.GetReg().Last().Email, "Info about my debt in debt notes app.\n" +
                                "Subject " + subjects.Subjectt + "\n" +
                                "Lessons amount " + subjects.Lessons + "\n" +
                                "Grade " + subjects.Grade + "\n" +
                                "Debt received " + subjects.dateGet + "\n" +
                                "Debt must be fixed " + subjects.dateToDo);
                        }
                        break;
                    case "By other application":
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Text =
                                 "Info about my debt in debt notes app.\n" +
                                "Subject " + subjects.Subjectt + "\n" +
                                "Lessons amount " + subjects.Lessons + "\n" +
                                "Grade " + subjects.Grade + "\n" +
                                "Debt received " + subjects.dateGet + "\n" +
                                "Debt must be fixed " + subjects.dateToDo,
                            Title = "Share",
                        });
                        break;
                    default:
                        break;
                }
            }
        }
    }
