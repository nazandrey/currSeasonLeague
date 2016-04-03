using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Diagnostics;

namespace prevVsCurrSeason
{
    static class RiotApi
    {
        private static readonly string RIOT_API_KEY = ConfigurationManager.AppSettings["RiotApiKey"];
        private static readonly string RIOT_API_SERVER = ConfigurationManager.AppSettings["RiotApiServer"];

        public static void getLeague(string region, string summonerIds) {
            using (var client = new HttpClient())
            {
                var responseString = client.GetStringAsync(RIOT_API_SERVER + String.Format("/api/lol/{{0}}/v2.5/league/by-summoner/{{1}}",
                    region,
                    summonerIds));
                // /api/lol/{region}/v1.4/summoner/by-name/{summonerNames}
            }
        }

        public static async Task<string> getPlayerIdByName(string region, string summonerNames)
        {
            Task<string> responseString;

            using (var client = new HttpClient())
            {
                responseString = client.GetStringAsync(RIOT_API_SERVER + String.Format("/api/lol/{{0}}/v1.4/summoner/by-name/{{1}}",
                    region,
                    summonerNames) + "?api_key=" + RIOT_API_KEY);
                Debug.WriteLine("");

                /*
                    {"zendwel": {
                       "id": 63750171,
                       "name": "Zendwel",
                       "profileIconId": 774,
                       "revisionDate": 1459627884000,
                       "summonerLevel": 30
                    }}
            
                */

                // /api/lol/{region}/v1.4/summoner/by-name/{summonerNames}
            }

            return responseString;
        }

        public static string Key { get { return RIOT_API_KEY; }}
        public static string Server { get { return RIOT_API_SERVER; }}
    }
}
