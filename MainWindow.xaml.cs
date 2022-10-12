using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncTestInWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        CancellationTokenSource cts;

        public MainWindow() {
            InitializeComponent();
            cts = new CancellationTokenSource();
        }

        private async void btnLongRunningProcess_Click(object sender, RoutedEventArgs e) {
            // We don't even need dispatcher here
            IProgress<int> progress = new Progress<int>(i => lblProgressReport.Content = i.ToString());
            lblProgressReport.Content = "Progress started";

            try {
                await Task.Run(() =>
                {
                    for (int i = 0; i < 10; i++) {
                        Thread.Sleep(1000);
                        progress.Report(i);
                        if (cts.Token.IsCancellationRequested) {
                            // Do some cleanup work here if required
                        }
                        cts.Token.ThrowIfCancellationRequested();
                    }
                }, cts.Token);  // Passing this token to task is optional, but it is useful: https://stackoverflow.com/questions/48312544/whats-the-benefit-of-passing-a-cancellationtoken-as-a-parameter-to-task-run
                lblProgressReport.Content = "Done";
            }
            catch (Exception ex) {
                lblProgressReport.Content = "Canceled";
            }
        }

        private void btnLongRunningProcessStop_Click(object sender, RoutedEventArgs e) {
            cts.Cancel();
        }
    }
}
