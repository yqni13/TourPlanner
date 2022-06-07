﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models.Enums;

namespace TourPlanner.Models
{
    public class TourLogs
    {
        public Guid LogID { get; set; }
        public Guid TourID { get; set; }

        // Timestamp when tourlog was added.
        private DateTime _timestamp;
        public DateTime Timestamp { get; set; }
        
        public String Comment { get; set; }
        public ETourDifficulty Difficulty { get; set; }
        public TimeSpan TotalTime { get; set; }
        public ETourRating Rating { get; set; }
        public double Distance { get; set; }

        public TourLogs()
        {
            Comment = "";
        }
                
        public TourLogs(Guid logID, Guid tourID, DateTime stamp, TimeSpan time, ETourDifficulty difficulty, string comment, ETourRating rating, double distance)
        {
            LogID = logID;
            TourID = tourID;
            Timestamp = stamp;
            Comment = comment;
            Difficulty = difficulty;
            TotalTime = time;
            Rating = rating;
            Distance = distance;
        }
    }

    
}
