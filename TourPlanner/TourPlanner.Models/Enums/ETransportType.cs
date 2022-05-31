using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public enum ETransportType
    {
        [Description("fastest")] //Fastest driving route.
        FASTEST = 0,
        [Description("shortest")] //Shortest driving route.
        SHORTEST = 1,
        [Description("bicycle")]
        BICYCLE = 2,
        [Description("pedestrian")]
        PEDESTRIAN = 3
    }
}
