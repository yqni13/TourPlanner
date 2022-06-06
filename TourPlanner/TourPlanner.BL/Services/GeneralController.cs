using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Models;
using TourPlanner.Models.Enums;

namespace TourPlanner.BL.Services
{
    public static class GeneralController
    {
        public static double StringTimeConverterToSeconds(string time)
        {
            DateTime timeType = DateTime.Parse(time);
            double seconds = timeType.Second;
            double minutes = timeType.Minute * 60;
            double hours = timeType.Hour * 3600;
            return seconds + minutes + hours;
        }

        public static Tour AddTourDescription(Tour tour, string description)
        {
            tour.Description = description;
            return tour;
        }

        public static List<string> GetRouteTypes()
        {
            List<string> types = new();
            types.Add("fastest");
            types.Add("shortest");
            types.Add("pedestrian");
            types.Add("bicyle");

            return types;
        }

        public static int CalculatePopularity(Tour tour)
        {
            Collection<TourLogs> logs = LogController.GetSpecificLogs(tour.ID);
            return logs.Count;
        }

        public static bool CalculateChildFriendly(Tour tour)
        {
            Collection<TourLogs> logs = LogController.GetSpecificLogs(tour.ID);
            if (logs.Count == 0)
                return false;
            
            TimeSpan comparison = TimeSpan.FromSeconds(3600);
            double distanceAVG = AverageDistance(logs);
            double duration = AverageTimeInSeconds(logs);
            double difficultyAVG = AverageDifficulty(logs);            

            if (TimeSpan.FromSeconds(duration) <= comparison &&
               distanceAVG <= 30 &&
               difficultyAVG <= 2)
                return true;
            else
                return false;
        }        

        public static string AverageTime(Collection<TourLogs> tourLogs)
        {
            if (tourLogs.Count == 0)
                return "0";

            string convertingTime;
            double timeInSeconds = 0;
            
            foreach (TourLogs logs in tourLogs)
            {
                if(logs.TotalTime.TotalSeconds != 0)
                {
                    convertingTime = logs.TotalTime.ToString();
                    timeInSeconds += GeneralController.StringTimeConverterToSeconds(convertingTime);

                }
            }
            // Converting back to correct calculated and readable hh:mm:ss string.
            timeInSeconds = timeInSeconds / tourLogs.Count;
             
            return TimeSpan.FromSeconds(timeInSeconds).ToString();
            
        }

        public static double AverageDifficulty(Collection<TourLogs> tourlogs)
        {
            if (tourlogs.Count == 0)
                return 0;

            double number = 0;
            foreach (TourLogs logs in tourlogs)
            {
                var enumNumber = EnumCalculations.EnumDifficultyToDouble(logs.Difficulty);
                number += enumNumber;
            }
            return number / tourlogs.Count;
        }

        public static double AverageRating(Collection<TourLogs> tourlogs)
        {
            if (tourlogs.Count == 0)
                return 0;

            double number = 0;
            foreach (TourLogs logs in tourlogs)
            {
                var enumNumber = EnumCalculations.EnumRatingToDouble(logs.Rating);
                number += enumNumber;
            }
            return number / tourlogs.Count;
        }

        public static double AverageDistance(Collection<TourLogs> tourlogs)
        {
            if (tourlogs.Count == 0)
                return 0;

            double number = 0;
            foreach (TourLogs logs in tourlogs)
            {
                var distance = logs.Distance;
                number += distance;
            }
            return number / tourlogs.Count;
        }

        public static double AverageTimeInSeconds(Collection<TourLogs> tourLogs)
        {
            if (tourLogs.Count == 0)
                return 0;

            string convertingTime;
            double timeInSeconds = 0;
            foreach (TourLogs logs in tourLogs)
            {
                convertingTime = logs.TotalTime.ToString();
                timeInSeconds += GeneralController.StringTimeConverterToSeconds(convertingTime);
            }

            // Converting back to correct calculated and readable hh:mm:ss string.
            timeInSeconds = timeInSeconds / tourLogs.Count;
            return timeInSeconds;
        }
                
    }    
}
