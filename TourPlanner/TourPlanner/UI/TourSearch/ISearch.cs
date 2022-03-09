using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UI.TourSearch
{
    interface ISearch
    {
        Dictionary<string, string> TourSearch(string entry);
    }
}
