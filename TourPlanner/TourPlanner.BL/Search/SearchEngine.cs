using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.Search
{
    public class SearchEngine
    {
        public static Collection<Tour> searchTours(Collection<Tour> tours, String seachText)
        {
            Collection<Tour> returnCollection = new Collection<Tour>();

            if(seachText != null)
            {
                var results = from tour in tours
                              where tour.Name.Contains(seachText)
                              select tour;

                foreach (var result in results)
                {
                    returnCollection.Add(result);
                }
                return returnCollection;
            }
            return tours;
            
        }
    }
}
