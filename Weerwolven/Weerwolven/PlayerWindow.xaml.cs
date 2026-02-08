using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Weerwolven.Models;

namespace Weerwolven
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        private Player _player;
        private MainWindow _parent;

        public PlayerWindow(Player player, MainWindow parent)
        {
            InitializeComponent();
            this._player = player;
            this._parent = parent;

            InitializeUI();
        }

        private void InitializeUI()
        {
            nameTextBlock.Text = _player.Name;
            roleTextBlock.Text = _player.Role.ToString();
            roleImage.Source = new BitmapImage(new Uri(_player.Role.ImageSource, UriKind.Relative));
            UpdateUI();
        }

        private void UpdateUI()
        {
            roleTextBlock.Visibility = _player.IsRevealed ? Visibility.Visible : Visibility.Hidden;
            showButton.Content = _player.IsRevealed ? "Verberg rol" : "Toon rol";
            roleImage.Visibility = _player.IsRevealed ? Visibility.Visible : Visibility.Hidden;

            if (_player.IsDead)
            {
                card.Opacity = 0.4;
                this.Title = $"{_player.Name} (Dood)";
            }
            _parent.UpdateGame();
        }

        private void Reveal_Click(object sender, RoutedEventArgs e)
        {
            if (_player.IsRevealed)
            {
                _player.IsRevealed = false;
                UpdateUI();
            }
            else
            {
                _player.IsRevealed = true;
                UpdateUI();
            }
        }

        private void Die_Click(object sender, RoutedEventArgs e)
        {
            _player.IsDead = true;
            UpdateUI();
        }
    }
}