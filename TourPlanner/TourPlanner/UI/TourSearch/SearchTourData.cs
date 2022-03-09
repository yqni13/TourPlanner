using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UI.TourSearch
{
    class SearchTourData
    {
        public Dictionary<string, string> TourSearch(string entry)
        {
            Dictionary<string, string> tourData = new();
            for (int i = 0; i < 3; ++i)
                tourData.Add($"march{i}", $"Tour entry #{i}");

            return tourData;
        }
    }
}
