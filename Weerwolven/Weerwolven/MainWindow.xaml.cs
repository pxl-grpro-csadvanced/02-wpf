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
using Weerwolven.Models;

namespace Weerwolven
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Player> _players = new List<Player>();
        private List<PlayerWindow> _playerWindows = new List<PlayerWindow>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
                return;

            Player player = new Player
            {
                Name = nameTextBox.Text,
            };

            _players.Add(player);
            playersListBox.Items.Add(player);

            nameTextBox.Clear();
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            if (_players.Count < 5)
            {
                MessageBox.Show("Voeg meer spelers toe om het spel te kunnen starten", "Niet genoeg spelers");
            }
            else
            {
                AppointRolesToAllPlayers();

                foreach (Player player in _players)
                {
                    PlayerWindow window = new PlayerWindow(player, this);
                    _playerWindows.Add(window);
                    window.Show();
                    startGameButton.IsEnabled = false;
                    addPlayerButton.IsEnabled = false;
                }
            }
        }

        public void UpdateGame()
        {
            // Alle weerwolven zijn dood
            bool areAllWerewolfsDead = true;
            // Alle villagers zijn dood
            bool areAllVillagersDead = true;
            foreach (Player player in _players)
            {
                if (player.Role.Name == "Werewolf")
                {
                    areAllWerewolfsDead &= player.IsDead;
                }
                else
                {
                    areAllVillagersDead &= player.IsDead;
                }
            }
            if (areAllVillagersDead)
            {
                MessageBox.Show("De weerwolven hebben gewonnen", "Game over");
                startGameButton.IsEnabled = true;
                addPlayerButton.IsEnabled = true;
            }
            if (areAllWerewolfsDead)
            {
                MessageBox.Show("De burgers hebben gewonnen", "Game over");
                startGameButton.IsEnabled = true;
                addPlayerButton.IsEnabled = true;
            }
            if (areAllVillagersDead || areAllWerewolfsDead)
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            foreach (PlayerWindow window in _playerWindows)
            {
                window.Close();
            }
            playersListBox.Items.Clear();
            _players.Clear();
        }

        /*
         * 5 spelers - 1 wolf
         * 7 spelers - 2 wolven
         * 9 spelers - 3 wolven
         * 11 spelers - 4 wolven
         * etc.
         */
        private void AppointRolesToAllPlayers()
        {
            Random r = new Random();
            List<Player> appointedPlayers = new List<Player>();
            for (int i = 0; i < (_players.Count - 3)/2; i++)
            {
                int randomIndex = r.Next(_players.Count);
                Player newWerewolf = _players[randomIndex];
                _players.RemoveAt(randomIndex);
                newWerewolf.Role = new Role("Werewolf", "Images/werewolf.jpg");
                appointedPlayers.Add(newWerewolf);
            }

            List<Role> specialRoles = GetAllSpecialVillagerRoles();
            while (_players.Count > 0 && specialRoles.Count > 0)
            {
                int randomPlayerIndex = r.Next(_players.Count);
                Player player = _players[randomPlayerIndex];
                _players.RemoveAt(randomPlayerIndex);

                int randomRoleIndex = r.Next(specialRoles.Count);
                Role role = specialRoles[randomRoleIndex];
                specialRoles.RemoveAt(randomRoleIndex);

                player.Role = role;
                appointedPlayers.Add(player);
            }

            while(_players.Count > 0)
            {
                int randomIndex = r.Next(_players.Count);
                Player player = _players[randomIndex];
                _players.RemoveAt(randomIndex);
                player.Role = new Role("Villager", "Images/villager.jpg");
                appointedPlayers.Add(player);
            }

            _players = appointedPlayers;
        }

        private List<Role> GetAllSpecialVillagerRoles()
        {
            return new List<Role>()
            {
                new Role("Changeling", "Images/changeling.jpg"),
                new Role("Crystal Ball", "Images/crystal-ball.jpg"),
                new Role("Curse", "Images/curse.jpg"),
                new Role("Familiar", "Images/familiar.jpg"),
                new Role("Guard", "Images/guard.jpg"),
                new Role("Hermit", "Images/hermit.jpg"),
                new Role("Little Girl", "Images/little-girl.jpg"),
                new Role("Witch", "Images/witch.jpg")
            };
        }
    }
}