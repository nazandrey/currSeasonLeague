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
            RiotApi.getSummonerIdByName("euw","Zendwel,Gllebo").ContinueWith(summonerIdListTask => {
                showPlayerId(summonerIdListTask.Result);
                RiotApi.getCurrSeasonLeague("euw", String.Join(",", summonerIdListTask.Result.Values.ToArray<string>())).ContinueWith(leagueTask => {
                    showLeagueList(leagueTask.Result, summonerIdListTask.Result);
                });
            });
        }

        public void showPlayerId(Dictionary<string,string> summonerNameIdList) {
            foreach (KeyValuePair<string, string> summonerNameId in summonerNameIdList)
            {
                Debug.WriteLine("show summoner " + summonerNameId.Key + " id: " + summonerNameId.Value);
            }
        }

        public void showLeagueList(Dictionary<string, string> summonerLeagueList, Dictionary<string, string> summonerIdList)
        {
            foreach (KeyValuePair<string, string> summonerLeague in summonerLeagueList)
            {
                string summonerName = summonerIdList.FirstOrDefault(summonerIdEntry => summonerIdEntry.Value == summonerLeague.Key).Key;
                Debug.WriteLine("show summoner" + (summonerName ?? summonerLeague.Key) + "league: " + summonerLeague.Value);
            }
        }
    }
}
