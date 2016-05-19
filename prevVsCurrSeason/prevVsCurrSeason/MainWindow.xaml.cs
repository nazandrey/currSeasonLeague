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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace prevVsCurrSeason
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine("RiotApiKey: " + RiotApi.Key);
            Debug.WriteLine("RiotApiKey: " + RiotApi.Server);
            Task<string> playerIdTask = RiotApi.getPlayerIdByName("euw","Zendwel");
            try
            {
                playerIdTask.Wait(1000);
            }
            catch (Exception)
            {

                throw;
            }
            
            Debug.Write("show player id: " + playerIdTask.Result);
        }

        public void showPlayerId(string playerId) {
            
        }
    }
}
