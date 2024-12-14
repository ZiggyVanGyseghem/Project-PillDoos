using System.IO.Ports;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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

            // voor mijn Seconde lijst
            Minutes = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                Minutes.Add(i.ToString("D2")); // Format numbers as two-digit strings
            
            }
            // voor mijn Uur lijst
            Hours = new List<string>();
            for (int i = 1; i <= 24; i++)
            {
                Hours.Add(i.ToString("D2")); // Format numbers as two-digit strings
            }

            // Bind the data context for the ComboBox
            this.DataContext = this;

         


        }

        



        private void Timer_Tick(object? sender, EventArgs e)
        {
            
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("HH:mm");
            int minute = currentTime.Minute;
            int hour = currentTime.Hour;
            LBLTime.Content = time;

            check = !check;

           
            if (!check)
            {
                byte data = 0;
                if ((_serialPort != null) && (_serialPort.IsOpen))
                    _serialPort.Write(new byte[] { data }, 0, 1);
                return; 
            }
            // dont forget to addd the minutes back

            if (check == true )
            {
                if (hour == Convert.ToInt32(Hbox1.SelectedItem))
                {
                    SendSerialData(1);
                    SendSerialData(5);
                }

                if (hour == Convert.ToInt32(Hbox2.SelectedItem))
                {
                    SendSerialData(2);
                    SendSerialData(5);
                }

                if (hour == Convert.ToInt32(Hbox3.SelectedItem))
                {
                    SendSerialData(3);
                    SendSerialData(5);
                }

                if (hour == Convert.ToInt32(Hbox4.SelectedItem))
                {
                    SendSerialData(4);
                    SendSerialData(5);
                }
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
            // Temporarily disable SelectionChanged event
            cbxComPorts.SelectionChanged -= cbxComPorts_SelectionChanged;

            // Refresh ComboBox
            cbxComPorts.IsEnabled = false;
            cbxComPorts.Items.Clear();

            // Populate ComboBox with available COM ports
            cbxComPorts.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxComPorts.Items.Add(s);

            cbxComPorts.SelectedIndex = 0; // Default selection to "None"
            cbxComPorts.IsEnabled = true;

            // Re-enable SelectionChanged event
            cbxComPorts.SelectionChanged += cbxComPorts_SelectionChanged;

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
            }
            else
            {
                Hbox1.IsEnabled = true;
                Mbox1.IsEnabled = true;
            }
        }

        private void Button_C2(object sender, RoutedEventArgs e)
        {
            if (Hbox2.IsEnabled)
            {
                Hbox2.IsEnabled = false;
                Mbox2.IsEnabled = false;
            }
            else
            {
                Hbox2.IsEnabled = true;
                Mbox2.IsEnabled = true;
            }
        }

        private void Button_C3(object sender, RoutedEventArgs e)
        {
            if (Hbox3.IsEnabled)
            {
                Hbox3.IsEnabled = false;
                Mbox3.IsEnabled = false;
            }
            else
            {
                Hbox3.IsEnabled = true;
                Mbox3.IsEnabled = true;
            }
        }

        private void Button_C4(object sender, RoutedEventArgs e)
        {
            if (Hbox4.IsEnabled)
            {
                Hbox4.IsEnabled = false;
                Mbox4.IsEnabled = false;
            }
            else
            {
                Hbox4.IsEnabled = true;
                Mbox4.IsEnabled = true;
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            SendSerialData(0);
        }
        private void SendSerialData(byte data)
        {
            if ((_serialPort != null) && (_serialPort.IsOpen))
            {
                _serialPort.Write(new byte[] { data }, 0, 1);
            }
        }
    }
}