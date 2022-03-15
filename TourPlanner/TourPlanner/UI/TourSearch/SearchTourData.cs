using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UI.TourSearch
{
    public class SearchTourData : ISearch
    {
        public string[] TourSearch(string searchText)
        {
            string[] array = new string[2];
            array[0] = $"hello, string2 {searchText}";
            array[1] = $"hello, string1 {searchText}";
            return array;
            //return new[] { $"Filtered result for: {searchText}\n=> Example tour route abc\n=> Example tour route xyz" };
        }
    }
}
