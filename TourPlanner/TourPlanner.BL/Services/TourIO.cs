using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.Services
{
    public class TourIO
    {
        private static readonly JsonSerializerSettings _options
        = new() { NullValueHandling = NullValueHandling.Ignore };

        public static void ExportTour(Tour tour, String path)
        {

            var jsonString = JsonConvert.SerializeObject(tour, _options);
            File.WriteAllText(path, jsonString);
        }

    }
}
