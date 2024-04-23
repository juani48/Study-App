using System.Data;

namespace AppEstudio1;

public partial class FlowTimePage : ContentPage
{
    private bool ok = false;
    private bool pause = true;
    private bool first = true;
    private bool isState = false;

    public FlowTimePage()
	{
		InitializeComponent();
	}
    private async void Start_Stop(object sender, EventArgs e)
    {
        isState = !isState;

        btn_State.Text = isState ? "Tiempo de Descanso" : "Timepo de Actividad";
        state.Text = isState ? "En Actividad" : "En Descanso";

        if (first)
        {
            first = false;
            ok = true;
            pause =  true;
            btn_Pause.Text = "Pausar";
            RunTime();
        }
        else
        {
            ok = false;            
            pause = true;
            btn_Pause.Text = "Pausar";
            timeMinutes.Text = "00";
            timeSeconds.Text = "00";
            await Task.Delay(TimeSpan.FromSeconds(1));
            ok = true;
            RunTime();
        }
        
    }
    private void State(object sender, EventArgs e)
    {
        pause = !pause;
        btn_Pause.Text = (pause) ? "Pausar" : "Reanudar";
    }

    private void SetTime(TimeOnly time)
    {
        timeMinutes.Text = (time.Minute < 10) ? "0" + time.Minute.ToString() : time.Minute.ToString();
        timeSeconds.Text = (time.Second < 10) ? "0" + time.Second.ToString() : time.Second.ToString();
        
    }
    private void RestartTime(object sender, EventArgs e)
    {
        ok = false;
        first = true;
        pause = true;

        timeMinutes.Text = "00";
        timeSeconds.Text = "00";
        btn_State.Text = "Iniciar sesion";
        state.Text = "Sin Iniciar";
        btn_Pause.Text = "Pausar";
    }

    private async void RunTime()
    {
        TimeOnly time = new();

        while (ok)
        {
            if(pause)
            {
                time = time.Add(TimeSpan.FromSeconds(1)); 
                SetTime(time);

            }
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}