using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncTestInWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly System.Timers.Timer timer;
        private int progress = default(int);
        public MainWindow() {
            InitializeComponent();
            timer = new System.Timers.Timer(500);
            timer.Start();
        }

        private async void btnLongRunningProcess_Click(object sender, RoutedEventArgs e) {
            progress = 1;
            lblProgressReport.Content = "Progress started";
            timer.Elapsed += Timer_Elapsed;
            await Task.Run(() =>
            {
                // long running process (like merging contract PDFs)
                Thread.Sleep(5000);
            });
            lblProgressReport.Content = "Done";
            timer.Elapsed -= Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) {
            Dispatcher.BeginInvoke(() => lblProgressReport.Content = progress++);
        }
    }
}
