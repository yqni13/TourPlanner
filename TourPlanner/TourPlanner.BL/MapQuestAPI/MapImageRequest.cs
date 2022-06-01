using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BL.MapQuestAPI
{
    public class MapImageRequest
    {
        public String MapImageURL { get; set; } = "https://www.mapquestapi.com/staticmap/v5/map";
        public String SessionID { get; set; }
        public String ImageDirectoryPath { get; set; } = Environment.CurrentDirectory + "../../../../MapQuestImages";

        public MapImageRequest()
        {
            // Create new directory for images. If directory exists, it will not overwrite.
            Directory.CreateDirectory(ImageDirectoryPath);


        }

    }
}
