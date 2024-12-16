using System.IO.Ports;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using PROJECT_PILLENDOOS;
namespace PROJECT_PILLENDOOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
     public partial class MainWindow : Window
    {
        private bool check = false;
        public List<string> Minutes { get; set; }
        public List<string> Hours { get; set; }

        SerialPort _serialPort;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Start();
            timer.Tick += Timer_Tick;
  
            _serialPort = new SerialPort();
            _serialPort.BaudRate = 9600;
            _serialPort.DataBits = 8;
            // voor mijn Seconde lijst

            Minutes = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                Minutes.Add(i.ToString("D2")); // Format numbers as two-digit strings
                if(i == 59)
                {
                    Minutes.Add("None");
                    // make the box select none
                    Mbox1.SelectedIndex = 60;
                    Mbox2.SelectedIndex = 60;
                    Mbox3.SelectedIndex = 60;
                    Mbox4.SelectedIndex = 60;
                }
            }

            // voor mijn Uur lijst
            Hours = new List<string>();
            for (int i = 0; i <= 23; i++)
            {
                Hours.Add(i.ToString("D2")); // Format numbers as two-digit strings
                if (i == 23)
                {
                    Hours.Add("None");
                    // make the box select none
                    Hbox1.SelectedIndex = 24;
                    Hbox2.SelectedIndex = 24;
                    Hbox3.SelectedIndex = 24;
                    Hbox4.SelectedIndex = 24;
                }
            }
            // Bind the data context for the ComboBox
            this.DataContext = this;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {

            
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("HH:mm");
            LBLTime.Content = time;
            check = !check;

            if (!check)
            {
                AlarmProcessor _alarmProcessor = new AlarmProcessor(SendSerialData);
                _alarmProcessor.Process(Hbox1, Mbox1, S1?.IsChecked == true, 1);
                _alarmProcessor.Process(Hbox2, Mbox2, S2?.IsChecked == true, 2);
                _alarmProcessor.Process(Hbox3, Mbox3, S3?.IsChecked == true, 3);
                _alarmProcessor.Process(Hbox4, Mbox4, S4?.IsChecked == true, 4);
            }
            else
            {
                byte data = 0;
                SendSerialData(data);
            }
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            Controls.Visibility = Visibility.Hidden;
            StartGroupBox.Visibility = Visibility.Visible;
        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Controls.Visibility = Visibility.Hidden;
            Settingsbox.Visibility = Visibility.Visible;
        }

        private void Button_Back (object sender, RoutedEventArgs e)
        {
          Controls.Visibility = Visibility.Visible;
          StartGroupBox.Visibility = Visibility.Hidden;
          Settingsbox.Visibility = Visibility.Hidden;
        }

        private void cbxComPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();
                if (cbxComPorts.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = cbxComPorts.SelectedItem.ToString();
                    _serialPort.Open();
                }
            }
        }

        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            cbxComPorts.SelectionChanged -= cbxComPorts_SelectionChanged;
            cbxComPorts.IsEnabled = false;
            cbxComPorts.Items.Clear();
            cbxComPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxComPorts.Items.Add(s);
            cbxComPorts.SelectedIndex = 0; 
            cbxComPorts.IsEnabled = true;
            cbxComPorts.SelectionChanged += cbxComPorts_SelectionChanged;
            SendSerialData(0);
            _serialPort.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Settingsbox.Visibility = Visibility.Hidden;
            StartGroupBox.Visibility = Visibility.Hidden;
            Controls.Visibility = Visibility.Visible; 
        }

        private void Button_C1(object sender, RoutedEventArgs e)
        {
            if (Hbox1.IsEnabled)
            {
                Hbox1.IsEnabled = false;
                Mbox1.IsEnabled = false;
                S1.IsEnabled = false;
            }
            else
            {
                Hbox1.IsEnabled = true;
                Mbox1.IsEnabled = true;
                S1.IsEnabled = true;
            }
        }

        private void Button_C2(object sender, RoutedEventArgs e)
        {
            if (Hbox2.IsEnabled)
            {
                Hbox2.IsEnabled = false;
                Mbox2.IsEnabled = false;
                S2.IsEnabled = false;
            }
            else
            {
                Hbox2.IsEnabled = true;
                Mbox2.IsEnabled = true;
                S2.IsEnabled = true;
            }
        }

        private void Button_C3(object sender, RoutedEventArgs e)
        {
            if (Hbox3.IsEnabled)
            {
                Hbox3.IsEnabled = false;
                Mbox3.IsEnabled = false;
                S3.IsEnabled = false;
            }
            else
            {
                Hbox3.IsEnabled = true;
                Mbox3.IsEnabled = true;
                S3.IsEnabled = true;
            }
        }

        private void Button_C4(object sender, RoutedEventArgs e)
        {
            if (Hbox4.IsEnabled)
            {
                Hbox4.IsEnabled = false;
                Mbox4.IsEnabled = false;
                S4.IsEnabled = false;
            }
            else
            {
                Hbox4.IsEnabled = true;
                Mbox4.IsEnabled = true;
                S4.IsEnabled = true;
            }
        }

        private void SendSerialData(byte data)
        {
            if ((_serialPort != null) && (_serialPort.IsOpen))
            {
                _serialPort.Write(new byte[] { data }, 0, 1);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SendSerialData(0);
            _serialPort.Close();
            _serialPort.Dispose();
        }
    }
}