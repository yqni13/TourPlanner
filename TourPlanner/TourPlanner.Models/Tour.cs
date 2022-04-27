using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    class Tour
    {
        string name;
        string description;
        Adress from;
        Adress to;
        TransportType transport;
        double distance;
        TimeSpan duration; //needs testing see if we can calculate average of list of TimeSpan (adding and subtracting two TimeSpan should work)
        
        /*
         * Missing: the image 
         */
        private List<TourLogs> _logs = new List<TourLogs>();


    }
}
