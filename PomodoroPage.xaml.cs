using System.ComponentModel;
using System.Diagnostics;

namespace AppEstudio1;

public partial class PomodoroPage : ContentPage
{
	private bool ok = false;
	private bool first = true;
	private bool pause = false;

	private string msj = "Empezar";

	private int numMinutes;
	private int numSeconds;

	private int totalMinutes;

    public PomodoroPage()
	{
		InitializeComponent();
	}

    private void OnStartStop(object sender, EventArgs e)
    {
        ok = !ok;
        btn_StartStop.Text = ok ? "Detener" : msj;

        if (first)
        {
            numMinutes = (timeMinutes.Text == "00") ? 24 : int.Parse(timeMinutes.Text) - 1;
            first = false;
            msj = "Continuar";
            totalMinutes = (numMinutes % 2 == 0) ? numMinutes + 1 : numMinutes;
        }
        RunTime();
    }

    private void SetTimeLabels()
	{
        timeMinutes.Text = (numMinutes < 10) ? "0" + numMinutes.ToString() : numMinutes.ToString();

        timeSeconds.Text = (numSeconds < 10) ? "0" + numSeconds.ToString() : numSeconds.ToString();
    }
	private void SetTime(TimeOnly time)
	{	
        numSeconds--;

        if ((numSeconds == -1) & (numMinutes <= 0))
        {
			if(!pause)
			{
                numMinutes = (totalMinutes / 5) - 1;
                numSeconds = 59;
				pause = true;
            }
			else
			{
				numMinutes = totalMinutes - 1;
				numSeconds = 59;
				pause = false;
			}
            
        }

        else if (numSeconds == -1 )
        {
            numMinutes = int.Parse(timeMinutes.Text) - 1;
            numSeconds = 59;
        }

		SetTimeLabels();
    }

    private void RestartTime(object sender, EventArgs e)
    {
        ok = false;
        first = true;

		numMinutes = 0;
		numSeconds = 0;
		totalMinutes = 0;

        timeMinutes.Text = "00";
        timeSeconds.Text = "00";
		btn_StartStop.Text = "Empezar";
    }

    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
		if(first)
		{
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                timeMinutes.Text = (string)picker.ItemsSource[selectedIndex];
            }
        }
    }

	private async void RunTime()
	{
        TimeOnly time = new();

        while (ok)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            SetTime(time);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}