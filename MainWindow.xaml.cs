using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncTestInWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void btnLongRunningProcess_Click(object sender, RoutedEventArgs e) {
            // We don't even need dispatcher here
            IProgress<int> progress = new Progress<int>(i => lblProgressReport.Content = i.ToString());
            lblProgressReport.Content = "Progress started";
            await Task.Run(() =>
            {
                for (int i = 0; i < 10; i++) {
                    Thread.Sleep(1000);         // 10 cycles of a long-running operation
                    progress.Report(i);
                }
            });
            lblProgressReport.Content = "Done";
        }
    }
}
