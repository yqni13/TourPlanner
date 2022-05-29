using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models.Enums;

namespace TourPlanner.Models
{    

    public class LogModel
    {
        public Guid TourID { get; set; }
        public DateTime Timestamp { get; set; }
        public String Comment { get; set; }
        public ETourDifficulty Difficulty { get; set; }
        public DateTime TotalTime { get; set; }
        public ETourRating Rating { get; set; }
    }
}
