namespace Kalender.Views;

public partial class ErrorPage : ContentPage
{
	public ErrorPage()
	{
		InitializeComponent();
	}

    private async void OnClickedHomePage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
}