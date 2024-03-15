using Kalender.Models;
using Kalender.ViewModels;

namespace Kalender;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
		ChangeSignName();
        GetApiData();
    }

	private void ChangeSignName()
	{
        var user = Singletons.Authorized.GetAuthStatus().WhoIsUser();
		WelcomeLabel.Text = "V�lkommen in " + user;
    }
    private async void GetApiData()
    {

        WeatherApi weather = await HomePageViewModel.GetWeather();
        if (weather != null)
        {
            TempLabel.Text = "Tempraturen �r: " + weather.temp + "�C";
            HumidityLabel.Text = "Fuktigheten �r: " + weather.humidity + "%";
        }

        string facts = await HomePageViewModel.GetDateFact();
        if (facts != null)
        {
            FactsLabel.Text = "Slumpm�ssig fakta: " + facts;
        }
    }

    private async void OnClickedCalender(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CalenderPage());
    }

    private async void OnClickedCreateEvent(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateEventPage());
    }
}