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

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoWindow _videoWindow;

        public MainWindow()
        {
            InitializeComponent();
            videoListBox.Items.Add("Videos/video1.mp4");
            videoListBox.Items.Add("Videos/video2.mp4");
            videoListBox.Items.Add("Videos/video3.mp4");

        }

        private void videoListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (videoListBox.SelectedItem == null)
                return;

            string selectedVideo = videoListBox.SelectedItem.ToString();

            if (newWindowCheckBox.IsChecked == true)
            {
                // Always open a new window
                VideoWindow newWindow = new VideoWindow(selectedVideo);
                newWindow.Show();
            }
            else
            {
                // Reuse existing window if possible
                if (_videoWindow == null || !_videoWindow.IsVisible)
                {
                    _videoWindow = new VideoWindow(selectedVideo);
                    _videoWindow.Show();
                }
                else
                {
                    _videoWindow.LoadVideo(selectedVideo);
                    _videoWindow.Activate();
                }
            }
        }
    }
}