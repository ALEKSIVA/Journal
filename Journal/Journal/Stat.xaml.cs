using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;

namespace Journal
{

    public partial class Stat : ContentPage
    {
        public Stat()
        {
            InitializeComponent();
            Appearing += (sender, e) =>
            {
                Statistic.LessonsCount = 0;
                Statistic.grades.Clear();
                if (App.Database.GetItems().Count() != 0)
                {
                    if(App.Database.GetReg().Count()!=0)
                    {
                        Title = "Statistic" + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " "
                    + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + " " + App.Database.GetReg().Last().Name;
                    }
                    btnShare.Text = "Share";
                    foreach (var item in App.Database.GetItems())
                    {
                        Statistic.grades.Add(item.Grade);
                        Statistic.LessonsCount += item.Lessons;
                    }
                    var list = App.Database.GetItems().OrderByDescending(x => x.dateGet);
                    List<DateTime> dates = new List<DateTime>();
                    foreach (var item in list)
                    {
                        dates.Add(item.dateGet);
                    }
                    Statistic.lastGradeDate = App.Database.GetItems().ToList().Find(x => x.dateGet == dates.First()).Grade;
                    Statistic.debtAmount = App.Database.GetItems().Where(x => x.Grade <= 2).Count();
                    Statistic.lastGrade = App.Database.GetItems().Last().Grade;
                    Statistic.getAverage();
                    lblAverage.IsVisible = true;
                    lblDebtAmount.IsVisible = true;
                    lblLastGrade.IsVisible = true;
                    lblLessons.IsVisible = true;
                    lblAverage.Text = "Grade point average:   " + Statistic.GradeAverage.ToString();
                    lblDebtAmount.Text = "Debt amount:   " + Statistic.debtAmount.ToString();
                    lblLastGradeDate.Text = "Latest grade by date " + Statistic.lastGradeDate.ToString();
                    lblLastGrade.Text = "Latest grade:" + Statistic.lastGrade.ToString();
                    lblLessons.Text = "Amount of lessons:  " + Statistic.LessonsCount.ToString();
                }
                else
                {
                    lblAverage.IsVisible = false;
                    lblDebtAmount.IsVisible = false;
                    lblLastGrade.IsVisible = false;
                    lblLessons.IsVisible = false;
                    lblLastGradeDate.Text = "To see statistics create a record";
                    btnShare.Text = "Create record";
                }
            };
        }
        
        async void Button_Clicked(object sender, EventArgs e)
        {
            if(lblLastGradeDate.Text== "To see statistics create a record")
            {
                Subjects subjects = new Subjects();
                SubjectPage subjectPage = new SubjectPage();
                subjectPage.BindingContext = subjects;
                await Navigation.PushAsync(subjectPage);
            }
            else
            {
                if(App.Database.GetReg().Count()!=0)
                {
                    var actionSheet = await DisplayActionSheet("What way do you want to share your statistic?",
                            "Cancel", "", "By SMS", "By Email", "By other application");
                    switch (actionSheet)
                    {
                        default:
                            break;
                        case "By SMS":
                            var smsMessanger = CrossMessaging.Current.SmsMessenger;
                            if (smsMessanger.CanSendSms)
                            {
                                smsMessanger.SendSms(App.Database.GetReg().Last().Phone, "My statistic in debt notes app.\n" + "\n" +
                                     lblAverage.Text + "\n" +
                                     lblDebtAmount.Text + "\n" +
                                     lblLessons.Text + "\n" +
                                     lblLastGradeDate.Text + "\n" +
                                     lblLastGrade.Text);
                            }
                            break;
                        case "By Email":
                            var emailMessenger = CrossMessaging.Current.EmailMessenger;
                            if (emailMessenger.CanSendEmail)
                            {
                                emailMessenger.SendEmail(App.Database.GetReg().Last().Email, "My statistic in debt notes app.\n" + "\n" +
                                     lblAverage.Text + "\n" +
                                     lblDebtAmount.Text + "\n" +
                                     lblLessons.Text + "\n" +
                                     lblLastGradeDate.Text + "\n" +
                                     lblLastGrade.Text);
                            }
                            break;
                        case "By other application":
                            await Share.RequestAsync(new ShareTextRequest
                            {
                                Text =
                                     "My statistic in debt notes app.\n" + "\n" +
                                     lblAverage.Text + "\n" +
                                     lblDebtAmount.Text + "\n" +
                                     lblLessons.Text + "\n" +
                                     lblLastGradeDate.Text + "\n" +
                                     lblLastGrade.Text,
                                Title = "Share",
                            });
                            break;
                    }
                }
                else
                {
                    await DisplayAlert("Error", "To share enter your data in the page \"My data\"", "Ok");
                }
            }
        }
    }
}