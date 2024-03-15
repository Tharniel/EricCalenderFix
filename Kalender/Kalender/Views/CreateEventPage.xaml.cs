using Kalender.Data;
using Kalender.Views;

namespace Kalender;

public partial class CreateEventPage : ContentPage
{
	public CreateEventPage()
	{
		InitializeComponent();
	}

    private void ClickedMakeTask(object sender, EventArgs e)
    {
        try
        {
            var user = Singletons.Authorized.GetAuthStatus().WhoIsUser();
            DateTime StartDate = MyDatePicker.Date;
            StartDate = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, 12, 0, 0);

            if (RecurringCheckBox.IsChecked == true)
            {
                var recurringFrequency = RecurrencePicker.SelectedItem.ToString();
                var recurringRate = int.Parse(NumberEntry.Text);
                var totalEvents = int.Parse(NumberOfEventsEntry.Text);
                for (int i = 0; i < totalEvents; i++)
                {
                    DateTime nextEventDate;
                    switch (recurringFrequency)
                    {
                        case "Dagar":
                            nextEventDate = StartDate.AddDays(recurringRate * i);
                            break;
                        case "Veckor":
                            nextEventDate = StartDate.AddDays(7 * recurringRate * i);
                            break;
                        case "Månader":
                            nextEventDate = StartDate.AddMonths(recurringRate * i);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }

                    CreateEvent(nextEventDate, user);
                }
            }
            else
            {
                CreateEvent(StartDate, user);
            }

            Navigation.PushAsync(new HomePage());
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Något gick fel med frekvensväljaren.");
        }
        catch (Exception)
        {
            Console.WriteLine("Något gick fel.");
        }
        finally
        {
            Console.WriteLine("Skickas tillbaka till homepage");
            Navigation.PushAsync(new HomePage());
        }
    }

    private void CreateEvent(DateTime startDate, string user)
    {
        Models.Event newTask = new Models.Event
        {
            Title = MyInputTask.Text,
            Description = MyInputDescription.Text,
            StartDate = startDate,
            Username = user,
        };
        var EventCollection = DB.TaskCollection();
        EventCollection.InsertOne(newTask);
    }

    private void OnRecurringCheckBoxChecked(object sender, CheckedChangedEventArgs e)
    {
        bool isChecked = e.Value;
        NumberOfEventsEntry.IsVisible = isChecked;
        RecurrencePicker.IsVisible = isChecked;
        NumberEntry.IsVisible = isChecked;
    }
}