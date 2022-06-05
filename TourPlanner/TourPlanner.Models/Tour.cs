using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        // Not needed anymore, because coordinates are saved to BoundingBox. ?
        public Double StartCoord { get; set; }
        public Double EndCoord { get; set; }


        private string _routeType = "fastest";
        public String Transport
        {
            get
            {
                return _routeType;
            }
            set
            {
                if (value == "fastest" || value == "shortest" || value == "pedestrian" || value == "bicycle")
                    _routeType = value;
                else
                    _routeType = "fastest";
            }
        }
        private String _mapType;
        public String MapType
        {
            get
            {
                return _mapType;
            }
            set
            {
                if (value == "dark" || value == "light" || value == "map" || value == "hyb" || value == "sat")
                    _mapType = value;
                else
                    _mapType = "map";
            }
        }
        public Double Distance { get; set; }

        //Needs testing see if we can calculate average of list of TimeSpan (adding and subtracting two TimeSpan should work)
        //public String Duration { get; set; }
        public TimeSpan Duration { get; set; }
        public String Session { get; set; }
        public String BoundingBox { get; set; }

        //Placeholder for the image.
        public String MapPath { get; set; }        
        
        public List<TourLogs> TourLogs = new();

        public Tour()
        {
            this.From = new Adress();
            this.To = new Adress();
        }

        public Tour(Guid id, string name, string description, Adress from, Adress to, double startC, double endC, string transport, double distance, TimeSpan duration, string mappath)

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

        public Tour(Guid id, string name, string description, Adress from, Adress to, double startC, double endC, string transport, double distance, TimeSpan duration, string mappath, List<TourLogs> logs)
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
            // Including Logs.
            TourLogs = logs;
        }
    }
}
