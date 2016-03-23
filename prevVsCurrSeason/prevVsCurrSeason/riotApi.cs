using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;

namespace prevVsCurrSeason
{
    static class RiotApi
    {
        private static readonly string RIOT_API_KEY = ConfigurationManager.AppSettings["RiotApiKey"];

        public static void getLeague(string region, string summonerId) {
            using (var client = new HttpClient())
            {
                var responseString = client.GetStringAsync("http://www.example.com/recepticle.aspx");
            }
        }

        public static string Key { get { return RIOT_API_KEY; }}
    }
}
