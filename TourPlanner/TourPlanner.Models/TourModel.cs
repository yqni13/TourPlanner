using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourModel
    {
        public string _name { get; private set;}
        private List<LogModel> _logs = new List<LogModel>();
    }
}
