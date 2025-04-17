using System;
using System.Drawing;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AlarmApp
{
    public partial class Form1 : Form
    {
        private DateTime targetTime;
        private System.Windows.Forms.Timer timer;
        private Random rand;
        public event EventHandler raiseAlarm;

        public Form1()
        {
            InitializeComponent();

            rand = new Random();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;

            raiseAlarm += Ring_alarm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(textBox1.Text, out targetTime))
            {
                timer.Start();
            }
            else
            {
                MessageBox.Show("Please enter time in HH:MM:SS format.");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Change background color every tick
            this.BackColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));

            DateTime now = DateTime.Now;
            if (now.Hour == targetTime.Hour && now.Minute == targetTime.Minute && now.Second == targetTime.Second)
            {
                timer.Stop();
                raiseAlarm?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Ring_alarm(object sender, EventArgs e)
        {
            MessageBox.Show("⏰ Alarm! Time matched!");
        }
    }
}