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
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += Timer_Tick;

        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("HH:mm:ss");
            //LBLTime.Content = time;
            //LED1.Fill = new SolidColorBrush(Colors.Red);
            //LED2.Fill = new SolidColorBrush(Colors.Red);
            //LED3.Fill = new SolidColorBrush(Colors.Red);
            //LED4.Fill = new SolidColorBrush(Colors.Red);

        }
        private void Button_Start(object sender, RoutedEventArgs e)
        {
            Controls.Visibility = Visibility.Hidden;
            Start3.Visibility = Visibility.Visible;


        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            Controls.Visibility = Visibility.Hidden;
            Settingsbox.Visibility = Visibility.Visible;
        }

        private void Button_Back (object sender, RoutedEventArgs e)
        {
          Controls.Visibility = Visibility.Visible;
          Start3.Visibility = Visibility.Hidden;
          Settingsbox.Visibility = Visibility.Hidden;
        }
    }
}