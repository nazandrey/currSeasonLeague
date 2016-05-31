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
            RiotApi.getSummonerIdByName("euw","Zendwel,Gllebo").ContinueWith(task => {
                showPlayerId(task.Result);
            });
        }

        public void showPlayerId(Dictionary<string,string> summonerNameIdList) {
            foreach (KeyValuePair<string, string> summonerNameId in summonerNameIdList)
            {
                Debug.WriteLine("show summoner " + summonerNameId.Key + " id: " + summonerNameId.Value);
            }
        }
    }
}
