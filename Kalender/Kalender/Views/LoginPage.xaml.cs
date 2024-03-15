using Kalender.Data;
using MongoDB.Driver;

namespace Kalender;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void ClickedSignInCheck(object sender, EventArgs e)
    {
        var users = await DB.GetUserFromDB(MyInputName.Text);
        var userLocation = users.FirstOrDefault().Location;

        var loginAuth = new LoginAuth();
        var username = MyInputName.Text.ToLower();
        var password = MyInputPassword.Text;
        var location = userLocation;

        bool auth = await loginAuth.LoginAuthAsync(username, password);

        if (auth == true)
        {
            Singletons.Authorized authStatus = Singletons.Authorized.GetAuthStatus();
            authStatus.SetUserLoggedIn(true, username, location);
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            SignIn.Text = "Fel information, försök igen";
        }

    }
}