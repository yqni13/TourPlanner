using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UI.TourSearch
{
    public interface ISearch
    {
        string[] TourSearch(string searchText);
    }
}
