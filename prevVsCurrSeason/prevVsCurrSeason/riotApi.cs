using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace prevVsCurrSeason
{
    static class RiotApi
    {
        private static readonly string RIOT_API_KEY = ConfigurationManager.AppSettings["RiotApiKey"];
        private static readonly string RIOT_API_SERVER = ConfigurationManager.AppSettings["RiotApiServer"];

        private static async Task<JObject> httpQuery(string query, string[] argList) {
            using (var client = new HttpClient())
            {
                string response;
                try
                {
                    response = await client.GetStringAsync(RIOT_API_SERVER + String.Format("/api/lol/" + query, argList) + "?api_key=" + RIOT_API_KEY);
                }
                catch (HttpRequestException)
                {

                    throw;
                }

                JObject responseObj = JObject.Parse(response);
                return responseObj;
            }
        } 

        public static void getLeague(string region, string summonerIds) {
            using (var client = new HttpClient())
            {
                var responseString = client.GetStringAsync(RIOT_API_SERVER + String.Format("/api/lol/{{0}}/v2.5/league/by-summoner/{{1}}",
                    region,
                    summonerIds));
            }
        }

        public static async Task<string> getPlayerIdByName(string region, string summonerName)
        {
            string playerId = null;
            await httpQuery("{0}/v1.4/summoner/by-name/{1}", new string[] { region, summonerName }).ContinueWith(task => {
                playerId = task.Result[summonerName.ToLower()]["id"].ToString();
            });
            return playerId; 

            /*
                {"zendwel": {
                    "id": 63750171,
                    "name": "Zendwel",
                    "profileIconId": 774,
                    "revisionDate": 1459627884000,
                    "summonerLevel": 30
                }}            
            */
        }

        public static string Key { get { return RIOT_API_KEY; }}
        public static string Server { get { return RIOT_API_SERVER; }}
    }
}
