using Xamarin.Forms;

namespace Journal
{

    public partial class App : Application
    {

        public const string DATABASE_NAME = "subjects.db";
        public static SubjectRepository database;
        public static SubjectRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new SubjectRepository(DATABASE_NAME);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#FF359886")
            };
            
        }
    }
}
