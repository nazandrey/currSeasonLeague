using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace currSeasonLeague
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string region = null;

        public MainWindow()
        {
            InitializeComponent();
            serverComboBox.SelectedIndex = 0;
            region = (serverComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            this.DataContext = this;
        }

        private ObservableCollection<SummonerLeague> _summonerLeagueList;
        public ObservableCollection<SummonerLeague> SummonerLeagueList
        {
            get
            {
                if (_summonerLeagueList == null)
                    _summonerLeagueList = new ObservableCollection<SummonerLeague>();
                return _summonerLeagueList;
            }
        }

        public void showPlayerId(Dictionary<string,string> summonerNameIdList) {
            foreach (KeyValuePair<string, string> summonerNameId in summonerNameIdList)
            {
                Debug.WriteLine("show summoner " + summonerNameId.Key + " id: " + summonerNameId.Value);
            }
        }

        public void showLeagueList(Dictionary<string, string> summonerLeagueUiList, Dictionary<string, string> summonerIdList)
        {
            _summonerLeagueList.Clear();
            foreach (KeyValuePair<string, string> summonerLeague in summonerLeagueUiList)
            {
                string summonerName = summonerIdList.FirstOrDefault(summonerIdEntry => summonerIdEntry.Value == summonerLeague.Key).Key;
                Debug.WriteLine("show summoner " + (summonerName ?? summonerLeague.Key) + " league: " + summonerLeague.Value);
                _summonerLeagueList.Add(new SummonerLeague() { Name = (summonerName ?? summonerLeague.Key), League = summonerLeague.Value });
            }
        }

        private void addSummonerBtn_Click(object sender, RoutedEventArgs e)
        {
            SummonerLeague summonerLeague = new SummonerLeague() { Name = summonerNameTextBox.Text, League = ""};
            summonerNameTextBox.Text = "";
            _summonerLeagueList.Add(summonerLeague);
        }

        private void showCurrLeagueBtn_Click(object sender, RoutedEventArgs e)
        {
            string[] summonerNameList = _summonerLeagueList.Select(summonerLeague => summonerLeague.Name).ToArray<string>();

            RiotApi.QueryService.getSummonerIdByName(region, String.Join(",", summonerNameList)).ContinueWith(summonerIdListTask => {
                showPlayerId(summonerIdListTask.Result);
                RiotApi.QueryService.getLeague(region, String.Join(",", summonerIdListTask.Result.Values.ToArray<string>())).ContinueWith(leagueTask => {
                    showLeagueList(leagueTask.Result, summonerIdListTask.Result);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
