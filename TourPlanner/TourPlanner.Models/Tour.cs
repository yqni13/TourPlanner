using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Tour
    {
        public Guid ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Adress From { get; set; }
        public Adress To { get; set; }
        public Double StartCoord { get; set; }
        public Double EndCoord { get; set; }
        public String Transport { get; set; }
        public Double Distance { get; set; }

        //Needs testing see if we can calculate average of list of TimeSpan (adding and subtracting two TimeSpan should work)
        public TimeSpan Duration { get; set; }

        //Placeholder for the image.
        public String MapPath { get; set; }        
        
        private List<TourLogs> _logs = new List<TourLogs>();

        public Tour(Guid id, string name, string description, Adress from, Adress to, double startC, double endC, String transport, double distance, TimeSpan duration, string mappath)
        {
            ID = id;
            Name = name;
            Description = description;
            From = from;
            To = to;
            StartCoord = startC;
            EndCoord = endC;
            Transport = transport;
            Distance = distance;
            Duration = duration;
            MapPath = mappath;
        }
        public Tour(string name, string description, Adress from, Adress to)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
        }
    }
}
