using System;
using System.Timers;

public class AlarmEventArgs : EventArgs
{
    public DateTime AlarmTime { get; set; }
}

public class Alarm
{
    private DateTime targetTime;
    private System.Timers.Timer timer;

    public event EventHandler<AlarmEventArgs> RaiseAlarm;

    public Alarm(DateTime time)
    {
        targetTime = time;
        timer = new System.Timers.Timer(1000); // every second
        timer.Elapsed += CheckTime;
        timer.Start();
    }

    private void CheckTime(object sender, ElapsedEventArgs e)
    {
        if (DateTime.Now.Hour == targetTime.Hour &&
            DateTime.Now.Minute == targetTime.Minute &&
            DateTime.Now.Second == targetTime.Second)
        {
            timer.Stop();
            OnRaiseAlarm(new AlarmEventArgs { AlarmTime = targetTime });
        }
    }

    protected virtual void OnRaiseAlarm(AlarmEventArgs e)
    {
        RaiseAlarm?.Invoke(this, e);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter target time (HH:MM:SS):");
        string input = Console.ReadLine();
        DateTime targetTime;

        while (!DateTime.TryParseExact(input, "HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out targetTime))
        {
            Console.WriteLine("Invalid format. Please enter time in HH:MM:SS format:");
            input = Console.ReadLine();
        }

        Alarm alarm = new Alarm(targetTime);
        alarm.RaiseAlarm += Ring_alarm;

        Console.WriteLine("Waiting for the alarm...");
        Console.ReadLine(); // keep app running
    }

    static void Ring_alarm(object sender, AlarmEventArgs e)
    {
        Console.WriteLine($"⏰ Alarm! It's {e.AlarmTime:HH:mm:ss} now!");
    }
}