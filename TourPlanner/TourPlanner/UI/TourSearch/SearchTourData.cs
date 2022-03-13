using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UI.TourSearch
{
    class SearchTourData : ISearch
    {
        public string[] TourSearch(string searchText)
        {
            return new[] { $"Filtered result for: {searchText}\n=> Example tour route abc\n=> Example tour route xyz" };
        }
    }
}
