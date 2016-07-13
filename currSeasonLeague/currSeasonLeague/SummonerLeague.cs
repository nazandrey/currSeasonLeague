using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace currSeasonLeague
{
    public class SummonerLeague : ObservableCollection<SummonerLeague>
    {
        public string Name { get; set; }

        public string League { get; set; }
    }
}
