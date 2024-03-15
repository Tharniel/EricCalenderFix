using Kalender.Models;
using Kalender.Data;
namespace Kalender;

public partial class Registered : ContentPage
{
	public Registered()
	{
		InitializeComponent();
	}
    private async void ClickedRegistationCheck(object sender, EventArgs e)
    {

        var loginAuth = new LoginAuth();
        var username = MyInputName.Text.ToLower();
        var password = MyInputPassword.Text;

        bool auth = await loginAuth.SignUpAuthAsync(username, password);

        if (auth == false)
        {
            User newUser = new User
            {
                Name = MyInputName.Text,
                Email = MyInputEmail.Text,
                Password = MyInputPassword.Text,
                Location = MyInputCity.Text
            };
            var userCollection = DB.UserCollection();

            userCollection.InsertOne(newUser);
            await Navigation.PushAsync(new LoginPage());
        }
        else
        {
            Registration.Text = "Personen finns redan";
        }
    }
}