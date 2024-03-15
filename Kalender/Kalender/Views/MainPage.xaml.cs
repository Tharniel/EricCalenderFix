namespace Kalender
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void OnClickedSignIn(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void OnClickedRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registered());
        }
    }

}
