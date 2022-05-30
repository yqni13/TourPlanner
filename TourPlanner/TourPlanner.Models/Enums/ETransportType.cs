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
        [Description("CAR")]
        CAR = 0,
        [Description("TRAIN")]
        TRAIN = 1,
        [Description("BICYCLE")]
        BICYCLE = 2,
        [Description("WALK")]
        WALK = 3
    }
}
